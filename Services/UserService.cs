using SmartTaskManager.Helpers;
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

        public IEnumerable<UserMaster> GetAllUsers() => _userRepository.GetAllUsers();

        public UserMaster? GetUserById(int id) => _userRepository.GetUserById(id);

        public void AddUser(UserMaster user)
        {
            user.PasswordHash = PasswordHelper.Hash(user, user.PasswordHash);
            _userRepository.AddUser(user);
            _userRepository.Save();
        }

        public void UpdateUser(UserMaster user)
        {
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                user.PasswordHash = PasswordHelper.Hash(user, user.PasswordHash);
            }

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
            if (user == null) return null;

            if (PasswordHelper.Verify(user, user.PasswordHash, password))
                return user;

            // Legacy fallback: account created before password hashing was introduced.
            // Auto-migrate it to a proper hash on this successful login.
            if (PasswordHelper.IsLegacyPlainTextMatch(user.PasswordHash, password))
            {
                user.PasswordHash = PasswordHelper.Hash(user, password);
                _userRepository.UpdateUser(user);
                _userRepository.Save();
                return user;
            }

            return null;
        }
    }
}