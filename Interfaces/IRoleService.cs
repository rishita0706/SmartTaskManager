using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleMaster> GetAllRoles();

        RoleMaster? GetRoleById(int id);

        void AddRole(RoleMaster role);

        void UpdateRole(RoleMaster role);

        void DeleteRole(int id);
    }
}