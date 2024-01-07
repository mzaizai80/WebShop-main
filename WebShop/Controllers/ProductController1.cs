using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class ProductController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
