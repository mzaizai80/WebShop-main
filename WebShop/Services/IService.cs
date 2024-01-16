using WebShop.Models;

namespace WebShop.Services
{
    public interface IService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}