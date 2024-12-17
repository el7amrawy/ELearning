using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["Error"] = "test";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
