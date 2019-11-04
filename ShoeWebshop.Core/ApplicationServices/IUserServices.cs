using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices
{
    public interface IUserServices
    {
        User ReadUser(int id);
        List<User> ReadAllUsers();
        User CreateUser(User userToCreate);
        User UpdateUser(User updatedUser);
        User DeleteUser(int id);
    }
}
