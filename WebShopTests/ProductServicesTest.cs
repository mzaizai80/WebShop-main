using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebShop.Models;
using WebShop.Services;
using Newtonsoft.Json;

namespace WebShopTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IFileService> _fileServiceMock;
        private ProductService _productService;
        private Mock<IOptions<WebShopFileServiceOptions>> _optionsMock;

        [SetUp]
        public void SetUp()
        {
            var productsFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "test_data/products_test.json");

            _fileServiceMock = new Mock<IFileService>();
            _optionsMock = new Mock<IOptions<WebShopFileServiceOptions>>();
            _optionsMock.Setup(x => x.Value).Returns(new WebShopFileServiceOptions { ProductsFilePath = productsFilePath });

            _productService = new ProductService(
                _fileServiceMock.Object,
                _optionsMock.Object,
                new List<Product>(),
                Mock.Of<ICategoryService>()
            );
        }

        
        //[SetUp]
        //public void SetUp()
        //{
        //    var productsFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "test_data/products_test.json");

        //    _fileServiceMock = new Mock<IFileService>();
        //    _optionsMock = new Mock<IOptions<WebShopFileServiceOptions>>();
        //    _optionsMock.Setup(x => x.Value.ProductsFilePath).Returns(productsFilePath);

        //    _productService = new ProductService(
        //        _fileServiceMock.Object,
        //        _optionsMock.Object,
        //        new List<Product>()
        //    );
        //}

        [Test]
        public void AddProduct_ValidProduct_ProductAddedSuccessfully1()
        {
            // Arrange
            var productToAdd = new Product { Id = 1, Name = "TestProduct", Price = 10.99m };
            var products = new List<Product>();
            _fileServiceMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

            // Act
            _productService.AddProduct(productToAdd);

            // Assert
            Assert.That(_fileServiceMock.Invocations.Count, Is.EqualTo(2)); 
            Assert.That(_productService, Contains.Item(productToAdd)); 
        }

        [Test]
        public void AddProduct_ValidProduct_ProductAddedSuccessfully()
        {
            // Arrange
            var productToAdd = new Product { Id = 1, Name = "TestProduct", Price = 10.99m };
            var products = new List<Product>();
            _fileServiceMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((path, content) =>
                {
                    // Capture the products being written to the file
                    products = JsonConvert.DeserializeObject<List<Product>>(content);
                });

            // Act
            _productService.AddProduct(productToAdd);

            // Assert
            Assert.That(products, Contains.Item(productToAdd));
            Assert.That(_productService.GetProductById(productToAdd.Id), Is.EqualTo(productToAdd));
        }

        [Test]
        public void AddProduct_ValidProduct_ProductAddedSuccessfully2w()
        {
            // Arrange
            var productToAdd = new Product { Id = 1000, Name = "TestProduct", Price = 10.99m };
            Console.WriteLine("productToAdd",productToAdd);
            var products = new List<Product>();
            Console.WriteLine("List<Product>()",products);
            _fileServiceMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _fileServiceMock.Setup(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((path, content) =>
                {
                    // Capture the products being written to the file
                    products = JsonConvert.DeserializeObject<List<Product>>(content);

                    // Print the content for debugging
                    Console.WriteLine($"Content written to file: {content}");
                });

            // Act
            _productService.AddProduct(productToAdd);

            //Assert
            //Uncomment the assertions for further testing

            Console.WriteLine($"Invocations count: {_fileServiceMock.Invocations.Count}");
            Console.WriteLine($"Products in list: {string.Join(", ", products)}");
            Console.WriteLine($"Retrieved product: {_productService.GetProductById(productToAdd.Id)}");

            //Assert.That(_fileServiceMock.Invocations.Count, Is.EqualTo(1));
            //Assert.That(products, Contains.Item(productToAdd));
            //Assert.That(_productService.GetProductById(productToAdd.Id), Is.Not.Null);
            Assert.That(_productService.GetProductById(productToAdd.Id), Is.EqualTo(productToAdd));
        }





        [Test]
        public void GetProductById_ProductExists_ReturnsCorrectProduct()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "TestProduct", Price = 10.99m };
            var products = new List<Product> { existingProduct };
            _fileServiceMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
            _fileServiceMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            // Act
            var result = _productService.GetProductById(existingProduct.Id);

            // Assert
            Assert.That(_fileServiceMock.Invocations.Count, Is.EqualTo(1));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(existingProduct.Id));
            Assert.That(result.Name, Is.EqualTo(existingProduct.Name));
        }
    }
}






















