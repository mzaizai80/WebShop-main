using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int? categoryId)
        {
            var model = new HomeViewModel
            {
                Products = _productService.GetProductsByCategory(categoryId.GetValueOrDefault()),
            };
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
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

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(); 
            }

            return View(product);
        }
    }
}


/*
[HttpGet]
        public IActionResult Manage(int productId)
        {
            var product = _productService.GetProductById(productId);
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Manage(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _productService.AddProduct(product);
                }
                else
                {
                    _productService.UpdateProduct(product);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAllCategories();

            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int productId)
        {
            var product = _productService.GetProductById(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int productId)
        {
            _productService.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }


/*using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int? categoryId)
        {
            
            return View();
        }

    }

}*/

/*           var productDetails = new Dictionary<int, List<int>>();

            var products = _productService.GetAllProducts();
            var PCRelation = _productCategoryRelationService.GetAllRelations();

            foreach (var relation in PCRelation)
            {
                foreach (var productId in relation.ProductIds)
                {
                    if (!productDetails.ContainsKey(productId))
                    {
                        productDetails[productId] = new List<int>();
                    }

                    productDetails[productId].AddRange(relation.CategoryIds);
                }
            }
            foreach (var kvp in productCategoryDetails)
            {
                var productId = kvp.Key;
                var details = kvp.Value;

                Console.WriteLine($"Product ID: {productId}");
                Console.WriteLine($"Product Name: {details.Product?.Name}");
                Console.WriteLine($"Category IDs: {string.Join(", ", details.CategoryIds)}");
                Console.WriteLine();

                // Access other category details using category ID
                foreach (var categoryId in details.CategoryIds)
                {
                    var category = categories.FirstOrDefault(c => c.Id == categoryId);
                    if (category != null)
                    {
                        Console.WriteLine($"Category ID: {categoryId}");
                        Console.WriteLine($"Category Name: {category.Name}");
                        // Other category details can be added here
                        Console.WriteLine();
                    }
                }
            }*/



/*    public class ProductController : Controller
    {

        private readonly IProductService _productServices;

        public ProductController(IProductService productServices)
        {
            _productServices = productServices;
        }

        // GET: ProductController
        public IActionResult Index()
        {
            var products = _productServices.GetAllProducts();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
*/

