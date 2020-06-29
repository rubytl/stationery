namespace Stationery.Common.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ClaimPermission
    /// </summary>
    public class Product
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the logical value.
        /// </summary>
        /// <value>
        /// The logical value.
        /// </value>
        [MaxLength(100)]
        public string Name
        {
            get; set;
        }


        public virtual Stock Stock { get; set; }

        public int? OrderId
        {
            get; set;
        }

        public Order Order { get; set; }

        #endregion Properties
    }
}