using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.View
{
    public class UserInputOutput
    {
        public string ReadCustomerName(string prompt)
        {//Reads customername
            string userInput ="";

            while(string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(prompt);
                userInput = Console.ReadLine().Trim();

                Regex validCustomerName = new Regex("[^0 - 9A-Za-z.,]");

                if (userInput == "")
                {
                    Console.WriteLine("\nThat was not a valid input. Please try again.\n");
                }
                else if (validCustomerName.IsMatch(userInput))
                {
                    Console.WriteLine("\nA valid customer name can contain, letters, numbers, commas, and periods. Please try again.\n ");
                    userInput = "";
                }
            }
            return userInput;
        }

        internal string ReadCustomerNameForEdit(string prompt, Order orderToEdit)
        {
            {//Reads customername
                string userInput = "";

                while (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine(prompt);
                    userInput = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        userInput = orderToEdit.CustomerName;
                    }
                }
                return userInput;
            }
        }

        public int ReadInt(string prompt, int min, int max)
        {//Reads and Parses from ShowMenuAndGetUserChoice
            int output;
            
            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if(int.TryParse(userInput, out output))
                {
                    if (output >= min && output <=max)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nPlease enter a number between {min} and {max}.\n");
                    }
                }
                else
                { 
                    Console.WriteLine("\nThat was not a valid input. Please try again.\n");
                }
            }
            return output;
        }

        public string ReadState(string prompt, List<Tax> taxes)
        {
            string userInput;

            while (true)
            {
                Console.WriteLine(prompt);
                userInput = Console.ReadLine().Trim();

                if ((taxes.Any(t => t.StateAbbreviation == userInput)))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("That input was invalid. Please try again.");
                }
            }
        }

        internal string ReadStateForEdit(string prompt, Order orderToEdit, List<Tax> states)
        {
            string userInput;

            while (true)
            {
                Console.WriteLine(prompt);
                userInput = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    userInput = orderToEdit.state.StateAbbreviation;
                }
                if ((states.Any(t => t.StateAbbreviation == userInput)))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("That input was invalid. Please try again.");
                }
            }
        }

        public string ReadProductType(string prompt, List<Product> products)
        {
            string userInput;

            while (true)
            {
                Console.WriteLine(prompt);
                userInput = Console.ReadLine().Trim();

                if ((products.Any(p => p.ProductType == userInput)))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("That input was invalid. Please try again.");
                }
            }
        }

        internal string ReadProductTypeForEdit(string prompt, Order orderToEdit, List<Product> products)
        {
            string userInput;

            while (true)
            {
                Console.WriteLine(prompt);
                userInput = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    userInput = orderToEdit.product.ProductType;
                }
                if ((products.Any(p => p.ProductType == userInput)))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("That input was invalid. Please try again.");
                }
            }
        }

        internal decimal ReadArea(string prompt)
        {
            decimal output;
            decimal min = 100;
            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (decimal.TryParse(userInput, out output))
                {
                    if (output >= min)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nPlease enter a number greater than or equal to 100 square feet.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid input. Please try again.\n");
                }
            }
            return output;
        }

        internal decimal ReadAreaForEdit(string prompt, Order orderToEdit)
        {
            decimal output;
            decimal min = 100;
            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    output = orderToEdit.Area;
                    break;
                }

                if (decimal.TryParse(userInput, out output))
                {

                    if(output >= min)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nPlease enter a number greater than or equal to 100 square feet.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nThat was not a valid input. Please try again.\n");
                }
            }
            return output;
        }
 
        internal int ReadOrderNumber(string prompt, List<Order> orderList)
        {
            int output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out output))
                {
                    if ((orderList.Any(ordList => ordList.OrderNumber == Convert.ToInt32(userInput))))
                    {
                        return output;
                    }
                    else
                    {
                        Console.WriteLine($"There is no order number matching your input for this order date.");
                    }
                }
            }
        }

        internal DateTime ReadNewOrderDate(string prompt)
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if(userInput.Length == 10)
                {
                    if (!DateTime.TryParse(userInput, out date))
                    {
                        Console.WriteLine("That was not a valid date. Please try again.\n");
                    }
                    else
                    {
                        if (date <= DateTime.Today)
                        {
                            Console.WriteLine("Order date must be in the future. Please try again.\n");
                        }

                        else
                        {
                            return date;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please use the format MM/DD/YYYY.");
                }
            }
        }

        public DateTime ReadDate(string prompt)
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                if (userInput.Length == 10)
                {
                    if (!DateTime.TryParse(userInput, out date))
                    {
                        Console.WriteLine("That was not a valid date. Please try again.\n");
                    }
                    else
                    {
                        return date;
                    }
                }    
            }
        }
  
        public bool ReadKey(string prompt)
        {
            Console.WriteLine(prompt);
            String userInput = Console.ReadLine();
            if(userInput == "Y" || userInput == "y")
            {

                return true;
            }
            return false;
        }
    }
}
