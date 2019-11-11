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
            context.Orders.Attach(orderToCreate).State = EntityState.Added;
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
            context.Orders.Attach(orderToUpdate).State = EntityState.Modified;
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