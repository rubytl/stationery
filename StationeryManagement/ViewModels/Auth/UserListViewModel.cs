namespace Stationery.UI.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;

    /// <summary>
    /// UserListViewModel
    /// </summary>
    public class UserListViewModel : BaseViewModel<UserRequest>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IEnumerable<AppIdentityUser> Users
        {
            get; set;
        }

        #endregion Properties
    }
}