using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<DepartmentMaster> GetAllDepartments();
        DepartmentMaster? GetDepartmentById(int id);
        void AddDepartment(DepartmentMaster department);
        void UpdateDepartment(DepartmentMaster department);
        void DeleteDepartment(int id);
        void Save();
    }
}