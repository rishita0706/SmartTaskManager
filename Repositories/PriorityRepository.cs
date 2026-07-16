using SmartTaskManager.Data;
using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly SmartTaskManagerDbContext _context;

        public PriorityRepository(SmartTaskManagerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PriorityMaster> GetAllPriorities() =>
            _context.PriorityMasters.ToList();

        public PriorityMaster? GetPriorityById(int id) =>
            _context.PriorityMasters.FirstOrDefault(p => p.PriorityId == id);
    }
}