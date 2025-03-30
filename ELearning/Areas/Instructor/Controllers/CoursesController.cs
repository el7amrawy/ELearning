using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    public class CoursesController : BaseInstructorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
