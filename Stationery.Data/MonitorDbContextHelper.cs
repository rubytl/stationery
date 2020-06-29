namespace Stationery.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// MonitorDbContextHelper
    /// </summary>
    public static class MonitorDbContextHelper
    {
        #region Fields

        /// <summary>
        /// The database field keyword
        /// </summary>
        private const string DatabaseFieldKeyword = "Database";

        /// <summary>
        /// The repositories
        /// </summary>
        private static Dictionary<string, DbContextOptionsBuilder<MonitorDbContext>> contextBuilders = new Dictionary<string, DbContextOptionsBuilder<MonitorDbContext>>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Changes the database name in connection string.
        /// </summary>
        /// <param name="defaultConnection">The default connection.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<MonitorDbContext> ChangeDatabaseNameInConnectionString(string databaseName)
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<MonitorDbContext>();
            if (!string.IsNullOrEmpty(databaseName))
            {
                contextOptionsBuilder.UseSqlServer(databaseName);
            }

            return contextOptionsBuilder;
        }

        /// <summary>
        /// Changes the database name in connection string.
        /// </summary>
        /// <param name="defaultConnection">The default connection.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<MonitorDbContext> ChangeDatabaseNameInConnectionString(string defaultConnection, string databaseName)
        {
            if (contextBuilders == null)
            {
                contextBuilders = new Dictionary<string, DbContextOptionsBuilder<MonitorDbContext>>();
            }

            if (!contextBuilders.ContainsKey(databaseName))
            {
                // Create DbContextOptionsBuilder with new connection string
                var contextOptionsBuilder = new DbContextOptionsBuilder<MonitorDbContext>();
                if (!string.IsNullOrEmpty(databaseName))
                {
                    contextOptionsBuilder.UseSqlServer(databaseName);
                }
                else
                {
                    contextOptionsBuilder.UseSqlServer(defaultConnection);
                }

                // try add value here because there are many threads may execute at the same time
                contextBuilders.TryAdd(databaseName, contextOptionsBuilder);
                return contextOptionsBuilder;
            }
            else
            {
                return contextBuilders[databaseName];
            }
        }

        #endregion Methods
    }
}