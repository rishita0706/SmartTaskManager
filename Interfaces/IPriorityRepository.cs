using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IPriorityRepository
    {
        IEnumerable<PriorityMaster> GetAllPriorities();
        PriorityMaster? GetPriorityById(int id);
    }
}