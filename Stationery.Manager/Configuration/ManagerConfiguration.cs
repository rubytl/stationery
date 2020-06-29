namespace Stationery.Manager.Configuration
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// ManagerConfiguration
    /// </summary>
    public static class ManagerConfiguration
    {
        #region Methods

        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IStockManager, StockManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IAuthManager, AuthManager>();
        }

        #endregion Methods
    }
}