using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Stationery.Common.Helpers
{
    /// <summary>
    /// Utilities
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Logs the application error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The e.</param>
        public static void LogAppError(this ILogger logger, Exception e)
        {
            logger.LogError(e, e.Message);
        }

        /// <summary>
        /// Logs the application information.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public static void LogAppInformation(this ILogger logger, string message)
        {
            logger.LogInformation(message);
        }

        /// <summary>
        /// Logs the SQL.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="statement">The statement.</param>
        public static void LogSQL(this ILogger logger, string statement)
        {
            logger.LogInformation(statement);
        }
    }
}
