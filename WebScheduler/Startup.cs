using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebScheduler.Data;
using WebScheduler.Models;
using WebScheduler.Services;
using Microsoft.AspNetCore.Identity;

namespace WebScheduler
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            CreateRoles(serviceProvider).Wait();
        }
        //Create user roles
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Staff" };
            IdentityResult roleResult;

            //check to see if roles exist
            foreach (string roleName in roleNames)
            {
                bool roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    //create roles and add to database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Create object with values for Admin account to be added
            ApplicationUser admin = new ApplicationUser
            {
                UserName = Configuration["AppSettings:Username"],
                Email = Configuration["AppSettings:UserEmail"],
                Name = "Admin",
                IsActive = true
            };

            //Password for admin account to be used if not already created
            string userPassword = Configuration["AppSettings:Password"];
            //If a user exists with declared AdminEmail in appsettings.json, store as user
            ApplicationUser user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminEmail"]);

            //if the Admin user doesn't exists, create it with the values declared above
            if (user == null)
            {
                //get the result of creating the Admin user
                IdentityResult createAdmin = await UserManager.CreateAsync(admin, userPassword);
                //if creation succeeded, assign Admin role
                if (createAdmin.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                    
                }
            }
        }
    }

   
}
