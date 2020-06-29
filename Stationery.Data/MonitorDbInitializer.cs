
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stationery.Common.Entities;
using Stationery.Common.Helpers;
using System;

namespace Stationery.Data
{
    /// <summary>
    /// MonitorDbInitializer
    /// </summary>
    public static class MonitorDbInitializer
    {
        /// <summary>
        /// Seeds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public static void Seed(IApplicationBuilder app, RoleManager<AppIdentityRole> roleManager,
            UserManager<AppIdentityUser> userManager, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    MonitorDbContext context = serviceScope.ServiceProvider.GetService<MonitorDbContext>();
                    SeedRoles(roleManager);
                    SeedUser(userManager);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ILogger logger = loggerFactory.CreateLogger("MonitorDbInitializer");
                logger.LogAppError(ex);
            }
        }

        /// <summary>
        /// Seeds the roles.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        public static void SeedRoles(RoleManager<AppIdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                AppIdentityRole role = new AppIdentityRole();
                role.Name = "Admin";
                role.IsAdmin = true;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                AppIdentityRole role = new AppIdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        /// <summary>
        /// Seeds the user.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public static void SeedUser(UserManager<AppIdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                AppIdentityUser user = new AppIdentityUser();
                user.UserName = "admin";
                user.CreatedDate = DateTime.Today;
                user.LastModify = DateTime.Today;
                user.PhoneNumber = "99918877";
                user.Email = "pham.ngoc@outlook.com";
                user.ImageSource = "img/avatars/admin.jpg";

                IdentityResult result = userManager.CreateAsync(user, "Admin123.").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
