using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stationery.Common;

namespace Stationery.API.Configuration
{
    public static class ConfigurationOptions
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));
        }
    }
}
