using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
