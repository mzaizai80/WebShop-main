using WebShop.Models;


namespace WebShop.Services
{
    public class Services : IService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public Services(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        IEnumerable<Product> IService.GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        IEnumerable<Category> IService.GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

List<Product> IService.GetProductsByCategory(int categoryId)
        {
            return _productService.GetAllProductsByCategory(categoryId);
        }
    }
}