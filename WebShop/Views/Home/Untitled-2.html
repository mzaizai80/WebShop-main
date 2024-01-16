Home\Index.cshtml
@{
    ViewData["Title"] = "Welcome to Our Web Shop";
}
@model WebShop.ViewModels.HomeViewModel

<div class="container">
    <div>
        <h1 style="mt-5">Welcome to Our Web Shop</h1>
        <p>
            Thank you for visiting our online store. Explore our wide range of products and find the perfect items for
            you.
            Whether you are looking for the latest gadgets, fashionable clothing, or accessories, we have something for
            everyone.
        </p>
        <p>
            Enjoy a seamless shopping experience and discover the best deals on quality products. Happy shopping!
        </p>
    </div>
<div class="container mt-5">
    @foreach (var category in Model.Categories)
    {
        <div class="" style="margin: 2rem;">
            <h2>@category.Name</h2>
            <div class="row mt-4">
                    @foreach (var product in Model.Products)
                    {
                        if (product.CategoryId == category.Id)
                        {
                            <div class="col-md-4 mt-4">
                                <div class="card">
                                    <img src="@product.PictureUrl" class="card-img-top" alt="Product Image">
                                    <div class="card-body">
                                        <h5 class="card-title">@product.Name</h5>
                                        <p class="card-text">Price: $@product.Price</p>
                                        <br/>
                                        <a asp-area="" asp-controller="Product" asp-action="Details" asp-route-id="@product.Id">View Details</a>
                                    </div>
                                </div>
                            </div>
                        }
                    }
            </div>
        </div>
    }
</div>

Home\ProductsByCategory.cshtml
@model WebShop.ViewModels.HomeViewModel

<h2>Products By Category</h2>

<div>
    @foreach (var product in Model.Products)
    {
    <div>
        <h4>@product.Name</h4>
        <p>Price: @product.Price</p>
    </div>
    <div class="col-md-4 mt-4">
        <div class="card">
            <img src="@product.PictureUrl" class="card-img-top" alt="Product Image">
            <div class="card-body">
                <h5 class="card-title">@product.Name</h5>
                <p class="card-text">Price: $@product.Price</p>
                <br />
                <a asp-area="" asp-controller="Product" asp-action="Details" asp-route-id="@product.Id">View Details</a>
            </div>
        </div>
    </div>
    }
</div>

HomeController.cs

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


Product\Details.cshtml
@model WebShop.Models.Product

<h2>Product Details</h2>

<div>
    <div class="col-md-4 mt-4">
        <div class="card">
            <img src="@Model.PictureUrl" class="card-img-top" alt="Product Image">
            <div class="card-body">
                <h5 class="card-title">@Model.Name</h5>
                <p class="card-text">Price: $@Model.Price</p>
                <br />
                <a asp-area="" asp-controller="Product" asp-action="Details" asp-route-id="@Model.Id">View Details</a>
            </div>
        </div>
    </div>
</div>

