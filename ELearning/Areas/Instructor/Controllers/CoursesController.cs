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
        private readonly IInstructorService _instructorService;
        public CoursesController(IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper, IInstructorService instructorService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _mapper = mapper;
            _instructorService = instructorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Courses.GetInstructorCoursesAsync<Course_ViewModel>(User.GetUserId(), ["Image"]);

            return View(courses);
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
                var instructor = await _unitOfWork.Users.GetByIdAsync(User.GetUserId());

                var course = _mapper.Map<Course>(model);

                var res = await _photoService.AddPhotoAsync(model.ImageFile, 900, 1600);

                if (res.Error != null)
                {
                    TempData["Error"] = res.Error.Message;
                    return View(model);
                }

                course.Image = new Image { CreatedAt = DateTime.Now, PublicId = res.PublicId, URL = res.SecureUrl.AbsoluteUri };

                instructor.Courses.Add(course);

                if (await _unitOfWork.CompleteAsync() < 1)
                    TempData["Error"] += ",problem creating course";

                TempData["Success"] = "Created course successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _instructorService.ValidateCourseAsync(User.GetUserId(), id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.ErrorMessage;
                return this.RedirectToPrevious();
            }

            var course = await _unitOfWork.Courses.GetItemAsync<EditCourse_ViewModel>(c => c.Id == id, ["Image"]);

            return View(course);
        }
        [HttpGet]
        public async Task<IActionResult> Manage(int id)
        {
            var result = await _instructorService.ValidateCourseAsync(User.GetUserId(), id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.ErrorMessage;
                return this.RedirectToPrevious();
            }

            var course = await _unitOfWork.Courses.GetItemAsync<Course_ViewModel>(c => c.Id == id);
            var instructor = await _unitOfWork.Users.GetItemAsync<Instructor_ViewModel>(c => c.Id == User.GetUserId());

            return View(new ManageCourse_ViewModel { Course = course, Instructor = instructor });
        }
    }
}