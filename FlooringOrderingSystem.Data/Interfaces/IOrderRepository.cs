using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Data.Interfaces
{
    public interface IOrderRepository
    {
        Order SaveOrderToRepo(Order order);
        List<Order> ReadAllOrdersByDate(DateTime orderDate);
        Order ReadOrdersByDateThenByOrderNumber(DateTime orderDate, int orderNumber);
        void DeleteOrder(Order order);
    }
}
