using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class Courses : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
