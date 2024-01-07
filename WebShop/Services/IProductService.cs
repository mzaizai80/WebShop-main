using System.Collections.Generic;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(int productId);
        Product GetProductById(int productId);
        List<Product> GetAllProducts();
        
        //Dictionary<Product, List<Category>> GetProductCategoryAssociations();
    }
}
