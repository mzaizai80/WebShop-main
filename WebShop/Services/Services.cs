using WebShop.Models;


namespace WebShop.Services
{
    public class Services : IService
    {
        private readonly ProductService _productService;
        private readonly ICategoryService _categoryService;
private readonly IProductCategoryService _productCategoryService;

public Services(IProductService productService, ICategoryService categoryService, IProductCategoryService productCategoryService)
{
    _productService = (ProductService?)(productService ?? throw new ArgumentNullException(nameof(productService)));
    _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    _productCategoryService = productCategoryService ?? throw new ArgumentNullException(nameof(productCategoryService));
}

        //public Services(IProductService productService)
        //{
        //    _productService= productService;
        //}

        public IEnumerable<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        public IEnumerable<ProductCategoryRelation> GetProductCategoryRelation()
        {
            return _productCategoryService.GetAllProductCategoryRelation();
        }
    }
}