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

        public CategoryService(IFileService fileService,   IOptions<ProductServiceOptions> options, List<Category> categories)
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
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
                Console.WriteLine(categories);
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
                SaveCategories(categories);
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
    }
}