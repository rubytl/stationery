using System;
using System.Collections.Generic;
using System.Text;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// Contains functions to manipulate EF transactions
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbContext DbContext { get; set; }

        IEntityBaseRepository<TEntity> GetRepository<TEntity, TRepository>() where TEntity : class where TRepository : class;

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Repository</returns>
        IEntityBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }
}
