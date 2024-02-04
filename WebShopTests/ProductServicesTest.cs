using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;


namespace WebShopTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IFileService> _fileServiceMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private ProductService _productService;


        [SetUp]
        public void SetUp()
        {
            _fileServiceMock = new Mock<IFileService>();
            _categoryServiceMock = new Mock<ICategoryService>();

            _productService = new ProductService(_fileServiceMock.Object,
                new OptionsWrapper<FilepathServiceOptions>(new FilepathServiceOptions { ProductsFilePath = "test_data/products_test.json" }),
                new List<Product>(),
                _categoryServiceMock.Object);
        }

        [Test]
        public void GetProductById_ProductExists_ReturnsProduct()
        {
            // Arrange
            var expectedProduct = new Product { Id = 1, Name = "Test Product", Price = 9.99m };

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns("[{ \"Id\": 1, \"Name\": \"Test Product\", \"Price\": 9.99 }]");

            // Act
            var result = _productService.GetProductByProductId(1);


            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(expectedProduct.Id));
            Assert.That(result.Name, Is.EqualTo(expectedProduct.Name));
            Assert.That(result.Price, Is.EqualTo(expectedProduct.Price));
        }

        [Test]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 99.9m},
                new Product { Id = 2, Name = "Product 2", Price = 8.55m}
            };

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns("[{ \"Id\": 1, \"Name\": \"Product 1\", \"Price\": 99.9 },{ \"Id\": 2, \"Name\": \"Product 2\", \"Price\": 8.55 }]");

            //// Act
            var result = _productService.GetAllProducts();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedProducts.Count));
            Assert.That(result[0].Id, Is.EqualTo(expectedProducts[0].Id));
            Assert.That(result[1].Name, Is.EqualTo(expectedProducts[1].Name));
        }

        [Test]
        public void AddProduct_ProductAdded_Success()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", Price = 14.99m };
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns("[]");

            // Act
            _productService.AddProduct(newProduct);

            // Assert
            _fileServiceMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UpdateProduct_ExistingProductUpdated_Success()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { Id = 1, Name = "Existing Product", PictureUrl = "existing.jpg" }
    };
            var updatedProduct = new Product { Id = 1, Name = "Updated Product", PictureUrl = "updated.jpg" };

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            _productService.UpdateProduct(updatedProduct);

            // Assert
            _fileServiceMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
            _fileServiceMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void DeleteProduct_ExistingProductDeleted_Success()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { Id = 1, Name = "Product to Delete", PictureUrl = "delete.jpg" }
    };

            _fileServiceMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            _productService.DeleteProduct(1);

            // Assert
            _fileServiceMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
            _fileServiceMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAllProducts_ReturnsListOfProducts_Success()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", PictureUrl = "product1.jpg" },
        new Product { Id = 2, Name = "Product 2", PictureUrl = "product2.jpg" }
    };

            _fileServiceMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));

            // Act
            var result = _productService.GetAllProducts();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(products.Count));
        }

        [Test]
        public void SaveProducts_ShouldWriteToFile()
        {
            // Arrange
            var productsToSave = new List<Product> { new Product { Id = 1, Name = "Product1", Price = 10, CategoryId = 1 } };

            // Act
            _productService.SaveProducts(productsToSave);

            // Assert
            _fileServiceMock.Verify(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
