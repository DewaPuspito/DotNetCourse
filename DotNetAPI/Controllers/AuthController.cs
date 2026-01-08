using System.Data;
using AutoMapper;
using Dapper;
using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Helpers;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DapperData _dapper;
        private readonly AuthHelper _authHelper;
        private readonly ReusableSQL _reusableSQL;
        private readonly IMapper _mapper;
        public AuthController(IConfiguration config)
        {
            _dapper = new DapperData(config);
            _authHelper = new AuthHelper(config);
            _reusableSQL = new ReusableSQL(config);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<UserRegisterDTO, UserComplete>();
                    }
                )
            );
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
                    UserLoginDTO userForSetPassword = new UserLoginDTO()
                    {
                        Email = userRegister.Email,
                        Password = userRegister.Password
                    };

                    if(_authHelper.SetPassword(userForSetPassword))
                    {
                        UserComplete userComplete = _mapper.Map<UserComplete>(userRegister);
                        userComplete.Active = true;

                        if (_reusableSQL.UpsertUser(userComplete))
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
            string sqlForHashAndSalt = @"EXEC TutorialAppSchema.spLoginConfirmation_Get 
                @Email = @EmailParam";

            
            DynamicParameters sqlParameters = new DynamicParameters();

            // SqlParameter emailParameter = new SqlParameter("@EmailParam", SqlDbType.VarChar);
            // emailParameter.Value = userLogin.Email;
            // sqlParameters.Add(emailParameter);

            sqlParameters.Add("@EmailParam", userLogin.Email, DbType.String);
            
            LoginConfirmationDTO loginConfirmation = _dapper
                .LoadDataSingleWithParameters<LoginConfirmationDTO>(sqlForHashAndSalt, sqlParameters);

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

        [HttpPut("Reset Password")]
        public IActionResult ResetPassword(UserLoginDTO userForSetPassword)
        {
            if(_authHelper.SetPassword(userForSetPassword))
            {
                return Ok();
            }
            throw new Exception("Failed to reset password!");
        }
    }
}