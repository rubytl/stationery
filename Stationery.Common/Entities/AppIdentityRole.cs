using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// AppIdentityRole
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRole" />
    public class AppIdentityRole : IdentityRole<int>
    {
        /// <summary>
        /// Gets or sets the is admin.
        /// </summary>
        /// <value>
        /// The is admin.
        /// </value>
        public bool IsAdmin { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityRole"/> class.
        /// </summary>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public AppIdentityRole()
           : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityRole"/> class.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public AppIdentityRole(string roleName)
            :base(roleName)
        { }
    }
}
