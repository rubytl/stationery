using Stationery.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// ChangeLog
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [ForeignKey("Product")]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int LedgeQuantity { get; set; }

        [NotMapped]
        public int ProductId { get; set; }

        public int Status { get; set; }

        public virtual Product Product
        {
            get; set;
        }
    }
}