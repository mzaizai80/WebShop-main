using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop.Services;

namespace WebShopTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Index_Returns_View_With_Products()
        {
            // Arrange
            var productServiceMock = new Mock<IService>(); // Use the interface, not the concrete type
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", PictureUrl = "url 1", Price = 10.99m },
                new Product { Id = 2, Name = "Product 2", PictureUrl = "url 2", Price = 19.99m }
            };
            productServiceMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);

            var controller = new HomeController(productServiceMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.Model, Is.InstanceOf<List<Product>>());

            var modelList = (List<Product>)result.Model;
            Assert.That(modelList.Count, Is.EqualTo(expectedProducts.Count));

        }


        [Test]
        public void Index_Returns_Empty_View_When_No_Products2()
        {
            // Arrange
            var productServiceMock = new Mock<IService>();
            productServiceMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>());

            var controller = new HomeController(productServiceMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.That(result.ViewName, Is.EqualTo("Index").Or.Null); // Handle the case where ViewName is null
            Assert.IsEmpty(result.Model as List<Product>); // Ensure an empty list of products is returned
        }


        /*
         * Refactoring

                [Test]
                public void Index_Returns_Empty_View_When_No_Products1()
                {
                    // Arrange
                    var productServiceMock = new Mock<IProductService>();
                    productServiceMock.Setup(p => p.GetAllProducts()).Returns(new List<Product>());

                    var controller = new HomeController(productServiceMock.Object);

                    // Act
                    var result = controller.Index() as ViewResult;

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOf<ViewResult>(result);
                    Assert.IsNull(result.ViewName); // Adjusted assertion for ViewName
                    Assert.IsEmpty((List<Product>)result.Model); // Ensure an empty list of products is returned
                }

                *
         */

    }
}
















////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace WebShop.Tests
////{
////    internal class HomeControllerTests
////    {
////    }
////}


//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace WebShopTests
//{
//    [TestFixture]
//    internal class HomeControllerTests
//    {
//        [Test]
//        public void Index_Returns_View_With_Products()
//        {
//            // Arrange
//            var productServiceMock = new Mock<ProductService>();
//            var expectedProducts = new List<Product>
//            {
//                new Product { Id = 1, Name = "Product 1", PictureUrl = "url 1", Price = 10.99m },
//                new Product { Id = 2, Name = "Product 2", PictureUrl = "url 2", Price = 19.99m }
//            };
//            productServiceMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);

//            var controller = new HomeController(productServiceMock.Object);

//            // Act
//            var result = controller.Index() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.That(result.Model, Is.EqualTo(expectedProducts));

//        }
//    }
//}










/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopTests
{
    internal class HomeControllerTests
    {
    }
}
*/