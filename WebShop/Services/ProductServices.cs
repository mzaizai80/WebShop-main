using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Services
{
    
public class ProductService : IProductService
{
    private readonly IFileService _fileService;
    private readonly string _productsFilePath;
    private readonly ICategoryService _categoryService;
    private readonly IProductCategoryService _productCategoryService;

    public ProductService(IFileService fileService, IOptions<ProductServiceOptions> options,
        ICategoryService categoryService, IProductCategoryService productCategoryService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _productsFilePath = options?.Value?.ProductsFilePath ??
                            throw new ArgumentNullException(nameof(options.Value.ProductsFilePath));
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _productCategoryService =
            productCategoryService ?? throw new ArgumentNullException(nameof(productCategoryService));
    }

    public void AddProduct(Product product)
    {
        try
        {
            var products = GetAllProducts();
            products.Add(product);
            SaveProducts(products);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
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
            // Log or handle the exception as needed
            throw new ProductServiceException("Error retrieving product by ID.", ex);
        }
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            var productsJson = _fileService.ReadAllText(_productsFilePath);
            Console.WriteLine(productsJson);
            Console.WriteLine(_productsFilePath);

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();
            Console.WriteLine($"product obj {products}");
            return products;
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
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
        throw new NotImplementedException();
    }

}
}


/*public List<Category> GetAllCategories()
{
    try
    {
        var Categorylist = _categoryService.GetAllCategories();
        return Categorylist;
    }
    catch (Exception ex)
    {
        // Log or handle the exception as needed
        throw new ProductServiceException("Error getting all categories.", ex);
    }
}*/

    /*public List<ProductCategoryRelation> GetAllProductCategoryRelation()
    {
        return _productCategoryService.GetAllProductCategoryRelation();
    }
    

    public Dictionary<Product, List<Category>> GetProductCategoryAssociations()
    {
        var products = GetAllProducts();
        var categories = GetAllCategories();
        var productCategoryRelation = GetAllProductCategoryRelation();

        return GenerateProductCategoryMap(products, categories, productCategoryRelation);
    }

    private Dictionary<Product, List<Category>> GenerateProductCategoryMap(
        List<Product> products,
        List<Category> categories,
        List<ProductCategoryRelation> productCategoryRelation)
    {
        var productCategoryMap = new Dictionary<Product, List<Category>>();

        foreach (var relation in productCategoryRelation)
        {
            var product = products.FirstOrDefault(p => p.Id == relation.ProductId);
            var category = FindCategory(categories, relation.CategoryId);

            AddProductCategoryToMap(product, category, productCategoryMap);
        }

        return productCategoryMap;
    }

    private Category FindCategory(List<Category> categories, int categoryId)
    {
        throw new NotImplementedException();
    }

    private void AddProductCategoryToMap(
        Product product,
        Category category,
        Dictionary<Product, List<Category>> productCategoryMap)
    {
        if (product != null && category != null)
        {
            if (!productCategoryMap.ContainsKey(product))
            {
                productCategoryMap[product] = new List<Category>();
            }

            productCategoryMap[product].Add(category);
        }
    }
    */

    /*public Dictionary<Product, List<Category>> GetProductCategoryAssociations()
    {
        var products = GetAllProducts();
        var categories = GetAllCategories();
        var productCategoryRelation = GetAllProductCategoryRelation();

        var productCategoryMap = new Dictionary<Product, List<Category>>();

        foreach (var relation in productCategoryRelation)
        {
            var product = products.FirstOrDefault(p => p.Id == relation.ProductId);
            var category = FindCategory(categories, relation.CategoryId);

            if (product != null && category != null)
            {
                if (!productCategoryMap.ContainsKey(product))
                {
                    productCategoryMap[product] = new List<Category>();
                }

                productCategoryMap[product].Add(category);
            }
        }

        return productCategoryMap;
    }
    */



