using Microsoft.EntityFrameworkCore;
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
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email);
        }
    }
}