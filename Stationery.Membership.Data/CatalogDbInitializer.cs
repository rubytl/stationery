namespace Stationery.Membership.Data
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Stationery.Common.Entities;
    using System.Linq;

    /// <summary>
    /// CatalogDbInitializer
    /// </summary>
    public static class CatalogDbInitializer
    {
        #region Methods

        /// <summary>
        /// Seeds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="userManager">The user manager.</param>
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                CatalogDbContext context = serviceScope.ServiceProvider.GetService<CatalogDbContext>();
                SeedTenant(context);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Seeds the tenant.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void SeedTenant(CatalogDbContext context)
        {
            if (!context.Tenant.Any(s => s.TanentName == "demo"))
            {
                Tenant tenant = new Tenant() { TanentName = "demo", DatabaseName = "SOFTWARE3\\SQLEXPRESS;Database=Stationery-demo;Trusted_Connection=True;MultipleActiveResultSets=true" };
                context.Tenant.Add(tenant);
            }
        }

        #endregion Methods
    }
}