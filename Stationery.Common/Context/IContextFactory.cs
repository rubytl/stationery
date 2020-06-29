namespace Stationery.Common.Entities
{
    /// <summary>
    /// IContextFactory
    /// </summary>
    public interface IContextFactory
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        IDbContext DbContext { get; }
    }
}
