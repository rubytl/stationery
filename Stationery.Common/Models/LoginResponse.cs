using Stationery.Common.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace Stationery.Common.Models
{
    /// <summary>
    /// LoginResponse
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DBName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>
        public string ImageSource { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public IEnumerable<Claim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the check login result.
        /// </summary>
        /// <value>
        /// The check login result.
        /// </value>
        public LoginResult LoginResult { get; set; }
    }
}
