using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;


namespace WebShopTests
{
    [TestFixture]
    public class ProductServiceTests
    {/*
        private ProductService _productService;
        private Mock<IProductService> _productServiceMock;
        private Mock<IFileService> _fileServicesMock;
        private Mock<ICategoryService> categoryServiceMock;
        private Mock<IProductCategoryRelationService> productCategoryServiceMock;
        private Mock<IOptions<ProductServiceOptions>> productServiceOptionsMock ;

        private const string _testProductsFilePath = "test_data/products_test.json";
        private const string _testCategoriesFilePath = "test_data/categories_test.json";
        private const string _testProductCategoryFilePath = "test_data/productCategoryRelation_test.json";

        [SetUp]
        public void Setup()
        {
            _fileServicesMock = new Mock<IFileService>();
            productServiceOptionsMock = new Mock<IOptions<ProductServiceOptions>>();
            _productServiceMock = new Mock<IProductService>(); 
            categoryServiceMock = new Mock<ICategoryService>();
            productCategoryServiceMock = new Mock<IProductCategoryRelationService>();

            var productServiceOptions = Options.Create(new ProductServiceOptions
            {
                ProductsFilePath = _testProductsFilePath,
                CategoriesFilePath = _testCategoriesFilePath,
                ProductCategoryFilePath = _testProductCategoryFilePath
            });

            _productService = new ProductService(
                _fileServicesMock.Object,
                productServiceOptions,
                Mock.Of<ICategoryService>(),
                Mock.Of<IProductCategoryRelationService>()
            );

            //string testContent = "Test file content";
            //_fileServicesMock.Setup(f => f.ReadAllText(_testProductsFilePath)).Returns(testContent);
            //_fileServicesMock.Setup(f => f.Exists(_testProductsFilePath)).Returns(true);

            // Clean up files before each test
            if (File.Exists(_testProductsFilePath))
                File.Delete(_testProductsFilePath);

            if (File.Exists(_testCategoriesFilePath))
                File.Delete(_testCategoriesFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testProductsFilePath))
            {
                File.Delete(_testProductsFilePath);
            }
        }
        
        [Test]
        public void GetProductById_ProductExists_ReturnsProduct()
        {
            var productId = 1;

            // Mock IFileService
            var mockFileReader = new Mock<IFileService>();
            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("[{\"Id\": 1,\"Name\": \"Laptop\",\"Price\": 999.99,\"PictureUrl\": \"laptop.jpg\"}," +
                         "{\"Id\": 2,\"Name\": \"Smartphone\",\"Price\": 499.99,\"PictureUrl\": \"smartphone.jpg\"}," +
                         "{\"Id\": 3,\"Name\": \"Headphones\",\"Price\": 79.99,\"PictureUrl\": \"headphones.jpg\"}]");

            // Mock IOptions<ProductServiceOptions>
            var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
            {
                ProductsFilePath = "data/products.json", // Correct file path
                CategoriesFilePath = "data/categories.json",
                ProductCategoryFilePath = "data/productCategoryRelation.json"
            });

            var productService = new ProductService(
                mockFileReader.Object,
                mockOptions.Object,
                Mock.Of<ICategoryService>(),
                Mock.Of<IProductCategoryRelationService>());

            // Act
            var result = productService.GetProductById(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
        }

        [Test]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            // Mock IFileService
            var mockFileReader = new Mock<IFileService>();
            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("[{\"Id\": 1,\"Name\": \"Laptop\",\"Price\": 999.99,\"PictureUrl\": \"laptop.jpg\"}]");

            // Mock IOptions<ProductServiceOptions>
            var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
            {
                ProductsFilePath = "data/products.json",
                CategoriesFilePath = "data/categories.json",
                ProductCategoryFilePath = "data/productCategoryRelation.json"
            });

            var productService = new ProductService(
                mockFileReader.Object,
                mockOptions.Object,
                Mock.Of<ICategoryService>(), 
                Mock.Of<IProductCategoryRelationService>()); 

            // Act
            List<Product> products = productService.GetAllProducts();

            // Assert
            Assert.That(products, Is.Not.Null);
            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products.First().Name, Is.EqualTo("Laptop"));
        }

/// <summary>
/// ////
/// </summary>
        //[Test]
        //public void GetAllCategories_ReturnsCategories()
        //{


        //    // Arrange
        //    //var categoriesJson = "[{\"Id\": 1, \"Name\": \"Electronics\"},{\"Id\": 2, \"Name\": \"Accessories\"}]";
        //    //_fileServicesMock.Setup(x => x.ReadAllText("data/categories.json")).Returns(categoriesJson);
        //    var categoriesJson = "[{\"Id\": 1, \"Name\": \"Electronics\"},{\"Id\": 2, \"Name\": \"Accessories\"}]";
        //    _fileServicesMock.Setup(x => x.ReadAllText("test_data/categories.json")).Returns(categoriesJson);


        //    // Act
        //    var result = _productService.GetAllCategories();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<List<Category>>(result);

        //}


        //[Test]
        //public void GetAllCategories_ReturnsCategories()
        //{
        //    // Arrange
        //    var categories = new List<Category>
        //    {
        //        new Category { Id = 1, Name = "Category X" },
        //        new Category { Id = 2, Name = "Category Y" },
        //    };
        //    var categoriesJson = JsonConvert.SerializeObject(categories);
        //    _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json")).Returns(categoriesJson);

        //    // Act
        //    var result = _productService.GetAllCategories();

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

    [Test]
    public void GetAllCategories_ReturnsNonNullList()
    {
        // Arrange
        var mockCategoryService = new Mock<ICategoryService>();
        var expectedCategories = new List<Category>();
        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);

        // Mock IOptions<ProductServiceOptions>
        var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
        mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
        {
            ProductsFilePath = "data/products.json",
            CategoriesFilePath = "data/categories.json",
            ProductCategoryFilePath = "data/productCategoryRelation.json"
        });

        var productService = new ProductService(
            _fileServicesMock.Object,
            mockOptions.Object,
            mockCategoryService.Object,
            Mock.Of<IProductCategoryRelationService>());

        // Act
        List<Category> result = Service.GetAllCategories();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetAllCategories_ReturnsEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(new List<Category>());

        // Mock IOptions<ProductServiceOptions>
        var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
        mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
        {
            ProductsFilePath = "data/products.json",
            CategoriesFilePath = "data/categories.json",
            ProductCategoryFilePath = "data/productCategoryRelation.json"
        });

        var productService = new ProductService(
            _fileServicesMock.Object,
            mockOptions.Object,
            mockCategoryService.Object,
            Mock.Of<IProductCategoryRelationService>());

        // Act
        List<Category> result = productService.GetAllCategories();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetAllCategories_ReturnsExpectedNumberOfCategories()
    {
        // Arrange
        var expectedCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Category A" },
            new Category { Id = 2, Name = "Category B" }
        };

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);

        // Mock IOptions<ProductServiceOptions>
        var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
        mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
        {
            ProductsFilePath = "data/products.json",
            CategoriesFilePath = "data/categories.json",
            ProductCategoryFilePath = "data/productCategoryRelation.json"
        });

        var productService = new ProductService(
            _fileServicesMock.Object,
            mockOptions.Object,
            mockCategoryService.Object,
            Mock.Of<IProductCategoryRelationService>());

        // Act
        List<Category> result = productService.GetAllCategories();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(expectedCategories.Count));
    }

    [Test]
    public void GetAllCategories_ReturnsCategoriesWithMatchingNames()
    {
        // Arrange
        var expectedCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Category A" },
            new Category { Id = 2, Name = "Category B" }
        };

        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService.Setup(c => c.GetAllCategories()).Returns(expectedCategories);
        
        // Mock IOptions<ProductServiceOptions>
        var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
        mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
        {
            ProductsFilePath = "data/products.json",
            CategoriesFilePath = "data/categories.json",
            ProductCategoryFilePath = "data/productCategoryRelation.json"
        });

        var productService = new ProductService(
            _fileServicesMock.Object,
            mockOptions.Object,
            mockCategoryService.Object,
            Mock.Of<IProductCategoryRelationService>());


        // Act
        List<Category> result = productService.GetAllCategories();

        // Assert
        foreach (var expectedCategory in expectedCategories)
        {
                        Assert.That(result, Has.Some.Matches<Category>(c => c.Name == expectedCategory.Name));
        }
    }



    [Test]
    public void AddProduct_ProductAdded_Success()
    {
        // Arrange
        var products = new List<Product>();
        var product = new Product { Id = 1, Name = "Test Product", PictureUrl = "test.jpg" };

        _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns("[]");
        _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

        // Act
        _productService.AddProduct(product);
        
        // Assert
        _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
        _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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

    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
    _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

    // Act
    _productService.UpdateProduct(updatedProduct);

    // Assert
    _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
    _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
}

[Test]
public void DeleteProduct_ExistingProductDeleted_Success()
{
    // Arrange
    var products = new List<Product>
    {
        new Product { Id = 1, Name = "Product to Delete", PictureUrl = "delete.jpg" }
    };

    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));
    _fileServicesMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

    // Act
    _productService.DeleteProduct(1);

    // Assert
    _fileServicesMock.Verify(f => f.ReadAllText(It.IsAny<string>()), Times.Once);
    _fileServicesMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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

    _fileServicesMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(products));

    // Act
    var result = _productService.GetAllProducts();

    // Assert
    Assert.That(result.Count(), Is.EqualTo(products.Count));
}




*/




