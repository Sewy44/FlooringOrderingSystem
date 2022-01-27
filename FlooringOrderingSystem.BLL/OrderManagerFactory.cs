using FlooringOrderingSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "test":
                    return new OrderManager(new TestOrderRepository(), new TaxRepository(), new ProductRepository());
                case "prod":
                    return new OrderManager(new OrderRepository(), new TaxRepository(), new ProductRepository());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
