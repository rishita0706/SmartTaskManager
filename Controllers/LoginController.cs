using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Constants;
using SmartTaskManager.Interfaces;
using SmartTaskManager.ViewModels;

namespace SmartTaskManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.ValidateUser(model.Email, model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View(model);
            }

            HttpContext.Session.SetInt32(SessionKeys.UserId, user.UserId);
            HttpContext.Session.SetString(SessionKeys.UserName, user.FirstName);
            HttpContext.Session.SetString(SessionKeys.Role, user.Role?.RoleName ?? "");

            return user.Role?.RoleName switch
            {
                RoleNames.Admin => RedirectToAction("AdminDashboard", "Dashboard"),
                RoleNames.Manager => RedirectToAction("ManagerDashboard", "Dashboard"),
                RoleNames.Employee => RedirectToAction("EmployeeDashboard", "Dashboard"),
                _ => RedirectToAction("Index", "Login")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}