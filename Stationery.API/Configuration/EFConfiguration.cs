using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stationery.Common.Entities;
using Stationery.Data;
using Stationery.Membership.Data;
using System;

namespace Stationery.API.Configuration
{
    /// <summary>
    /// EFConfiguration
    /// </summary>
    public static class EFConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            //Add monitor db context
            services.AddDbContext<MonitorDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppIdentityUser, AppIdentityRole>().AddEntityFrameworkStores<MonitorDbContext>().AddDefaultTokenProviders();

            // Add catalog db context
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CatalogConnection")));

            // config identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
        }
    }
}
