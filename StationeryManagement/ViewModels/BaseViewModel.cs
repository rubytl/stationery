using Stationery.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.UI.ViewModels
{
    public class BaseViewModel<T>
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public PageViewModel Page
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public T Request
        {
            get; set;
        }
    }
}
