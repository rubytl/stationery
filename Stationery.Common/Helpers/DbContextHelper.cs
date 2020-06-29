namespace Stationery.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Stationery.Common.Entities;
    using Stationery.Common.Helpers;

    /// <summary>
    /// DbContextHelper
    /// </summary>
    public static class DbContextHelper
    {
        #region Methods

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        public static string GetDatabaseName(HttpContext httpContext)
        {
            ValidateHttpContext(httpContext);
            string dbName = httpContext.Request.Headers[Constants.DatabaseFieldName].ToString();
            ValidateDbName(dbName);
            return dbName;
        }

        /// <summary>
        /// Validates the name of the database.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <exception cref="ArgumentNullException">dbName</exception>
        private static void ValidateDbName(string dbName)
        {
            if (dbName == null)
            {
                throw new ArgumentNullException(nameof(dbName));
            }
        }

        /// <summary>
        /// Validates the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <exception cref="ArgumentNullException">httpContext</exception>
        private static void ValidateHttpContext(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
        }

        #endregion Methods
    }
}