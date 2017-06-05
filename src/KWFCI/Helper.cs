using KWFCI.Models;
using KWFCI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public static string Today { get; set; }

        public static void GetToday()
        {
            DateTime todayDate = DateTime.Now;
            string[] todaySplit = todayDate.ToString().Split(' ');
            Today = todaySplit[0];
        }

        public static StaffProfile DetermineProfile(IStaffProfileRepository adbc)
        {
            StaffProfile stafProf = new Models.StaffProfile();

            var profList = adbc.GetAllStaffProfiles();

            stafProf = (from sp in profList
                       where sp.User.Id == StaffUserLoggedIn.Id
                       select sp).Include(s => s.Tasks).Include(s => s.Interactions).FirstOrDefault<StaffProfile>();

            return stafProf;
        }

    }
}
