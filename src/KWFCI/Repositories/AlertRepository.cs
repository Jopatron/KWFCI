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

        public IQueryable<Alert> GetAllAlerts()
        {
            return context.Alerts;
        }
    }
}
