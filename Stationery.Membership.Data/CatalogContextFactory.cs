using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stationery.Common;
using Stationery.Common.Entities;
using System;
using System.Data.SqlClient;

namespace Stationery.Membership.Data
{
    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <seealso cref="IContextFactory"/>
    public class CatalogContextFactory : IContextFactory
    {
        private readonly IOptions<ConnectionSettings> connectionOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogContextFactory"/> class.
        /// </summary>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="connectionOptions">The connection options.</param>
        /// <param name="dataBaseManager">The data base manager.</param>
        public CatalogContextFactory(IOptions<ConnectionSettings> connectionOptions)
        {
            this.connectionOptions = connectionOptions;
        }

        /// <inheritdoc />
        public IDbContext DbContext => new CatalogDbContext(ChangeDatabaseNameInConnectionString().Options);

        private DbContextOptionsBuilder<CatalogDbContext> ChangeDatabaseNameInConnectionString()
        {
            // 1. Create Connection String Builder using Default connection string
            var sqlConnectionBuilder = new SqlConnectionStringBuilder(this.connectionOptions.Value.CatalogConnection);

            // 2. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }
    }
}
