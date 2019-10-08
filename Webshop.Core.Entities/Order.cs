using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Core.Entities
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Shoe> OrderList { get; set; }

    }
}
