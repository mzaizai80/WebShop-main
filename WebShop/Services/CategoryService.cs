using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebShop.Models;

namespace WebShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IFileReader _fileReader;
        private readonly string _categoriesFilePath;
        private readonly IFileWriter _fileWriter;

        public CategoryService(IFileReader fileReader, IFileWriter fileWriter, IOptions<ProductServiceOptions> options)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
            _categoriesFilePath = options.Value.CategoriesFilePath ??
                                  throw new ArgumentNullException(nameof(options.Value.CategoriesFilePath));
        }

        public void SaveCategories(List<Category> categories)
        {
            var categoriesJson = JsonConvert.SerializeObject(categories);
            _fileWriter.WriteAllText(_categoriesFilePath, categoriesJson);
        }
        
        List<Category> ICategoryService.GetAllCategories()
        {
            var categoriesJson = _fileReader.ReadAllText(_categoriesFilePath);
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson) ?? new List<Category>();
            return categories;
        }

        void ICategoryService.AddCategory(Category category)
        {
            var categories = ((ICategoryService)this).GetAllCategories();
            categories.Add(category);
            SaveCategories(categories);
        }

        void ICategoryService.DeleteCategory(int categoryId)
        {
            var categories = ((ICategoryService)this).GetAllCategories();
            var categoryToRemove = categories.FirstOrDefault(c => c.Id == categoryId);

            if (categoryToRemove != null)
            {
                categories.Remove(categoryToRemove);
                SaveCategories(categories);
            }
        }

        void ICategoryService.UpdateCategory(Category updatedCategory)
        {
            var categories = ((ICategoryService)this).GetAllCategories();
            var existingCategory = categories.FirstOrDefault(c => c.Id == updatedCategory.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                SaveCategories(categories);
            }
        }

    }
}
