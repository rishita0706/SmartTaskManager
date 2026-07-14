using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IUserService
    {
        UserMaster? ValidateUser(string email, string password);

        IEnumerable<UserMaster> GetAllUsers();

        UserMaster? GetUserById(int id);

        void AddUser(UserMaster user);

        void UpdateUser(UserMaster user);

        void DeleteUser(int id);
    }
}
