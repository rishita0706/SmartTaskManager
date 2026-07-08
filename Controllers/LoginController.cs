using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Interfaces;
using SmartTaskManager.ViewModels;
using Microsoft.AspNetCore.Http;

namespace SmartTaskManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.ValidateUser(
                model.Email,
                model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View(model);
            }

            HttpContext.Session.SetInt32(
                "UserId",
                user.UserId);

            HttpContext.Session.SetString(
                "UserName",
                user.FirstName);

            HttpContext.Session.SetString(
                "Role",
                user.Role?.RoleName ?? "");

            return RedirectToAction(
                "Index",
                "Dashboard");
        }
    }
}