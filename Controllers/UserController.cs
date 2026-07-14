using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartTaskManager.Data;

namespace SmartTaskManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly SmartTaskManagerDbContext _context;

        public UserController(
            IUserService userService,
            SmartTaskManagerDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();

            return View(users);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(
                _context.RoleMasters,
                "RoleId",
                "RoleName");

            ViewBag.Departments = new SelectList(
                _context.DepartmentMasters,
                "DepartmentId",
                "DepartmentName");

            ViewBag.Managers = new SelectList(
                _context.UserMasters.Where(u => u.RoleId == 2),
                "UserId",
                "FullName",
                "LastName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(UserMaster user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);

                return RedirectToAction("Index");
            }

            ViewBag.Roles = new SelectList(
                _context.RoleMasters,
                "RoleId",
                "RoleName");

            ViewBag.Departments = new SelectList(
                _context.DepartmentMasters,
                "DepartmentId",
                "DepartmentName");

            ViewBag.Managers = new SelectList(
                _context.UserMasters.Where(u => u.RoleId == 2),
                "UserId",
                "FullName",
                "LastName");

            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            ViewBag.Roles =
                new SelectList(
                    _context.RoleMasters,
                    "RoleId",
                    "RoleName",
                    user.RoleId);

            ViewBag.Departments =
                new SelectList(
                    _context.DepartmentMasters,
                    "DepartmentId",
                    "DepartmentName",
                    user.DepartmentId);

            ViewBag.Managers =
                new SelectList(
                    _context.UserMasters.Where(u => u.RoleId == 2),
                    "UserId",
                    "FullName",
                    user.RoleId);

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserMaster user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(user);

                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}