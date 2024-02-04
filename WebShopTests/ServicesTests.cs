using Moq;
using WebShop.Models;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class ServicesTests
    {
        private IService _services;
        private Mock<IProductService> _mockProductService;
        private Mock<ICategoryService> _mockCategoryService;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _mockCategoryService = new Mock<ICategoryService>();

            _services = new WebShop.Services.Services(_mockProductService.Object, _mockCategoryService.Object); 
        }

        [Test]
        public void GetAllCategories_Returns_Categories_From_CategoryService()
        {
            // Arrange
            var expectedCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Accessories" }
            };

           
            _mockCategoryService.Setup(s => s.GetAllCategories()).Returns(expectedCategories);
            // Act
            var result = _services.GetAllCategories();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedCategories.Count()));
        }

        [Test]
        public void GetAllProducts_Returns_Products_From_ProductService()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };

            _mockProductService.Setup(s => s.GetAllProducts()).Returns(expectedProducts);

            // Act
            var result = _services.GetAllProducts();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedProducts.Count()));
        }
    }
}