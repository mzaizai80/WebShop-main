using WebShop.Models;

namespace WebShop.Services
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void UpdateCategory(Category updatedCategory);
        void DeleteCategory(int categoryId);
        List<Category> GetAllCategories();
        void SaveCategories(List<Category> categories);
        Category GetCategoryById(int categoryId);
        //Category GetCategoryById(List<Category> categories, int categoryId);
        void UpdateProductsIdListInCategory(int productId, int previousCategoryId, int newCategoryId);
    }
}