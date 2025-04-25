using ELearning.Core.Interfaces.Services;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELearning.Filters
{
    public class CourseAccessFilter : IAsyncAuthorizationFilter
    {
        private readonly ICourseService _courseService;

        public CourseAccessFilter(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var result = new RedirectToActionResult("AccessDenied", "Error", new { area = "" });

            var userId = context.HttpContext.User.GetUserId();

            int courseId = 0;

            if (!int.TryParse(context.HttpContext.Request.RouteValues["Id"]?.ToString(),out courseId))
            {
                context.Result = result;
                return;
            }

            var res = await _courseService.ValidateCoursePayment(courseId, userId);

            if (!res.IsSuccess)
            {
                context.Result = result;
                return;
            }
        }
    }
}
