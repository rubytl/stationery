using Microsoft.Extensions.DependencyInjection;
using Stationery.Membership.Data.Repositories;

namespace Stationery.Membership.Data.Configuration
{
    /// <summary>
    /// IOC contaner start-up configuration
    /// </summary>
    public static class CatalogConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<ITenantRepository, TenantRepository>();
        }
    }
}
