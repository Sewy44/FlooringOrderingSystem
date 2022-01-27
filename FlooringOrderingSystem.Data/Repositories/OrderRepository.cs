using FlooringOrderingSystem.Data.Interfaces;
using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlooringOrderingSystem.Model.Responses;

namespace FlooringOrderingSystem.Data.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private string _orderFilePath = ConfigurationManager.AppSettings["orderFilePath"].ToString();

        private List<Order> ReadOrdersFromFile(DateTime orderDate)
        {
            var convertedOrderDate = orderDate.ToString("MMddyyyy");
            string fullOrderFilePath = _orderFilePath + "\\Orders_" + convertedOrderDate + ".txt";
            var _orders = new List<Order>();
            try
            {
                string[] rows = File.ReadAllLines(fullOrderFilePath);

                for (int i = 1; i < rows.Length; i++)
                {
                    var _order = UnMarshallOrder(rows[i]);
                    _order.orderDate = orderDate;
                    _orders.Add(_order);
                }

                return _orders;
            }
            catch
            {
                
            }
            return _orders;
        }

        private Order UnMarshallOrder(string orderString)
        {
            string[] orderElements = orderString.Split(',');

            Order order = new Order();

            order.OrderNumber = Convert.ToInt32(orderElements[0]);
            order.CustomerName = orderElements[1].Replace("-",",");
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

            return (order);
        }
        public List<Order> ReadAllOrdersByDate(DateTime orderDate)
        {
            List<Order> orders = ReadOrdersFromFile(orderDate);

            return orders;
        }
        private string MarshallOrder(Order order)
        {
           
           string orderString = order.OrderNumber + "," + order.CustomerName.Replace(",", "-") + "," + order.state.StateAbbreviation + "," + order.state.TaxRate + "," + order.product.ProductType + "," + order.Area + "," + order.product.CostPerSquareFoot + "," + order.product.LaborCostPerSquareFoot + "," + order.MaterialCost + "," + order.LaborCost + "," + order.Tax + "," + order.Total;

           return orderString;
        }
        
        private void SaveOrderToFile(Order order)
        {

            var convertedOrderDate = order.orderDate.ToString("MMddyyyy");
            string fullOrderFilePath = _orderFilePath + "\\Orders_" + convertedOrderDate + ".txt";
            bool found = false;
            string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
            if (!File.Exists(fullOrderFilePath))
            {
                    File.WriteAllText(fullOrderFilePath,header);
            }

                List<string> rows = new List<string>(File.ReadAllLines(fullOrderFilePath));

                for (int i = 1; i < rows.Count; i++)
                { 
                    if (rows[i].StartsWith(Convert.ToString(order.OrderNumber)))
                    {
                    found = true;

                        rows[i] = MarshallOrder(order);
                    }  
                }
            if (found == false)
            {
                rows.Add(MarshallOrder(order));
            }
            File.WriteAllLines(fullOrderFilePath, rows);
        }

        public void DeleteOrder(Order order)
        {
            var convertedOrderDate = order.orderDate.ToString("MMddyyyy");
            string fullOrderFilePath = _orderFilePath + "\\Orders_" + convertedOrderDate + ".txt";
            List<string> rows = new List<string>(File.ReadAllLines(fullOrderFilePath));
            string orderString = MarshallOrder(order);
            rows.Remove(orderString);

            File.WriteAllLines(fullOrderFilePath, rows);
        }

        public Order ReadOrdersByDateThenByOrderNumber(DateTime orderDate, int orderNumber)
        {
            List<Order> orders = ReadOrdersFromFile(orderDate);
            return orders.Where(ord => ord.OrderNumber == orderNumber).Single();
        }

        public Order SaveOrderToRepo(Order order)
        {
            SaveOrderToFile(order);
            return order;
        }
    }
}
