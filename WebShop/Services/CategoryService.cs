using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.Services
{
    public class CategoryService : ICategoryService, IEnumerable<Category>
    {
        private readonly IFileService _fileService;
        private readonly string _categoriesFilePath;
        private readonly List<Category> _categories;

        public CategoryService(IFileService fileService, IOptions<WebShopFileServiceOptions> options,
            List<Category> categories)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _categories = categories;
            _categoriesFilePath = options.Value.CategoriesFilePath ??
                                  throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
        }

        public IEnumerator<Category> GetEnumerator()
        {
            return _categories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ??
                                            new List<Category>();
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
            if (updatedCategory == null)
            {
                throw new ArgumentNullException(nameof(updatedCategory));
            }

            var categories = GetAllCategories();
            var existingCategory = GetCategoryById(categories, updatedCategory.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                existingCategory.Description = updatedCategory.Description;
                existingCategory.AssociatedProductIds = updatedCategory.AssociatedProductIds;

                SaveCategories(categories);
            }
            else
            {
                throw new CategoryServiceException($"Category with ID {updatedCategory.Id} not found.");
            }
        }


        public Category GetCategoryById(int categoryId)
        {
            var categories = GetAllCategories();
            return categories.FirstOrDefault(c => c.Id == categoryId);
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

        public void UpdateAssociationOfCategoryWithProducts(int  updatedProductId, int categoryId)
        {
            if (updatedProductId == null)
            {
                throw new ArgumentNullException(nameof(updatedProductId));
            }

            var categories = GetAllCategories();
            var existingCategory = GetCategoryById(categories, categoryId);

            if (existingCategory != null)
            {
                SaveCategories(categories);
            }
            else
            {
                throw new CategoryServiceException($"Category with ID {categoryId} not found.");
            }
        }
    }
}


//public void UpdateReverseLookUpCategoryForProduct(Product existingProduct, int categoryId)
        //public void UpdateCategoryForProduct(Product existingProduct, List<int> ListOfProductIdsIncategory)
        //{
        //    try
        //    {
        //        if (existingProduct == null)
        //        {
        //            throw new ArgumentNullException(nameof(existingProduct));
        //        }

        //        // Remove the product from the old categories
        //        foreach (var oldCategoryId in existingProduct.ReverseLookupOfCategoryIds)
        //        {
        //            var oldCategory = GetCategoryById(oldCategoryId);
        //            oldCategory?.AssociatedProductIds?.Remove(existingProduct.Id);
        //            UpdateCategory(oldCategory);
        //        }

        //        // Add the product to the new categories
        //        foreach (var newProductIdsIncategory in ListOfProductIdsIncategory)
        //        {
        //            var newCategory = GetCategoryById(newProductIdsIncategory);
        //            if (newCategory != null)
        //            {
        //                if (newCategory.AssociatedProductIds == null)
        //                {
        //                    newCategory.AssociatedProductIds = new List<int>();
        //                }

        //                newCategory.AssociatedProductIds.Add(existingProduct.Id);
        //                UpdateCategory(newCategory);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CategoryServiceException($"Error updating category for product. {ex.Message}", ex);
        //    }
        //}









//public void UpdateCategoryForProduct(Product existingProduct, int categoryId)
//{
//    if (existingProduct == null)
//    {
//        throw new ArgumentNullException(nameof(existingProduct));
//    }

//    var oldCategory = GetCategoryById(existingProduct.CategoryId);
//    if (oldCategory != null)
//    {
//        oldCategory.ReverseLookutOfCategoryIds?.Remove(existingProduct.Id);
//        UpdateCategory(oldCategory);
//    }

//    if (!categoryId.Equals(null) )
//    {
//        var newCategory = GetCategoryById(categoryId);
//        if (newCategory != null)
//        {
//            if (newCategory.ReverseLookutOfCategoryIds == null)
//            {
//                newCategory.ReverseLookutOfCategoryIds = new List<int>();
//            }

//            newCategory.ReverseLookutOfCategoryIds.Add(existingProduct.Id);
//            UpdateCategory(newCategory);
//        }
//    }
//}
