using FlooringOrderingSystem.BLL;
using FlooringOrderingSystem.BLL.Exception_Handling;
using FlooringOrderingSystem.Model;
using FlooringOrderingSystem.Model.Responses;
using FlooringOrderingSystem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadTaxResponse = FlooringOrderingSystem.Model.Responses.LoadTaxResponse;

namespace FlooringOrderingSystem.Controller
{
    public class OrderController
    {
        private FlooringView _flooringView;
        private OrderManager _orderManager;

        public OrderController()
        {
            _flooringView = new FlooringView();
            _orderManager = OrderManagerFactory.Create();
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                try
                {
                    int menuChoice = _flooringView.ShowMenuAndGetUserChoice();

                    switch (menuChoice)
                    {
                        case 1:
                            DisplayOrders();
                            break;
                        case 2:
                            CreateOrder();
                            break;
                        case 3:
                            EditOrder();
                            break;
                        case 4:
                            RemoveOrder();
                            break;
                        case 5:
                            keepRunning = false;
                            break;
                    }
                }
                catch(DataDoesNotExistException ex)
                {
                    _flooringView.ShowActionOutcome(ex.Message);
                }
                catch (InvalidCustomerNameException ex)
                {
                    _flooringView.ShowActionOutcome(ex.Message);
                }
                catch (InvalidDecimalException ex)
                {
                    _flooringView.ShowActionOutcome(ex.Message);
                }
                catch (OrderDateNotInFutureException ex)
                {
                    _flooringView.ShowActionOutcome(ex.Message);
                }
            }
        }
        private void DisplayOrders()
        {
            LoadTaxResponse loadTax = _orderManager.LoadTaxes();
            DateTime orderDateToDisplay = _flooringView.GetOrderDateForDisplay();
            LookUpOrderResponse orderList = _orderManager.ListOrdersByDate(orderDateToDisplay);
            foreach (var order in orderList.Orders)
            {
                _flooringView.OrderSummary(order);
            }
            _flooringView.ShowActionOutcome(orderList.Message);
        }

        private void CreateOrder()
        {
            LoadTaxResponse loadTax = _orderManager.LoadTaxes();
            LoadProductResponse products = _orderManager.LoadProductList();
            CreateOrderResponse orderToCreate = new CreateOrderResponse();
            orderToCreate.Order = _flooringView.GetNewOrderInformationFromUser(products.Products, loadTax.Taxes);
            orderToCreate.Order = _orderManager.CalculateOrder(orderToCreate.Order);
            _flooringView.OrderSummary(orderToCreate.Order);
            bool saveOrder = _flooringView.ConfirmSave();
            if(saveOrder == true)
            {
                orderToCreate.Message = "\nOrder successfully saved!\n";
                _orderManager.SendOrderToRepository(orderToCreate.Order);
            }
            else
            {
                orderToCreate.Message = "\nCancelling order. Returning to main menu.\n";
            }

            _flooringView.ShowActionOutcome(orderToCreate.Message);
        }

        private void EditOrder()
        {
            LoadTaxResponse loadTax = _orderManager.LoadTaxes();
            LoadProductResponse products = _orderManager.LoadProductList();
            DateTime orderDate = _flooringView.GetOrderDateToEdit();
            LookUpOrderResponse orderList = _orderManager.ListOrdersByDate(orderDate);
            _flooringView.OrdersAvailableOnDate(orderList.Orders);
            if(orderList.Orders.Count > 0)
            {
                int orderNumberToEdit = _flooringView.GetOrderNumberForEditing(orderList.Orders);
                EditOrderResponse orderToEdit = new EditOrderResponse();
                orderToEdit.Order = _orderManager.GetOrderFromRepo(orderDate, orderNumberToEdit);
                _flooringView.GetEditOrderInformationFromUser(orderToEdit.Order, loadTax.Taxes, products.Products);
                Order orderInMemory = _orderManager.GetOrderFromRepo(orderDate, orderNumberToEdit);

                if (orderToEdit.Order.state.StateAbbreviation != orderInMemory.state.StateAbbreviation || orderToEdit.Order.product.ProductType != orderInMemory.product.ProductType || orderToEdit.Order.Area != orderInMemory.Area)
                {
                    orderToEdit.Order = _orderManager.CalculateOrder(orderToEdit.Order);
                }
                _flooringView.OrderSummary(orderToEdit.Order);
                bool saveOrder = _flooringView.ConfirmSave();
                if (saveOrder == true)
                {
                    orderToEdit.Message = "\nOrder successfully updated!\n";
                    _orderManager.SendOrderToRepository(orderToEdit.Order);
                }
                else
                {
                    orderToEdit.Message = "\nCancelling order update. Returning to main menu.\n";
                }
                _flooringView.ShowActionOutcome(orderToEdit.Message);
            }
        }

        private void RemoveOrder()
        {
            LoadTaxResponse loadTax = _orderManager.LoadTaxes();
            DateTime orderDateForRemoval = _flooringView.GetOrderDateForRemoval();
            LookUpOrderResponse orderList = _orderManager.ListOrdersByDate(orderDateForRemoval);
            _flooringView.OrdersAvailableOnDate(orderList.Orders);
            if (orderList.Orders.Count > 0)
            {
                int orderNumberForRemoval = _flooringView.GetOrderNumberForRemoval(orderList.Orders);

                DeleteOrderResponse orderToRemove = new DeleteOrderResponse();
                orderToRemove.Order = _orderManager.GetOrderFromRepo(orderDateForRemoval, orderNumberForRemoval);
                _flooringView.OrderSummary(orderToRemove.Order);
                bool removeOrder = _flooringView.ConfirmDelete();
                if (removeOrder == true)
                {
                    orderToRemove.Message = "\nOrder successfully deleted!\n";
                    _orderManager.RemoveOrder(orderToRemove.Order);
                }
                else
                {
                    orderToRemove.Message = "\nOrder not deleted. Returning to Main Menu.\n";
                }
                _flooringView.ShowActionOutcome(orderToRemove.Message);
            }
            
        }

    }
}
