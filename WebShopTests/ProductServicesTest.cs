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
        private ProductService _productService;
        private const string TestFilePath = "test_data/products_test.json";
        private Mock<IFileReader> _fileReaderMock;
        private string _testCategoriesFilePath = "test/categories.json";
        private string _testProductsFilePath = "test/products.json";

        [SetUp]
        public void Setup()
        {
            _fileReaderMock = new Mock<IFileReader>();

            // Create options for ProductService
            var productServiceOptions = Options.Create(new ProductServiceOptions
            {
                ProductsFilePath = "data/products.json",
                CategoriesFilePath = "data/categories.json",
                ProductCategoryFilePath = "data/productCategoryRelation.json"
            });

            _productService = new ProductService(_fileReaderMock.Object, productServiceOptions);

            CreateTestJsonFile();

            // Clean up files before each test
            if (File.Exists(_testProductsFilePath))
                File.Delete(_testProductsFilePath);

            if (File.Exists(_testCategoriesFilePath))
                File.Delete(_testCategoriesFilePath);
        }

        //private ProductService _productService;
        //private const string TestFilePath = "test_data/products_test.json";
        //private Mock<IFileReader> _fileReaderMock;
        //private string _testCategoriesFilePath = "test/categories.json";
        //private string _testProductsFilePath = "test/products.json"; 

        //[SetUp]
        //public void Setup()
        //{
        //    _fileReaderMock = new Mock<IFileReader>();
        //    _productService = new ProductService(
        //        _fileReaderMock.Object,
        //        "data/products.json",
        //        "data/categories.json",
        //        "data/productCategoryRelation.json"
        //    );

        //    CreateTestJsonFile();

        //    // Clean up files before each test
        //    if (File.Exists(_testProductsFilePath))
        //        File.Delete(_testProductsFilePath);

        //    if (File.Exists(_testCategoriesFilePath))
        //        File.Delete(_testCategoriesFilePath);
        //}

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [Test]
        public void GetAllProducts_WhenFileExists_ReturnsListOfProducts()
        {
            var products = _productService.GetAllProducts();

            Assert.IsNotNull(products);
            Assert.IsInstanceOf<List<Product>>(products);
        }

        private void CreateTestJsonFile()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.99m },
                new Product { Id = 2, Name = "Product 2", Price = 20.49m },
            };

            var json = JsonConvert.SerializeObject(products);

            File.WriteAllText(TestFilePath, json);
        }

        [Test]
        public void GetAllProducts_ShouldReturnProducts_WhenValidJsonFilesExist()
        {
            // Arrange
            _fileReaderMock.Setup(x => x.ReadAllText("data/products.json"))
                .Returns(JsonConvert.SerializeObject(new List<Product>
                {
                    /* Provide sample products */
                }));

            _fileReaderMock.Setup(x => x.ReadAllText("data/categories.json"))
                .Returns(JsonConvert.SerializeObject(new List<Category>
                {
                    /* Provide sample categories */
                }));

            _fileReaderMock.Setup(x => x.ReadAllText("data/productcategory.json"))
                .Returns(JsonConvert.SerializeObject(new List<ProductCategoryRelation>
                {
                    /* Provide sample relations */
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
                    /* Provide sample categories */
                }));

            // Act
            var result = _productService.GetAllCategories();

            // Assert
                        Assert.IsNotNull(result);
        }




        /*
               Refactoring test
           ###############################################################################
        */
    //    [Test]
    //    public void GetAllProducts_Returns_EmptyList_When_FileDoesNotExist()
    //    {
    //        /*
    //        var mockFile = new Mock<IFileReader>(); // Assuming a mocked IFile interface for file operations
    //        mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(false);

    //        //var productService = new ProductService();
    //        var productService = new ProductService(mockFile.Object, _testProductsFilePath);

    //        var result = productService.GetAllProducts();

    //        Assert.IsNotNull(result);
    //        Assert.That(result.Count, Is.EqualTo(0));
    //    */
    //    }

    //    [Test]
    //        public void GetAllProducts_Returns_ProductList_From_ExistingFile()
    //        {
    //            /*
    //            // Sample JSON data for testing
    //            var testData = @"[
    //            { 'Id': 1, 'Name': 'Product 1', 'PictureUrl': 'url1', 'Price': 10.99 },
    //            { 'Id': 2, 'Name': 'Product 2', 'PictureUrl': 'url2', 'Price': 19.99 }
    //            ]";

    //            File.WriteAllText(_testProductsFilePath, testData); // Creating a test JSON file

    //            var mockFile = new Mock<IFileReader>();
    //            mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
    //            mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

    //            var productService = new ProductService(mockFile.Object, _testProductsFilePath);


    //            //var mockFile = new Mock<IFileReader>(); // Mocking the file system
    //            //mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
    //            //mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

    //            //var productService = new ProductService(mockFile.Object, _testProductsFilePath);
    //            //var productService = new ProductService();

    //            var result = productService.GetAllProducts();

    //            Assert.IsNotNull(result);
    //            Assert.That(result.Count, Is.EqualTo(2));
    //            Assert.That(result[0].Name, Is.EqualTo("Product 1"));
    //            Assert.That(result[0].PictureUrl, Is.EqualTo("url1"));
    //            Assert.That(result[1].Price, Is.EqualTo(19.99m));
    //            Assert.That(result[1].PictureUrl, Is.EqualTo("url2"));

    //            File.Delete(_testProductsFilePath); // Cleaning up the test file
    //            */
    //        }



    //        [Test]
    //        public void GetAllProducts_Returns_ProductList_From_ExistingFile1()
    //        {
    //            /*
    //            // Sample JSON data for testing
    //            var testData = @"[
    //            { 'Id': 1, 'Name': 'Product 1', 'PictureUrl': 'url1', 'Price': 10.99 },
    //            { 'Id': 2, 'Name': 'Product 2', 'PictureUrl': 'url2', 'Price': 19.99 }
    //        ]";

    //            File.WriteAllText(_testProductsFilePath, testData); // Creating a test JSON file

    //            var mockFile = new Mock<IFileReader>(); // Mocking the file system
    //            mockFile.Setup(fr => fr.Exists(_testProductsFilePath)).Returns(true);
    //            mockFile.Setup(fr => fr.ReadAllText(_testProductsFilePath)).Returns(testData);

    //            var productService = new ProductService(mockFile.Object, _testProductsFilePath);
    //            var result = productService.GetAllProducts();

    //            // Convert the IEnumerable to a list for indexing
    //            var resultList = result.ToList(); // Import System.Linq for ToList()

    //            Assert.IsNotNull(resultList);
    //            Assert.That(resultList.Count, Is.EqualTo(2));
    //            Assert.That(resultList[0].Name, Is.EqualTo("Product 1"));
    //            Assert.That(resultList[0].PictureUrl, Is.EqualTo("url1"));
    //            Assert.That(resultList[1].Price, Is.EqualTo(19.99m));
    //            Assert.That(resultList[1].PictureUrl, Is.EqualTo("url2"));

    //            File.Delete(_testProductsFilePath); // Cleaning up the test file
    //            */
    //        }

    //        [Test]
    //        public void GetAllCategories_Returns_EmptyList_When_CategoriesFileDoesNotExist()
    //        {
    //            /*

    //            var mockFile = new Mock<IFileReader>();
    //            mockFile.Setup(fr => fr.Exists(_testCategoriesFilePath)).Returns(false);

    //            var productService = new ProductService(mockFile.Object, _testProductsFilePath, _testCategoriesFilePath);

    //            var result = productService.GetAllCategories();

    //            Assert.IsNotNull(result);
    //            Assert.That(result.Count(), Is.EqualTo(0));
    //            */
    //        }


    //        [Test]
    //        public void GetAllCategories_Returns_CategoryList_From_ExistingCategoriesFile()
    //        {
    //            /*
    //            var testData = @"[
    //                    { 'Id': 1, 'Name': 'Category 1' },
    //                    { 'Id': 2, 'Name': 'Category 2' }
    //                ]";

    //            File.WriteAllText(_testCategoriesFilePath, testData);

    //            var mockFile = new Mock<IFileReader>();
    //            mockFile.Setup(fr => fr.Exists(_testCategoriesFilePath)).Returns(true);
    //            mockFile.Setup(fr => fr.ReadAllText(_testCategoriesFilePath)).Returns(testData);

    //            var productService = new ProductService(mockFile.Object, _testProductsFilePath, _testCategoriesFilePath);
    //            var result = productService.GetAllCategories();

    //            var resultList = result.ToList();

    //            Assert.IsNotNull(resultList);
    //            Assert.That(resultList.Count, Is.EqualTo(2));
    //            Assert.That(resultList[0].Name, Is.EqualTo("Category 1"));
    //            Assert.That(resultList[1].Name, Is.EqualTo("Category 2"));
    //*/
    //        }

        }

    }