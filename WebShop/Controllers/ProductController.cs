using Microsoft.AspNetCore.Mvc;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("products")]
    public class ProductController : HomeController
    {
        public ProductController(ILogger<HomeController> logger,
            IProductService productService,
            ICategoryService categoryService, IService service) : base(
            logger, service,
            productService,
            categoryService
            )
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}", Name = "GetDetails")]
        public IActionResult Details(int id)
        {
            {
                var Product = _productService.GetProductById(id);

                if (Product == null)
                {
                    return NotFound();

                }
                //Need to be fixed Product
                return View(Product);
            }
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Models.Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpGet("{id}/edit")]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpPost("{id}/edit")]
        public IActionResult Edit(Models.Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost("{id}/delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}