using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Handle404()
        {
            return View("NotFound");
        }
    }
}
