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

        public IQueryable<StaffProfile> GetAllStaffProfiles(bool getNotifications = false)
        {
             if (getNotifications == true)
            {
                return (from s in context.StaffProfiles
                        where s.EmailNotifications == true
                        select s).Include(u => u.User).Include(u => u.Interactions).AsQueryable();
            }
             else
                return context.StaffProfiles.Include(u => u.User).Include(u => u.Interactions).AsQueryable();
        }

        public StaffProfile GetStaffProfileByFullName(string firstName, string lastName)
        {
            return (from sp in context.StaffProfiles
                    where sp.FirstName + sp.LastName == firstName + lastName
                    select sp).Include(sp => sp.Interactions).Include(sp => sp.User).FirstOrDefault();
        }

        public int AddStaff(StaffProfile staff)
        {
            context.StaffProfiles.Add(staff);
            return context.SaveChanges();
        }
    }
}
