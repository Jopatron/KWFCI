using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    interface IStaffProfileRepository
    {
        IQueryable<StaffProfile> GetAllStaffProfiles();
        StaffProfile GetStaffProfileByFullName(string firstName, string lastName);
    }
}
