namespace Stationery.UI.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    /// <summary>
    /// PageViewModel
    /// </summary>
    public class PageViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string Parameters { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int PageSize
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the next page.
        /// </summary>
        /// <value>
        /// The next page.
        /// </value>
        public int NextPage
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int PageCount
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the previous page.
        /// </summary>
        /// <value>
        /// The previous page.
        /// </value>
        public int PreviousPage
        {
            get; set;
        }


        /// <summary>
        /// Gets or sets the page options.
        /// </summary>
        /// <value>
        /// The page options.
        /// </value>
        public List<SelectListItem> PageOptions { get; set; }
        #endregion Properties
    }
}