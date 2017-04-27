using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IStaffProfileRepository
    {
        IQueryable<StaffProfile> GetAllStaffProfiles(bool getNotifications = false);
        StaffProfile GetStaffProfileByFullName(string firstName, string lastName);
        int AddStaff(StaffProfile staff);
    }
}
