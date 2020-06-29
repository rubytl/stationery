namespace Stationery.UI.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// LoginViewModel
    /// </summary>
    public class LoginViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string ReturnUrl
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the tenant.
        /// </summary>
        /// <value>
        /// The name of the tenant.
        /// </value>
        [Required]
        [Display(Name = "Tenant name")]
        public string TenantName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "User name")]
        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }
}