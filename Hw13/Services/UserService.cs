using Hw13.Contracts;
using Hw13.Entities;
using Hw13.Enum;
using Hw13.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private User CurrentUser;
        private const string EncryptionKey = "Hw-13";
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Register(string fullName, string userName, string password, string phone , int role)
        {
            try
            {
                bool isSpecial = password.Any(s => (s >= 33 && s <= 47) || s == 64);

                if (password.Length < 5 || password.Length > 10 || !isSpecial)
                {
                    throw new Exception("Password > 4  Char And One Special Character");
                }

                var user1 = _userRepository.Get(userName);
                if (user1 != null)
                {
                    throw new Exception("Username Already Exists");
                }
                if (phone.Length != 11 || !phone.StartsWith("09"))
                {
                    throw new Exception("Wrong:Phone 11 Digits|Start 09");
                }
                string encryptedPassword = password.EncryptString(EncryptionKey);

                var user = new User
                {
                    FullName = fullName,
                    UserName = userName,
                    Password = encryptedPassword,
                    Phone = phone,
                    Role =(RoleUserEnum)role,
                    Books = new List<Book>()
                };

                _userRepository.Add(user);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }

        public void Login(string username, string password)
        {
            try
            {
                var user = _userRepository.Get(username);
                if (user == null)
                {
                    throw new Exception("Not Found User");
                }
                string decryptedPassword = user.Password.DecryptString(EncryptionKey);
                
                if (decryptedPassword != password)
                {
                    throw new Exception("Invalid Password");
                }
                if (user.UserName != username)
                {
                    throw new Exception("Invalid  UserName");
                }
                if (user == null)
                {
                    throw new Exception("Not Found User");
                }

                CurrentUser = user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");

            }
        }
        public List<User> GetAllUser()
        {
            try
            {
                return _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
        public bool Logout()
        {
            CurrentUser = null;
            return true;
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }
    }
}
