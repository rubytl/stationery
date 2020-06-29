namespace Stationery.UI.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// PagingHelper
    /// </summary>
    public static class PagingHelper
    {
        #region Methods

        /// <summary>
        /// Gets the page options from configuration.
        /// </summary>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <returns></returns>
        public static List<SelectListItem> GetPageOptionsFromConfiguration(IConfiguration configuration)
        {
            string itemsPerPage = configuration.GetValue<string>("ItemsPerPage");
            string[] names = itemsPerPage.Split(",");
            return names.Select(r => new SelectListItem
            {
                Text = r,
                Value = r
            }).ToList();
        }

        #endregion Methods
    }
}