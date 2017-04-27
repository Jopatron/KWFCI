using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IStaffProfileRepository
    {
        IQueryable<StaffProfile> GetAllStaffProfiles(bool getNotifications = false);
        StaffProfile GetStaffProfileByFullName(string firstName, string lastName);
        StaffProfile GetStaffProfileByID(int id);
        int AddStaff(StaffProfile staff);
        int DeleteStaff(StaffProfile staff);
    }
}
