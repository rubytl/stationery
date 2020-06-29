namespace Stationery.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// AppIdentityRoleClaim
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRoleClaim{System.String}" />
    public class AppIdentityRoleClaim : IdentityRoleClaim<int>
    {
        #region Fields

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the create mode.
        /// </summary>
        /// <value>
        /// The create mode.
        /// </value>
        public bool CanCreate
        {
            get; set;
        } = false;

        /// <summary>
        /// Gets or sets the delete mode.
        /// </summary>
        /// <value>
        /// The delete mode.
        /// </value>
        public bool CanDelete
        {
            get; set;
        } = false;

        /// <summary>
        /// Gets or sets the update mode.
        /// </summary>
        /// <value>
        /// The update mode.
        /// </value>
        public bool CanUpdate
        {
            get; set;
        } = false;

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>
        /// The view mode.
        /// </value>
        public bool CanView
        {
            get; set;
        } = false;

        //
        // Summary:
        //     Gets or sets the claim value for this claim.
        /// <summary>
        /// Gets or sets the claim parameter.
        /// </summary>
        /// <value>
        /// The claim parameter.
        /// </value>
        [MaxLength(100)]
        public string ClaimParameter
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>
        /// The page identifier.
        /// </value>
        public int PageId
        {
            get; set;
        }

        #endregion Properties
    }
}