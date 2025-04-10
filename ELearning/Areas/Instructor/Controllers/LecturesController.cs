using ELearning.Areas.Instructor.ViewModels;
using ELearning.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    [Route("[area]/Courses/{courseId:int}/Sections/{sectionId:int}/[Controller]/{action=index}/{id?}")]
    [CourseOwner]
    [SectionExists]
    public class LecturesController : BaseInstructorController
    {
        [FromRoute]
        public int CourseId { get; set; }
        [FromRoute]
        public int SectionId { get; set; }
        public IActionResult Index() => View(new CreateLecture_ViewModel { SectionId = SectionId, CourseId = CourseId });
    }
}