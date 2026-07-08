using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleMaster> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public RoleMaster? GetRoleById(int id)
        {
            return _roleRepository.GetRoleById(id);
        }

        public void AddRole(RoleMaster role)
        {
            _roleRepository.AddRole(role);
            _roleRepository.Save();
        }

        public void UpdateRole(RoleMaster role)
        {
            _roleRepository.UpdateRole(role);
            _roleRepository.Save();
        }

        public void DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);
            _roleRepository.Save();
        }
    }
}