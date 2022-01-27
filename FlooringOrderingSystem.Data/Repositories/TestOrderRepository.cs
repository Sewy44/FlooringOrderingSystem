using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Data.Interfaces;
using FlooringOrderingSystem.Model;
using System.IO;

namespace FlooringOrderingSystem.Data.Repositories
{
    public class TestOrderRepository : IOrderRepository
    {
        private string testOrderFileName = ConfigurationManager.AppSettings["testOrderFileName"].ToString();

        private List<Order> ReadOrdersFromFile(DateTime orderDate)
        {
            var convertedOrderDate = orderDate.ToString("MMddyyyy");
            var _orders = new List<Order>();
            string[] rows = File.ReadAllLines(testOrderFileName);

            if (testOrderFileName.Contains(convertedOrderDate))
            {
                for (int i = 1; i < rows.Length; i++)
                {
                    var _order = UnMarshallOrder(rows[i]);
                    _order.orderDate = orderDate;
                    _orders.Add(_order);
                }        
            }
            return _orders;

        }

        public List<Order> ReadAllOrdersByDate(DateTime orderDate)
        {
            List<Order> orders = ReadOrdersFromFile(orderDate);

            return orders;
        }

        public Order ReadOrdersByDateThenByOrderNumber(DateTime orderDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order EditOrder(Order order)
        {

            return order;
        }
        public void DeleteOrder(Order order)
        {

        }
        public Order SaveOrderToRepo(Order order)
        {
            return order;
        }

        public Order CreateOrder(Order order)
        {
            return order;
        }

        private Order UnMarshallOrder(string orderString)
        {
            string[] orderElements = orderString.Split(',');

            Order order = new Order();

            order.OrderNumber = Convert.ToInt32(orderElements[0]);
            order.CustomerName = orderElements[1];
            order.state.StateAbbreviation = orderElements[2];
            order.state.TaxRate = Convert.ToDecimal(orderElements[3]);
            order.product.ProductType = orderElements[4];
            order.Area = Convert.ToDecimal(orderElements[5]);
            order.product.CostPerSquareFoot = Convert.ToDecimal(orderElements[6]);
            order.product.LaborCostPerSquareFoot = Convert.ToDecimal(orderElements[7]);
            order.MaterialCost = Convert.ToDecimal(orderElements[8]);
            order.LaborCost = Convert.ToDecimal(orderElements[9]);
            order.Tax = Convert.ToDecimal(orderElements[10]);
            order.Total = Convert.ToDecimal(orderElements[11]);

            return order;
        }
    }
}
