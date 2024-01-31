using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IFileService> _fileServiceMock;
        private CategoryService _categoryService;
        private List<Category> _dummyCategories;

        [SetUp]
        public void Setup()
        {
            _fileServiceMock = new Mock<IFileService>();
            _dummyCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Laptops" },
                new Category { Id = 3, Name = "Smartphones" },
            };

            var categoriesFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "test_data/categories_test.json");

            _categoryService = new CategoryService(
                _fileServiceMock.Object,
                Options.Create(new WebShopFileServiceOptions
                {
                    CategoriesFilePath = categoriesFilePath
                }),
                new List<Category>()
            );
        }

        [Test]
        public void GetAllCategories_ReturnsAllCategories()
        {

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));


            var result = _categoryService.GetAllCategories();


            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(_dummyCategories.Count));
        }

        [Test]
        public void GetCategoryById_ExistingCategory_ReturnsCategory()
        {

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));


            var categoryIdToFind = 2;
            var result = _categoryService.GetCategoryById(categoryIdToFind);


            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(categoryIdToFind));
            Assert.That(result.Name, Is.EqualTo("Laptops"));
        }

        [Test]
        public void GetCategoryById_NonExistingCategory_ReturnsNull()
        {

            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));


            var categoryIdToFind = 99;
            var result = _categoryService.GetCategoryById(categoryIdToFind);


            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddCategory_ValidCategory_AddsCategory()
        {

            var categoryToAdd = new Category { Id = 100, Name = "TestCategory" };
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));


            _categoryService.AddCategory(categoryToAdd);

            _fileServiceMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void DeleteCategory_WhenCategoryExists_ShouldRemoveCategory()
        {
            var category = new Category { Id = 123, Name = "TestCategory" };
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));
            _categoryService.AddCategory(category);

            _categoryService.DeleteCategory(123);


            var deletedCategory = _categoryService.GetCategoryById(123);
            Assert.That(deletedCategory, Is.Null);
        }

        [Test]
        public void UpdateCategory_ShouldThrowExceptionIfCategoryNotFound()
        {
            // Arrange
            var updatedCategory = new Category { Id = 4, Name = "New Category" };

            _fileServiceMock.Setup(mock => mock.ReadAllText(It.IsAny<string>()))
                .Returns("[{ \"Id\": 1, \"Name\": \"Electronics\" }, { \"Id\": 2, \"Name\": \"Laptops\" }, { \"Id\": 3, \"Name\": \"Smartphones\" }]");

            // Act & Assert
            Assert.Throws<CategoryServiceException>(() => _categoryService.UpdateCategory(updatedCategory));
            _fileServiceMock.Verify(mock => mock.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}