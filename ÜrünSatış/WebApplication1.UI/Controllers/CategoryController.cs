using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
