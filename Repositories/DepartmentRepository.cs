using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SmartTaskManagerDbContext _context;

        public DepartmentRepository(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DepartmentMaster> GetAllDepartments() =>
            _context.DepartmentMasters.ToList();

        public DepartmentMaster? GetDepartmentById(int id) =>
            _context.DepartmentMasters.FirstOrDefault(d => d.DepartmentId == id);

        public void AddDepartment(DepartmentMaster department)
        {
            _context.DepartmentMasters.Add(department);
        }

        public void UpdateDepartment(DepartmentMaster department)
        {
            _context.DepartmentMasters.Update(department);
        }

        public void DeleteDepartment(int id)
        {
            var dept = _context.DepartmentMasters.Find(id);
            if (dept != null)
            {
                dept.IsActive = false; // soft delete
                _context.DepartmentMasters.Update(dept);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}