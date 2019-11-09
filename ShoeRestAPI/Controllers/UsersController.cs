using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeWebshop.Core.ApplicationServices;
using Webshop.Core.Entities;

namespace ShoeRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServices _userService;

        public UsersController(IUserServices userService)
        {
            _userService = userService;
        }

        // GET api/users
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery] Filter filter)
        {
            if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
            {
                return Ok(_userService.ReadAllUsers());
            }
            else
            {
                return Ok(_userService.AllFilteredUsers(filter));
            }
        }

        // GET api/users/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return _userService.ReadUser(id);
        }

        // POST api/users
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserDTO user)
        {
            try
            {
                return Ok(_userService.CreateUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/users/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] UserDTO user)
        {
            try
            {
                return Ok(_userService.UpdateUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/users/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}