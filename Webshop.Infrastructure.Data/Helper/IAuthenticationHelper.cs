using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.Helper
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(User user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
