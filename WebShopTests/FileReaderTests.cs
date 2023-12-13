using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class FileReaderTests
    {
        private FileReader _fileReader;
        private const string ExistingFilePath = "existing_file.txt";
        private const string NonExistingFilePath = "non_existing_file.txt";
        private const string FileContent = "This is a test file.";

        [SetUp]
        public void Setup()
        {
            _fileReader = new FileReader();
            // Create a test file for existing file tests
            File.WriteAllText(ExistingFilePath, FileContent);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up test files
            if (File.Exists(ExistingFilePath))
            {
                File.Delete(ExistingFilePath);
            }
        }

        [Test]
        public void Exists_ExistingFile_ReturnsTrue()
        {
            // Arrange
            // Act
            bool fileExists = _fileReader.Exists(ExistingFilePath);

            // Assert
            Assert.IsTrue(fileExists);
        }

        [Test]
        public void Exists_NonExistingFile_ReturnsFalse()
        {
            // Arrange
            // Act
            bool fileExists = _fileReader.Exists(NonExistingFilePath);

            // Assert
            Assert.IsFalse(fileExists);
        }

        [Test]
        public void ReadAllText_ExistingFile_ReturnsFileContent()
        {
            // Arrange
            // Act
            string content = _fileReader.ReadAllText(ExistingFilePath);

            // Assert
            Assert.That(content, Is.EqualTo(FileContent));
        }

        [Test]
        public void ReadAllText_NonExistingFile_ReturnsEmptyString()
        {
            // Arrange
            // Act
            string content = _fileReader.ReadAllText(NonExistingFilePath);

            // Assert
            Assert.That(content, Is.EqualTo(string.Empty));
        }
    }
}
