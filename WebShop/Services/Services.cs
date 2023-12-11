using WebShop.Models;
using WebShopTests;

namespace WebShop.Services
{
    public class Services : IService
    {
        private readonly ProductService _productService;

        public Services(ProductService productService)
        {
            _productService= productService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _productService.GetAllCategories();
        }

    }
}

