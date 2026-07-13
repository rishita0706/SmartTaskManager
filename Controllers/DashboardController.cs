using Microsoft.AspNetCore.Mvc;

namespace SmartTaskManager.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}