using KWFCI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Repositories
{
    public class SeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            UserManager<StaffUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<StaffUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();

            string firstName = "Bob";
            string lastName = "Loblaw";
            string email = "bloblaw@gmail.com";
            bool notify = true;
            string role = "Staff";
            string password = "secret123";

            //Create Identity User before StaffProfile, so you can add to the profile
            if (!context.StaffProfiles.Any())
            {
                StaffUser user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new StaffUser {Email = email };
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Staff"));
                        //await roleManager.CreateAsync(new IdentityRole("Musician"));
                        //await roleManager.CreateAsync(new IdentityRole("Leader"));

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }

                }

                /* StaffProfile Includes:
                * FirstName
                * LastName
                * Email
                * EmailNotifications
                * User
                * Role */

                StaffProfile profile = new StaffProfile
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    EmailNotifications = notify,
                    User = user,
                    Role = role
                };

                context.StaffProfiles.Add(profile);

                context.SaveChanges();
            }
        }
    }
}
