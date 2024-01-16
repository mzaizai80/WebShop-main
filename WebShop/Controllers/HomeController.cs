using Microsoft.AspNetCore.Mvc;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService _service;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IService service,
            IProductService productService,
            ICategoryService categoryService
             )
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var categories = _service.GetAllCategories();
            var products = _service.GetAllProducts();

            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };
            Console.WriteLine($"Products Controller{products.ToList()}");
            //ViewData["_LayoutModel"] = homeViewModel;

            return View(homeViewModel);
        }

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

        public IActionResult Indexa(int? categoryId)
        {
            var products = categoryId.HasValue
                ? _service.GetProductsByCategory(categoryId.Value)
                : _service.GetAllProducts();

            return View(products);
        }

    }
}