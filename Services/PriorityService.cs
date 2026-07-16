using SmartTaskManager.Interfaces;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityService(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public IEnumerable<PriorityMaster> GetAllPriorities() =>
            _priorityRepository.GetAllPriorities();

        public PriorityMaster? GetPriorityById(int id) =>
            _priorityRepository.GetPriorityById(id);
    }
}