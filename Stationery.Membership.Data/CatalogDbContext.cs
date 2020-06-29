using Microsoft.EntityFrameworkCore;
using Stationery.Common.Entities;
using Stationery.Common.Models;

namespace Stationery.Membership.Data
{
    /// <summary>
    /// CatalogDbContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="Stationery.Common.Entities.IDbContext" />
    public class CatalogDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Get or sets the devices data model
        /// </summary>
        /// <value>
        /// The tenant.
        /// </value>
        public DbSet<Tenant> Tenant { get; set; }
    }
}
