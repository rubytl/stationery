using System;
using System.Collections.Generic;
using System.Text;

namespace Stationery.Common.Models
{
    public class BaseModel<T> where T : class
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IEnumerable<T> Models
        {
            get; set;
        }
    }
}
