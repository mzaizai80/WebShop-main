using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class ServicesTests
    {
        private IService _services;
        private Mock<ProductService> _mockProductService;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<ProductService>();
            _services = new Services(_mockProductService.Object); 
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
    }
}