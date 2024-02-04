using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    [Route("category")]
    public class CategoryController : HomeController
    {
        public CategoryController(ILogger<HomeController> logger,
            IProductService productService,
            ICategoryService categoryService, IService service) : base(
            logger, service,
            productService,
            categoryService
        )
        {
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Categories = _categoryService.GetAllCategories();
            return View("Index", model);
        }

        [HttpGet("Details/{id}", Name = "Details")]
        public IActionResult Details(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var category = _categoryService.GetCategoryById(id);
            var products = _productService.GetAllProductsByCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            model.Products = products;
            model.Categories = new List<Category> { category };

            return View("Details", model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            HomeViewModel model = new HomeViewModel();
            Category category = new Models.Category();
            model = new HomeViewModel();
            {
                model.Categories = new List<Category> { category };
                model.Products = new List<Product>();
            };
            return View("Create", model);
        }

        [HttpPost("create")]
        public IActionResult Create(Models.Category category)
        {
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Details", new { id = category.Id });
            }

            Console.WriteLine($"{ModelState.IsValid},\n {ModelState.ValidationState}");

            HomeViewModel model = new HomeViewModel();

            return View("Create", model);
        }

        [HttpGet("edit/{id}", Name = "EditCategory")]
        public IActionResult Edit(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var categories = _categoryService.GetCategoryById(id);
            var product = _productService.GetAllProductsByCategory(id);

            model = new HomeViewModel
            {
                Categories = new List<Category> { categories },
                Products = product
            };

            return View("Edit", model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeViewModel model)
        {
            try
            {
                Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
                foreach (var key in ModelState.Keys)
                {
                    var modelStateEntry = ModelState[key];
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                    }
                }

                model.Categories ??= new List<Category>();

                if (model.Categories.Any())
                {
                    var updatedCategories = model.Categories.First();

                    if (ModelState.IsValid)
                    {
                        _categoryService.UpdateCategory(updatedCategories);
                        return RedirectToAction("Details", new { id = updatedCategories.Id });
                    }
                }

                var products = _productService.GetAllProductsByCategory(model.Categories?.FirstOrDefault()?.Id ?? 0);
                model.Products = products;
                return View("Edit", model);
            }
            catch (ProductServiceException ex)
            {
                var categories = _categoryService.GetAllCategories();
                model.Categories = categories;
                return View();
            }
        }


        [HttpGet("Delete/{id}", Name = "DeleteC")]
        public IActionResult Delete(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var categories = _categoryService.GetCategoryById(id);
            var product = _productService.GetAllProductsByCategory(id);

            model = new HomeViewModel
            {
                Categories = new List<Category> { categories },
                Products = product
            };

            return View(model);
        }

        [HttpPost("Delete/{id}", Name = "DeleteC")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            _productService.DeleteProductByCategoryId(id);
            return RedirectToAction("Index");
        }
    }
}