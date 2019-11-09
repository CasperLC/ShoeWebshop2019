using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Core.Entities
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Shoe> ShoeList { get; set; }
        public User User { get; set; }
        public bool ActiveOrder { get; set; } // Active = shows as an order, Inactive = shows up in order history

    }
}
