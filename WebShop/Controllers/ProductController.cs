using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    [Route("product")]
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

        [HttpGet("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Products = _productService.GetAllProducts();
            return View("Index", model);
        }

        [HttpGet("product/Details/{id}", Name = "GetProductDetails")]
        public IActionResult Details(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var product = _productService.GetProductByProductId(id);

            if (product == null)
            {
                return NotFound();
            }

            model.Products = new List<Product> { product };
            return View("Details", model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            HomeViewModel model = new HomeViewModel();
            var product = new Product();
            var categories = _categoryService.GetAllCategories();

            model = new HomeViewModel
            {
                Products = new List<Product> { product },
                Categories = categories
            };

            return View("Create", model);
        }

        [HttpPost("create")]
        public IActionResult Create(Models.Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Details", new { id = product.Id });
            }

            HomeViewModel model = new HomeViewModel();
            model.Categories = _categoryService.GetAllCategories();
            return View("Create", model);
        }

        [HttpGet("edit/{id}", Name = "EditProduct")]
        public IActionResult Edit(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var product = _productService.GetProductByProductId(id);
            var categories = _categoryService.GetAllCategories();

            model = new HomeViewModel
            {
                Products = new List<Product> { product },
                Categories = categories
            };

            return View("Edit", model);
        }


        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeViewModel model)
        {
            try
            {
                if (model.Products != null && model.Products.Any())
                {

                    var updatedProduct = model.Products.First();

                    if (ModelState.IsValid)
                    {
                        _productService.UpdateProduct(updatedProduct);
                        return RedirectToAction("Details", new { id = updatedProduct.Id });
                    }
                }

                var categories = _categoryService.GetAllCategories();
                model.Categories = categories;
                return View("Edit", model);
            }
            catch (ProductServiceException ex)
            {
                var categories = _categoryService.GetAllCategories();
                model.Categories = categories;
                return View();
            }
        }

        [HttpGet("Delete/{id}", Name = "DeleteDetails")]
        public IActionResult Delete(int id)
        {

            HomeViewModel model = new HomeViewModel();
            var product = _productService.GetProductByProductId(id);

            if (product == null)
            {
                return NotFound();
            }

            model.Products = new List<Product> { product };
            return View("Delete", model);
        }

        [HttpPost("Delete/{id}", Name = "DeleteDetails")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productService.GetProductByProductId(id);

            if (product == null)
            {
                return NotFound();
            }

            int categoryId = product.CategoryId;

            _productService.DeleteProduct(id);

            return RedirectToAction("ProductsByCategory", "Home", new { categoryId });
        }
    }
}
