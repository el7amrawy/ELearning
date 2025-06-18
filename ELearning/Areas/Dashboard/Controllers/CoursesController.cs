using ELearning.Areas.Dashboard.ViewModels;
using ELearning.Core.Consts;
using ELearning.Core.Interfaces;
using ELearning.Helpers;
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

        public async Task<IActionResult> Index(int pageNumber, int pageSize)
        {
            var pagination = new Pagination(pageNumber > 0 ? pageNumber : 1, pageSize > 0 ? pageSize : 10, await _unitOfWork.Categories.CountAsync());
            ViewBag.Pagination = pagination;

            return View(await _unitOfWork.Courses
                .GetAllAsync<Course_ViewModel>(pageNumber: pagination.PageNumber, pageSize: pagination.PageSize, orderBy: c => c.CreatedAt, orderByDirection: OrderBy.Descending));
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
