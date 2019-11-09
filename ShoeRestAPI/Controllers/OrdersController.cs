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
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/orders
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        {
            if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
            {
                return Ok(_orderService.ReadAllOrders());
            }
            else
            {
                return Ok(_orderService.AllFilteredOrders(filter));
            }
        }

        // GET api/orders/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            return _orderService.ReadOrder(id);
        }

        // POST api/orders
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/orders/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.UpdateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/orders/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            return Ok(_orderService.DeleteOrder(id));
        }
    }
}