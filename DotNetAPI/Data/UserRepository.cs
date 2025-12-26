using DotNetAPI.Models;

namespace DotNetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        EFData _entityFramework;    
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new EFData(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T addEntity)
        // public bool AddEntity<T>(T addEntity)
        {
            if (addEntity != null)
            {
                _entityFramework.Add(addEntity);
                // return true;
            }
            // return false;
        }

        public void RemoveEntity<T>(T addEntity)
        // public bool RemoveEntity<T>(T addEntity)
        {
            if (addEntity != null)
            {
                _entityFramework.Remove(addEntity);
                // return true;
            }
            // return false;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;
        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }
            
            throw new Exception("Failed to Get User");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();

            if (userSalary != null)
            {
                return userSalary;
            }
            
            throw new Exception("Failed to Get User Salary");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }
            
            throw new Exception("Failed to Get User");
        }
    }
}   