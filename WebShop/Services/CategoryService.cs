using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections;
using WebShop.Models;

namespace WebShop.Services
{
    public class CategoryService : ICategoryService, IEnumerable<Category>
        {
            private readonly IFileService _fileService;
            private readonly string _categoriesFilePath;
            private readonly List< Category> _categories;


            public CategoryService(IFileService fileService, IOptions<FilepathServiceOptions> options, List<Category> categories)
            {
                _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
                _categoriesFilePath = options.Value.CategoriesFilePath ?? throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
                _categories = categories;
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
            try
            {
                var categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);
                _fileService.WriteAllText(_categoriesFilePath, categoriesJson);
            }
            catch (Exception ex)
            {
                throw new CategoryServiceException("Error saving categories.", ex);
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var categoriesJson = _fileService.ReadAllText(_categoriesFilePath);
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
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
            int maxId = categories.Any() ? categories.Max(p => p.Id) : 0;
            category.Id = maxId + 1;
            categories.Add(category);
            SaveCategories(categories);
        }

        public void UpdateCategory(Category updatedCategory)
        {
            var categories = GetAllCategories();
            var existingCategory = GetCategoryById(updatedCategory.Id);

            foreach (var existingCategoryProductId in existingCategory.ProductIds)
            {
                Console.WriteLine(existingCategoryProductId);
            }

            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                SaveCategories(categories);
            }
        }

        public void UpdateProductsIdListInCategory(int productId, int previousCategoryId, int newCategoryId)
        {
            var categories = GetAllCategories();

            categories.FirstOrDefault(
                c => c.Id == previousCategoryId)?.ProductIds.Remove(productId);

            categories.FirstOrDefault(
                c => c.Id == newCategoryId)?.ProductIds.Add(productId);

            SaveCategories(categories);
        }


        public Category GetCategoryById(int Id)
        {
            try
            {
                var categories = GetAllCategories();
                return categories.FirstOrDefault(c => c.Id == Id);

            }
            catch (Exception ex)
            {
                throw new CategoryServiceException("Error retrieving product by ID.", ex);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                var categories = GetAllCategories();
                var categoryToRemove = categories.FirstOrDefault(c => c.Id == categoryId);

                if (categoryToRemove != null)
                {
                    categories.Remove(categoryToRemove);
                    SaveCategories(categories);
                }
                else
                {
                    throw new CategoryServiceException($"Category with ID {categoryId} not found for deletion.");
                }
            }
            catch (Exception ex)
            {
                throw new CategoryServiceException("Error deleting Category.", ex);
            }
        }


        public void DeleteCategory1(int categoryId)
        {
            var categories = GetAllCategories();
            var categoryToRemove = GetCategoryById(categoryId);

            if (categoryToRemove != null)
            {
                categories.Remove(categoryToRemove);
                SaveCategories(categories);
            }
        }
    }
}
