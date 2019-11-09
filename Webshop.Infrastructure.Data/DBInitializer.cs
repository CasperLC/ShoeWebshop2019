using System;
using System.Collections.Generic;
using System.Linq;
using Webshop.Core.Entities;
using Webshop.Infrastructure.Data.Helper;

namespace Webshop.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(WebShopDBContext context)
        {
            context.Database.EnsureCreated();
            if (context.Shoes.Any() || context.Users.Any())
            {
                return;
            }


            var shoe1 = context.Shoes.Add(new Shoe()
            {
                ProductName = "Shoe_Name",
                Color = "Blue",
                Size = 40.0,
                Type = "Men",
                Price = 200.0,


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

            var order1 = context.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                ActiveOrder = true,
                ShoeList = new List<Shoe>
                {
                    shoe1,
                    shoe2,
                    shoe3
                }

            }).Entity;

            // Create two users with hashed and salted passwords
            string password = "1234";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
            CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
            CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);

            // Create two users with hashed and salted passwords
            List<User> users = new List<User>
            {
                new User {
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = false,
                    orderList = new List<Order>
                    {
                        order1
                    }
                },
                new User {
                    Username = "AdminAnn",
                    PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn,
                    IsAdmin = true
                }
            };



            context.Users.AddRange(users);

            context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}