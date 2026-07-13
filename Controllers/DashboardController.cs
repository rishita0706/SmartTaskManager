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
}