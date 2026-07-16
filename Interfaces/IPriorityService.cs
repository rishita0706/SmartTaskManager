using SmartTaskManager.Models;

namespace SmartTaskManager.Interfaces
{
    public interface IPriorityService
    {
        IEnumerable<PriorityMaster> GetAllPriorities();
        PriorityMaster? GetPriorityById(int id);
    }
}