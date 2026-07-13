using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Data;

public class DashboardController : Controller
{
    private readonly SmartTaskManagerDbContext _context;

    public DashboardController(SmartTaskManagerDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AdminDashboard()
    {
        ViewBag.TotalUsers = _context.UserMasters.Count();

        ViewBag.TotalTasks = _context.TaskMasters.Count();

        ViewBag.TotalDepartments =
            _context.DepartmentMasters.Count();

        ViewBag.ActiveEmployees =
            _context.UserMasters.Count(u => u.IsActive);

        return View();
    }

    public IActionResult ManagerDashboard()
    {
        int managerId = 2; // temporary hardcoded manager

        ViewBag.TasksAssignedByMe =
            _context.TaskMasters.Count(t => t.AssignedBy == managerId);

        ViewBag.PendingTasks =
            _context.TaskMasters.Count(
                t => t.AssignedBy == managerId &&
                     t.StatusId == 1);

        ViewBag.CompletedTasks =
            _context.TaskMasters.Count(
                t => t.AssignedBy == managerId &&
                     t.StatusId == 3);

        ViewBag.MyTeamMembers =
            _context.UserMasters.Count(
                u => u.RoleId == 3);

        return View();
    }

    public IActionResult EmployeeDashboard()
    {
        int employeeId = 3; // temporary employee

        ViewBag.MyTasks =
            _context.TaskMasters.Count(
                t => t.AssignedTo == employeeId);

        ViewBag.CompletedTasks =
            _context.TaskMasters.Count(
                t => t.AssignedTo == employeeId &&
                     t.StatusId == 3);

        ViewBag.DueTasks =
            _context.TaskMasters.Count(
                t => t.AssignedTo == employeeId &&
                     t.DueDate < DateOnly.FromDateTime(DateTime.Now));

        ViewBag.RecentComments =
            _context.TaskComments.Count();

        return View();
    }
}