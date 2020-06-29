using System.ComponentModel.DataAnnotations;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// The Device data model
    /// </summary>
    public class Tenant
    {
        #region Properties

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the device title.
        /// </summary>
        /// <value>
        /// The device title.
        /// </value>
        [MaxLength(50)]
        public string TanentName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        [MaxLength(500)]
        public string DatabaseName
        {
            get; set;
        }

        #endregion Properties
    }
}