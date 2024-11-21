using Hw13.Configuration;
using Hw13.Contracts;
using Hw13.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User Get(string userName)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == userName);
        }

        public List<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

    }
}
