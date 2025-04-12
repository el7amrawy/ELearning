using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Attributes;
using ELearning.Core.DTOs;
using ELearning.Core.Interfaces.Services;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    [Route("[area]/Courses/{courseId:int}/Sections/{sectionId:int}/[Controller]/{action=index}/{id?}")]
    [CourseOwner]
    [SectionExists]
    public class LecturesController : BaseInstructorController
    {
        [FromRoute]
        public int CourseId { get; set; }
        [FromRoute]
        public int SectionId { get; set; }
        private readonly ILectureService _lectureService;
        private readonly IMapper _mapper;
        public LecturesController(ILectureService lectureService, IMapper mapper)
        {
            _lectureService = lectureService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(CreateLecture_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var lectureDto= _mapper.Map<CreateLectureDto>(model);
            lectureDto.SectionId = SectionId;

            var res = await _lectureService.CreateAsync(lectureDto, User.GetUsername(), CourseId);

            if (res.IsSuccess) TempData["Success"] = "Created lecture successfully";
            else TempData["Error"] = res.ErrorMessage;

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> SwapOrder(int lecId1, int lecId2) {

            var res = await _lectureService.SwapOrderAsync(SectionId, lecId1, lecId2);

            if (res.IsSuccess) TempData["Success"] = "Swapped lectures order successuflly";
            else TempData["Error"] = res.ErrorMessage;

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _lectureService.DeleteAsync(id, SectionId);

            if (res.IsSuccess) TempData["Success"] = "Deleted lecture successfully";
            else TempData["Error"] = res.ErrorMessage;

            return RedirectToAction("Index");
        }
    }
}