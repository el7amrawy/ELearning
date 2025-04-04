using ELearning.Attributes;
using ELearning.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    [Route("[area]/courses/{courseid:int}/[controller]/{action=index}/{id?}")]
    [CourseOwner]
    public class SectionsController : BaseInstructorController
    {
        [FromRoute]
        public int CourseId {  get; set; }
        public IActionResult Index()
        {
            return View(model: CourseId);
        }
    }
}