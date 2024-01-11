using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class IServicesTests
    {/*
        private Services _services;
        private Mock<ProductService> _mockProductService;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<ProductService>(MockBehavior.Strict);
            _services = new Services(_mockProductService.Object);
        }

        [Test]
        public void GetAllProducts_Returns_Products_From_ProductService()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Smartphone", Price = 499.99m }
                // Add more sample products if needed
            };

            _mockProductService.Setup(s => s.GetAllProducts()).Returns(expectedProducts);

            // Act
            var result = _services.GetAllProducts();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedProducts.Count()));
        }

        [Test]
        public void GetAllCategories_Returns_Categories_From_ProductService()
        {
            // Arrange
            var expectedCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Accessories" }
            };

            _mockProductService.Setup(s => s.GetAllCategories()).Returns(expectedCategories);

            // Act
            var result = _services.GetAllCategories();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedCategories.Count()));
        }
*/
    }
}
