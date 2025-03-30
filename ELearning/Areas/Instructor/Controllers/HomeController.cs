using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    public class HomeController : BaseInstructorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
