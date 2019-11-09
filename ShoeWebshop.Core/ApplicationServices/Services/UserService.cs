using ShoeWebshop.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.IO;
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
        public User CreateUser(UserDTO userToCreate)
        {
            if (string.IsNullOrEmpty(userToCreate.Username) || string.IsNullOrEmpty(userToCreate.Password))
            {
                throw new InvalidDataException("The user most have a Username and a Password");
            }
            return userRepo.CreateUser(userToCreate);
        }

        public User DeleteUser(int id)
        {
            return userRepo.DeleteUser(id);
        }

        public List<User> AllFilteredUsers(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("Current page and items pr page must be more than 0 or 0");
            }

            if ((filter.CurrentPage - 1) * filter.ItemsPrPage >= userRepo.Count())
            {
                throw new InvalidDataException("Index out of bound to high page");
            }
            return userRepo.ReadAllUsers(filter);
        }

        public List<User> ReadAllUsers()
        {
            return userRepo.ReadAllUsers();
        }

        public User ReadUser(int id)
        {
            return userRepo.ReadUser(id);
        }

        public User UpdateUser(UserDTO updatedUser)
        {
            if (string.IsNullOrEmpty(updatedUser.Username) || string.IsNullOrEmpty(updatedUser.Password))
            {
                throw new InvalidDataException("The user most have a Username and a Password");
            }
            return userRepo.UpdateUser(updatedUser);
        }
    }
}
