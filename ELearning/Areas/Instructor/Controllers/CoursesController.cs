using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    public class CoursesController : BaseInstructorController
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpGet]
        public IActionResult Create() => View();
    }
}