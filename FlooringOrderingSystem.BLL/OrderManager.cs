using FlooringOrderingSystem.BLL.Exception_Handling;
using FlooringOrderingSystem.Data.Interfaces;
using FlooringOrderingSystem.Data.Repositories;
using FlooringOrderingSystem.Model;
using FlooringOrderingSystem.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        public OrderManager(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        public LookUpOrderResponse ListOrdersByDate(DateTime orderDate)
        {
            LoadTaxResponse states = LoadTaxes();
            LookUpOrderResponse response = new LookUpOrderResponse();
            
                response.Orders = _orderRepository.ReadAllOrdersByDate(orderDate);
                if(response.Orders == null)
                {
                    response.Success = false;
                    response.Message = "There are no orders available on the date entered.";
                    return response;
                }
                else
                {
                    response.Success = true;
                    foreach (var state in states.Taxes)
                    {
                        foreach (var order in response.Orders)
                        {
                            if (state.StateAbbreviation == order.state.StateAbbreviation)
                            {
                                order.state.StateName = state.StateName;
                            }
                        }
                    }

                }
            return response;
        }

        public LoadTaxResponse LoadTaxes()
        {
            LoadTaxResponse response = new LoadTaxResponse();
            response.Taxes = _taxRepository.ReadAllTaxes();
            if(response.Taxes == null)
            {
                response.Success = false;
                response.Message = "Tax file failed to load.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public LoadProductResponse LoadProductList()
        {
            LoadProductResponse response = new LoadProductResponse();
            response.Products = _productRepository.ReadAllProducts();
            if (response.Products == null)
            {
                response.Success = false;
                response.Message = "Product file failed to laod.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        private Order GetOrderNumber(Order order)
        {
            List<Order> ordersOnDate = ListOrdersByDate(order.orderDate).Orders.OrderBy(ord => ord.OrderNumber).ToList();
            if (ordersOnDate.Any() != true)
            {
                order.OrderNumber = 1;
            }
            else
            {
                int previousOrderNumber = ordersOnDate.Last().OrderNumber;
                order.OrderNumber = previousOrderNumber + 1;
            }
            return order;
        }

        public Order CalculateOrder(Order order)
        {
            if(order.OrderNumber == 0) { order = GetOrderNumber(order); }

            Product orderProduct = _productRepository.ReadProductByType(order.product.ProductType);
            if(orderProduct == null) { throw new DataDoesNotExistException("That product does not exist."); }
            order.product = orderProduct;

            Tax state = _taxRepository.ReadTaxesByState(order.state.StateAbbreviation);
            if(state == null) { throw new DataDoesNotExistException("That state does not exist. "); }
            order.state = state;

            if(order.orderDate < DateTime.Today) { throw new OrderDateNotInFutureException("Order date must be in the future. "); }
            
            if(order.Area < 100) { throw new InvalidDecimalException("Area must be greater than 100 square feet. "); }

            Regex validCustomerName = new Regex("[^0 - 9A-Za-z.,]");

            if(validCustomerName.IsMatch(order.CustomerName)){ throw new InvalidCustomerNameException("Customer name can include letters, numbers, periods(.), and commas(,) "); }

            order.MaterialCost = decimal.Round((order.Area * order.product.CostPerSquareFoot), 2);
            order.LaborCost = decimal.Round((order.Area * order.product.LaborCostPerSquareFoot), 2);
            order.Tax = decimal.Round(((order.MaterialCost + order.LaborCost) * (order.state.TaxRate / 100)), 2);
            order.Total = decimal.Round((order.MaterialCost + order.LaborCost + order.Tax), 2);

            return order;
        }

        public Order GetOrderFromRepo(DateTime orderDate, int orderNumberToEdit)
        {

            Order order = _orderRepository.ReadOrdersByDateThenByOrderNumber(orderDate, orderNumberToEdit);
            if (order == null)
            {
                throw new DataDoesNotExistException("There are no orders available on the date entered.");
            }
            else
            {
                return order;
            }  
        }

        public void SendOrderToRepository(Order order)
        {
            _orderRepository.SaveOrderToRepo(order);
        }

        public void RemoveOrder(Order orderToRemove)
        {
            _orderRepository.DeleteOrder(orderToRemove);
        }
    }
}
