namespace AUSKF.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Interfaces;
    using Interfaces;
    using Services.Interfaces;

    public class CachingRepository<TEntity, TKey> : EntityRepository<TEntity, TKey>, ICachingRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="cacheService">The cache service.</param>
        public CachingRepository(IDataContext dataContext, ICacheService cacheService)
            : base(dataContext)
        {
            this.cacheService = cacheService;
        }

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
        public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int? take = null, string includeProperties = "")
        {
            string cacheKey = typeof(TEntity).FullName + includeProperties + skip + take;
            return this.cacheService.TryGet(cacheKey, filter, orderBy, skip, take, includeProperties, base.Get, null);
        }

        /// <summary>
        /// Get count of Entities
        /// </summary>
        /// <returns></returns>
        public override int GetCount()
        {
            return this.cacheService.TryGet(typeof(TEntity).FullName + "Count", base.GetCount, null);
        }

        /// <summary>
        /// Updates the specified updated.
        /// </summary>
        /// <param name="updated">The updated.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override TEntity Update(TEntity updated, TKey key)
        {
            string cacheKey = updated.GetType().FullName + ":key:" + key;
            this.cacheService.Invalidate(cacheKey);
            var entity = base.Update(updated, key);
            this.cacheService.Add(cacheKey, entity);
            return entity;
        }

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <exception cref="System.NotImplementedException">Use the Delete(id) method when using the caching repository.</exception>
        public override void Delete(TEntity entityToDelete)
        {
            throw new NotImplementedException("Use the Delete(id) method when using the caching repository.");
        }

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        public override void Delete(TKey id)
        {
            TEntity entityToDelete = this.dataContext.SetEntity<TEntity>().Find(id);
            string cacheKey = entityToDelete.GetType().FullName + ":key:" + id;
            this.cacheService.Invalidate(cacheKey);
            this.Delete(entityToDelete);
        }
    }
}