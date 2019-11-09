using System.Collections.Generic;

namespace Webshop.Core.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Order> orderList { get; set; }
    }
}