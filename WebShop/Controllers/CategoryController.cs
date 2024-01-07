using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var categories = _categoryService.GetAllCategories();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.AddCategory(category);
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(int id, Category updatedCategory)
    {
        if (id != updatedCategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _categoryService.UpdateCategory(updatedCategory);
            return RedirectToAction("Index");
        }
        return View(updatedCategory);
    }

    public IActionResult Delete(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _categoryService.DeleteCategory(id);
        return RedirectToAction("Index");
    }
}



//namespace WebShop.Controllers
//{
//    public class CategoryController
//    {
//    }
//}
