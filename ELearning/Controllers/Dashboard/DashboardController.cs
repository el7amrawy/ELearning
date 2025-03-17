using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers.Dashboard
{
    public class DashboardController : BaseDashboardController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
