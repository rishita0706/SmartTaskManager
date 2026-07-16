using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Constants;
using SmartTaskManager.Data;
using SmartTaskManager.Filters;

[SessionAuthorize]
public class DashboardController : Controller
{
    private readonly SmartTaskManagerDbContext _context;

    public DashboardController(SmartTaskManagerDbContext context)
    {
        _context = context;
    }

    [SessionAuthorize(RoleNames.Admin)]
    public IActionResult AdminDashboard()
    {
        ViewBag.TotalUsers = _context.UserMasters.Count();
        ViewBag.TotalTasks = _context.TaskMasters.Count();
        ViewBag.TotalDepartments = _context.DepartmentMasters.Count();
        ViewBag.ActiveEmployees = _context.UserMasters.Count(u => u.IsActive);
        return View();
    }

    [SessionAuthorize(RoleNames.Manager)]
    public IActionResult ManagerDashboard()
    {
        var managerId = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;

        ViewBag.TasksAssignedByMe = _context.TaskMasters.Count(t => t.AssignedBy == managerId);
        ViewBag.PendingTasks = _context.TaskMasters.Count(t => t.AssignedBy == managerId && t.StatusId == 1);
        ViewBag.CompletedTasks = _context.TaskMasters.Count(t => t.AssignedBy == managerId && t.StatusId == 3);
        ViewBag.MyTeamMembers = _context.UserMasters.Count(u => u.ManagerId == managerId);

        return View();
    }

    [SessionAuthorize(RoleNames.Employee)]
    public IActionResult EmployeeDashboard()
    {
        var employeeId = HttpContext.Session.GetInt32(SessionKeys.UserId)!.Value;

        ViewBag.MyTasks = _context.TaskMasters.Count(t => t.AssignedTo == employeeId);
        ViewBag.CompletedTasks = _context.TaskMasters.Count(t => t.AssignedTo == employeeId && t.StatusId == 3);
        ViewBag.DueTasks = _context.TaskMasters.Count(
            t => t.AssignedTo == employeeId && t.DueDate < DateOnly.FromDateTime(DateTime.Now));
        ViewBag.RecentComments = _context.TaskComments.Count(c =>
            c.Task != null && c.Task.AssignedTo == employeeId);

        return View();
    }
}