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
                new Category { Id = 1, Name = "Electronics", Subcategories = new List<int> { 2, 3 } },
                new Category { Id = 2, Name = "Laptops", Subcategories = new List<int>() },
                new Category { Id = 3, Name = "Smartphones", Subcategories = new List<int>() },
            };

            //_categoryService = new CategoryService(_fileServiceMock.Object, Options.Create(new ProductServiceOptions
            //{
            //    CategoriesFilePath = "test_data/categories_test.json"
            //}));
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

        [Test]
        public void DeleteCategory_WhenCategoryExists_ShouldRemoveCategory()
        {
            // Arrange - Add a category first
            var category = new Category { Id = 3, Name = "TestCategory", Subcategories = new List<int>() };
            _categoryService.AddCategory(category);

            // Act - Then delete the added category
            _categoryService.DeleteCategory(3);

            // Assert
            var deletedCategory = _categoryService.GetCategoryById(3);
            Assert.That(deletedCategory, Is.Null);
        }

        [Test]
        public void UpdateCategory_WhenCategoryExists_ShouldModifyCategoryDetails()
        {
            // Arrange - Add a category first
            var category = new Category { Id = 2, Name = "Laptops", Subcategories = new List<int>() };
            _categoryService.AddCategory(category);

            // Act - Then update the added category
            var categoryToUpdate = new Category { Id = 2, Name = "Updated Laptops", Subcategories = new List<int>() };
            _categoryService.UpdateCategory(categoryToUpdate);

            // Assert
            var updatedCategory = _categoryService.GetCategoryById(2);
            Assert.That(updatedCategory, Is.Not.Null);
            Assert.That(updatedCategory.Name, Is.EqualTo("Updated Laptops"));
        }


        [Test]
        public void UpdateCategory_WhenCategoryExists_ShouldModifyCategoryDetails1()
        {
            // Arrange - Create a file with dummy categories
            var dummyCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Subcategories = new List<int> { 2, 3 } },
                new Category { Id = 2, Name = "Laptops", Subcategories = new List<int>() },
                new Category { Id = 3, Name = "Smartphones", Subcategories = new List<int>() },
            };

            _fileServiceMock.Setup(fs => fs.ReadAllText("test_data/categories_test.json"))
                .Returns(JsonConvert.SerializeObject(dummyCategories));

            // Act - Update the category
            var category = new Category { Id = 2, Name = "Laptops", Subcategories = new List<int>() };
            _categoryService.AddCategory(category);

            var updatedCategory = new Category { Id = 2, Name = "Updated Laptops", Subcategories = new List<int>() };
            _categoryService.UpdateCategory(updatedCategory);

            // Assert
            var retrievedCategory = _categoryService.GetCategoryById(2);
            Assert.That(retrievedCategory, Is.Not.Null);
            Assert.That(retrievedCategory.Name, Is.EqualTo("Updated Laptops"));
}


//[Test]
//public void DeleteCategory_WhenCategoryExists_ShouldRemoveCategory()
//{
//    // Arrange
//    _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
//        .Returns(JsonConvert.SerializeObject(_dummyCategories));

//    // Act
//    var categoryIdToDelete = 3;
//    _categoryService.DeleteCategory(categoryIdToDelete);

//    // Assert
//    var deletedCategory = _categoryService.GetCategoryById(categoryIdToDelete);
//    Assert.IsNull(deletedCategory);
//}

//[Test]
//public void UpdateCategory_WhenCategoryExists_ShouldModifyCategoryDetails()
//{
//    // Arrange
//    _fileServiceMock.Setup(fs => fs.ReadAllText(It.IsAny<string>()))
//        .Returns(JsonConvert.SerializeObject(_dummyCategories));

//    // Act
//    var categoryToUpdate = new Category { Id = 2, Name = "Updated Laptops", Subcategories = new List<int>() };
//    _categoryService.UpdateCategory(categoryToUpdate);

//    // Assert
//    var updatedCategory = _categoryService.GetCategoryById(2);
//    Assert.IsNotNull(updatedCategory);
//    Assert.AreEqual("Updated Laptops", updatedCategory.Name);
//}
//
}
}





/*using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IFileService> _fileServiceMock;
        private Mock<IOptions<ProductServiceOptions>> _optionsMock;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            _fileServiceMock = new Mock<IFileService>();
            _optionsMock = new Mock<IOptions<ProductServiceOptions>>();
            _optionsMock.Setup(opt => opt.Value.CategoriesFilePath).Returns("categories.json");

            _categoryService = new CategoryService(_fileServiceMock.Object, _optionsMock.Object);
        }

        [Test]
        public void AddCategory_ValidCategory_CallsWriteAllText()
        {
            // Arrange
            _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns("[]");
            _fileServiceMock.Setup(fs => fs.WriteAllText("categories.json", It.IsAny<string>())).Verifiable();

            // Act
            _categoryService.AddCategory(new Category { Id = 1, Name = "Test Category" });

            // Assert
            _fileServiceMock.Verify(fs => fs.WriteAllText("categories.json", It.IsAny<string>()), Times.Once);
        }
*/


