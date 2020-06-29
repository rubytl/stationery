namespace Stationery.Common.Entities
{
    using Stationery.Common.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// CollectionTemplate
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? CreatedDate
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        [MaxLength(50)]
        public string CreatedBy
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        [MaxLength(50)]
        public string UpdatedBy
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public ICollection<Product> Products { get; set; }

        public int Status { get; set; } = (int)OrderStatus.OPEN;

        [NotMapped]
        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)this.Status;
            }
        }


        #endregion Properties
    }
}