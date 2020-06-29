namespace Stationery.Common.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ApplicationUser
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class AppIdentityUser : IdentityUser<int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the is council.
        /// </summary>
        /// <value>
        /// The is council.
        /// </value>
        public bool? IsCouncil { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the last modify.
        /// </summary>
        /// <value>
        /// The last modify.
        /// </value>
        public DateTime? LastModify
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the last modify.
        /// </summary>
        /// <value>
        /// The last modify.
        /// </value>
        public DateTime? LastLogin
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AppIdentityUser" /> is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if locked; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>
        [MaxLength(200)]
        public string ImageSource { get; set; }

        #endregion Properties
    }
}