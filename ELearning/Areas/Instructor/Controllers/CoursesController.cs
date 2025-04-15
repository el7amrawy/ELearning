using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Attributes;
using ELearning.Core.Enums;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;
using ELearning.Extensions;
using ELearning.Helpers;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    public class CoursesController : BaseInstructorController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly ISectionService _sectionService;
        public CoursesController(IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper, ISectionService sectionService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _mapper = mapper;
            _sectionService = sectionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 6) => View(new CoursesIndex_ViewModel
        {
            Courses = await _unitOfWork.Courses.GetInstructorCoursesAsync<Course_ViewModel>(User.GetUserId(), pageSize: pageSize, pageNumber: pageNumber),
            Pagination = new Pagination(pageNumber, pageSize, await _unitOfWork.Courses.GetInstructorCoursesCountAsync(User.GetUserId()))
        });
        [HttpGet]
        public IActionResult Create() => View();
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
        [HttpGet, CourseOwner]
        public async Task<IActionResult> Edit(int id) => View(await _unitOfWork.Courses.GetItemAsync<EditCourse_ViewModel>(c => c.Id == id));
        [HttpPost,CourseOwner]
        public async Task<IActionResult> Edit(EditCourse_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var course = await _unitOfWork.Courses.GetItemAsync(c => c.Id == model.Id, ["Image"]);

            _mapper.Map(model, course);

            if (model.ImageFile != null) {
                var res = await _photoService.AddPhotoAsync(model.ImageFile, 900, 1600);

                if (res.Error != null)
                {
                    TempData["Error"] = res.Error.Message;
                    return View(model);
                }

                await _photoService.DeletePhotoAsync(course.Image.PublicId);
                
                var oldImage = course.Image;

                course.Image = new Image { CreatedAt = DateTime.Now, PublicId = res.PublicId, URL = res.SecureUrl.AbsoluteUri };

                _unitOfWork.Images.Delete(oldImage);
            }

            course.UpdatedAt = DateTime.UtcNow;

            if (await _unitOfWork.CompleteAsync() < 1)
                TempData["Error"] += ",problem updating course";

            TempData["Success"] = "updated course successfully";
            return RedirectToAction("Manage", new { id = model.Id });
        }
        [HttpGet, CourseOwner]
        public async Task<IActionResult> Manage(int id) => View(new ManageCourse_ViewModel
        {
            Course = await _unitOfWork.Courses.GetItemAsync<Course_ViewModel>(c => c.Id == id),
            Instructor = await _unitOfWork.Users.GetItemAsync<Instructor_ViewModel>(c => c.Id == User.GetUserId()),
            Sections = (List<SectionWithLectureCount_ViewModel>)await _unitOfWork.Sections.GetAllAsync<SectionWithLectureCount_ViewModel>(s => s.CourseId == id)
        });
        [HttpGet,CourseOwner]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.Courses.GetItemAsync(c => c.Id == id, ["Sections", "Image"]);

            if(course == null)
            {
                TempData["Error"] = "Course does not exist";
                return this.RedirectToPrevious();
            }

            var sectionIds = course.Sections.Select(s => s.Id).ToList();

            foreach (var item in sectionIds)
            {
                await _sectionService.DeleteAsync(id, item);
            }

            await _photoService.DeletePhotoAsync(course.Image.PublicId);

            _unitOfWork.Images.Delete(course.Image);

            if (await _unitOfWork.CompleteAsync() < 1) TempData["Error"] = "problem deleting course";
            else TempData["Success"] = "Deleted course Successfully";

            return RedirectToAction("Index");
        }
        [HttpGet,CourseOwner]
        public async Task<IActionResult> Publish(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);

            if(course == null)
            {
                TempData["Error"] = "course does not exist";
                return this.RedirectToPrevious();
            }

            if (course.StatusId == (int)CourseStatusEnum.Published)
            {
                TempData["Error"] = "this course is already published";
                return this.RedirectToPrevious();
            }

            course.StatusId = (int)CourseStatusEnum.Published;

            if (await _unitOfWork.CompleteAsync() < 1) TempData["Error"] = "failed to publish this course";
            else TempData["Success"] = "this course is now published";

            return this.RedirectToPrevious();
        }
        [HttpGet, CourseOwner]
        public async Task<IActionResult> Unpublish(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
            {
                TempData["Error"] = "course does not exist";
                return this.RedirectToPrevious();
            }

            if (course.StatusId == (int)CourseStatusEnum.Draft)
            {
                TempData["Error"] = "this course is already unpublished";
                return this.RedirectToPrevious();
            }

            course.StatusId = (int)CourseStatusEnum.Draft;

            if (await _unitOfWork.CompleteAsync() < 1) TempData["Error"] = "failed to unpublish this course";
            else TempData["Info"] = "this course is now unpublished";

            return this.RedirectToPrevious();
        }
    }
}