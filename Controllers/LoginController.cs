using Microsoft.AspNetCore.Mvc;
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

            return RedirectToAction("Index", "Dashboard");
        }
    }
}