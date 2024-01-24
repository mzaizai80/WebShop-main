using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("categories")]
public class CategoryController : Controller 
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _categoryService.GetAllCategories();
        System.Console.WriteLine($"Categories  = {categories}");
        return View(categories);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("create")]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.AddCategory(category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        return View(category);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _categoryService.DeleteCategory(id);
        return RedirectToAction("Index");
    }
}
}



/*public class CategoryController : Controller
{
    private readonly List<Category> _categories; // Replace this with your actual data source or service

    public CategoryController()
    {
        // Initialize or fetch your category data here
        _categories = new List<Category>
        {
            // Your category data goes here
        };
    }

    public IActionResult Index()
    {
        var flatCategories = FlattenCategories(_categories);
        return View(flatCategories);
    }

    private List<Category> FlattenCategories(List<Category> categories)
    {
        List<Category> flatCategories = new List<Category>();

        foreach (var category in categories)
        {
            flatCategories.Add(category);

            if (category.Subcategories?.Any() == true)
            {
                var subcategories = _categories.Where(c => category.Subcategories.Contains(c.Id)).ToList();
                flatCategories.AddRange(FlattenCategories(subcategories));
            }
        }

        return flatCategories;
    }
}
*/

/*public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IActionResult Index1()
    {
        var categories = _categoryService.GetAllCategories();
        return View(categories);
    }

    public IActionResult Index(int page = 1, int pageSize = 5)
    {
        var categories = _categoryService.GetAllCategories();

        // Paginate the categories
        var paginatedCategories = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)categories.Count / pageSize);

        return View(paginatedCategories);
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

*/

//namespace WebShop.Controllers
//{
//    public class CategoryController
//    {
//    }
//}
