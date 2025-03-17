using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Authorize]
    [Area("Dashboard")]
    public class BaseDashboardController : Controller
    {
    }
}