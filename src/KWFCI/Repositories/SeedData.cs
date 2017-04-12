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
            string password = "Secret123!";

            //Create Identity User before StaffProfile, so you can add to the profile
            if (!context.StaffProfiles.Any())
            {
                StaffUser user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new StaffUser {UserName = email };
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Staff"));

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }

                }

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

                // Second identity + person, to test Authorization
                firstName = "Liz";
                lastName = "Lemon";
                email = "lizlem@gmail.com";
                notify = true;
                role = "Visitor";
                password = "Secret234!";

                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new StaffUser { UserName = email };
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Visitor"));

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }

                }

                profile = new StaffProfile
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

            if (!context.Brokers.Any())
            {
                Broker broker = new Broker { FirstName = "Lonny", LastName = "Jenkins", Email = "ljenkins@kw.com", EmailNotifications = true, Type = "New Broker" };

                context.Brokers.Add(broker);

                broker = new Broker { FirstName = "Samantha", LastName = "Coldwater", Email = "scoldwater@kw.com", EmailNotifications = true, Type = "In Transition" };

                context.Brokers.Add(broker);

                broker = new Broker { FirstName = "Brooke", LastName = "Schelling", Email = "bschelling@kw.com", EmailNotifications = true, Type = "New Broker" };

                context.Brokers.Add(broker);

                context.SaveChanges();
            }
        }
    }
}
