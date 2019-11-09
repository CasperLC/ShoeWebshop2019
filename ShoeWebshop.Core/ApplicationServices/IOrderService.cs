using System.Collections.Generic;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices
{
    public interface IOrderService
    {
        Order ReadOrder(int id);
        List<Order> ReadAllOrders();
        Order CreateOrder(Order orderToCreate);
        Order UpdateOrder(Order orderToUpdate);
        Order DeleteOrder(int id);
        List<Order> AllFilteredOrders(Filter filter);
    }
}