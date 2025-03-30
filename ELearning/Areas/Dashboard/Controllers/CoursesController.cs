using ELearning.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class CoursesController : BaseDashboardController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var levels = await _unitOfWork.Levels.GetAllAsync();
            ViewBag.Levels = new SelectList(levels, "Id", "Name");

            var langs = await _unitOfWork.Languages.GetAllAsync();
            ViewBag.Languages = new SelectList(langs, "Id", "Name");

            var status = await _unitOfWork.CoursesStatus.GetAllAsync();
            ViewBag.Status = new SelectList(status, "Id", "Name");
            return View();
        }
    }
}
