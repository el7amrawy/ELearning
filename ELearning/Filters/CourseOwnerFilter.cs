using ELearning.Core.Interfaces;
using ELearning.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELearning.Filters
{
    public class CourseOwnerFilter : IAsyncAuthorizationFilter
    {
        private readonly ICourseAccessService _courseAccessService;
        public CourseOwnerFilter(ICourseAccessService courseAccessService)
        {
            _courseAccessService = courseAccessService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            int courseId = 0;

            bool parsed = int.TryParse(context.HttpContext.Request.RouteValues["CourseId"]?.ToString(), out courseId);

            if (!parsed) {
                int.TryParse(context.HttpContext.Request.RouteValues["Id"]?.ToString(), out courseId);
            }

            var result= new RedirectToActionResult("AccessDenied", "Error", new { area = "" });

            if (courseId == 0)
            {
                context.Result = result;
                return;
            }

            var userId = context.HttpContext.User.GetUserId();

            var res = await _courseAccessService.ValidateCourseOwnerAsync(userId, courseId);

            if (!res.IsSuccess)
                context.Result = result;
        }
    }
}