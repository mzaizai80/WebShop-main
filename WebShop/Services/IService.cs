using WebShop.Models;

namespace WebShopTests
{
    public interface IService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Category> GetAllCategories();
    }
}