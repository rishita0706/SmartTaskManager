using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IUserRepository
    {
        UserMaster? GetUserByEmail(string email);
    }
}
