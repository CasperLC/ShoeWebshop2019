using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(WebShopDBContext context)
        {
            context.Database.EnsureCreated();

            var shoe1 = context.Shoes.Add(new Shoe()
            {
                ProductName = "Shoe_Name",
                Color = "Blue",
                Size = 40.0,
                Type = "Men",
                Price = 200.0


            }).Entity;

            var shoe2 = context.Shoes.Add(new Shoe()
            {
                ProductName = "Shoe_Name2",
                Color = "Red",
                Size = 35.0,
                Type = "Women",
                Price = 150.0

            }).Entity;

            var shoe3 = context.Shoes.Add(new Shoe()
            {
                ProductName = "Shoe_Name3",
                Color = "Black",
                Size = 30.0,
                Type = "Kid",
                Price = 100.0

            }).Entity;

            context.SaveChanges();
        }
    }
}