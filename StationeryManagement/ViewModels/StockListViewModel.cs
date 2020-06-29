namespace Stationery.UI.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// SiteTemplateListViewModel
    /// </summary>
    public class StockListViewModel : BaseViewModel<StockRequest>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IEnumerable<Stock> Templates
        {
            get; set;
        }

        #endregion Properties
    }

    public class StockRegisterViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        public int Quantity { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}