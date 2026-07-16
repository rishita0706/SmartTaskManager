using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartTaskManager.Constants;
using SmartTaskManager.Filters;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Controllers
{
    [SessionAuthorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;

        public TaskController(ITaskService taskService, IUserService userService, IDepartmentService departmentService)
        {
            _taskService = taskService;
            _userService = userService;
            _departmentService = departmentService;
        }

        // Role-aware list: Admin sees all, Manager sees what they assigned, Employee sees what's assigned to them.
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString(SessionKeys.Role);
            var userId = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;

            var tasks = role switch
            {
                RoleNames.Admin => _taskService.GetAllTasks(),
                RoleNames.Manager => _taskService.GetTasksAssignedBy(userId),
                RoleNames.Employee => _taskService.GetTasksAssignedTo(userId),
                _ => Enumerable.Empty<TaskMaster>()
            };

            return View(tasks);
        }

        [SessionAuthorize(RoleNames.Admin, RoleNames.Manager)]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(RoleNames.Admin, RoleNames.Manager)]
        public IActionResult Create(TaskMaster task)
        {
            task.AssignedBy = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;
            task.StatusId = TaskStatusIds.Pending;

            if (ModelState.IsValid)
            {
                _taskService.CreateTask(task);
                return RedirectToAction("Index");
            }

            PopulateDropdowns();
            return View(task);
        }

        [SessionAuthorize(RoleNames.Employee)]
        public IActionResult UpdateStatus(int id)
        {
            var task = _taskService.GetTaskById(id);
            var employeeId = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;

            if (task == null || task.AssignedTo != employeeId) return NotFound();

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(RoleNames.Employee)]
        public IActionResult UpdateStatus(int id, int statusId)
        {
            var task = _taskService.GetTaskById(id);
            var employeeId = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;

            if (task == null || task.AssignedTo != employeeId) return NotFound();

            _taskService.UpdateTaskStatus(id, statusId);
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            var employees = _userService.GetAllUsers().Where(u => u.RoleId == RoleNames.EmployeeId);
            ViewBag.Employees = new SelectList(employees, "UserId", "FirstName");
            ViewBag.Departments = new SelectList(_departmentService.GetAllDepartments(), "DepartmentId", "DepartmentName");
            // Priority dropdown needs IPriorityService — same shape as Role/Department.
        }
    }
}