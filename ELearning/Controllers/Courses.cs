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
        [CourseAccess,HttpGet]
        public IActionResult  Learn(int id)
        {
            return View("Learn", $"/Courses/Course/{id}");
        }
        [CourseAccess, HttpGet]
        public async Task<ActionResult<LearnCourse_ViewModel>> Course(int id) => await _unitOfWork.Courses.GetItemAsync<LearnCourse_ViewModel>(c => c.Id == id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseCard_ViewModel>>> Search(
            string search,
            int categoryId,
            decimal maxPrice,
            decimal minPrice,
            double duration,
            int pageNumber,
            int pageSize
            ) => Ok(await _unitOfWork.Courses.SearchAndFilterAsync<CourseCard_ViewModel>(search, categoryId, maxPrice, minPrice, duration, pageNumber, pageSize));
    }
}