using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Interfaces;

namespace SmartTaskManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();

            return View(users);
        }
    }
}