using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied() => View(model: Request.Headers.Referer.ToString());
        public IActionResult Handle404() => View("NotFound");
        public IActionResult ServerError() => View();
    }
}
