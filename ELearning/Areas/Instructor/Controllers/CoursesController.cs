using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearning.Areas.Instructor.Controllers
{
    public class CoursesController : BaseInstructorController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public CoursesController(IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Courses.GetInstructorCourses(User.GetUserId());

            return View(_mapper.Map<IEnumerable<Course_ViewModel>>(courses));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
            ViewBag.Languages = new SelectList(await _unitOfWork.Languages.GetAllAsync(), "Id", "Name");
            ViewBag.Levels = new SelectList(await _unitOfWork.Levels.GetAllAsync(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourse_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(model);

                var res = await _photoService.AddPhotoAsync(model.ImageFile, 900, 1600);

                if (res.Error != null)
                {
                    TempData["Error"] = res.Error.Message;
                    return View(model);
                }

                var image = new Image { CreatedAt = DateTime.Now, PublicId = res.PublicId, URL = res.SecureUrl.AbsoluteUri };


                course.Image = image;

                _unitOfWork.Courses.Add(course);

                if (await _unitOfWork.CompleteAsync() < 1)
                    TempData["Error"] += ",problem creating course";

                TempData["Success"] = "Created course successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}