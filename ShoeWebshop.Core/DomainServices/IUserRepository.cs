using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.DomainServices
{
    public interface IUserRepository
    {
        User ReadUser(int id);
        List<User> ReadAllUsers();
        User CreateUser(User userToCreate);
        User UpdateUser(User updatedUser);
        User DeleteUser(int id);
        int Count();
    }
}
