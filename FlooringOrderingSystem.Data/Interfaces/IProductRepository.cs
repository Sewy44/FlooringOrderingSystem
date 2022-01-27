using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Model;

namespace FlooringOrderingSystem.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> ReadAllProducts();
        Product ReadProductByType(string ProductType);
    }
}
