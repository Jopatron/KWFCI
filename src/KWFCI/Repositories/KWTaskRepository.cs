using KWFCI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Collections.Generic;

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
        public Interaction GetAssociatedInteraction(KWTask task)
        {
            foreach(Interaction i in context.Interactions)
            {
                if(i.TaskForeignKey == task.KWTaskID)
                    return i;
            }
            return null;
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
        public IQueryable<KWTask> GetAllTasksByType(string type)
        {
            return (from kwt in context.KWTasks
                    where kwt.Type == type
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

        public List<KWTask> GetTasksFromSQL(string sql)
        {
            return context.KWTasks.FromSql(sql).ToList();
        }
    }
}
