namespace Stationery.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using Stationery.Common;
    using Stationery.Common.Entities;
    using Stationery.Common.Helpers;
    using Stationery.Data.Configuration;

    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <seealso cref="Stationery.Common.Entities.IContextFactory" />
    /// <seealso cref="IContextFactory" />
    public class MonitorContextFactory : IContextFactory
    {
        #region Fields

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        /// <inheritdoc />
        public IDbContext DbContext => new MonitorDbContext(MonitorDbContextHelper.ChangeDatabaseNameInConnectionString(this.connectionOptions.Value.DefaultConnection, DbContextHelper.GetDatabaseName(this.httpContext)).Options);

        /// <summary>
        /// The connection options
        /// </summary>
        private readonly IOptions<ConnectionSettings> connectionOptions;

        /// <summary>
        /// The HTTP context
        /// </summary>
        private readonly HttpContext httpContext;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorContextFactory" /> class.
        /// </summary>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="connectionOptions">The connection options.</param>
        public MonitorContextFactory(IHttpContextAccessor httpContentAccessor, IOptions<ConnectionSettings> connectionOptions)
        {
            this.httpContext = httpContentAccessor.HttpContext;
            this.connectionOptions = connectionOptions;
        }

        #endregion Constructors
    }
}