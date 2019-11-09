using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;
using Webshop.Infrastructure.Data.Helper;

namespace ShoeRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IUserRepository repository;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(IUserRepository repos, IAuthenticationHelper authService)
        {
            repository = repos;
            authenticationHelper = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = repository.ReadUserLogin().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}