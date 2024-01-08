using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IFileService _fileService;
        private readonly string _categoriesFilePath;

        public CategoryService(IFileService fileService,   IOptions<ProductServiceOptions> options)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _categoriesFilePath = options.Value.CategoriesFilePath ??
                                  throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
        }

        public void SaveCategories(List<Category> categories)
        {
            var categoriesJson = JsonConvert.SerializeObject(categories);
            _fileService.WriteAllText(_categoriesFilePath, categoriesJson);
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var categoriesJson = _fileService.ReadAllText(_categoriesFilePath);
                Console.WriteLine(categoriesJson);
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ??
                                            new List<Category>();
                Console.WriteLine($"category obj {categories}");
                return categories;
            }
            catch (Exception ex)
            {
                throw new CategoryServiceException("Error getting all Category / Categories.", ex);
            }
        }

        public void AddCategory(Category category)
        {
            var categories = GetAllCategories();
            categories.Add(category);
            SaveCategories(categories);
        }

        public void DeleteCategory(int categoryId)
        {
            var categories = GetAllCategories();
            var categoryToRemove = GetCategoryById(categoryId);

            if (categoryToRemove != null)
            {
                categories.Remove(categoryToRemove);
                SaveCategories(categories);
            }
        }

        public void UpdateCategory(Category updatedCategory)
        {
            var categories = GetAllCategories();
            var existingCategory = GetCategoryById(updatedCategory.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                existingCategory.Subcategories =
                    updatedCategory.Subcategories; // Assuming you are updating subcategories too
                SaveCategories(categories);
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            var categories = GetAllCategories();
            return categories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}

//public class CategoryService : ICategoryService
    //{
    //    private readonly IFileService _fileService;
    //    private readonly string _categoriesFilePath;

    //    public CategoryService(IFileService fileService, IOptions<ProductServiceOptions> options)
    //    {
    //        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    //        _categoriesFilePath = options.Value.CategoriesFilePath ??
    //                              throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
    //    }

    //    public void SaveCategories(List<Category> categories)
    //    {
    //        var categoriesJson = JsonConvert.SerializeObject(categories);
    //        _fileService.WriteAllText(_categoriesFilePath, categoriesJson);
    //    }

    //    public List<Category> GetAllCategories()
    //    {
    //        var categoriesJson = _fileService.ReadAllText(_categoriesFilePath);
    //        var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
    //        return categories;
    //    }

    //    public void AddCategory(Category category)
    //    {
    //        var categories = GetAllCategories();
    //        categories.Add(category);
    //        SaveCategories(categories);
    //    }

    //    public void DeleteCategory(int categoryId)
    //    {
    //        var categories = GetAllCategories();
    //        var categoryToRemove = GetCategoryById(categoryId);

    //        if (categoryToRemove != null)
    //        {
    //            categories.Remove(categoryToRemove);
    //            SaveCategories(categories);
    //        }
    //    }

    //    public void UpdateCategory(Category updatedCategory)
    //    {
    //        var categories = GetAllCategories();
    //        var existingCategory = GetCategoryById(updatedCategory.Id);

    //        if (existingCategory != null)
    //        {
    //            existingCategory.Name = updatedCategory.Name;
    //            existingCategory.Subcategories = updatedCategory.Subcategories; // Assuming you are updating subcategories too
    //            SaveCategories(categories);
    //        }
    //    }

    //    public Category GetCategoryById(int categoryId)
    //    {
    //        var categories = GetAllCategories();
    //        return categories.FirstOrDefault(c => c.Id == categoryId);
    //    }
    //}







//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using WebShop.Models;

//namespace WebShop.Services
//{
//    public class CategoryService : ICategoryService
//    {
//        private readonly IFileService _fileService;
//        private readonly string _categoriesFilePath;

//        public CategoryService(IFileService fileService, IOptions<ProductServiceOptions> options)
//        {
//            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
//            _categoriesFilePath = options.Value.CategoriesFilePath ??
//                                  throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
//        }

//        public void SaveCategories(List<Category> categories)
//        {
//            var categoriesJson = JsonConvert.SerializeObject(categories);
//            _fileService.WriteAllText(_categoriesFilePath, categoriesJson);
//        }

//        public List<Category> GetAllCategories()
//        {
//            var categoriesJson = _fileService.ReadAllText(_categoriesFilePath);
//            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
//            return categories;
//        }

//        public void AddCategory(Category category)
//        {
//            var categories = GetAllCategories();
//            categories.Add(category);
//            SaveCategories(categories);
//        }

//        public void DeleteCategory(int categoryId)
//        {
//            var categories = GetAllCategories();
//            var categoryToRemove = GetCategoryById(categoryId);

//            if (categoryToRemove != null)
//            {
//                categories.Remove(categoryToRemove);
//                SaveCategories(categories);
//            }
//        }

//        public void UpdateCategory(Category updatedCategory)
//        {
//            var categories = GetAllCategories();
//            var existingCategory = GetCategoryById(updatedCategory.Id);

//            if (existingCategory != null)
//            {
//                existingCategory.Name = updatedCategory.Name;
//                SaveCategories(categories);
//            }
//        }

//        public Category GetCategoryById(int categoryId)
//        {
//            var categories = GetAllCategories();
//            return categories.FirstOrDefault(c => c.Id == categoryId);
//        }
//    }
//}