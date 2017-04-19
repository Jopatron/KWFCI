using KWFCI.Models;
using System.Linq;

namespace KWFCI.Repositories
{
    public interface IStaffProfileRepository
    {
        IQueryable<StaffProfile> GetAllStaffProfiles();
        StaffProfile GetStaffProfileByFullName(string firstName, string lastName);
    }
}
