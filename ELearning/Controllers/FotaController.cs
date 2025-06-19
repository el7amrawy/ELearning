using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class FotaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}