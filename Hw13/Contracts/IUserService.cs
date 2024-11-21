using Hw13.Entities;
using Hw13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Contracts
{
    public interface IUserService
    {
        public void Register(string fullName, string userName, string password ,string phone, int role);
        public void Login(string userName, string password);
        public bool Logout();
        public User GetCurrentUser();
        public List<User> GetAllUser();

    }
}
