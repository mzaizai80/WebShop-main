using WebShop.Models;

namespace WebShop.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(int productId);
        void SaveProduct(Product product);
        void SaveProducts(List<Product> products);
        Product GetProductByProductId(int productId);
        List<Product> GetAllProducts();

        // Category Associated methods
        void DeleteProductByCategoryId(int categoryId);
        List<Product> GetAllProductsByCategory(int categoryId);
    }
}
