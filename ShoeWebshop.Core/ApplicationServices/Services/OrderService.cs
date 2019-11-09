using System.Collections.Generic;
using System.IO;
using ShoeWebshop.Core.DomainServices;
using Webshop.Core.Entities;

namespace ShoeWebshop.Core.ApplicationServices.Services
{
    public class OrderService: IOrderService
    {
        private IOrderRepository orderRepo;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepo = orderRepository;
        }
        public Order ReadOrder(int id)
        {
            return orderRepo.ReadOrder(id);
        }

        public List<Order> ReadAllOrders()
        {
            return orderRepo.ReadAllOrders();
        }

        public Order CreateOrder(Order orderToCreate)
        {
            return orderRepo.CreateOrder(orderToCreate);
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            return orderRepo.UpdateOrder(orderToUpdate);
        }

        public Order DeleteOrder(int id)
        {
            return orderRepo.DeleteOrder(id);
        }

        public List<Order> AllFilteredOrders(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("Current page and items pr page must be more than 0 or 0");
            }

            if ((filter.CurrentPage - 1) * filter.ItemsPrPage >= orderRepo.Count())
            {
                throw new InvalidDataException("Index out of bound to high page");
            }
            return orderRepo.ReadAllOrders(filter);
        }
    }
}