using KWFCI.Models;
using KWFCI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI
{
    public static class Helper
    {
        public static string currentRole = "";
        public static StaffProfile StaffProfileLoggedIn { get; set; }
        public static StaffUser StaffUserLoggedIn { get; set; }
        public static string CurrentRole { get; set; }

        public static StaffProfile DetermineProfile(IStaffProfileRepository adbc)
        {
            StaffProfile stafProf = new Models.StaffProfile();

            var profList = adbc.GetAllStaffProfiles();

            stafProf = (from sp in profList
                       where sp.User.Id == StaffUserLoggedIn.Id
                       select sp).FirstOrDefault<StaffProfile>();

            return stafProf;
        }

        
    }
}
