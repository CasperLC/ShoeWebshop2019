using System.Collections.Generic;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.DomainServices
{
    public interface IOrderRepository
    {
        Order ReadOrder(int id);
        List<Order> ReadAllOrders(Filter filter = null);
        Order CreateOrder(Order orderToCreate);
        Order UpdateOrder(Order orderToUpdate);
        Order DeleteOrder(int id);
        int Count();
    }
}