using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class LanguagesController : BaseDashboardController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LanguagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index() => View(await _unitOfWork.Languages.GetAllAsync());
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Languages.Add(language);

                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Created language successfully";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Problem creating language";
            }
            return View(language);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var language = await _unitOfWork.Languages.GetByIdAsync(id);
            if (language == null)
            {
                TempData["Error"] = "language doesn't exist";
                return this.RedirectToPrevious();
            }

            return View(language);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Language model)
        {
            if (ModelState.IsValid)
            {
                var language = await _unitOfWork.Categories.GetByIdAsync(model.Id);
                if (language == null)
                {
                    TempData["Error"] = "Language doesn't exist";
                    return this.RedirectToPrevious();
                }

                _unitOfWork.Languages.Update(model);

                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Language has been edited successfully";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Problem editing language";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var language = await _unitOfWork.Languages.GetByIdAsync(id);

            if (language == null)
            {
                TempData["Error"] = "Language doesn't exist";
                return this.RedirectToPrevious();
            }

            _unitOfWork.Languages.Delete(language);

            if (await _unitOfWork.CompleteAsync() < 1)
            {
                TempData["Error"] = "problem deleting language";
                return this.RedirectToPrevious();
            }

            TempData["Success"] = "language deleted successfully";

            return RedirectToAction("Index");
        }
    }
}