using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductCategoryRelationService : IProductCategoryRelationService
    {
        private readonly IFileService _fileService;
        private readonly string _productCategoryFilePath;
        private List<ProductCategoryRelation> _relations;

        public ProductCategoryRelationService(IFileService fileService, IOptions<ProductServiceOptions> options)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _productCategoryFilePath = options.Value.ProductCategoryFilePath ??
                                       throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));

            InitializeRelations();
        }

        private void InitializeRelations()
        {
            if (_fileService.Exists(_productCategoryFilePath))
            {
                string fileContent = _fileService.ReadAllText(_productCategoryFilePath);
                _relations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(fileContent);
            }
            else
            {
                _relations = new List<ProductCategoryRelation>();
                SaveRelationsToFile(); // Create an empty file if it doesn't exist
            }
        }

        private void SaveRelationsToFile()
        {
            string serializedData = JsonConvert.SerializeObject(_relations);
            _fileService.WriteAllText(_productCategoryFilePath, serializedData);
        }

        public ProductCategoryRelationService()
        {
            _relations = new List<ProductCategoryRelation>();
        }

        public void AddRelation(ProductCategoryRelation relation)
        {
            _relations.Add(relation);
        }

        public List<ProductCategoryRelation> GetAllRelations()
        {
            return _relations;
        }

        public List<ProductCategoryRelation> GetRelationsByProductId(int productId)
        {
            return _relations.Where(r => r.ProductIds.Contains(productId)).ToList();
        }

        public List<ProductCategoryRelation> GetRelationsByCategoryId(int categoryId)
        {
            return _relations.Where(r => r.CategoryIds.Contains(categoryId)).ToList();
        }

        public void UpdateRelation(ProductCategoryRelation updatedRelation)
        {
            var existingRelation = _relations.FirstOrDefault(r =>
                r.ProductIds.SequenceEqual(updatedRelation.ProductIds) &&
                r.CategoryIds.SequenceEqual(updatedRelation.CategoryIds));

            if (existingRelation != null)
            {
                _relations.Remove(existingRelation);
                _relations.Add(updatedRelation);
            }
        }

        public void DeleteRelation(ProductCategoryRelation relationToDelete)
        {
            _relations.Remove(relationToDelete);
        }

        public void AddBulkRelations(List<ProductCategoryRelation> relations)
        {
            _relations.AddRange(relations);
        }

        public void DeleteBulkRelations(List<ProductCategoryRelation> relationsToDelete)
        {
            foreach (var relation in relationsToDelete)
            {
                _relations.Remove(relation);
            }
        }
    }
}


