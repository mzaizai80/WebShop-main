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
                new Category { Id = 2, Name = "Laptops"},
                new Category { Id = 3, Name = "Smartphones" },
            };

            _categoryService = new CategoryService(_fileServiceMock.Object, Options.Create(new FilepathServiceOptions
            {
                CategoriesFilePath = "data/categories.json"
            }), _dummyCategories);
        }
        
        [Test]
        public void GetAllCategories_ReturnsAllCategories1()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));

            // Act
            var result = _categoryService.GetAllCategories();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(_dummyCategories.Count));
        }

        [Test]
        public void GetCategoryById_ExistingCategory_ReturnsCategory()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));

            // Act
            var categoryIdToFind = 2;
            var result = _categoryService.GetCategoryById(categoryIdToFind);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(categoryIdToFind));
            Assert.That(result.Name, Is.EqualTo("Laptops"));
        }

        [Test]
        public void GetCategoryById_NonExistingCategory_ReturnsNull()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));

            // Act
            var categoryIdToFind = 99;
            var result = _categoryService.GetCategoryById(categoryIdToFind);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddCategory_ValidCategory_AddsCategory()
        {
            // Arrange
            var categoryToAdd = new Category { Id = 100, Name = "TestCategory" };
            _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(_dummyCategories));

            // Act
            _categoryService.AddCategory(categoryToAdd);

            // Assert
            _fileServiceMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

    }
}
