using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebShop;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModels;
using NUnit.Framework;

namespace WebShopTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private Mock<IService> _serviceMock;
        private Mock<IProductService> _productServiceMock;
        private Mock<ICategoryService> _categoryServiceMock;

        private HomeController? _controller;

        [TearDown]
        public void TearDown()
        {
            if (_controller is IDisposable disposableController)
            {
                disposableController.Dispose();
            }
            _controller = null;
        }


        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _serviceMock = new Mock<IService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _productServiceMock = new Mock<IProductService>();

            _controller = new HomeController(_loggerMock.Object, _serviceMock.Object, _productServiceMock.Object, _categoryServiceMock.Object);
        }

        [Test]
        public void Index_Returns_View_With_Products()
        {
            // Arrange

            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", PictureUrl = "url 1", Price = 10.99m },
                new Product { Id = 2, Name = "Product 2", PictureUrl = "url 2", Price = 19.99m }
            };

            _serviceMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _serviceMock.Setup(p => p.GetAllCategories()).Returns(new List<Category>());



            // Act
            var result = _controller?.Index() as ViewResult;

            // Assert


            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());

            var model = (HomeViewModel)result.Model;
            Assert.That(model.Products, Is.EqualTo(expectedProducts));


            Assert.That(model.Products.Select(p => p.Name), Is.EqualTo(expectedProducts.Select(p => p.Name)));

        }


        [Test]
        public void Index_Returns_Empty_View_When_No_Products2()
        {
            // Arrange
            _serviceMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>());
            _serviceMock.Setup(p => p.GetAllCategories()).Returns(new List<Category>());


            // Act
            var result = _controller?.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null.And.InstanceOf<ViewResult>());
            Assert.That(result.ViewName, Is.EqualTo("Index").Or.Null);
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());

            var model = (HomeViewModel)result.Model;
            Assert.That(model.Products, Is.Not.Null.And.Empty);
            Assert.That(model.Categories, Is.Not.Null.And.Empty);

        }

    }
}