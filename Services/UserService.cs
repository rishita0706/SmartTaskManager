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