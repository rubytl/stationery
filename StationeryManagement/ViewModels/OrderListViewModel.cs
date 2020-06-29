using Microsoft.AspNetCore.Mvc.Rendering;
using Stationery.Common;
using Stationery.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.UI.ViewModels
{
    /// <summary>
    /// SiteTemplateListViewModel
    /// </summary>
    public class OrderListViewModel : BaseViewModel<OrderRequest>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IEnumerable<Order> Templates
        {
            get; set;
        }

        #endregion Properties
    }

    public class OrderRegisterViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        public int Quantity { get; set; }
    }

    public class OrderRegisterListViewModel
    {
        public ICollection<OrderRegisterViewModel> Orders { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
