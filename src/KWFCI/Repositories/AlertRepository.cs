using KWFCI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private ApplicationDbContext context;

        public AlertRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public int AddAlert(Alert alert)
        {
            context.Alerts.Add(alert);
            return context.SaveChanges();
        }

        public int DeleteAlert(Alert alert)
        {
            context.Alerts.Remove(alert);
            return context.SaveChanges();
        }

        public int UpdateAlert(Alert alert)
        {
            context.Alerts.Update(alert);
            return context.SaveChanges();
        }

        public IQueryable<Alert> GetAllAlerts()
        {
            return context.Alerts;
        }

        public Alert GetAlertByID(int id)
        {
            return (from a in context.Alerts
                    where a.AlertID == id
                    select a).FirstOrDefault<Alert>();
        }
    }
}