//using Microsoft.Extensions.Options;
//using Moq;
//using WebShop.Models;
//using WebShop.Services;

//namespace WebShopTests
//{
//    [TestFixture]
//    public class ProductServiceTests
//    {
//        private ProductService _productService;
//        private Mock<IFileService> _fileServiceMock;
//        private Mock<IOptions<WebShopFileServiceOptions>> _optionsMock;

//        [SetUp]
//        public void Setup()
//        {
//            _fileServiceMock = new Mock<IFileService>();
//            _optionsMock = new Mock<IOptions<WebShopFileServiceOptions>>();
//            _productService = new ProductService(_fileServiceMock.Object, _optionsMock.Object, new List<Product>());
//        }

//        [Test]
//        public void AddProduct_ValidProduct_SavesProduct()
//        {
//            // Arrange
//            var product = new Product { Id = 1, Name = "TestProduct", PictureUrl = "test.jpg" };

//            // Act
//            _productService.AddProduct(product);

//            // Assert
//            Assert.That(_productService.Count(), Is.EqualTo(1));
//            Assert.That(_productService.GetProductById(1), Is.EqualTo(product));
//        }

//        [Test]
//        public void GetProductById_ProductExists_ReturnsProduct()
//        {
//            // Arrange
//            var product = new Product { Id = 1, Name = "TestProduct", PictureUrl = "test.jpg" };
//            _productService.AddProduct(product);

//            // Act
//            var result = _productService.GetProductById(1);

//            // Assert
//            Assert.That(result, Is.Not.Null);
//            Assert.That(result, Is.EqualTo(product));
//        }



//    }
//}

















////using NUnit.Framework;
////using Moq;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using WebShop.Models;
////using WebShop.Services;
////using Microsoft.Extensions.Options;

////namespace WebShopTests
////{
////    [TestFixture]
////    public class ProductServicesTest
////    {
////        private ProductService _productService;
////        private Mock<IFileService> _mockFileService;
////        private Mock<IOptions<WebShopFileServiceOptions>> _mockOptions;

////        [SetUp]
////        public void Setup()
////        {
////            _mockFileService = new Mock<IFileService>(MockBehavior.Strict);
////            _mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////            _productService = new ProductService(_mockFileService.Object, _mockOptions.Object, new List<Product>());
////        }

////        [Test]
////        public void GetAllProducts_Returns_Products()
////        {
////            // Arrange
////            var expectedProducts = new List<Product>
////            {
////                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
////                new Product { Id = 2, Name = "Smartphone", Price = 499.99m }
////                // Add more sample products if needed
////            };

////            _mockFileService.Setup(s => s.ReadAllText(It.IsAny<string>())).Returns("[]"); // Mocking an empty JSON array for GetAllProducts
////            _mockFileService.Setup(s => s.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));
////            _mockOptions.Setup(o => o.Value).Returns(new WebShopFileServiceOptions { ProductsFilePath = "dummyFilePath" });

////            // Act
////            var result = _productService.GetAllProducts();

////            // Assert
////            Assert.That(result.Count(), Is.EqualTo(0)); // Assuming no products are initially present in the mocked file
////        }

        
////        [Test]
////        public void GetProductById_ProductExists_ReturnsProduct()
////        {
////            var productId = 1;

////            // Mock IFileService
////            var mockFileReader = new Mock<IFileService>();
////            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
////                .Returns("[{\"Id\": 1,\"Name\": \"Laptop\",\"Price\": 999.99,\"PictureUrl\": \"laptop.jpg\"}," +
////                         "{\"Id\": 2,\"Name\": \"Smartphone\",\"Price\": 499.99,\"PictureUrl\": \"smartphone.jpg\"}," +
////                         "{\"Id\": 3,\"Name\": \"Headphones\",\"Price\": 79.99,\"PictureUrl\": \"headphones.jpg\"}]");

////            // Mock IOptions<WebShopFileServiceOptions>
////            var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////            mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////            {
////                ProductsFilePath = "data/products.json", // Correct file path
////                CategoriesFilePath = "data/categories.json",
////                ProductCategoryFilePath = "data/productCategoryRelation.json"
////            });

