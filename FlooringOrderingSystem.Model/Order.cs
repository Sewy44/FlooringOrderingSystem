using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderingSystem.Model
{
    public class Order
    {
        public Order()
        {
            state = new Tax();
            product = new Product();
        }
        public Tax state;
        public Product product;
        public DateTime orderDate;
 
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
