using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.Repositories
{
    public class ShoeRepository: IShoeRepository
    {
        private WebShopDBContext _context;

        public ShoeRepository(WebShopDBContext context)
        {
            _context = context;
        }


        public Shoe ReadShoe(int id)
        {
            return _context.Shoes
                .Include(s => s.Order)
                .FirstOrDefault(s => s.productid == id);
        }

        public List<Shoe> ReadAllShoes(Filter filter)
        {
            if (filter == null)
            {
                return _context.Shoes.ToList();
            }

            return _context.Shoes
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage).ToList();

        }

        public Shoe CreateShoe(Shoe shoeToCreate)
        {
            _context.Shoes.Attach(shoeToCreate).State = EntityState.Added;
            _context.SaveChanges();
            return shoeToCreate;
        }

        public Shoe UpdateShoe(Shoe updatedShoe)
        {
            _context.Shoes.Attach(updatedShoe).State = EntityState.Modified;
            _context.SaveChanges();
            return updatedShoe;
        }

        public Shoe DeleteShoe(int id)
        {
            var shoe = _context.Remove(new Shoe {productid = id}).Entity;
            _context.SaveChanges();
            return shoe;
        }

        public int Count()
        {
            return _context.Shoes.Count();
        }
    }
}