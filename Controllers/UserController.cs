using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartTaskManager.Constants;
using SmartTaskManager.Filters;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Controllers
{
    [SessionAuthorize(RoleNames.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index() => View(_userService.GetAllUsers());

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserMaster user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
                return RedirectToAction("Index");
            }

            PopulateDropdowns(user.RoleId, user.DepartmentId);
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            PopulateDropdowns(user.RoleId, user.DepartmentId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserMaster user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }

            PopulateDropdowns(user.RoleId, user.DepartmentId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns(int? selectedRoleId = null, int? selectedDeptId = null)
        {
            ViewBag.Roles = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName", selectedRoleId);

            var managers = _userService.GetAllUsers().Where(u => u.RoleId == RoleNames.ManagerId);
            ViewBag.Managers = new SelectList(managers, "UserId", "FirstName");

            // Departments still need a service method — see note below.
        }
    }
}