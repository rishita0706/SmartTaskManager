using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;

namespace SmartTaskManager.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();

            return View(roles);
        }
    }
}
