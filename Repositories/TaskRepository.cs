using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SmartTaskManagerDbContext _context;

        public TaskRepository(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        private IQueryable<TaskMaster> WithIncludes() =>
            _context.TaskMasters
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Department)
                .Include(t => t.AssignedEmployee)
                .Include(t => t.AssignedManager);

        public IEnumerable<TaskMaster> GetAllTasks() => WithIncludes().ToList();

        public IEnumerable<TaskMaster> GetTasksAssignedBy(int managerId) =>
            WithIncludes().Where(t => t.AssignedBy == managerId).ToList();

        public IEnumerable<TaskMaster> GetTasksAssignedTo(int employeeId) =>
            WithIncludes().Where(t => t.AssignedTo == employeeId).ToList();

        public TaskMaster? GetTaskById(int id) =>
            WithIncludes().FirstOrDefault(t => t.TaskId == id);

        public void AddTask(TaskMaster task)
        {
            _context.TaskMasters.Add(task);
        }

        public void UpdateTask(TaskMaster task)
        {
            _context.TaskMasters.Update(task);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}