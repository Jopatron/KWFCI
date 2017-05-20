using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KWFCI.Repositories;
using Microsoft.EntityFrameworkCore;
using KWFCI.Models;

namespace KWFCI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                  Configuration["KWFCI_Db:ConnectionString"]));

            // Sets identity password specifications as well as LoginPath
            services.AddIdentity<StaffUser, IdentityRole>(opts =>
                { opts.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireUppercase = true;
                    opts.Password.RequiredLength = 8;
                    opts.User.AllowedUserNameCharacters = null;
                })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            services.AddTransient<IBrokerRepository, BrokerRepository>();
            services.AddTransient<IStaffProfileRepository, StaffProfileRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IInteractionsRepository, InteractionRepository>();
            services.AddTransient<IKWTaskRepository, KWTaskRepository>();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Login"),
                AutomaticChallenge = true
            });
            app.UseMvc(routes =>
             {
                 routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
             });



            SeedData.EnsurePopulated(app);
        }
    }
}
