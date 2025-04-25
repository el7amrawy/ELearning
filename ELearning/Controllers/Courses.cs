using ELearning.Attributes;
using ELearning.Core.Enums;
using ELearning.Core.Interfaces;
using ELearning.Extensions;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class Courses : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public Courses(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var course = await _unitOfWork.Courses.GetItemAsync<CourseDetails_ViewModel>(c => c.Id == id);
            if (course == null)
            {
                TempData["Error"] = "course doesn't exist";
                return this.RedirectToPrevious();
            }

            if (course.StatusId != (int)CourseStatusEnum.Published) return RedirectToAction("Handle404", "Error");
            
            return View(course);
        }
        [CourseAccess]
        public async Task<IActionResult> Learn(int id) => View(await _unitOfWork.Courses.GetItemAsync<LearnCourse_ViewModel>(c => c.Id == id));
    }
}