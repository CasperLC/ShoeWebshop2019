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
        User CreateUser(UserDTO userToCreate);
        User UpdateUser(UserDTO updatedUser);
        User DeleteUser(int id);
        List<User> AllFilteredUsers(Filter filter);
    }
}
