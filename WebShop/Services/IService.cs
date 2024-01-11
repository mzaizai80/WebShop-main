using WebShop.Models;

namespace WebShop.Services
{
    public interface IService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ProductCategoryRelation> GetProductCategoryRelation();
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}