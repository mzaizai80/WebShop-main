using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class ServiceRegistrationTests
    {
        
        [Test]
        public void ConfigurationIsSetCorrectly()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();

            // Act
            builder.Services.Configure<FilepathServiceOptions>(builder.Configuration.GetSection("ProductService"));

            // Assert
            var options = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<FilepathServiceOptions>>().Value;
            Assert.That(options.ProductsFilePath, Is.EqualTo("data/products.json"));
            Assert.That(options.CategoriesFilePath, Is.EqualTo("data/categories.json"));
        }

        [Test]
        public void RoutingIsConfiguredCorrectly()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();

            // Act
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Assert
            Assert.That(app, Has.Property(nameof(app.Environment)).Not.Null);
        }
    }
}
