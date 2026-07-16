using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IEnumerable<TaskMaster> GetAllTasks() => _taskRepository.GetAllTasks();

        public IEnumerable<TaskMaster> GetTasksAssignedBy(int managerId) =>
            _taskRepository.GetTasksAssignedBy(managerId);

        public IEnumerable<TaskMaster> GetTasksAssignedTo(int employeeId) =>
            _taskRepository.GetTasksAssignedTo(employeeId);

        public TaskMaster? GetTaskById(int id) => _taskRepository.GetTaskById(id);

        public void CreateTask(TaskMaster task)
        {
            task.CreatedDate = DateTime.Now;
            _taskRepository.AddTask(task);
            _taskRepository.Save();
        }

        public void UpdateTaskStatus(int taskId, int statusId)
        {
            var task = _taskRepository.GetTaskById(taskId);
            if (task == null) return;

            task.StatusId = statusId;

            if (statusId == Constants.TaskStatusIds.Completed)
                task.CompletedDate = DateOnly.FromDateTime(DateTime.Now);

            _taskRepository.UpdateTask(task);
            _taskRepository.Save();
        }
    }
}