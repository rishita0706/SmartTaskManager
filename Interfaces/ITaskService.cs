using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskMaster> GetAllTasks();
        IEnumerable<TaskMaster> GetTasksAssignedBy(int managerId);
        IEnumerable<TaskMaster> GetTasksAssignedTo(int employeeId);
        TaskMaster? GetTaskById(int id);
        void CreateTask(TaskMaster task);
        void UpdateTaskStatus(int taskId, int statusId);
    }
}