        /* Refactoring test
           ###############################################################################


           [Test]
           public void GetAllProducts_ReturnsListOfProducts()
           {
           // Arrange
           var productsJson = JsonConvert.SerializeObject(new List<Product>
           {
           new Product { Id = 1, Name = "Product 1", Price = 10.99m },
           new Product { Id = 2, Name = "Product 2", Price = 20.49m },
           });
           _fileReaderMock.Setup(x => x.ReadAllText("data/products.json")).Returns(productsJson);

           // Act
           var products = _productService.GetAllProducts();

           // Assert
           Assert.IsNotNull(products);
           Assert.IsInstanceOf<List<Product>>(products);
           Assert.AreEqual(2, products.Count);
           }

        [Test]
        public void GetAllProducts_WhenFileExists_ReturnsListOfProducts()
        {
        // Arrange
        _fileReaderMock.Setup(x => x.ReadAllText("data/products.json"))
        .Returns(JsonConvert.SerializeObject(new List<Product>
        {
        new Product { Id = 1, Name = "Product 1", Price = 10.99m },
        new Product { Id = 2, Name = "Product 2", Price = 20.49m },
        }));

        // Act
        var products = _productService.GetAllProducts();

        // Assert
        Assert.IsNotNull(products);
        Assert.IsInstanceOf<List<Product>>(products);
        Assert.AreEqual(2, products.Count);
        }

        [Test]
        public void GetAllProducts_ShouldReturnProducts_WhenValidJsonFilesExist()
        {
        // Arrange
        _fileReaderMock.Setup(x => x.ReadAllText("data/products.json"))
        .Returns(JsonConvert.SerializeObject(new List<Product>
        {
        }));

        _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json"))
        .Returns(JsonConvert.SerializeObject(new List<Category>
        {
        }));

        _fileReaderMock.Setup(x => x.ReadAllText("data/productCategoryRelation.json"))
        .Returns(JsonConvert.SerializeObject(new List<SaveProductCategoryRelation>
        {
        }));

        // Act
        var result = _productService.GetAllProducts();

        // Assert
        Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllCategories_ShouldReturnCategories_WhenValidJsonFileExists()
        {
        // Arrange
        _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json"))
        .Returns(JsonConvert.SerializeObject(new List<Category>
        {
        }));

        // Act
        var result = _productService.GetAllCategories();

        // Assert
        Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllProducts_ShouldReturnProductsFromProductService()
        {
        // Arrange
        var productServiceMock = new Mock<ProductService>(MockBehavior.Strict, null, null);
        var services = new Services(productServiceMock.Object);
        var expectedProducts = new List<Product> { new Product { Id = 1, Name = "Product 1" } };
        productServiceMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);

        // Act
        var result = services.GetAllProducts();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedProducts, result);
        }

        [Test]
        public void GetAllCategories_ShouldReturnCategoriesFromProductService()
        {
        // Arrange
        var productServiceMock = new Mock<ProductService>();
        var services = new Services(productServiceMock.Object);
        var expectedCategories = new List<Category> { new Category { Id = 1, Name = "Category 1" } };
        productServiceMock.Setup(p => p.GetAllCategories()).Returns(expectedCategories);

        // Act
        var result = services.GetAllCategories();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedCategories, result);
        }

        [Test]
        public void GetAllProducts_Returns_EmptyList_When_FileDoesNotExist()
        {

        var mockFile = new Mock<IFileService>(); // Assuming a mocked IFile interface for file operations
        mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(false);

        //var productService = new ProductService();
        var productService = new ProductService(mockFile.Object, _testProductsFilePath);

        var result = productService.GetAllProducts();

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(0));

        }

        [Test]
        public void GetAllProducts_Returns_ProductList_From_ExistingFile()
        {

        // Sample JSON data for testing
        var testData = @"[
        { 'Id': 1, 'Name': 'Product 1', 'PictureUrl': 'url1', 'Price': 10.99 },
        { 'Id': 2, 'Name': 'Product 2', 'PictureUrl': 'url2', 'Price': 19.99 }
        ]";

        File.WriteAllText(_testProductsFilePath, testData); // Creating a test JSON file

        var mockFile = new Mock<IFileService>();
        mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
        mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

        var productService = new ProductService(mockFile.Object, _testProductsFilePath);


        //var mockFile = new Mock<IFileService>(); // Mocking the file system
        //mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
        //mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

        //var productService = new ProductService(mockFile.Object, _testProductsFilePath);
        //var productService = new ProductService();

        var result = productService.GetAllProducts();

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Product 1"));
        Assert.That(result[0].PictureUrl, Is.EqualTo("url1"));
        Assert.That(result[1].Price, Is.EqualTo(19.99m));
        Assert.That(result[1].PictureUrl, Is.EqualTo("url2"));

        File.Delete(_testProductsFilePath); // Cleaning up the test file

        }

        [Test]
        public void GetAllProducts_Returns_ProductList_From_ExistingFile1()
        {

        // Sample JSON data for testing
        var testData = @"[
        { 'Id': 1, 'Name': 'Product 1', 'PictureUrl': 'url1', 'Price': 10.99 },
        { 'Id': 2, 'Name': 'Product 2', 'PictureUrl': 'url2', 'Price': 19.99 }
        ]";

        File.WriteAllText(_testProductsFilePath, testData); // Creating a test JSON file

        var mockFile = new Mock<IFileService>(); // Mocking the file system
        mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
        mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

        var productService = new ProductService(mockFile.Object, _testProductsFilePath);
        var result = productService.GetAllProducts();

        // Convert the IEnumerable to a list for indexing
        var resultList = result.ToList(); // Import System.Linq for ToList()

        Assert.IsNotNull(resultList);
        Assert.That(resultList.Count, Is.EqualTo(2));
        Assert.That(resultList[0].Name, Is.EqualTo("Product 1"));
        Assert.That(resultList[0].PictureUrl, Is.EqualTo("url1"));
        Assert.That(resultList[1].Price, Is.EqualTo(19.99m));
        Assert.That(resultList[1].PictureUrl, Is.EqualTo("url2"));

        File.Delete(_testProductsFilePath); // Cleaning up the test file

        }

        [Test]
        public void GetAllCategories_Returns_EmptyList_When_CategoriesFileDoesNotExist()
        {
        var mockFile = new Mock<IFileService>();
        mockFile.Setup(fr => fr.Exists(_testCategoriesFilePath)).Returns(false);

        var productService = new ProductService(mockFile.Object, _testProductsFilePath, _testCategoriesFilePath);

        var result = productService.GetAllCategories();

        Assert.IsNotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));

        }

        [Test]
        public void GetAllCategories_Returns_CategoryList_From_ExistingCategoriesFile()
        {
            var testData = @"[
                    { 'Id': 1, 'Name': 'Category 1' },
                    { 'Id': 2, 'Name': 'Category 2' }
                ]";

            File.WriteAllText(_testCategoriesFilePath, testData);

            var mockFile = new Mock<IFileService>();
            mockFile.Setup(fr => fr.Exists(_testCategoriesFilePath)).Returns(true);
            mockFile.Setup(fr => fr.ReadAllText(_testCategoriesFilePath)).Returns(testData);

            var productService = new ProductService(mockFile.Object, _testProductsFilePath, _testCategoriesFilePath);
            var result = productService.GetAllCategories();

            var resultList = result.ToList();

            Assert.IsNotNull(resultList);
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].Name, Is.EqualTo("Category 1"));
            Assert.That(resultList[1].Name, Is.EqualTo("Category 2"));

        }
        *,/
    }
*/




    }
}
