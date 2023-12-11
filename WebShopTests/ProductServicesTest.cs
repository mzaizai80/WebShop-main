using Moq;
using Newtonsoft.Json;
using WebShop.Services;

namespace WebShopTests
{

    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;
        private const string TestFilePath = "test_data/products_test.json";

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService();
            CreateTestJsonFile();
        }

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
            // Add more specific assertions as needed based on the content of the test JSON file
        }

        private void CreateTestJsonFile()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.99m },
                new Product { Id = 2, Name = "Product 2", Price = 20.49m },
                // Add more sample products as needed for testing different scenarios
            };

            var json = JsonConvert.SerializeObject(products);

            File.WriteAllText(TestFilePath, json);
        }
    }








/*
    // [TestFixture]
    internal class ProductServicesTest
    {
        private string _testCategoriesFilePath = "test/categories.json";
        private string _testProductsFilePath = "test/products.json"; // Path used for testing

        [SetUp]
        public void Setup()
        {
            // Clean up files before each test
            if (File.Exists(_testProductsFilePath))
                File.Delete(_testProductsFilePath);

            if (File.Exists(_testCategoriesFilePath))
                File.Delete(_testCategoriesFilePath);
        }




        /*
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

                /*
            Refactoring test
            ###############################################################################

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
            */

        /*
                [Test]
                public void GetAllProducts_Returns_ProductList_From_ExistingFile()
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
        */


        /*
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


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WebShopTests
//{
//    internal class ProductServicesTest
//    {
//    }
//}
