using ELearning.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELearning.Filters
{
    public class SectionExistsFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionExistsFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = new RedirectToActionResult("Handle404", "Error", new { area = "" });

            int secionId = 0;
            
            if(!int.TryParse(context.RouteData.Values["SectionId"]?.ToString(), out secionId))
            {
                context.Result = result;
                return;
            }

            int courseId = 0;

            if(!int.TryParse(context.RouteData.Values["CourseId"]?.ToString(), out courseId))
            {
                context.Result = result;
                return;
            }

            if (courseId == 0 || secionId == 0) {
                context.Result = result;
                return;
            }

            var section = await _unitOfWork.Sections.GetItemAsync(s => s.Id == secionId && s.CourseId == courseId);

            if (section == null)
            {
                context.Result = result;
                return;
            }

            await next();
        }
    }
}