using AutoMapper;
using ELearning.Areas.Dashboard.ViewModels;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class CategoriesController : BaseDashboardController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Categories.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Category_ViewModel model)
        {
            if (ModelState.IsValid) {
                var category = _mapper.Map<Category>(model);
                
                _unitOfWork.Categories.Add(category);

                if(await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Created category successfully";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Problem creating category";
            }
            return View(model);
        }
    }
}