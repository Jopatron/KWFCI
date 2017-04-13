using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IAlertRepository
    {
        IQueryable<Alert> GetAllAlerts();
        //int return value represents whether or not operation completed: 1 for True, 0 for False
        int AddAlert(Alert alert);
    }
}