////            var productService = new ProductService(
////                mockFileReader.Object,
////                mockOptions.Object,
////                Mock.Of<ICategoryService>());

////            // Act
////            var result = productService.GetProductById(productId);

////            // Assert
////            Assert.That(result, Is.Not.Null);
////            Assert.That(result.Id, Is.EqualTo(productId));
////        }

////        [Test]
////        public void GetAllProducts_ReturnsListOfProducts()
////        {
////            // Mock IFileService
////            var mockFileReader = new Mock<IFileService>();
////            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
////                .Returns("[{\"Id\": 1,\"Name\": \"Laptop\",\"Price\": 999.99,\"PictureUrl\": \"laptop.jpg\"}]");

////            // Mock IOptions<WebShopFileServiceOptions>
////            var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////            mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////            {
////                ProductsFilePath = "data/products.json",
////                CategoriesFilePath = "data/categories.json",
////                ProductCategoryFilePath = "data/productCategoryRelation.json"
////            });

////            var productService = new ProductService(
////                mockFileReader.Object,
////                mockOptions.Object,
////                Mock.Of<ICategoryService>()); 

////            // Act
////            List<Product> products = productService.GetAllProducts();

////            // Assert
////            Assert.That(products, Is.Not.Null);
////            Assert.That(products.Count, Is.EqualTo(1));
////            Assert.That(products.First().Name, Is.EqualTo("Laptop"));
////        }

/////// <summary>
/////// ////
/////// </summary>
////        //[Test]
////        //public void GetAllCategories_ReturnsCategories()
////        //{


////        //    // Arrange
////        //    //var categoriesJson = "[{\"Id\": 1, \"Name\": \"Electronics\"},{\"Id\": 2, \"Name\": \"Accessories\"}]";
////        //    //_fileServicesMock.Setup(x => x.ReadAllText("data/categories.json")).Returns(categoriesJson);
////        //    var categoriesJson = "[{\"Id\": 1, \"Name\": \"Electronics\"},{\"Id\": 2, \"Name\": \"Accessories\"}]";
////        //    _fileServicesMock.Setup(x => x.ReadAllText("test_data/categories.json")).Returns(categoriesJson);


////        //    // Act
////        //    var result = _productService.GetAllCategories();

////        //    // Assert
////        //    Assert.IsNotNull(result);
////        //    Assert.IsInstanceOf<List<Category>>(result);

////        //}


////        //[Test]
////        //public void GetAllCategories_ReturnsCategories()
////        //{
////        //    // Arrange
////        //    var categories = new List<Category>
////        //    {
////        //        new Category { Id = 1, Name = "Category X" },
////        //        new Category { Id = 2, Name = "Category Y" },
////        //    };
////        //    var categoriesJson = JsonConvert.SerializeObject(categories);
////        //    _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json")).Returns(categoriesJson);

////        //    // Act
////        //    var result = _productService.GetAllCategories();

////        //    // Assert
////        //    Assert.IsNotNull(result);
////        //}

////    [Test]
////    public void GetAllCategories_ReturnsNonNullList()
////    {
////        // Arrange
////        var mockCategoryService = new Mock<ICategoryService>();
////        var expectedCategories = new List<Category>();
////        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);

////        // Mock IOptions<WebShopFileServiceOptions>
////        var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////        mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////        {
////            ProductsFilePath = "data/products.json",
////            CategoriesFilePath = "data/categories.json",
////            ProductCategoryFilePath = "data/productCategoryRelation.json"
////        });

////        var productService = new ProductService(
////            _fileServicesMock.Object,
////            mockOptions.Object,
////            mockCategoryService.Object);

////        // Act
////        List<Category> result = Category.GetAllCategories();

////        // Assert
////        Assert.That(result, Is.Not.Null);
////    }

////    [Test]
////    public void GetAllCategories_ReturnsEmptyList_WhenNoCategoriesExist()
////    {
////        // Arrange
////        var mockCategoryService = new Mock<ICategoryService>();
////        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(new List<Category>());

////        // Mock IOptions<WebShopFileServiceOptions>
////        var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////        mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////        {
////            ProductsFilePath = "data/products.json",
////            CategoriesFilePath = "data/categories.json",
////            ProductCategoryFilePath = "data/productCategoryRelation.json"
////        });

