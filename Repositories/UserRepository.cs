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

        private IQueryable<UserMaster> WithIncludes() =>
            _context.UserMasters
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Include(u => u.Manager);

        public IEnumerable<UserMaster> GetAllUsers() => WithIncludes().ToList();

        public UserMaster? GetUserById(int id) =>
            WithIncludes().FirstOrDefault(u => u.UserId == id);

        public void AddUser(UserMaster user)
        {
            _context.UserMasters.Add(user);
        }

        public void UpdateUser(UserMaster user)
        {
            _context.UserMasters.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _context.UserMasters.Find(id);

            if (user != null)
            {
                user.IsActive = false;
                _context.UserMasters.Update(user);
            }
        }

        public UserMaster? GetUserByEmail(string email)
        {
            return _context.UserMasters
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}