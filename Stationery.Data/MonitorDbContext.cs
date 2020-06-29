namespace Stationery.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Stationery.Common.Entities;
    using Stationery.Data.Mappings;

    /// <summary>
    /// The device DB (entity framework's) context
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Stationery.Common.Entities.AppIdentityUser, Stationery.Common.Entities.AppIdentityRole, System.String, Microsoft.AspNetCore.Identity.IdentityUserClaim{System.String}, Microsoft.AspNetCore.Identity.IdentityUserRole{System.String}, Microsoft.AspNetCore.Identity.IdentityUserLogin{System.String}, Stationery.Common.Entities.AppIdentityRoleClaim, Microsoft.AspNetCore.Identity.IdentityUserToken{System.String}}" />
    /// <seealso cref="Stationery.Common.Entities.IDbContext" />
    public partial class MonitorDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, int,
        AppIdentityUserClaim, AppIdentityUserRole, AppIdentityUserLogin,
        AppIdentityRoleClaim, AppIdentityUserToken>, IDbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public MonitorDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
            : base(options)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or sets the devices data model
        /// </summary>
        /// <value>
        /// The collection template.
        /// </value>
        public virtual DbSet<Product> Product
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public virtual DbSet<Stock> Stock
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>
        /// The notification.
        /// </value>
        public virtual DbSet<Order> Order
        {
            get; set;
        }

        #endregion Properties

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppIdentityRoleClaimMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityRoleMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityUserClaimMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityUserLoginMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityUserMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityUserRoleMapping());
            modelBuilder.ApplyConfiguration(new AppIdentityUserTokenMapping());
        }
    }
}