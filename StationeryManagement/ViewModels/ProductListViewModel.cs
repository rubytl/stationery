namespace Stationery.UI.ViewModels
{
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;

    /// <summary>
    /// SiteTemplateListViewModel
    /// </summary>
    public class ProductListViewModel : BaseViewModel<BaseRequest>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IEnumerable<Product> Templates
        {
            get; set;
        }

        #endregion Properties
    }
}