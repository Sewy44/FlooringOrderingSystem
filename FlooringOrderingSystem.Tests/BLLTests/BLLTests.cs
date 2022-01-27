using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Data.Interfaces;
using NUnit.Framework;
using FlooringOrderingSystem.BLL.Exception_Handling;
using FlooringOrderingSystem.Model;
using System.Text.RegularExpressions;
using FlooringOrderingSystem.BLL;

namespace FlooringOrderingSystem.Tests.BLLTests
{
    [TestFixture]
    public class BLLTests
    {
        private OrderManager orderManager = OrderManagerFactory.Create();


        [TestCase("Burt Mchandsome", "06/01/2022", 1, "OH", "Wood", 100)] 
        [TestCase("")]
        [Test]
        public void CalculateOrder_CanCalculateOrder(string customerName, DateTime orderDate, int orderNumber, string state, string productType, decimal area)
        {
            Order order = new Order();
            order.CustomerName = customerName;
            order.orderDate = Convert.ToDateTime(orderDate);
            order.OrderNumber = orderNumber;
            order.state.StateAbbreviation = state;
            order.product.ProductType = productType;
            order.Area = area;

            var orderToCalculate = orderManager.CalculateOrder(order);

            order.LaborCost = order.Area * order.product.LaborCostPerSquareFoot;
            order.MaterialCost = order.Area * order.product.CostPerSquareFoot;
            order.Tax = (order.MaterialCost + order.LaborCost) * (order.state.TaxRate / 100);
            order.Total = order.MaterialCost + order.LaborCost + order.Tax;
           
            Assert.AreEqual(order.LaborCost, orderToCalculate.LaborCost);
            Assert.AreEqual(order.MaterialCost, orderToCalculate.MaterialCost);
            Assert.AreEqual(order.Tax, orderToCalculate.Tax);
            Assert.AreEqual(order.Total, orderToCalculate.Total);
            if(order.OrderNumber == 0)
            {
                Assert.IsTrue(orderToCalculate.OrderNumber == 1);
            }
            
            
        }

        [TestCase("Burt Mchandsome", "06/01/2022", 1, "OH", "Wood", 100, true)]//should pass. 
        [TestCase("#winning", "08/30/2022", 0, "PA", "Tile", 150, true)]// should pass, increments order number
        [TestCase("Jack Sparrow", "08/30/2022", 12, "KY", "Laminate", 125, false )]//fail, invalid state
        [Test]
        public void CalculateOrder_CanValidateOrder(string customerName, DateTime orderDate, int orderNumber, string state, string productType, decimal area, bool expectedResult)
        {
            /*Order order = new Order();
            order.product.ProductType = null;

            var orderToCalculate = orderManager.CalculateOrder(order);
            
            Assert.AreEqual(expectedResult, )*/
            
        }

    }
}
