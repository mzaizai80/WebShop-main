using System.Collections.Generic;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IProductCategoryService
    {
        List<ProductCategoryRelation> GetAllProductCategoryRelation();
        Category FindCategory(List<Category> categories, int categoryId);

    }
}