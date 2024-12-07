using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return Content("1");
        }
    }
}
