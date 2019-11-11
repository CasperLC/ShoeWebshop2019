using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private WebShopDBContext context;

        public OrderRepository(WebShopDBContext context)
        {
            this.context = context;
        }
        public Order ReadOrder(int id)
        {
            return context.Orders
                .Include(o => o.ShoeList)
                .FirstOrDefault(o => o.orderId == id);
        }

        public List<Order> ReadAllOrders(Filter filter)
        {
            if (filter == null)
            {
                return context.Orders.ToList();
            }

            return context.Orders
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage).ToList();
        }

        public Order CreateOrder(Order orderToCreate)
        {
            context.Attach(orderToCreate).State = EntityState.Added;
            context.SaveChanges();

            for (int i = 0; i < orderToCreate.ShoeList.Count; i++)
            {
                Shoe shoe = context.Shoes.FirstOrDefault(s => s.productid == orderToCreate.ShoeList[i].productid);
                shoe.Order = orderToCreate;
                orderToCreate.ShoeList[i] = shoe;
                context.Orders.Attach(orderToCreate).State = EntityState.Modified;
            }
            context.SaveChanges();

            return orderToCreate;
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            context.Attach(orderToUpdate).State = EntityState.Modified;
            context.Entry(orderToUpdate).Collection(o => o.ShoeList).IsModified = true;
            context.Entry(orderToUpdate).Reference(o => o.User).IsModified = true;

            var shoes = context.Shoes.Where(s => s.Order.orderId == orderToUpdate.orderId && !orderToUpdate.ShoeList.Exists(sl => sl.productid == s.productid));

            foreach (var shoe in shoes)
            {
                shoe.Order = null;
                context.Entry(shoe).Reference(s => s.Order)
                    .IsModified = true;
            }
            context.SaveChanges();

            return orderToUpdate;
        }

        public Order DeleteOrder(int id)
        {
            var deletedOrder = context.Orders.FirstOrDefault(o => o.orderId == id);
            context.Orders.Remove(deletedOrder);
            context.SaveChanges();
            return deletedOrder;
        }

        public int Count()
        {
            return context.Orders.Count();
        }
    }
}