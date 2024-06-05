using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
