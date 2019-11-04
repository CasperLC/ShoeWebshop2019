using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data
{
    public class WebShopDBContext: DbContext
    {
        public WebShopDBContext(DbContextOptions<WebShopDBContext> opt) : base(opt)
        {

        }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>()
                .HasKey(s => s.productid);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}