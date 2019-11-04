using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WebShopDBContext _context;

        public UserRepository(WebShopDBContext context)
        {
            _context = context;
        }
        public int Count()
        {
            return _context.Users.Count();
        }

        public User CreateUser(User userToCreate)
        {
            _context.Users.Attach(userToCreate).State = EntityState.Added;
            _context.SaveChanges();
            return userToCreate;
        }

        public User DeleteUser(int id)
        {
            var deletedUser = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(deletedUser);
            _context.SaveChanges();
            return deletedUser;
        }

        public List<User> ReadAllUsers()
        {
            return _context.Users.ToList();
        }

        public User ReadUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(User updatedUser)
        {
            _context.Entry(updatedUser).State = EntityState.Modified;
            _context.SaveChanges();
            return updatedUser;
        }
    }
}
