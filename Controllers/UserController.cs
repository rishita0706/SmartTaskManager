using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartTaskManager.Constants;
using SmartTaskManager.Filters;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;
using SmartTaskManager.ViewModels;

namespace SmartTaskManager.Controllers
{
    [SessionAuthorize(RoleNames.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;

        public UserController(IUserService userService, IRoleService roleService, IDepartmentService departmentService)
        {
            _userService = userService;
            _roleService = roleService;
            _departmentService = departmentService;
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

            var vm = new UserEditViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                DepartmentId = user.DepartmentId,
                ManagerId = user.ManagerId,
                IsActive = user.IsActive
            };

            PopulateDropdowns(user.RoleId, user.DepartmentId, user.ManagerId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserById(vm.UserId);
                if (user == null) return NotFound();

                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.Phone = vm.Phone;
                user.RoleId = vm.RoleId;
                user.DepartmentId = vm.DepartmentId;
                user.ManagerId = vm.ManagerId;
                user.IsActive = vm.IsActive;

                if (!string.IsNullOrWhiteSpace(vm.NewPassword))
                    user.PasswordHash = vm.NewPassword; // UserService hashes it

                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }

            PopulateDropdowns(vm.RoleId, vm.DepartmentId, vm.ManagerId);
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns(int? selectedRoleId = null, int? selectedDeptId = null, int? selectedManagerId = null)
        {
            ViewBag.Roles = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName", selectedRoleId);

            var managers = _userService.GetAllUsers().Where(u => u.RoleId == RoleNames.ManagerId);
            ViewBag.Managers = new SelectList(managers, "UserId", "FirstName", selectedManagerId);

            ViewBag.Departments = new SelectList(
                _departmentService.GetAllDepartments(), "DepartmentId", "DepartmentName", selectedDeptId);
        }
    }
}