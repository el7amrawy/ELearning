using ELearning.Areas.Instructor.ViewModels;
using ELearning.Core.Interfaces;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    public class HomeController : BaseInstructorController
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Courses
                .GetInstructorCoursesAsync<Course_ViewModel>(User.GetUserId(), includes: ["Image"]);
            
            return View(courses);
        }
    }
}