using WebShop.Models;

namespace WebShop.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(int productId);
        void SaveProducts(Product product);
        Product GetProductById(int productId);
        List<Product> GetAllProducts();
        //List<Product> GetProductById(List<int> productIds);
        IEnumerable<Product> GetProductsByCategory(int categoryId, int page, int pageSize);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}