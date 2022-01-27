using FlooringOrderingSystem.Data.Interfaces;
using FlooringOrderingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace FlooringOrderingSystem.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        private static Product _product = new Product();
        private string productFileName = ConfigurationManager.AppSettings["productFileName"].ToString();

        public List<Product> ReadAllProducts()
        {//read the product file. Store the result in a List and return the list. Stores products in memory
            _products = new Dictionary<string, Product>();

            string[] rows = File.ReadAllLines(productFileName);

            for (int i = 1; i < rows.Length; i++)
            {
                _product = UnMarshallProduct(rows[i]);
                _products.Add(_product.ProductType, _product);
            }
            return _products.Values.ToList();
        }

        public Product ReadProductByType(string productType)
        {
            List<Product> productList = ReadAllProducts();

            return _products[productType];
        }

        private Product UnMarshallProduct(string productString)
        {
            string[] productElements = productString.Split(',');

            Product product = new Product();

            product.ProductType = productElements[0];
            product.CostPerSquareFoot = Convert.ToDecimal(productElements[1]);
            product.LaborCostPerSquareFoot = Convert.ToDecimal(productElements[2]);

            return product;
        }
    }
}