/*
public class ProductCategoryRelationService : IProductCategoryRelationService
{
    private IFileService _fileService;
    private object? productCategoryRelation;
    private readonly string _productCategoryFilePath;

    public ProductCategoryRelationService(IFileService fileService, IOptions<ProductServiceOptions> options)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _productCategoryFilePath = options.Value.ProductCategoryFilePath ?? throw new ArgumentNullException(nameof(options.Value.ProductsFilePath));
    }

    public void SaveProductCategoryRelation(int productId, int categoryId)
    {
        var productCategoryRelationJson = JsonConvert.SerializeObject(productCategoryRelation);
        _fileService.WriteAllText(_productCategoryFilePath, productCategoryRelationJson);
    }

    public List<ProductCategoryRelation> GetAllProductCategoryRelation()
    {
        try
        {
            var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryFilePath);
            Console.WriteLine(productCategoryRelationJson);
            List<ProductCategoryRelation> productCategoryRelations = JsonConvert.DeserializeObject<List<ProductCategoryRelation>>(productCategoryRelationJson) ?? new List<ProductCategoryRelation>();
            Console.WriteLine($"category obj {productCategoryRelations}");
            return productCategoryRelations;
        }
        catch (Exception ex)
        {
            throw new ProductCategoryRelationServiceException("Error getting all Category / Categories.", ex);
        }
    }

    public void AddProductCategoryRelation(ProductCategoryRelation productCategoryRelation)
    {
        var productCategoryRelations = GetAllProductCategoryRelation();

            bool relationExists = productCategoryRelations.Any(
                pcr => pcr.ProductId == productCategoryRelation.ProductId
                       && pcr.CategoryId == productCategoryRelation.CategoryId);

            if (!relationExists)
            {
                productCategoryRelations.Add(productCategoryRelation);
                SaveProductCategoryRelation(productCategoryRelation.ProductId, productCategoryRelation.CategoryId);
            }
            else
            {
                throw new InvalidOperationException("Relation already exists for this product and category.");
            }

    }


    public void Remove_a_Product_From_CategoryRelation(int productId, int categoryId)
    {
        throw new NotImplementedException();
    }
    public void Remove_a_Product_From_CategoryRelation(ProductCategoryRelation productCategoryRelation)
    {
        throw new NotImplementedException();
    }

    public void UpdateProductCategoryRelation(ProductCategoryRelation productCategoryRelation)
    {

    }


    public List<Product> GetProductIdsByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public List<Category> GetAllSubcategories(int categoryId)
    {
        throw new NotImplementedException();
    }


    //     private readonly List<SaveProductCategoryRelation> _productCategoryRelations; // Replace this with your actual data source or service
    //     private readonly ICategoryService _categoryService;

    //     public ProductCategoryAssociationService(ICategoryService categoryService)
    //     {
    //         _productCategoryRelations = new List<SaveProductCategoryRelation>();
    //         _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    //     }

    //     public void AssociateProductWithCategory(int productId, int categoryId)
    //     {
    //         _productCategoryRelations.Add(new SaveProductCategoryRelation { ProductId = productId, CategoryId = categoryId });
    //     }

    //     public void DisassociateProductFromCategory(int productId, int categoryId)
    //     {
    //         var relationToRemove = _productCategoryRelations.FirstOrDefault(r => r.ProductId == productId && r.CategoryId == categoryId);
    //         if (relationToRemove != null)
    //         {
    //             _productCategoryRelations.Remove(relationToRemove);
    //         }
    //     }

    //     public List<int> GetProductIdsByCategory(int categoryId)
    //     {
    //         var relatedProducts = _productCategoryRelations.Where(r => r.CategoryId == categoryId).Select(r => r.ProductId).ToList();

    //         var subcategoryIds = GetAllSubcategories(categoryId);

    //         var subcategoryProducts = _productCategoryRelations
    //             .Where(r => subcategoryIds.Contains(r.CategoryId))
    //             .Select(r => r.ProductId)
    //             .ToList();

    //         relatedProducts.AddRange(subcategoryProducts);

    //         return relatedProducts.Distinct().ToList();
    //     }

    //     public List<int> GetAllSubcategories(int categoryId)
    //     {
    //         var categories = _categoryService.GetAllCategories();

    //         var subcategories = new List<int>();
    //         GetSubcategoriesRecursive(categories, categoryId, subcategories);

    //         return subcategories;
    //     }

    //     private void GetSubcategoriesRecursive(List<Category> categories, int categoryId, List<int> subcategories)
    //     {
    //         var category = categories.FirstOrDefault(c => c.Id == categoryId);
    //         if (category != null)
    //         {
    //             subcategories.Add(categoryId);
    //             foreach (var subcategoryId in category.Subcategories)
    //             {
    //                 GetSubcategoriesRecursive(categories, subcategoryId, subcategories);
    //             }
    //         }
    //     }
    // }
    //         public class ProductCategoryAssociationService : IProductCategoryAssociationService
    //         {
    //             private readonly List<SaveProductCategoryRelation> _productCategoryRelations; // Replace this with your actual data source or service
    //             private readonly ICategoryService _categoryService;

    //             public ProductCategoryAssociationService(ICategoryService categoryService)
    //             {
    //                 _productCategoryRelations = new List<SaveProductCategoryRelation>();
    //                 _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    //             }

    //             public void AssociateProductWithCategory(int productId, int categoryId)
    //             {
    //                 _productCategoryRelations.Add(new SaveProductCategoryRelation { ProductId = productId, CategoryId = categoryId });
    //             }

    //             public void DisassociateProductFromCategory(int productId, int categoryId)
    //             {
    //                 var relationToRemove = _productCategoryRelations.FirstOrDefault(r => r.ProductId == productId && r.CategoryId == categoryId);
    //                 if (relationToRemove != null)
    //                 {
    //                     _productCategoryRelations.Remove(relationToRemove);
    //                 }
    //             }

    //             public List<int> GetProductIdsByCategory(int categoryId)
    //             {
    //                 var relatedProducts = _productCategoryRelations.Where(r => r.CategoryId == categoryId).Select(r => r.ProductId).ToList();

    //                 var subcategoryIds = GetAllSubcategories(categoryId);

    //                 var subcategoryProducts = _productCategoryRelations
    //                     .Where(r => subcategoryIds.Contains(r.CategoryId))
    //                     .Select(r => r.ProductId)
    //                     .ToList();

    //                 relatedProducts.AddRange(subcategoryProducts);

    //                 return relatedProducts.Distinct().ToList();
    //             }

    //             public List<int> GetAllSubcategories(int categoryId)
    //             {
    //                 var categories = _categoryService.GetAllCategories();

    //                 var subcategories = new List<int>();
    //                 GetSubcategoriesRecursive(categories, categoryId, subcategories);

    //                 return subcategories;
    //             }

    //             private void GetSubcategoriesRecursive(List<Category> categories, int categoryId, List<int> subcategories)
    //             {
    //                 var category = categories.FirstOrDefault(c => c.Id == categoryId);
    //                 if (category != null)
    //                 {
    //                     subcategories.Add(categoryId);
    //                     foreach (var subcategoryId in category.Subcategories)
    //                     {
    //                         GetSubcategoriesRecursive(categories, subcategoryId, subcategories);
    //                     }
    //                 }
    //             }




    /*
    private readonly IFileService _fileService;
    private readonly string _productCategoryRelationFilePath;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private List<SaveProductCategoryRelation> _productCategoryRelations;

    public ProductCategoryRelationService(
        IFileService fileService,
        IOptions<ProductServiceOptions> options)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _productCategoryRelationFilePath = options?.Value?.ProductCategoryFilePath
?? throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
        InitializeProductCategoryRelations();
    }

    private void InitializeProductCategoryRelations()
    {
        var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryRelationFilePath);
        var _productCategoryRelations = JsonConvert.DeserializeObject<List<SaveProductCategoryRelation>>(productCategoryRelationJson) ?? new List<SaveProductCategoryRelation>();
    }

    public List<SaveProductCategoryRelation> GetAllProductCategoryRelation()
    {
        return _productCategoryRelations;
    }* /


}
*/
/* public List<Product> GetProductsByCategory(int categoryId)
   {
   var productIds = GetProductIdsByCategory(categoryId);
   return _productService.GetProductById(productIds);
   }

   private List<int> GetProductIdsByCategory(int categoryId)
   {
   var relatedProductIds = _productCategoryRelations
   .Where(r => r.CategoryId == categoryId)
   .Select(r => r.ProductId)
   .ToList();

   var subcategoryIds = GetAllSubcategories(categoryId);

   var subcategoryProducts = _productCategoryRelations
   .Where(r => subcategoryIds.Contains(r.CategoryId))
   .Select(r => r.ProductId)
   .ToList();

   relatedProductIds.AddRange(subcategoryProducts);

   return relatedProductIds.Distinct().ToList();
   }

   private List<int> GetAllSubcategories(int categoryId)
   {
   var categories = _categoryService.GetAllCategories();
   var subcategories = new List<int>();
   GetSubcategoriesRecursive(categories, categoryId, subcategories);
   return subcategories;
   }

   private void GetSubcategoriesRecursive(List<Category> categories, int categoryId, List<int> subcategories)
   {
   var category = categories.FirstOrDefault(c => c.Id == categoryId);
   if (category != null)
   {
   subcategories.Add(categoryId);
   foreach (var subcategoryId in category.Subcategories)
   {
   GetSubcategoriesRecursive(categories, subcategoryId, subcategories);
   }
   }
   }

*/

