using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartTaskManagerDbContext _context;

        public UserRepository(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        public UserMaster? GetUserByEmail(string email)
        {
            return _context.UserMasters
                .FirstOrDefault(u => u.Email == email);
        }
    }
}