using System;
using System.Collections.Generic;
using System.Text;

namespace Stationery.Common.Models
{
    /// <summary>
    /// BaseRequest
    /// </summary>
    public class BaseRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the site ids.
        /// </summary>
        /// <value>
        /// The site ids.
        /// </value>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the site ids.
        /// </summary>
        /// <value>
        /// The site ids.
        /// </value>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public PagingRequest Page
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// PagingRequest
    /// </summary>
    public class PagingRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// UserRequest
    /// </summary>
    /// <seealso cref="Redzone.Common.Models.BaseRequest" />
    public class RoleRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool? IsAdmin
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// UserRequest
    /// </summary>
    /// <seealso cref="Redzone.Common.Models.BaseRequest" />
    public class UserRequest: BaseRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool IsActive
        {
            get; set;
        } = true;

        #endregion Properties
    }

    /// <summary>
    /// UserRequest
    /// </summary>
    /// <seealso cref="Redzone.Common.Models.BaseRequest" />
    public class StockRequest : BaseRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public decimal Price
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public long Quantity
        {
            get; set;
        }

        #endregion Properties
    }
}
