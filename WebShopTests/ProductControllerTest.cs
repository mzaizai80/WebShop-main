using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShopTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private Mock<IProductService> _productServiceMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private Mock<IService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _productServiceMock = new Mock<IProductService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _serviceMock = new Mock<IService>();
        }

        [Test]
        public void Index_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object, 
                _productServiceMock.Object,
                _categoryServiceMock.Object, 
                _serviceMock.Object);

            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>());

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Details_ExistingId_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object, 
                _productServiceMock.Object,
                _categoryServiceMock.Object, 
                _serviceMock.Object);

            int existingId = 1;
            _productServiceMock.Setup(x => x.GetProductByProductId(existingId)).Returns(new Product());

            // Act
            var result = controller.Details(existingId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Details"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Create_Get_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            _categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<Category>());

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Create_Post_InvalidModel_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            controller.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = controller.Create(new Product()) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }


        [Test]
        public void Edit_Get_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            int productId = 1;
            _productServiceMock.Setup(x => x.GetProductByProductId(productId)).Returns(new Product());
            _categoryServiceMock.Setup(x => x.GetAllCategories()).Returns(new List<Category>());

            // Act
            var result = controller.Edit(productId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Edit_Post_ValidModel_RedirectsToDetails()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            var product = new Product { Id = 1, Name = "TestProduct" };
            var model = new HomeViewModel { Products = new List<Product> { product } };
            controller.ModelState.Clear(); // Ensure ModelState is valid

            // Act
            var result = controller.Edit(model) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(product.Id));
        }


 [Test]
        public void Delete_Get_ExistingId_ReturnsViewResultWithModel()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            int productId = 1;
            _productServiceMock.Setup(x => x.GetProductByProductId(productId)).Returns(new Product());

            // Act
            var result = controller.Delete(productId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Delete"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void DeleteConfirmed_ExistingId_RedirectsToProductsByCategory()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            int productId = 1;
            var product = new Product { Id = productId, CategoryId = 2 };
            _productServiceMock.Setup(x => x.GetProductByProductId(productId)).Returns(product);

            // Act
            var result = controller.DeleteConfirmed(productId) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("ProductsByCategory"));
            Assert.That(result.RouteValues["categoryId"], Is.EqualTo(product.CategoryId));
        }

        [Test]
        public void Delete_Get_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            int nonExistingId = 99;
            _productServiceMock.Setup(x => x.GetProductByProductId(nonExistingId)).Returns((Product)null);

            // Act
            var result = controller.Delete(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void DeleteConfirmed_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var controller = new ProductController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object);

            int nonExistingId = 99;
            _productServiceMock.Setup(x => x.GetProductByProductId(nonExistingId)).Returns((Product)null);

            // Act
            var result = controller.DeleteConfirmed(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

    }
}

