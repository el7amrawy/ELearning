using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class HomeController : BaseDashboardController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
