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

        [HttpGet("Index", Name = "ProductIndex")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Products = _productService.GetAllProducts();
            return View(model);
        }

        [HttpGet("details/{id}", Name = "GetDetails")]
        public IActionResult Details(int id)
        {
            HomeViewModel model = new HomeViewModel();

            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            model.Products = new List<Product> { product };

            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View("Index");
        }

        [HttpPost("create")]
        public IActionResult Create(Models.Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Details", new { id = product.Id });
            }

            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(product);
        }

        [HttpGet("edit/{id}", Name = "EditProduct")]
        public IActionResult Edit(int id)
        {
            HomeViewModel model = new HomeViewModel();
            var product = _productService.GetProductById(id);
            var categories = _categoryService.GetAllCategories();

            if (product == null)
            {
                return NotFound();
            }

            model = new HomeViewModel
            {
                Products = new List<Product> { product },
                Categories = categories
            };

            return View(model);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, HomeViewModel viewModel)
        {
            try
            {
                HomeViewModel model = new HomeViewModel();
                var product = _productService.GetProductById(id);
                var category = _categoryService.GetCategoryById(product.ReverseLookupOfCategoryIds);


                if (viewModel.Products == null)
                {
                    viewModel.Products = new List<Product> { product };
                    viewModel.Categories = new List<Category> { category };
                }

                _productService.UpdateProduct(product);
                return RedirectToAction("Details", new { id = viewModel.Products.First().Id });
            }
            catch (ProductServiceException ex)
            {
                ModelState.AddModelError(string.Empty, "Error updating product.");
                viewModel.Categories = _categoryService.GetAllCategories();
                return View(viewModel);
            }
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {

            HomeViewModel model = new HomeViewModel();

            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            model.Products = new List<Product> { product };


            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}



//[HttpPost("edit/{id}")]
//public IActionResult Edit(int id, HomeViewModel viewModel)
//{
//        try
//        {
//            _productService.UpdateProduct(viewModel.Products.FirstOrDefault());
//            return RedirectToAction("Details", new { id = viewModel.Products.First().Id });
//        }
//        catch (ProductServiceException ex)
//        {
//            ModelState.AddModelError(string.Empty, "Error updating product.");
//            viewModel.Categories = _categoryService.GetAllCategories();
//            return View(viewModel);
//        }
//    //if (ModelState.IsValid)
//    //{
//    //}

//    viewModel.Categories = _categoryService.GetAllCategories();
//    return View(viewModel);
//}





//[HttpGet("edit/{id}")]
//public IActionResult Edit(int id)
//{
//    var product = _productService.GetProductById(id);
//    var categories = _categoryService.GetAllCategories();
//    ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

//    var model = new HomeViewModel
//    {
//        Products = new List<Product> { product }
//    };

//    return View(model);
//}

//[HttpPost("edit/{id}")]
//public IActionResult Edit(HomeViewModel model)
//{
//    if (ModelState.IsValid)
//    {
//        var product = model.Products.FirstOrDefault();
//        if (product != null)
//        {
//            _productService.UpdateProduct(product);
//            return RedirectToAction("Details", new { id = product.Id });
//        }
//    }

//    var categories = _categoryService.GetAllCategories();
//    ViewBag.Categories = new SelectList(categories, "Id", "Name", model.Products?.FirstOrDefault()?.CategoryId);
//    return View(model);
//}




//[HttpGet("edit/{id}")]
//public IActionResult Edit(int id)
//{
//    var product = _productService.GetProductById(id);
//    var categories = _categoryService.GetAllCategories();
//    ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
//    return View(product);
//}

//[HttpPost("edit/{id}")]
//public IActionResult Edit(Models.Product product)
//{
//    if (ModelState.IsValid)
//    {
//        _productService.UpdateProduct(product);
//        return RedirectToAction("Details", new { id = product.Id });
//    }

//    var categories = _categoryService.GetAllCategories();
//    ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
//    return View(product);
//}

//[HttpGet("edit/{id}")]
//public IActionResult Edit(int id)
//{
//    var product = _productService.GetProductById(id);
//    ViewBag.Categories = _categoryService.GetAllCategories();
//    return View(product);
//}

//[HttpPost("edit/{id}")]
//public IActionResult Edit(Models.Product product)
//{
//    if (ModelState.IsValid)
//    {
//        _productService.UpdateProduct(product);
//        return RedirectToAction("Index");
//    }

//    ViewBag.Categories = _categoryService.GetAllCategories();
//    return View(product);
//}
