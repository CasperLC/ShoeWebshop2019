using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoeWebshop.Core.ApplicationServices;
using Webshop.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ShoeRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {

        private IShoeService _shoeService;

        public ShoesController(IShoeService shoeService)
        {
            _shoeService = shoeService;
        }

        // GET api/shoes
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Shoe>> Get([FromQuery] Filter filter)
        {
            if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
            {
                return Ok(_shoeService.ReadAllShoes());
            }
            else
            {
                return Ok(_shoeService.AllFilteredShoes(filter));
            }
        }

        // GET api/shoes/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Shoe> Get(int id)
        {
            return _shoeService.ReadShoe(id);
        }

        // POST api/shoes
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Shoe> Post([FromBody] Shoe shoe)
        {
            try
            {
                return Ok(_shoeService.CreateShoe(shoe));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/shoes/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Shoe> Put(int id, [FromBody] Shoe shoe)
        {
            try
            {
                return Ok(_shoeService.UpdateShoe(shoe));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/shoes/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Shoe> Delete(int id)
        {
            return Ok(_shoeService.DeleteShoe(id));
        }
    }
}