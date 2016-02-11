namespace AUSKF.Domain.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Domain.Interfaces;

    public interface ICachingRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        /// <summary>
        /// Generic method to get a collection of Entities
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <returns>
        /// A Enumerable of Entities
        /// </returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int? take = null, string includeProperties = "");

        /// <summary>
        /// Get count of Entities
        /// </summary>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        /// Updates the specified updated.
        /// </summary>
        /// <param name="updated">The updated.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        TEntity Update(TEntity updated, TKey key);

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <exception cref="System.NotImplementedException">Use the Delete(id) method when using the caching repository.</exception>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        void Delete(TKey id);

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        IDataContext Context { get; set; }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        /// Generic method to get a collection of Entities
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <returns>A Enumerable of Entities</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Generic Method to get an Entity by Identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TKey id, string includeProperties = null);

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        /// <summary>
        /// Generic method for add an Entity to the context
        /// </summary>
        /// <param name="entity">The Entity to Add</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///             A database command did not affect the expected number of rows. This usually indicates an optimistic 
        ///             concurrency violation; that is, a row has been changed in the database since it was queried.
        ///             </exception>
        /// <exception cref="DbEntityValidationException">
        ///             The save was aborted because validation of entity property values failed.
        ///             </exception>
        Task DeleteAsync(TEntity entityToDelete);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="updated">The updated.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///             A database command did not affect the expected number of rows. This usually indicates an optimistic 
        ///             concurrency violation; that is, a row has been changed in the database since it was queried.
        ///             </exception>
        /// <exception cref="DbEntityValidationException">
        ///             The save was aborted because validation of entity property values failed.
        ///             </exception>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        Task<TEntity> UpdateAsync(TEntity updated, TKey key);

        /// <summary>
        /// Generic implementation for get Paged Entities
        /// </summary>
        /// <typeparam name="TKey">Key for order Expression</typeparam>
        /// <param name="pageIndex">Index of the Page</param>
        /// <param name="pageCount">Number of Entities to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="orderby">if set to <c>true</c> [orderby].</param>
        /// <returns>
        /// Enumerable of Entities matching the conditions
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        IEnumerable<TEntity> GetPagedElements<TKey>(
            int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool orderby = true);

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="sqlQuery">The Query to be executed</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// List of Entity
        /// </returns>
        /// <exception cref="ArgumentNullException">The value of 'sqlQuery' cannot be null. </exception>
        IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Execute a command in database
        /// </summary>
        /// <param name="sqlCommand">The sql query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>integer representing the sql code</returns>
        /// <exception cref="ArgumentNullException">The value of 'parameters' cannot be null. </exception>
        int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters);
    }
}