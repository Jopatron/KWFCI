using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;

namespace KWFCI.Repositories
{
    public class StaffProfileRepository : IStaffProfileRepository
    {
        private ApplicationDbContext context;

        public StaffProfileRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<StaffProfile> GetAllStaffProfiles()
        {
            throw new NotImplementedException();
        }

        public StaffProfile GetStaffProfileByFullName(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
