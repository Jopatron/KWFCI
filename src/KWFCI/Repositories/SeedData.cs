﻿using KWFCI.Models;
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
            string email = "bloblaw@fake.com";
            bool notify = true;
            string role = "Staff";
            string password = "Secret123!";

            //Create Identity User before StaffProfile, so you can add to the profile
            //Populates staff members
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
                email = "lizlem@fake.com";
                notify = true;
                role = "Admin";
                password = "Secret234!";

                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new StaffUser { UserName = email };
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));

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

            //Populates Brokers
            if (!context.Brokers.Any())
            {
                //Not specifying a status should default to active
                Broker broker = new Broker {
                    FirstName = "Lonny",
                    LastName = "Jenkins",
                    Email = "ljenkins@fake.com",
                    EmailNotifications = true,
                    Type = "New Broker" };

                context.Brokers.Add(broker);
                //Not specifying a status shoudl default to active
                broker = new Broker {
                    FirstName = "Samantha",
                    LastName = "Coldwater",
                    Email = "scoldwater@fake.com",
                    EmailNotifications = true,
                    Type = "In Transition" };

                context.Brokers.Add(broker);

                broker = new Broker { FirstName = "Brooke",
                    LastName = "Schelling",
                    Email = "bschelling@fake.com",
                    EmailNotifications = true,
                    Type = "New Broker",
                    Status = "Inactive" };

                context.Brokers.Add(broker);

                broker = new Broker {
                    FirstName = "Jack",
                    LastName = "Johnson",
                    Email = "jjohnson@fake.com",
                    EmailNotifications = false,
                    Type = "In Transition",

                };

                context.Brokers.Add(broker);

                broker = new Broker
                {
                    FirstName = "Janet",
                    LastName = "Schwimmer",
                    Email = "jschwimmer@fake.com",
                    EmailNotifications = true,
                    Type = "In Transition",

                };

                context.Brokers.Add(broker);

                broker = new Broker
                {
                    FirstName = "Wyatt",
                    LastName = "Earp",
                    Email = "wearp@fake.com",
                    EmailNotifications = false,
                    Type = "In Transition",

                };

                context.Brokers.Add(broker);

                broker = new Broker
                {
                    FirstName = "Richard",
                    LastName = "Kimble",
                    Email = "rkimble@fake.com",
                    EmailNotifications = false,
                    Type = "New Broker",

                };

                context.Brokers.Add(broker);

                broker = new Broker
                {
                    FirstName = "Ty",
                    LastName = "Burrell",
                    Email = "tburrell@fake.com",
                    EmailNotifications = true,
                    Type = "New Broker",

                };

                context.Brokers.Add(broker);

                context.SaveChanges();
            }

            //Populates Alerts
            if (!context.Alerts.Any())
            {
                Alert alert = new Alert { Priority = 1, AlertDate = DateTime.Parse("4/12/2017"), Message = "Get Brooke Key Fob" };
                context.Alerts.Add(alert);

                alert = new Alert { Priority = 2, AlertDate = DateTime.Parse("5/21/2017"), Message = "Call Samantha About Paperwork" };
                context.Alerts.Add(alert);

                alert = new Alert { Priority = 3, AlertDate = DateTime.Parse("9/1/2017"), Message = "Verify Lonnie Paperwork Complete" };
                context.Alerts.Add(alert);

                context.SaveChanges();
            }

            //Populate Messages
            if (!context.Messages.Any())
            {
                StaffProfile profile = context.StaffProfiles.First();
                Message message = new Message { Subject = "Staff Meeting", Body = "Don't forget we have a morning meeting tomorrow", From = profile, DateSent = DateTime.Now };
                Message message2 = new Message { Subject = "Broker Meeting", Body = "All Brokers please make sure you meet us at the office tomorrow", From = profile, DateSent = DateTime.Parse("May 4, 2017") };

                context.Messages.Add(message);
                context.Messages.Add(message2);

                context.SaveChanges();
            }

            //Populates Interactions
            if (!context.Interactions.Any())
            {
                Broker lonny = context.Brokers.First();
                StaffProfile profile = context.StaffProfiles.First();
                if (Helper.StaffProfileLoggedIn == null)
                {
                    Helper.StaffProfileLoggedIn = profile;
                }
                Interaction i = new Interaction { Notes = "Interaction Numero Uno", NextStep = "Do the thing" };
                Interaction i1 = new Interaction { Notes = "Interaction: The seconding", NextStep = "Do the other thing" };

                context.Interactions.Add(i);
                context.Interactions.Add(i1);

                profile.Interactions.Add(i);
                profile.Interactions.Add(i1);

                lonny.Interactions.Add(i);
                lonny.Interactions.Add(i1);

                context.SaveChanges();
            }

            //Populates KWTasks
            if (!context.KWTasks.Any())
            {
                Alert alert = context.Alerts.First();
                StaffProfile profile = context.StaffProfiles.First();

                KWTask kwt1 = new KWTask { Description = "A task to be accomplished", Alert = alert, StaffProfile = profile };
                KWTask kwt2 = new KWTask { Description = "Enjoy your day", Alert = alert, StaffProfile = profile };

                context.KWTasks.Add(kwt1);
                context.KWTasks.Add(kwt2);

                context.SaveChanges();
            }
        }
    }
}
