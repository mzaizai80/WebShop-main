using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private FileService _fileService;
        private const string ExistingFilePath = "existing_file.txt";
        private const string NonExistingFilePath = "non_existing_file.txt";
        private const string FileContent = "This is a test file.";

        [SetUp]
        public void Setup()
        {
            _fileService = new FileService();
            File.WriteAllText(ExistingFilePath, FileContent);
        }

        [TearDown]
        public void TearDown()
        {
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
            bool fileExists = _fileService.Exists(ExistingFilePath);

            // Assert
            Assert.That(fileExists, Is.True);
        }

        [Test]
        public void Exists_NonExistingFile_ReturnsFalse()
        {
            // Arrange
            // Act
            bool fileExists = _fileService.Exists(NonExistingFilePath);

            // Assert
            Assert.That(fileExists, Is.False);
        }

        [Test]
        public void ReadAllText_ExistingFile_ReturnsFileContent()
        {
            // Arrange
        // Act
            string content = _fileService.ReadAllText(ExistingFilePath);

            // Assert
            Assert.That(content, Is.EqualTo(FileContent));
        }

        [Test]
        public void ReadAllText_NonExistingFile_ReturnsEmptyString()
        {
            // Arrange
            // Act
            string content = _fileService.ReadAllText(NonExistingFilePath);

            // Assert
            Assert.That(content, Is.EqualTo(string.Empty));
        }
    }
}