/*[Test]
        public void DeleteCategory_CategoryExists_CallsWriteAllText()
        {
            // Arrange
            var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
            _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns(JsonConvert.SerializeObject(categories));
            _fileServiceMock.Setup(fs => fs.WriteAllText("categories.json", It.IsAny<string>())).Verifiable();

            // Act
            _categoryService.DeleteCategory(1);

            // Assert
            _fileServiceMock.Verify(fs => fs.WriteAllText("categories.json", It.IsAny<string>()), Times.Once);
        }

        // Other tests in CategoryServiceTests class...

[Test]
public void UpdateCategory_CategoryExists_CallsWriteAllText()
{
    // Arrange
    var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
    _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns(JsonConvert.SerializeObject(categories));
    _fileServiceMock.Setup(fs => fs.WriteAllText("categories.json", It.IsAny<string>())).Verifiable();

    // Act
    _categoryService.UpdateCategory(new Category { Id = 1, Name = "Updated Category" });

    // Assert
    _fileServiceMock.Verify(fs => fs.WriteAllText("categories.json", It.IsAny<string>()), Times.Once);
}

[Test]
public void GetAllCategories_ExistingCategories_ReturnsCategories()
{
    // Arrange
    var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
    _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns(JsonConvert.SerializeObject(categories));

    // Act
    var result = _categoryService.GetAllCategories();

    // Assert
    Assert.AreEqual(categories.Count, result.Count);
    // Additional assertions for content comparison
}

[Test]
public void GetCategoryById_ExistingCategory_ReturnsCategory()
{
    // Arrange
    var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
    _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns(JsonConvert.SerializeObject(categories));

    // Act
    var result = _categoryService.GetCategoryById(1);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(1, result.Id);
    Assert.AreEqual("Test Category", result.Name);
}

[Test]
public void GetCategoryById_NonExistingCategory_ReturnsNull()
{
    // Arrange
    var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
    _fileServiceMock.Setup(fs => fs.ReadAllText("categories.json")).Returns(JsonConvert.SerializeObject(categories));

    // Act
    var result = _categoryService.GetCategoryById(2);

    // Assert
    Assert.IsNull(result);
}

    }
}
*/

/* using WebShop.Models;

namespace WebShopTests
{
    [TestFixture]
    public class CategoryTests
    {
        [Test]
        public void Category_Should_Have_Default_Values()
        {
            // Arrange
            Category category = new Category();

            // Assert
            Assert.That(category.Id, Is.EqualTo(0));
            Assert.That(category.Name, Is.EqualTo(""));
                    }

        [Test]
        public void Category_Should_Allow_Setting_Properties()
        {
            // Arrange
            Category category = new Category();

            // Act
            category.Id = 1;
            category.Name = "Test Category";

            // Assert
            Assert.That(category.Id, Is.EqualTo(1));
            Assert.That(category.Name, Is.EqualTo("Test Category"));
        }


        //[Test]
        //public void Category_Should_Allow_Adding_Subcategories()
        //{
        //    // Arrange
        //    Category parentCategory = new Category();
        //    Category subcategory = new Category();

        //    // Act
        //    parentCategory.Subcategories.Add(subcategory);

        //    // Assert
        //    Assert.That(parentCategory.Subcategories.Count, Is.EqualTo(1));
        //    Assert.That(parentCategory.Subcategories[0], Is.SameAs(subcategory));

        //}


        //[Test]
        //public void Category_Subcategories_Should_Allow_Setting_Properties()
        //{
        //    // Arrange
        //    Category category = new Category();
        //    Category subcategory = new Category();

        //    // Act
        //    subcategory.Id = 2;
        //    subcategory.Name = "Subcategory";

        //    category.Subcategories.Add(subcategory);

        //    // Assert
        //    Assert.That(category.Subcategories.Count, Is.EqualTo(1));
        //    Assert.That(category.Subcategories[0].Id, Is.EqualTo(2));
        //    Assert.That(category.Subcategories[0].Name, Is.EqualTo("Subcategory"));
        //}

        //[Test]
        //public void Category_Subcategories_Should_Allow_Adding_Subsubcategories()
        //{
        //    // Arrange
        //    Category category = new Category();
        //    Category subcategory = new Category();
        //    Category subsubcategory = new Category();

        //    // Act
        //    subcategory.Subcategories.Add(subsubcategory);
        //    category.Subcategories.Add(subcategory);

        //    // Assert
        //    Assert.That(category.Subcategories.Count, Is.EqualTo(1));
        //    Assert.That(category.Subcategories[0].Subcategories.Count, Is.EqualTo(1));
        //    Assert.That(category.Subcategories[0].Subcategories[0], Is.SameAs(subsubcategory));
        //}
    }
}

*/