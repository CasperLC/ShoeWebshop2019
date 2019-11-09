using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.DomainServices
{
    public interface IUserRepository
    {
        User ReadUser(int id);
        List<User> ReadAllUsers(Filter filter = null);
        User CreateUser(UserDTO userToCreate);
        User UpdateUser(UserDTO updatedUser);
        User DeleteUser(int id);
        List<User> ReadUserLogin();
        int Count();
    }
}
