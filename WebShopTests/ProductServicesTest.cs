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
/*
        private ProductService _productService;
        private Mock<IFileReader> _fileReaderMock;
        private const string TestFilePath = "test_data/products_test.json";
        private const string _testCategoriesFilePath = "test_data/categories.json";
        private const string _testProductsFilePath = "test_data/products.json";
        private const string _testProductCategoryFilePath = "test_data/productCategoryRelation.json";

        [SetUp]
        public void Setup()
        {
            _fileReaderMock = new Mock<IFileReader>();

            // Create options for ProductService
            var productServiceOptions = Options.Create(new ProductServiceOptions
            {
                ProductsFilePath = "test_data/products.json",
                CategoriesFilePath = "test_data/categories.json",
                ProductCategoryFilePath = "test_data/productCategoryRelation.json"
            });

            _productService = new ProductService(_fileReaderMock.Object, productServiceOptions);

            //CreateTestJsonFile();

            // Clean up files before each test
            if (File.Exists(_testProductsFilePath))
                File.Delete(_testProductsFilePath);

            if (File.Exists(_testCategoriesFilePath))
                File.Delete(_testCategoriesFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        //[Test]
        //public void GetAllProducts_ReturnsProducts()
        //{
        //    // Arrange
        //    var productsJson = "[{ \"Id\": 1, \"Name\": \"Laptop\", \"Price\": 999.99, \"PictureUrl\": \"laptop.jpg\" }]";
        //    var categoriesJson = "[{ \"Id\": 1, \"Name\": \"Electronics\", \"Subcategories\": [] }]";
        //    var productCategoryRelationJson = "[{ \"ProductId\": 1, \"CategoryId\": 1 }]";

        //    _fileReaderMock.Setup(x => x.ReadAllText("products.json")).Returns(productsJson);
        //    _fileReaderMock.Setup(x => x.ReadAllText("categories.json")).Returns(categoriesJson);
        //    _fileReaderMock.Setup(x => x.ReadAllText("productCategoryRelation.json")).Returns(productCategoryRelationJson);

        //    // Act
        //    var result = _productService.GetAllProducts();

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual(1, result.Count);
        //    Assert.AreEqual(1, result.First().Id);
        //    Assert.AreEqual("Laptop", result.First().Name);
        //}

        [Test]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            // Mock IFileReader
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("[{\"Id\": 1,\"Name\": \"Laptop\",\"Price\": 999.99,\"PictureUrl\": \"laptop.jpg\"}]");

            // Mock IOptions<ProductServiceOptions>
            var mockOptions = new Mock<IOptions<ProductServiceOptions>>();
            mockOptions.Setup(opt => opt.Value).Returns(new ProductServiceOptions
            {
                ProductsFilePath = "fake/path/products.json",
                CategoriesFilePath = "fake/path/categories.json",
                ProductCategoryFilePath = "fake/path/productCategoryRelation.json"
            });

            var productService = new ProductService(mockFileReader.Object, mockOptions.Object);

            // Act
            List<Product> products = productService.GetAllProducts();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Laptop", products.First().Name);
        }


        [Test]
        public void GetAllProducts_And_their_Relevant_Category_ReturnsProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Sample Product 1", Price = 15.99m },
                new Product { Id = 2, Name = "Sample Product 2", Price = 25.49m },
            };
            var productsJson = JsonConvert.SerializeObject(products);
            _fileReaderMock.Setup(x => x.ReadAllText("test_data/products.json")).Returns(productsJson);

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category A" },
                new Category { Id = 2, Name = "Category B" },
            };
            var categoriesJson = JsonConvert.SerializeObject(categories);
            _fileReaderMock.Setup(x => x.ReadAllText("test_data/categories.json")).Returns(categoriesJson);

            var relations = new List<ProductCategoryRelation>
            {
                new ProductCategoryRelation(1, 1),
                new ProductCategoryRelation(2, 2),
            };
            var relationsJson = JsonConvert.SerializeObject(relations);
            _fileReaderMock.Setup(x => x.ReadAllText("test_data/productCategoryRelation.json")).Returns(relationsJson);

            // Act
            var result = _productService.GetAllProducts();

            // Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void GetAllCategories_ReturnsCategories()
        {
            
            /*
            // Arrange
            var categoriesJson = "[{\"Id\": 1, \"Name\": \"Electronics\"},{\"Id\": 2, \"Name\": \"Accessories\"}]";
            _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json")).Returns(categoriesJson);

            // Act
            var result = _productService.GetAllCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Category>>(result);
            */
            }


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
        .Returns(JsonConvert.SerializeObject(new List<ProductCategoryRelation>
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

        var mockFile = new Mock<IFileReader>(); // Assuming a mocked IFile interface for file operations
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

        var mockFile = new Mock<IFileReader>();
        mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
        mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

        var productService = new ProductService(mockFile.Object, _testProductsFilePath);


        //var mockFile = new Mock<IFileReader>(); // Mocking the file system
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

        var mockFile = new Mock<IFileReader>(); // Mocking the file system
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
        var mockFile = new Mock<IFileReader>();
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

            var mockFile = new Mock<IFileReader>();
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
