using System.Security.Claims;

namespace Stationery.Common.Models
{
    /// <summary>
    /// RedzoneClaim
    /// </summary>
    /// <seealso cref="System.Security.Claims.Claim" />
    public class StationeryClaim : Claim
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StationeryClaim"/> class.
        /// </summary>
        /// <param name="type">The claim type.</param>
        /// <param name="value">The claim value.</param>
        public StationeryClaim(string type, string value)
            : base(type, value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StationeryClaim"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="claimParameter">The claim parameter.</param>
        public StationeryClaim(string type, string value, bool? isAdmin)
            : base(type, value)
        {
            this.IsAdmin = isAdmin;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool? IsAdmin { get; set; }
    }
}
