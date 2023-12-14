using Microsoft.AspNetCore.Mvc;
using WebShop.Services;

namespace WebShopTests
{
    //[Route("[controller]")]

    public class HomeController : Controller
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        } 
        
        //[Route("[action]")]

        public IActionResult Index()
        {
            var products = _service.GetAllProducts();
            return View("Index",products);
            //return View("/Views/Home/Index.cshtml", products);
            //return View("/Pages/Product/Index.cshtml", products);
        }
    }
}