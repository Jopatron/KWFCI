using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using Microsoft.EntityFrameworkCore;

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
            return context.StaffProfiles.Include(u => u.User).AsQueryable();
        }

        public StaffProfile GetStaffProfileByFullName(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
