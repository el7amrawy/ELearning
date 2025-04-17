using ELearning.Core.Enums;
using ELearning.Core.Interfaces;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index() => View(new HomeIndex_ViewModel
        {
            Courses = await _unitOfWork.Courses.GetAllAsync<CourseCard_ViewModel>(pageSize: 3, criteria: c => c.StatusId == (int)CourseStatusEnum.Published)
        });
        public IActionResult About() => View();
    }
}