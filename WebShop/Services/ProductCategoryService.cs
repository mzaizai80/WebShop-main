using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IFileReader _fileReader;
        private readonly string _productCategoryFilePath;

        public ProductCategoryService(IFileReader fileReader, IOptions<ProductServiceOptions> options)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _productCategoryFilePath = options.Value.ProductCategoryFilePath ??
                                       throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
        }

        /*
        public List<ProductCategoryRelation> GetAllProductCategoryRelation()
        {
            var productCategoryRelationJson = _fileReader.ReadAllText(_productCategoryFilePath);
            var productCategoryRelation =
                JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(productCategoryRelationJson) ??
                new List<ProductCategoryRelation>();
            return productCategoryRelation;
        }
        */

        public Category FindCategory(List<Category> categories, int categoryId)
        {
            foreach (var category in categories)
            {
                if (category.Id == categoryId)
                {
                    return category;
                }

                if (category.Subcategories != null)
                {
                    var foundSubcategory = category.Subcategories.FirstOrDefault(sub => sub.Id == categoryId);
                    if (foundSubcategory != null)
                    {
                        return foundSubcategory;
                    }
                }
            }

            return null;
        }
    }
}
