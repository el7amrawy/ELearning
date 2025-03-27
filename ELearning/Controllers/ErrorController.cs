using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Handle404() => View("NotFound");
        public IActionResult AccessDenied() => View();
    }
}
