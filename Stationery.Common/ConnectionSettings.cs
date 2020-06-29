using System;
using System.Collections.Generic;
using System.Text;

namespace Stationery.Common
{
    /// <summary>
    /// Connection configuration options
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Gets or sets the default connection.
        /// </summary>
        /// <value>
        /// The default connection.
        /// </value>
        public string DefaultConnection { get; set; }

        /// <summary>
        /// Gets or sets the default connection.
        /// </summary>
        /// <value>
        /// The default connection.
        /// </value>
        public string CatalogConnection { get; set; }
    }
}
