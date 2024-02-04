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
            var categories = _service.GetAllCategories().ToList();
            var products = _service.GetAllProducts().ToList();

            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };

            return View("Index", homeViewModel);
        }

        [HttpGet("ProductsByCategory")]
        public IActionResult ProductsByCategory(int categoryId)
        {
            var products = _service.GetProductsByCategory(categoryId).ToList();
            var categories = _service.GetAllCategories().ToList();

            var homeViewModel = new HomeViewModel
            {
                Products = products,
                Categories = categories
            };

            return View("ProductsByCategory", homeViewModel);
        }
    }
}