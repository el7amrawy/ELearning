using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Route("dashboard/{controller=dashboard}/{action=index}/{id?}")]
    public class BaseDashboardController : Controller
    {
    }
}
