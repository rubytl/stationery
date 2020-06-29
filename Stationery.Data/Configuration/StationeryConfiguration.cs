using Microsoft.Extensions.DependencyInjection;
using Stationery.Common.Entities;

namespace Stationery.Data.Configuration
{
    /// <summary>
    /// IOC contaner start-up configuration
    /// </summary>
    public static class StationeryConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
            services.AddScoped<IContextFactory, MonitorContextFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
