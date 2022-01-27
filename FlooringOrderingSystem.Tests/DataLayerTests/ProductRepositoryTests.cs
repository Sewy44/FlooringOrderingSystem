using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderingSystem.Data.Repositories;
using NUnit.Framework;
using FlooringOrderingSystem.Model;

namespace FlooringOrderingSystem.Tests.DataLayerTests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        ProductRepository productRepository = new ProductRepository();

        [Test]
        public void ReadAllProducts_CanReadAllProducts()
        {
            var productResult = productRepository.ReadAllProducts();
            Assert.IsNotNull(productResult);
        }

        [TestCase("Wood")]
        [TestCase("Carpet")]
        [TestCase("Tile")]
        [TestCase("Laminate")]
        [Test]
        public void ReadProductByType_CanReadProductByType(string productType)
        {
            Product productReturned = productRepository.ReadProductByType(productType);
            Assert.AreEqual(productType, productReturned.ProductType);
        }

    }
}
