using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class CoursesController : BaseDashboardController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create() => View();
    }
}
