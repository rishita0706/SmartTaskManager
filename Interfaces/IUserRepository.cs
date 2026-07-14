using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IUserRepository
    {
        UserMaster? GetUserByEmail(string email);

        IEnumerable<UserMaster> GetAllUsers();

        UserMaster? GetUserById(int id);

        void AddUser(UserMaster user);

        void UpdateUser(UserMaster user);

        void DeleteUser(int id);

        void Save();
    }
}
