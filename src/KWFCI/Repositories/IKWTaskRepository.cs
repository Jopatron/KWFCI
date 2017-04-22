using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IKWTaskRepository
    {
        IQueryable<KWTask> GetAllKWTasks();
        int AddKWTask(KWTask kwtask);
        int DeleteKWTask(KWTask kwtask);
        int UpdateKWTask(KWTask kwtask);
        KWTask GetKWTaskByID(int id);
    }
}
