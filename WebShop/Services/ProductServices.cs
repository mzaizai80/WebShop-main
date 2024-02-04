using Microsoft.CodeAnalysis;
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

        public ProductService(IFileService fileService, IOptions<FilepathServiceOptions> options, List<Product> products, ICategoryService categoryService)
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
                int maxId = products.Any() ? products.Max(p => p.Id) : 0;
                product.Id = maxId + 1;

                products.Add(product);
                SaveProducts(products);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error adding product.", ex);
            }
        }

        public Product GetProductByProductId(int productId)
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
                var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.PictureUrl = updatedProduct.PictureUrl;
                    existingProduct.Description = updatedProduct.Description;
                    existingProduct.Price = updatedProduct.Price;



                    _categoryService.UpdateProductsIdListInCategory(existingProduct.Id, existingProduct.CategoryId, updatedProduct.CategoryId);

                    existingProduct.CategoryId = updatedProduct.CategoryId;

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

        public void DeleteProductByCategoryId(int categoryId)
        {
            try
            {
                var products = GetAllProducts();
                var productsToRemove = products.Where(p => p.CategoryId == categoryId).ToList();

                if (productsToRemove.Count > 0)
{
                    foreach (var productToRemove in productsToRemove)
                    {
                        products.Remove(productToRemove);
                    }

                    SaveProducts(products);
}
            }
            catch (Exception ex)
            {
                    throw new ProductServiceException($"No products found with Category ID {categoryId} for deletion.");
            }
        }

        public void SaveProducts(List<Product> products)
        {
            try
            {
                var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                _fileService.WriteAllText(_productsFilePath, productsJson);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error saving products.", ex);
            }
        }

        public void SaveProduct(Product product)
        {
            try
            {
                var products = GetAllProducts();

                var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.CategoryId = product.CategoryId;
                }
                else
                {
                    products.Add(product);
                }

                SaveProducts(products);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Error saving product.", ex);
            }
        }
        
        public List<Product> GetAllProductsByCategory(int categoryId)
        {
            try
            {
                var allProducts = GetAllProducts();
                List<Product> productsInCategory = allProducts.Where(p => p.CategoryId == categoryId).ToList();
                return productsInCategory;
            }
            catch (Exception ex)
            {
                throw new ProductServiceException($"Error getting products by category ID {categoryId}.", ex);
            }
        }

    }
}
