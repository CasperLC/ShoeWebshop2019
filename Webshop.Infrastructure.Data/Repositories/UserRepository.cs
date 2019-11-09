using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Webshop.Infrastructure.Data.Helper;

namespace Webshop.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WebShopDBContext _context;
        private IAuthenticationHelper _auth;

        public UserRepository(WebShopDBContext context, IAuthenticationHelper authentication)
        {
            _context = context;
            _auth = authentication;
        }

        public List<User> ReadUserLogin()
        {
            return _context.Users
                .ToList();
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public User CreateUser(UserDTO userToCreate)
        {
            byte[] newPassHash, newPassSalt;
            _auth.CreatePasswordHash(userToCreate.Password, out newPassHash, out newPassSalt);
            User NewUser = new User
            {
                Username = userToCreate.Username,
                PasswordHash = newPassHash,
                PasswordSalt = newPassSalt,
                IsAdmin = userToCreate.IsAdmin,
                orderList = userToCreate.orderList
            };
            _context.Users.Attach(NewUser).State = EntityState.Added;
            _context.SaveChanges();
            return NewUser;
        }

        public User DeleteUser(int id)
        {
            var deletedUser = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(deletedUser);
            _context.SaveChanges();
            return deletedUser;
        }

        public List<User> ReadAllUsers(Filter filter)
        {
            if (filter == null)
            {
                return _context.Users
                    .Select(u => new User
                    {
                        Id = u.Id,
                        IsAdmin = u.IsAdmin,
                        Username = u.Username,
                        orderList = u.orderList
                    })
                    .ToList();
            }

            return _context.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    IsAdmin = u.IsAdmin,
                    Username = u.Username,
                    orderList = u.orderList
                })
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .ToList();
        }

        public User ReadUser(int id)
        {
            return _context.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    IsAdmin = u.IsAdmin,
                    Username = u.Username,
                    orderList = u.orderList
                })
                .Include(u => u.orderList)
                .FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(UserDTO updatedUser)
        {
            byte[] updatedPassHash, updatedPassSalt;
            _auth.CreatePasswordHash(updatedUser.Password, out updatedPassHash, out updatedPassSalt);
            User TheUpdatedUser = new User
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username,
                PasswordHash = updatedPassHash,
                PasswordSalt = updatedPassSalt,
                IsAdmin = updatedUser.IsAdmin,
                orderList = updatedUser.orderList
            };
            _context.Entry(updatedUser).State = EntityState.Modified;
            _context.SaveChanges();
            return TheUpdatedUser;
        }
    }
}
