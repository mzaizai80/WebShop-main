using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebShop.Services;

namespace WebShop
{
    public class HomeController : Controller
    {
        private readonly IService _service;
        private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger, IService service)
            {
                _logger = logger;
            _service = service;
            }

        public IActionResult Index()
        {
            return View();
        }


        //private readonly IService _service;

        //public HomeController(IService service)
        //{
        //    _service = service;
        //}

        ////[Route("[action]")]

        ////public IActionResult Index()
        ////{
        ////    var products = _service.GetAllProducts();
        ////    return View("Index", products);
        ////    //return View("/Views/Home/Index.cshtml", products);
        ////    //return View("/Pages/Product/Index.cshtml", products);
        ////}

        //public IActionResult Index()
        //{
        //    var categories = _service.GetAllCategories();
        //    return View("Categories", categories);
        //    //return View("/Views/Home/Index.cshtml", products);
        //    //return View("/Pages/Product/Index.cshtml", products);
        //}
    }
}