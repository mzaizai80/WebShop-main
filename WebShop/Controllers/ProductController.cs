using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryRelationService _productCategoryRelationService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IProductCategoryRelationService productCategoryRelationService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productCategoryRelationService = productCategoryRelationService;
        }

        public IActionResult Index(int? categoryId)
        {
            var productCategoryDetails = new Dictionary<int, Tuple<Product, List<int>>>();
            var products = _productService.GetAllProducts();
            var categories = _categoryService.GetAllCategories();
            var PCRelation = _productCategoryRelationService.GetAllRelations();

            foreach (var relation in PCRelation)
            {
                foreach (var productId in relation.ProductIds)
                {
                    if (!productCategoryDetails.ContainsKey(productId))
                    {
                        productCategoryDetails[productId] = new Tuple<Product, List<int>>(
                            products.FirstOrDefault(p => p.Id == productId),
                            new List<int>()
                        );
                    }

                    productCategoryDetails[productId].Item2.AddRange(relation.CategoryIds);
                    int a = 1;
                    Console.WriteLine($"\nthis is productCategoryDetails {a}\n {productCategoryDetails}");
                    a++;
                }
            }
            Console.WriteLine($"\nthis is productCategoryDetails \n {productCategoryDetails}");

            foreach (var kvp in productCategoryDetails)
            {
                var productId = kvp.Key;
                var product = kvp.Value.Item1;
                var categoryIds = kvp.Value.Item2;

                Console.WriteLine($"Product ID: {productId}, Product Name: {product?.Name}, Category IDs: {string.Join(", ", categoryIds)}");
            }

            if (categoryId.HasValue)
            {
                var category = _categoryService.GetCategoryById(categoryId.Value);
                if (category == null)
                {
                    // Handle the case when the category is not found
                    return NotFound();
                }

                products = _productCategoryRelationService.GetRelationsByCategoryId(categoryId.Value)
                    .SelectMany(relation => relation.ProductIds.Select(productId => _productService.GetProductById(productId)))
                    .ToList();
            }
            else
            {
                products = _productService.GetAllProducts();
            }

            ViewBag.SelectedCategoryId = categoryId;
            return View(productCategoryDetails.Values.ToList());

            //return View(products);
        }

    }

}

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

