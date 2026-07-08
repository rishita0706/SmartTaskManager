using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IUserService
    {
        UserMaster? ValidateUser(string email, string password);
    }
}