////        var productService = new ProductService(
////            _fileServicesMock.Object,
////            mockOptions.Object,
////            mockCategoryService.Object);

////        // Act
////        List<Category> result = productService.GetAllProducts();

////        // Assert
////        Assert.That(result, Is.Empty);
////    }

////    [Test]
////    public void GetAllCategories_ReturnsExpectedNumberOfCategories()
////    {
////        // Arrange
////        var expectedCategories = new List<Category>
////        {
////            new Category { Id = 1, Name = "Category A" },
////            new Category { Id = 2, Name = "Category B" }
////        };

////        var mockCategoryService = new Mock<ICategoryService>();
////        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);

////        // Mock IOptions<WebShopFileServiceOptions>
////        var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////        mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////        {
////            ProductsFilePath = "data/products.json",
////            CategoriesFilePath = "data/categories.json",
////            ProductCategoryFilePath = "data/productCategoryRelation.json"
////        });

////        var productService = new ProductService(
////            _fileServicesMock.Object,
////            mockOptions.Object,
////            mockCategoryService.Object);

////        // Act
////        List<Category> result = productService.GetAllProducts();

////        // Assert
////        Assert.That(result, Is.Not.Null);
////        Assert.That(result.Count, Is.EqualTo(expectedCategories.Count));
////    }

////    [Test]
////    public void GetAllCategories_ReturnsCategoriesWithMatchingNames()
////    {
////        // Arrange
////        var expectedCategories = new List<Category>
////        {
////            new Category { Id = 1, Name = "Category A" },
////            new Category { Id = 2, Name = "Category B" }
////        };

////        var mockCategoryService = new Mock<ICategoryService>();
////        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);
        
////        // Mock IOptions<WebShopFileServiceOptions>
////        var mockOptions = new Mock<IOptions<WebShopFileServiceOptions>>();
////        mockOptions.Setup(opt => opt.Value).Returns(new WebShopFileServiceOptions
////        {
////            ProductsFilePath = "data/products.json",
////            CategoriesFilePath = "data/categories.json",
////            ProductCategoryFilePath = "data/productCategoryRelation.json"
////        });

////        var productService = new ProductService(
////            _fileServicesMock.Object,
////            mockOptions.Object,
////            mockCategoryService.Object);


////        // Act
////        List<Category> result = productService.GetAllProducts();

////        // Assert
////        foreach (var expectedCategory in expectedCategories)
////        {
////                        Assert.That(result, Has.Some.Matches<Category>(c => c.Name == expectedCategory.Name));
////        }
////    }



////    [Test]
////    public void AddProduct_ProductAdded_Success()
////    {
////        // Arrange
////        var products = new List<Product>();
////        var product = new Product { Id = 1, Name = "Test Product", PictureUrl = "test.jpg" };

////        _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns("[]");
////        _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

////        // Act
////        _productService.AddProduct(product);
        
////        // Assert
////        _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
////        _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
////    }


////[Test]
////public void UpdateProduct_ExistingProductUpdated_Success()
////{
////    // Arrange
////    var products = new List<Product>
////    {
////        new Product { Id = 1, Name = "Existing Product", PictureUrl = "existing.jpg" }
////    };
////    var updatedProduct = new Product { Id = 1, Name = "Updated Product", PictureUrl = "updated.jpg" };

////    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
////    _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

////    // Act
////    _productService.UpdateProduct(updatedProduct);

////    // Assert
////    _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
////    _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
////}

////[Test]
////public void DeleteProduct_ExistingProductDeleted_Success()
////{
////    // Arrange
////    var products = new List<Product>
////    {
////        new Product { Id = 1, Name = "Product to Delete", PictureUrl = "delete.jpg" }
////    };

////    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
////    _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

////    // Act
////    _productService.DeleteProduct(1);

////    // Assert
////    _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
////    _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
////}

////[Test]
////public void GetAllProducts_ReturnsListOfProducts_Success()
////{
////    // Arrange
////    var products = new List<Product>
////    {
////        new Product { Id = 1, Name = "Product 1", PictureUrl = "product1.jpg" },
////        new Product { Id = 2, Name = "Product 2", PictureUrl = "product2.jpg" }
////    };

////    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));

////    // Act
////    var result = _productService.GetAllProducts();

////    // Assert
////    Assert.That(result.Count(), Is.EqualTo(products.Count));
////}
////    }
////}
