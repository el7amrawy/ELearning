using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    [Area("Dashboard"), Authorize(Roles = "Admin")]
    public class BaseDashboardController : Controller
    {
    }
}