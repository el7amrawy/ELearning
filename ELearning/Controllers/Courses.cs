using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class Courses : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
