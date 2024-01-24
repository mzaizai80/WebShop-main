using Microsoft.AspNetCore.Mvc;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop
{
    public class HomeController : Controller
    {
        protected readonly ILogger<HomeController> _logger;
        protected readonly IService _service;
        protected readonly IProductService _productService;
        protected readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IService service,
            IProductService productService,
            ICategoryService categoryService
             )
        {
            _logger = logger;
            _service = service;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _service.GetAllCategories();
            var products = _service.GetAllProducts();

            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(homeViewModel);
        }


        [HttpGet]
        public IActionResult ProductsByCategory(int categoryId)
        {
            var products = _service.GetProductsByCategory(categoryId);
            var categories = _service.GetAllCategories();

            var homeViewModel = new HomeViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(homeViewModel);
        }

    }
}




//public IActionResult Index()
//{
//    var categories = _service.GetAllCategories();
//    var products = _service.GetAllProducts();

//    var homeViewModel = new HomeViewModel
//    {
//        Categories = categories,
//        Products = products
//    };
//    Console.WriteLine($"Products Controller{products.ToList()}");
//    //ViewData["_LayoutModel"] = homeViewModel;

//    return View(homeViewModel);
//}




//public IActionResult Index()
//{
//    var categories = _service.GetAllCategories();
//    var products = _service.GetAllProducts();

//    var categoryViewModels = categories.Select(category => new CategoryViewModel
//    {
//        Id = category.Id,
//        Name = category.Name,
//        Subcategories = category.Subcategories?.Select(subcategoryId => new CategoryViewModel
//        {
//            Id = subcategoryId,
//            Name = "Subcategory Name" 
//        })
//    });

//    var homeViewModel = new HomeViewModel
//    {
//        Categories = categoryViewModels,
//        Products = products
//    };

//    return View(homeViewModel);
//}


//public IActionResult ProductsByCategory(int categoryId)
//{
//    var products = _service.GetProductsByCategory(categoryId);
//    var categories = _service.GetAllCategories();

//    var homeViewModel = new HomeViewModel
//    {
//        Products = products,
//        Categories = (IEnumerable<CategoryViewModel>)categories
//    };

//    return View(homeViewModel);
//}

//public IActionResult Indexa(int? categoryId)
//{
//    var products = categoryId.HasValue
//        ? _service.GetProductsByCategory(categoryId.Value)
//        : _service.GetAllProducts();

//    return View(products);
//}
