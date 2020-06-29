using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stationery.Common.Entities
{
    public interface IEntityBaseRepository<T>
    {
        #region Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Alls the including.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Alls the including.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IQueryable<T> AllIncluding(string[] includeProperties);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Counts the where.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the where.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        void DeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string[] includeProperties);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        Task<bool> HasAnyAsync(Expression<Func<T, bool>> predicate);

        #endregion Methods
    }
}
