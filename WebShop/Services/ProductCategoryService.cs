using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IFileService _fileService;
        private readonly string _productCategoryFilePath;

        public ProductCategoryService(IFileService fileService, IOptions<ProductServiceOptions> options)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _productCategoryFilePath = options.Value.ProductCategoryFilePath ??
                                       throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
        }

        public List<ProductCategoryRelation> GetAllProductCategoryRelation()
        {
            var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryFilePath);
            var productCategoryRelation =
                JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(productCategoryRelationJson) ??
                new List<ProductCategoryRelation>();
            return productCategoryRelation;
        }

        public Category GetCategoryById(List<Category> categories, int categoryId)
        {
            foreach (var category in categories)
            {
                if (category.Id == categoryId)
                {
                    return category;
                }
            }
            return null;
        }
    }
}




//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using WebShop.Models;

//namespace WebShop.Services
//{
//    public class ProductCategoryService : IProductCategoryService
//    {
//        private readonly IFileService _fileService;
//        private readonly string _productCategoryFilePath;

//        public ProductCategoryService(IFileService fileService, IOptions<ProductServiceOptions> options)
//        {
//            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
//            _productCategoryFilePath = options.Value.ProductCategoryFilePath ??
//                                       throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
//        }

//        public List<ProductCategoryRelation> GetAllProductCategoryRelation()
//        {
//            var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryFilePath);
//            var productCategoryRelation =
//                JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(productCategoryRelationJson) ??
//                new List<ProductCategoryRelation>();
//            return productCategoryRelation;
//        }
        

//        public Category FindCategory(List<Category> categories, int categoryId)
//        {
//            foreach (var category in categories)
//            {
//                if (category.Id == categoryId)
//                {
//                    return category;
//                }

//                if (category.Subcategories != null)
//                {
//                    var foundSubcategory = category.Subcategories.FirstOrDefault(sub => sub.Id == categoryId);
//                    if (foundSubcategory != null)
//                    {
//                        return foundSubcategory;
//                    }
//                }
//            }
//            return null;
//        }
//    }
//}
