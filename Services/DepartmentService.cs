using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentMaster> GetAllDepartments() =>
            _departmentRepository.GetAllDepartments();

        public DepartmentMaster? GetDepartmentById(int id) =>
            _departmentRepository.GetDepartmentById(id);

        public void AddDepartment(DepartmentMaster department)
        {
            _departmentRepository.AddDepartment(department);
            _departmentRepository.Save();
        }

        public void UpdateDepartment(DepartmentMaster department)
        {
            _departmentRepository.UpdateDepartment(department);
            _departmentRepository.Save();
        }

        public void DeleteDepartment(int id)
        {
            _departmentRepository.DeleteDepartment(id);
            _departmentRepository.Save();
        }
    }
}