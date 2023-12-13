using Microsoft.AspNetCore.Mvc;
using WebShop.Services;

namespace WebShopTests
{
    public class HomeController : Controller
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var products = _service.GetAllProducts();
            return View("Index",products);
            //return View("/Views/Home/Index.cshtml", products);
        }
    }
}