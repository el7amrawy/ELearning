using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [Authorize(Roles ="Instructor"),Route("Instructor/[Controller]/{Action=Index}/{Id?}")]
    public class BaseInstructorController : Controller
    {
    }
}