using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Data;

namespace SmartTaskManager.Controllers
{
    public class RoleController : Controller
    {
        private readonly SmartTaskManagerDbContext _context;

        public RoleController(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.RoleMasters.ToList();
            return View(roles);
        }
    }
}
