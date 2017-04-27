using KWFCI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KWFCI.Repositories
{
    public class KWTaskRepository : IKWTaskRepository
    {
        private ApplicationDbContext context;

        public KWTaskRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public int AddKWTask(KWTask kwtask)
        {
            context.KWTasks.Add(kwtask);
            return context.SaveChanges();
        }

        public int DeleteKWTask(KWTask kwtask)
        {
            context.KWTasks.Remove(kwtask);
            return context.SaveChanges();
        }

        public int UpdateKWTask(KWTask kwtask)
        {
            context.KWTasks.Update(kwtask);
            return context.SaveChanges();
        }

        public IQueryable<KWTask> GetAllTasksByPriority(int priority)
        {
            return (from kwt in context.KWTasks
                    where kwt.Priority == priority
                    select kwt);
        }

        public IQueryable<KWTask> GetAllKWTasks()
        {
            return context.KWTasks.AsQueryable();
        }

        public KWTask GetKWTaskByID(int id)
        {
            return (from kwt in context.KWTasks
                    where kwt.KWTaskID == id
                    select kwt).FirstOrDefault<KWTask>();
        }
    }
}
