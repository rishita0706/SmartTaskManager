using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserMaster> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public UserMaster? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void AddUser(UserMaster user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
        }

        public void UpdateUser(UserMaster user)
        {
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
            _userRepository.Save();
        }

        public UserMaster? ValidateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
                return null;

            if (user.PasswordHash != password)
                return null;

            return user;
        }
    }
}