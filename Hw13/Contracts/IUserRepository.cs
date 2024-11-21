using Hw13.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Contracts
{
    public interface IUserRepository
    {
        public void Add(User user);
        public List<User> GetAll();
        public User Get(string userName);
    }
}
