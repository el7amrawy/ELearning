using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Attributes;
using ELearning.Core.DTOs;
using ELearning.Core.Interfaces.Services;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    [Route("[area]/Courses/{courseid:int}/[controller]/{action=index}/{id?}")]
    [CourseOwner]
    public class SectionsController : BaseInstructorController
    {
        private readonly ISectionService _sectionService;
        private readonly IMapper _mapper;
        [FromRoute]
        public int CourseId {  get; set; }
        public SectionsController(ISectionService sectionService, IMapper mapper)
        {
            _sectionService = sectionService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index() => View(new CreateSection_ViewModel { CourseId = CourseId });
        [HttpPost]
        public async Task<IActionResult> Index(CreateSection_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _sectionService.CreateAsync(_mapper.Map<SectionDto>(model));

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.ErrorMessage;
                return View(model);
            }

            TempData["Success"] = "Created section sucessfully";

            return RedirectToAction("Index", new { CourseId });
        }
        [HttpGet]
        public async Task<IActionResult> SwapOrder(int sectionId1,int sectionId2)
        {
            var result = await _sectionService.SwapOrder(CourseId, sectionId1, sectionId2);

            if (!result.IsSuccess) TempData["Error"] = result.ErrorMessage;
            else TempData["Success"] = "order changed successfully";

            return this.RedirectToPrevious();
        }
    }
}