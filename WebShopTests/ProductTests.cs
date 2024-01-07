namespace WebShopTests
{
    internal class ProductTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Product_Id_DefaultValue_ShouldBeZero()
        {
            // Arrange
            var product = new Product();

            // Act

            // Assert
            Assert.That(product.Id, Is.EqualTo(0));
        }

        [Test]
        public void Empty_Name_And_PictureUrl_Initialization()
        {
            var product = new Product();

            Assert.That(product.Name, Is.EqualTo(""));
            Assert.That(product.PictureUrl, Is.EqualTo(""));
        }

        [Test]
        public void Set_Name_And_PictureUrl()
        {
            var product = new Product();
            product.Name = "Test Name";
            product.PictureUrl = "https://example.com/image.jpg";

            Assert.That(product.Name, Is.EqualTo("Test Name"));
            Assert.That(product.PictureUrl, Is.EqualTo("https://example.com/image.jpg"));
        }

        [Test]
        public void Set_Price()
        {
            var product = new Product();
            product.Price = 10.99m;

            Assert.That(product.Price, Is.EqualTo(10.99m));
        }

        [Test]
        public void Set_CategoryId()
        {
            var product = new Product();
//            product.CategoryId = 123; // Assigning a sample category ID

//            Assert.That(product.CategoryId, Is.EqualTo(123));
        }

        [Test]
        public void Create_New_Product()
        {
            // Arrange & Act
            var product = new Product();

            // Assert
            Assert.That(product, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo(""));
            Assert.That(product.PictureUrl, Is.EqualTo(""));
            Assert.That(product.Price, Is.EqualTo(0m));
            }
    }


}
