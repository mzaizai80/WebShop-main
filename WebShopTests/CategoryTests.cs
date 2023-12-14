using WebShop.Models;

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

        [Test]
        public void Category_Should_Allow_Adding_Subcategories()
        {
            // Arrange
            Category parentCategory = new Category();
            Category subcategory = new Category();

            // Act
            parentCategory.Subcategories.Add(subcategory);

            // Assert
            Assert.That(parentCategory.Subcategories.Count, Is.EqualTo(1));
            Assert.That(parentCategory.Subcategories[0], Is.SameAs(subcategory));

        }


        [Test]
        public void Category_Subcategories_Should_Allow_Setting_Properties()
        {
            // Arrange
            Category category = new Category();
            Category subcategory = new Category();

            // Act
            subcategory.Id = 2;
            subcategory.Name = "Subcategory";

            category.Subcategories.Add(subcategory);

            // Assert
            Assert.That(category.Subcategories.Count, Is.EqualTo(1));
            Assert.That(category.Subcategories[0].Id, Is.EqualTo(2));
            Assert.That(category.Subcategories[0].Name, Is.EqualTo("Subcategory"));
        }

        [Test]
        public void Category_Subcategories_Should_Allow_Adding_Subsubcategories()
        {
            // Arrange
            Category category = new Category();
            Category subcategory = new Category();
            Category subsubcategory = new Category();

            // Act
            subcategory.Subcategories.Add(subsubcategory);
            category.Subcategories.Add(subcategory);

            // Assert
            Assert.That(category.Subcategories.Count, Is.EqualTo(1));
            Assert.That(category.Subcategories[0].Subcategories.Count, Is.EqualTo(1));
            Assert.That(category.Subcategories[0].Subcategories[0], Is.SameAs(subsubcategory));
        }
    }
}

