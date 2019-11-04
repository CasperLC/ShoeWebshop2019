using ShoeWebshop.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices.Services
{
    public class UserService : IUserServices
    {
        private IUserRepository userRepo;

        public UserService(IUserRepository userRepository)
        {
            this.userRepo = userRepository;
        }
        public User CreateUser(User userToCreate)
        {
            return userRepo.CreateUser(userToCreate);
        }

        public User DeleteUser(int id)
        {
            return userRepo.DeleteUser(id);
        }

        public List<User> ReadAllUsers()
        {
            return userRepo.ReadAllUsers();
        }

        public User ReadUser(int id)
        {
            return userRepo.ReadUser(id);
        }

        public User UpdateUser(User updatedUser)
        {
            return userRepo.UpdateUser(updatedUser);
        }
    }
}
