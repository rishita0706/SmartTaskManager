using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<TaskMaster> GetAllTasks();
        IEnumerable<TaskMaster> GetTasksAssignedBy(int managerId);
        IEnumerable<TaskMaster> GetTasksAssignedTo(int employeeId);
        TaskMaster? GetTaskById(int id);
        void AddTask(TaskMaster task);
        void UpdateTask(TaskMaster task);
        void Save();
    }
}