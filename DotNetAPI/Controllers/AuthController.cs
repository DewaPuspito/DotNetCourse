using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DapperData _dapper;
        private readonly AuthHelper _authHelper;
        public AuthController(IConfiguration config)
        {
            _dapper = new DapperData(config);
            _authHelper = new AuthHelper(config);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserRegisterDTO userRegister)
        {
            if (userRegister.Password == userRegister.PasswordConfirm)
            {
                string sqlCheckUserExists = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" + 
                    userRegister.Email + "'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);
                if (existingUsers.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = _authHelper.GetPasswordHash(userRegister.Password, passwordSalt);

                    string sqlAddAuth = @"
                        INSERT INTO TutorialAppSchema.Auth ([Email], 
                        [PasswordHash], 
                        [PasswordSalt]
                        ) VALUES ('" + userRegister.Email + 
                        "', @PasswordHash, @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;

                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);

                    if(_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        string sqlAddUser = @"
                            INSERT INTO TutorialAppSchema.Users(
                                [FirstName],
                                [LastName],
                                [Email],
                                [Gender],
                                [Active]
                            ) VALUES (" +
                                "'" + userRegister.FirstName + 
                                "', '" + userRegister.LastName +
                                "', '" + userRegister.Email + 
                                "', '" + userRegister.Gender + 
                                "', 1)";
                        if (_dapper.ExecuteSql(sqlAddUser))
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to add user");
                    }
                    throw new Exception("Failed to register user");
                }
                throw new Exception("User with this email already exists");
            }
            throw new Exception("Password do not match");
        }

        [AllowAnonymous]
        [HttpPost("Login")] 
        public IActionResult Login(UserLoginDTO userLogin)
        {
            string sqlForHashAndSalt = @"SELECT
                [PasswordHash], 
                [PasswordSalt] 
                FROM TutorialAppSchema.Auth WHERE Email = '" + 
            userLogin.Email + "'";
            
            LoginConfirmationDTO loginConfirmation = _dapper
                .LoadDataSingle<LoginConfirmationDTO>(sqlForHashAndSalt);

            byte[] passwordHash = _authHelper.GetPasswordHash(userLogin.Password, loginConfirmation.PasswordSalt);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != loginConfirmation.PasswordHash[i])
                {
                    return StatusCode(401, "Password is incorrect");
                }
            }

            string userIdSql = @"
                SELECT * FROM TutorialAppSchema.Users WHERE Email = '" + 
                userLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelper.CreateToken(userId)}
            });
        }

        [HttpGet("RefreshToken")]
        public  IActionResult RefreshToken()
        {
            string userId = User.FindFirst("userId")?.Value + "";

            string userIdSql = @"
                SELECT UserId FROM TutorialAppSchema.Users WHERE UserId = " + 
                userId;

            int userIdFromDB = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelper.CreateToken(userIdFromDB )}
            });
        }

    }
}