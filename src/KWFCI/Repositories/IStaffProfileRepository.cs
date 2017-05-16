using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IStaffProfileRepository
    {
        StaffProfile GetProfileByTask(KWTask task);
        IQueryable<StaffProfile> GetAllStaffProfiles(bool getNotifications = false);
        StaffProfile GetStaffProfileByFullName(string firstName, string lastName);
        StaffProfile GetStaffProfileByID(int id);
        int AddStaff(StaffProfile staff);
        int DeleteStaff(StaffProfile staff);
        int UpdateStaff(StaffProfile staff);
    }
}
