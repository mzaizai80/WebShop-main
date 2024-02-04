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
    public class CategoryControllerTests
    {
        private CategoryController _controller;
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

            _controller = new CategoryController(
                _loggerMock.Object,
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _serviceMock.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public void Index_ReturnsViewWithModel()
        {
            // Arrange
            _categoryServiceMock.Setup(c => c.GetAllCategories()).Returns(new List<Category> { new Category() });

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Create_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("SomeField", "Invalid input");

            // Act
            var result = _controller.Create(new Category()) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Create_ValidModel_RedirectsToDetails()
        {
            // Arrange
            var validCategory = new Category { Id = 1 };

            // Act
            var result = _controller.Create(validCategory) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(validCategory.Id));
        }

        [Test]
        public void Details_CategoryExists_ReturnsViewWithModel()
        {
            // Arrange
            int categoryId = 1;
            _categoryServiceMock.Setup(c => c.GetCategoryById(categoryId)).Returns(new Category());
            _productServiceMock.Setup(p => p.GetAllProductsByCategory(categoryId)).Returns(new List<Product>());

            // Act
            var result = _controller.Details(categoryId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Details"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Details_CategoryDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int nonExistingCategoryId = 2;
            _categoryServiceMock.Setup(c => c.GetCategoryById(nonExistingCategoryId)).Returns((Category)null);

            // Act
            var result = _controller.Details(nonExistingCategoryId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }



        [Test]
        public void Edit_Get_ReturnsViewWithModel()
        {
            // Arrange
            int categoryId = 1;
            _categoryServiceMock.Setup(c => c.GetCategoryById(categoryId)).Returns(new Category());
            _productServiceMock.Setup(p => p.GetAllProductsByCategory(categoryId)).Returns(new List<Product>());

            // Act
            var result = _controller.Edit(categoryId) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.Model, Is.InstanceOf<HomeViewModel>());
        }

        [Test]
        public void Edit_Post_InvalidModel_ReturnsViewWithModelError()
        {
            // Arrange
            var invalidModel = new HomeViewModel();
            _controller.ModelState.AddModelError("SomeField", "Invalid input");

            // Act
            var result = _controller.Edit(invalidModel) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Edit"));
            Assert.That(result.Model, Is.EqualTo(invalidModel));
        }

        [Test]
        public void Edit_Post_ValidModel_RedirectsToDetails()
        {
            // Arrange
            var validModel = new HomeViewModel
            {
                Categories = new List<Category> { new Category { Id = 1 } },
                Products = new List<Product>()
            };

            // Act
            var result = _controller.Edit(validModel) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(validModel.Categories.First().Id));
        }

        [Test]
        public void DeleteConfirmed_DeletesCategoryAndRedirectsToIndex()
        {
            // Arrange
            var categoryId = 1;

            // Act
            var result = _controller.DeleteConfirmed(categoryId) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));

            _categoryServiceMock.Verify(c => c.DeleteCategory(categoryId), Times.Once);
            _productServiceMock.Verify(p => p.DeleteProductByCategoryId(categoryId), Times.Once);
        }
    }
}
