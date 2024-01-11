using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Moq;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class DependencyInjectionTests
    {
           private Mock<IOptions<ProductServiceOptions>> optionsMock;
        
           [SetUp]
        public void Setup()
        {
            optionsMock = new Mock<IOptions<ProductServiceOptions>>();
        
        }

        [Test]
        public void ConfigureServices_Registrations_AreValid()
        {
            // Arrange
            var services = new ServiceCollection();

            // Create options for ProductService
            var productServiceOptions = Options.Create(new ProductServiceOptions
            {
                ProductsFilePath = "test_data/products_test.json",
                CategoriesFilePath = "test_data/categories.json",
                ProductCategoryFilePath = "test_data/productCategoryRelation.json"
            });

            // Act
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IOptions<ProductServiceOptions>>(productServiceOptions);
            services.AddScoped<IProductCategoryRelationService, ProductCategoryRelationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<IService, Services>();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.That(serviceProvider.GetService<IFileService>(), Is.Not.Null);
            Assert.That(serviceProvider.GetService<IProductCategoryRelationService>(), Is.Not.Null);
            Assert.That(serviceProvider.GetService<ICategoryService>(), Is.Not.Null);
            Assert.That(serviceProvider.GetService<ProductService>(), Is.Not.Null);
            Assert.That(serviceProvider.GetService<IService>(), Is.Not.Null);
        }
    }
}
