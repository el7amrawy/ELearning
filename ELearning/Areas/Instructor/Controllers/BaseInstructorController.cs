using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor"), Area("Instructor")]
    public class BaseInstructorController : Controller
    {
    }
}