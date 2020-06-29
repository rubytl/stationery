using System;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// EntityContext
    /// </summary>
    public class EntityContext
    {
        /// <summary>
        /// Gets or sets the type of the enity.
        /// </summary>
        /// <value>
        /// The type of the enity.
        /// </value>
        public Type EnityType { get; set; }

        /// <summary>
        /// Gets or sets the type of the repository.
        /// </summary>
        /// <value>
        /// The type of the repository.
        /// </value>
        public Type RepositoryType { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Equalses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool Equals(EntityContext value)
        {
            return this.EnityType == value.EnityType && this.DatabaseName == value.DatabaseName && this.RepositoryType == value.RepositoryType;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.EnityType.GetHashCode() ^ this.DatabaseName.GetHashCode() ^ this.RepositoryType.GetHashCode();
        }
    }
}
