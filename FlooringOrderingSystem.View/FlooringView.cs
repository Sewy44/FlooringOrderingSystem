using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.View
{
    public class FlooringView
    {
        private UserInputOutput userInputOutput;

        public FlooringView()
        {
            userInputOutput = new UserInputOutput();
        }

        public int ShowMenuAndGetUserChoice()
        {
            int min = 1;
            int max = 5;
            Console.WriteLine("****************************************");
            Console.WriteLine("* Flooring Program");
            Console.WriteLine("*");
            Console.WriteLine("* 1. Display Orders");
            Console.WriteLine("* 2. Add an Order");
            Console.WriteLine("* 3. Edit an Order");
            Console.WriteLine("* 4. Remove an Order");
            Console.WriteLine("* 5. Quit");
            Console.WriteLine("*");
            Console.WriteLine("****************************************\n");

            int userChoice = userInputOutput.ReadInt($"Please enter your choice:\n",min, max);
            Console.Clear();
            return userChoice;
        }

        public Order GetNewOrderInformationFromUser(List<Product> products,List<Tax> taxes)
        {
            Order order = new Order();
            order.orderDate = userInputOutput.ReadNewOrderDate("Please enter a date for the order you wish to place (MM/DD/YYYY): ");
            order.CustomerName = userInputOutput.ReadCustomerName("Please enter the name of the customer (cannot be blank): \n\n");
            AvailableStates(taxes);
            order.state.StateAbbreviation = userInputOutput.ReadState("Please enter the two letter abbreviation for the state where the order is being placed: \n\n", taxes);
            DisplayProductListAndPricing(products);
            order.product.ProductType = userInputOutput.ReadProductType("Plese enter the name of the product you wish to order:", products);
            Console.Clear();
            order.Area = userInputOutput.ReadArea("Please enter the area of the flooring to install (minimum 100 square feet):");
            Console.Clear();
            return order;
        }

        public void GetEditOrderInformationFromUser(Order orderToEdit, List<Tax> states, List<Product> products)
        {
            orderToEdit.CustomerName = userInputOutput.ReadCustomerNameForEdit($"Upate customer Name or press Enter to continue ({orderToEdit.CustomerName}):", orderToEdit);
            AvailableStates(states);
            orderToEdit.state.StateAbbreviation = userInputOutput.ReadStateForEdit($"Update state or press Enter to continue ({orderToEdit.state.StateAbbreviation}):", orderToEdit, states);
            DisplayProductListAndPricing(products);
            orderToEdit.product.ProductType = userInputOutput.ReadProductTypeForEdit($"Update the product type or press Enter to continue({orderToEdit.product.ProductType}):", orderToEdit, products);
            orderToEdit.Area = userInputOutput.ReadAreaForEdit($"Update the area (in square feet) or press Enter to continue({orderToEdit.Area}):", orderToEdit);
        }

        public void OrderSummary(Order order)
        {
            Console.WriteLine("");
            Console.WriteLine("******************************************");
            Console.WriteLine($"OrderNumber: {order.OrderNumber}  |  Order Date: {order.orderDate:d}");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"State:    {order.state.StateName}");
            Console.WriteLine($"Product :   {order.product.ProductType}");
            Console.WriteLine($"Materials : ${order.MaterialCost}");
            Console.WriteLine($"Labor :     ${order.LaborCost}");
            Console.WriteLine($"Tax :       ${order.Tax}");
            Console.WriteLine($"Total :     ${order.Total}");
            Console.WriteLine("******************************************");
            Console.WriteLine("");
        }

        public void DisplayProductListAndPricing(List<Product> products)
        {
            Console.Clear();
            foreach (var product in products)
            {
                Console.WriteLine($"Product Type: {product.ProductType}");
                Console.WriteLine($"Cost per sq. ft.: {product.CostPerSquareFoot}   Labor Cost per sq. ft.: {product.LaborCostPerSquareFoot}");
                Console.WriteLine("==================================================================\n");
            }
            Console.WriteLine("\n\n");
        }

        public List<string> AvailableStates(List<Tax> taxes)
        {
            List<string> states = new List<string>();
            Console.WriteLine("\n\nStates we currently operate in: ");
            foreach (var tax in taxes)
            {
                Console.WriteLine($"{tax.StateName} - {tax.StateAbbreviation}");
                states.Add(tax.StateAbbreviation);
            }
            return states;
        }

        public List<Order> OrdersAvailableOnDate(List<Order> orderList)
        {
            try 
            {
                Console.WriteLine($"\nOrder numbers available on {orderList.Select(ol => ol.orderDate).First()}\n");
                foreach (var order in orderList)
                {
                    Console.Write($"{order.OrderNumber}  ");
                }
                Console.WriteLine("\n");

                return orderList;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("\nThere are no orders available on the date entered. Press any key to return to the main menu.\n");
                Console.ReadKey();
                Console.Clear();
            }
            return orderList; 
        }

        public DateTime GetOrderDateToEdit()
        {
            DateTime orderDate = userInputOutput.ReadDate("Please enter a date for the order you with to edit (MM/DD/YYYY):");
            return orderDate;
        }

        public DateTime GetOrderDateForNewOrder()
        {
            DateTime orderDate = userInputOutput.ReadNewOrderDate("Please enter a date for the order you wish to place (MM/DD/YYYY): ");
            return orderDate;
        }
        public DateTime GetOrderDateForRemoval()
        {
            DateTime orderDate = userInputOutput.ReadDate("Please enter a date for the order you wish to delete (MM/DD/YYYY): ");
            return orderDate;
        }

        public DateTime GetOrderDateForDisplay()
        {
            Console.Clear();
            DateTime orderDate = userInputOutput.ReadDate("Please enter a date for the orders you wish to view (MM/DD/YYYY): ");
            return orderDate;
        }

        public  int GetOrderNumberForEditing(List<Order> orderList)
        {
            int orderNumberForEditing = userInputOutput.ReadOrderNumber("Please select the order number you would like to edit: ", orderList);
            return orderNumberForEditing;
        }

        public int GetOrderNumberForRemoval(List<Order> orderList)
        {
            int orderNumberForRemoval = userInputOutput.ReadOrderNumber("Please select the order number you would like to delete: ", orderList);
            return orderNumberForRemoval;
        }

        public bool ConfirmSave()
        {
            bool saveOrder = userInputOutput.ReadKey("Would you like to complete and process this order? (Y/N)");
            return saveOrder;
        }

        public bool ConfirmDelete()
        {

            bool confirm = userInputOutput.ReadKey("\nPlease confirm that you want to delete this order. Press Y to delete. Press any other key to continue.\n");
            Console.WriteLine("\n\n");
            return confirm;
        }

        public void ShowActionOutcome(string message)
        {
            Console.WriteLine(message);
            PressKeyToContinue();
        }
        public void PressKeyToContinue()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
