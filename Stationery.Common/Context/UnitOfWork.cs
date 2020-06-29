using Microsoft.AspNetCore.Http;
using Stationery.Common.Helpers;
using System;
using System.Collections.Generic;

namespace Stationery.Common.Entities
{
    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    /// <seealso cref="Stationery.Common.Entities.IUnitOfWork" />
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        public IDbContext DbContext { get; set; }

        /// <summary>
        /// The repositories
        /// </summary>
        private Dictionary<EntityContext, object> repositories;
        /// <summary>
        /// The HTTP context
        /// </summary>
        private readonly HttpContext httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        public UnitOfWork(IContextFactory contextFactory, IHttpContextAccessor httpContentAccessor)
        {
            this.DbContext = contextFactory.DbContext;
            this.httpContext = httpContentAccessor.HttpContext;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <returns></returns>
        public IEntityBaseRepository<TEntity> GetRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<EntityContext, object>();
            }

            EntityContext entityContext = new EntityContext() { EnityType = typeof(TEntity), RepositoryType = typeof(TRepository), DatabaseName = DbContextHelper.GetDatabaseName(this.httpContext) };
            if (!this.repositories.ContainsKey(entityContext))
            {
                object[] args = new object[] { this.DbContext };
                this.repositories[entityContext] = Activator.CreateInstance(typeof(TRepository), args);
            }

            return (IEntityBaseRepository<TEntity>)this.repositories[entityContext];
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// Repository
        /// </returns>
        public IEntityBaseRepository<TEntity> GetRepository<TEntity>()
          where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<EntityContext, object>();
            }

            EntityContext entityContext = new EntityContext() { EnityType = typeof(TEntity), RepositoryType = typeof(EntityBaseRepository<TEntity>), DatabaseName = DbContextHelper.GetDatabaseName(this.httpContext) };
            if (!this.repositories.ContainsKey(entityContext))
            {
                this.repositories[entityContext] = new EntityBaseRepository<TEntity>(this.DbContext);
            }

            return (IEntityBaseRepository<TEntity>)this.repositories[entityContext];
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>
        /// The number of objects in an Added, Modified, or Deleted state
        /// </returns>
        public int Commit()
        {
            // Save changes with the default options
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(obj: this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DbContext != null)
                {
                    this.DbContext.Dispose();
                    this.DbContext = null;
                }
            }
        }

    }
}
