using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections;
using WebShop.Models;

namespace WebShop.Services
{

    public class ProductService : IProductService, IEnumerable<Product>
    {
        private readonly IFileService _fileService;
        private readonly string _productsFilePath;
        private readonly List<Product> _products;
        private readonly ICategoryService _categoryService;

        public ProductService(IFileService fileService, IOptions<WebShopFileServiceOptions> options, List<Product> products, ICategoryService categoryService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _products = products;
            _productsFilePath = options?.Value?.ProductsFilePath ??
                                throw new ArgumentNullException(nameof(options.Value.ProductsFilePath));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddProduct(Product product)
        {
            try
            {
                var products = GetAllProducts();
                if (products.Any())
                {
                    product.Id = products.Max(p => p.Id) + 1;
                }
                else
                {
                    product.Id = 10;
                }
                products.Add(product);
                SaveProducts(products);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error adding product.", ex);
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                var products = GetAllProducts();
                return products.SingleOrDefault(p => p.Id == productId);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error retrieving product by ID.", ex);
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                var productsJson = _fileService.ReadAllText(_productsFilePath);

                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();
                return products;
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error getting all products.", ex);
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            try
            {
                var products = GetAllProducts();
                System.Console.WriteLine(updatedProduct);
                var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                System.Console.WriteLine(existingProduct);
                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.PictureUrl = updatedProduct.PictureUrl;
                    existingProduct.Product_Discription = updatedProduct.Product_Discription;
                    existingProduct.ReverseLookupOfCategoryIds = updatedProduct.ReverseLookupOfCategoryIds;

                    _categoryService.UpdateAssociationOfCategoryWithProducts(existingProduct.Id, existingProduct.ReverseLookupOfCategoryIds);

                    SaveProducts(products);
                }
                else
                {
                    throw new ProductServiceException($"Product with ID {updatedProduct.Id} not found for update.");
                }
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error updating product.", ex);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                var products = GetAllProducts();
                var productToRemove = products.FirstOrDefault(p => p.Id == productId);

                if (productToRemove != null)
                {
                    products.Remove(productToRemove);
                    SaveProducts(products);
                }
                else
                {
                    throw new ProductServiceException($"Product with ID {productId} not found for deletion.");
                }
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error deleting product.", ex);
            }
        }

        private void SaveProducts(List<Product> products)
        {
            try
            {
                var productsJson = JsonConvert.SerializeObject(products);
                _fileService.WriteAllText(_productsFilePath, productsJson);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error saving products.", ex);
            }
        }

        public void SaveProducts(Product product)
        {
            try
            {
                var products = GetAllProducts();
                products.Add(product);
                SaveProducts(products);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error saving product.", ex);
            }
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId, int page, int pageSize)
        {
            try
            {
                var allProducts = GetAllProducts();
                var productsInCategory = allProducts
                    .Where(p => p.ReverseLookupOfCategoryIds == categoryId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return productsInCategory.ToList();
            }
            catch (Exception ex)
            {
                throw new ProductServiceException($"Error getting products by category ID {categoryId}.", ex);
            }
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            try
            {
                var allProducts = GetAllProducts();
                var productsInCategory = allProducts.Where(p => p.ReverseLookupOfCategoryIds == categoryId);
                return productsInCategory.ToList();
            }
            catch (Exception ex)
            {
                throw new ProductServiceException($"Error getting products by category ID {categoryId}.", ex);
            }
        }
    }
}


/*public IEnumerable<Product> GetProductsByCategory(int categoryId, int page, int pageSize)
   {
       try
       {
           var allProducts = GetAllProducts();
           var productsInCategory = allProducts
               .Where(p => p.CategoryId == categoryId)
               .Skip((page - 1) * pageSize)
               .Take(pageSize);

           return productsInCategory.ToList();
       }
       catch (Exception ex)
       {
           throw new ProductServiceException($"Error getting products by category ID {categoryId}.", ex);
       }
   }

   public IEnumerable<Product> GetProductsByCategory(int categoryId)
   {
       try
       {
           var allProducts = GetAllProducts();
           var productsInCategory = allProducts.Where(p => p.CategoryId == categoryId);
           return productsInCategory.ToList();
       }
       catch (Exception ex)
       {
           throw new ProductServiceException($"Error getting products by category ID {categoryId}.", ex);
       }
   }
*/


//    public void UpdateProduct(Product updatedProduct)
//    {
//        try
//        {
//            var products = GetAllProducts();
//            var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

//            if (existingProduct != null)
//            {
//                existingProduct.Name = updatedProduct.Name;
//                existingProduct.Price= updatedProduct.Price;
//                existingProduct.PictureUrl = updatedProduct.PictureUrl;
//existingProduct.Product_Discription = updatedProduct.Product_Discription;

//                _categoryService.UpdateCategoryForProduct(existingProduct, updatedProduct.CategoryId);

//                SaveProducts(products);
//            }
//            else
//            {
//                throw new ProductServiceException($"Product with ID {updatedProduct.Id} not found for update.");
//            }
//        }
//        catch (Exception ex)
//        {
//            throw new ProductServiceException("Error updating product.", ex);
//        }
//    }



//public void UpdateProduct(Product updatedProduct)
//{
//    try
//    {
//        var products = GetAllProducts();
//        var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

//        if (existingProduct != null)
//        {
//            existingProduct.Name = updatedProduct.Name;
//            existingProduct.PictureUrl = updatedProduct.PictureUrl;
//            SaveProducts(products);
//        }
//        else
//        {
//            throw new ProductServiceException($"Product with ID {updatedProduct.Id} not found for update.");
//        }
//    }
//    catch (Exception ex)
//    {
//        throw new ProductServiceException("Error updating product.", ex);
//    }
//}