/*using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;

namespace WebShop.Services
{
    public class ProductCategoryRelationService : IProductCategoryRelationService
    {
        private readonly IFileService _fileService;
        private readonly string _productCategoryRelationFilePath;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly List<IProductCategoryRelationService> _productCategoryRelationServices;

        public ProductCategoryRelationService(IFileService fileService, IOptions<ProductServiceOptions> options, ICategoryService categoryService, IProductCategoryRelationService productCategoryRelationService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _productCategoryRelationFilePath = 
                options.Value.ProductCategoryFilePath ?? throw new ArgumentNullException(nameof(options.Value.ProductCategoryFilePath));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productCategoryRelationServices = 
                (List<IProductCategoryRelationService>?)(productCategoryRelationService ?? throw new ArgumentNullException(nameof(productCategoryRelationService)));
        }

        public List<SaveProductCategoryRelation> GetAllProductCategoryRelation()
        {
            var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryRelationFilePath);
            var _productCategoryRelation =
                JsonConvert.DeserializeObject<List<SaveProductCategoryRelation>>(productCategoryRelationJson) ??
                new List<SaveProductCategoryRelation>();
            return _productCategoryRelation;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var productIds = GetProductIdsByCategory(categoryId);
            return _productService.GetProductById(productIds);
        }

        private List<int> GetProductIdsByCategory(int categoryId)
        {
            var relatedProducts = _productCategoryRelationServices.Where(r => r.CategoryId == categoryId).ToList();

            var subcategoryIds = GetAllSubcategories(categoryId);

            var subcategoryProducts = _productCategoryRelationServices
                .Where(r => subcategoryIds.Contains(r.CategoryId))
                .ToList();

            relatedProducts.AddRange(subcategoryProducts);

            return relatedProducts.Select(r => r.ProductId).Distinct().ToList();
        }

        private List<int> GetAllSubcategories(int categoryId)
        {
            var categories = _categoryService.GetAllCategories();

            var subcategories = new List<int>();
            GetSubcategoriesRecursive(categories, categoryId, subcategories);

            return subcategories;
        }

        private void GetSubcategoriesRecursive(List<Category> categories, int categoryId, List<int> subcategories)
        {
            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                subcategories.Add(categoryId);
                foreach (var subcategoryId in category.Subcategories)
                {
                    GetSubcategoriesRecursive(categories, subcategoryId, subcategories);
                }
            }
        }
}
}

        
*/

/*public Category GetCategoryById(List<Category> categories, int categoryId)
        {
            foreach (var category in categories)
            {
                if (category.Id == categoryId)
                {
                    return category;
                }
            }
            return null;
        }*/

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

//        public List<SaveProductCategoryRelation> GetAllProductCategoryRelation()
//        {
//            var productCategoryRelationJson = _fileService.ReadAllText(_productCategoryFilePath);
//            var productCategoryRelation =
//                JsonConvert.DeserializeObject<List<SaveProductCategoryRelation>>(productCategoryRelationJson) ??
//                new List<SaveProductCategoryRelation>();
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
