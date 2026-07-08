using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SmartTaskManagerDbContext _context;

        public RoleRepository(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoleMaster> GetAllRoles()
        {
            return _context.RoleMasters.ToList();
        }

        public RoleMaster? GetRoleById(int id)
        {
            return _context.RoleMasters.FirstOrDefault(r => r.RoleId == id);
        }

        public void AddRole(RoleMaster role)
        {
            _context.RoleMasters.Add(role);
        }

        public void UpdateRole(RoleMaster role)
        {
            _context.RoleMasters.Update(role);
        }

        public void DeleteRole(int id)
        {
            var role = _context.RoleMasters.Find(id);

            if (role != null)
            {
                _context.RoleMasters.Remove(role);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}