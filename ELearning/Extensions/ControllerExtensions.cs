using Microsoft.AspNetCore.Mvc;

namespace ELearning.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectToPrevious(this Controller controller)
        {
            var referer = controller.HttpContext.Request.Headers.Referer.ToString();
            if (!string.IsNullOrEmpty(referer))
                return controller.Redirect(referer);
            else 
                return controller.RedirectToAction("", "Home");
        }
    }
}
