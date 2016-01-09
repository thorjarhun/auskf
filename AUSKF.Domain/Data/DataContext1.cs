﻿namespace AUSKF.Domain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities.Configurations;
    using Exceptions;
    using Interfaces;
    using NLog;

    public sealed partial class DataContext : DbContext, IDataContext, ITransientLifestyle
    {
        private static readonly Logger logger = LogManager.GetLogger("DataContext");


        /// <summary>
        ///     Initializes the <see cref="Domain.Data.DataContext" /> class.
        /// </summary>
        static DataContext()
        {
            Database.SetInitializer(new EntityContextInitializer());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Domain.Data.DataContext" /> class.
        /// </summary>
        public DataContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        ///     Tracks the changes.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void TrackChanges(bool value)
        {
            this.Configuration.AutoDetectChangesEnabled = value;
        }

        /// <summary>
        ///     Returns a ("$1") instance for access to entities of the given type in the context,
        ///     the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        /// <summary>
        ///     Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (this.Entry(entity).State == EntityState.Detached)
            {
                this.Set<TEntity>().Attach(entity);
            }
        }

        /// <summary>
        ///     Commit all changes made in a container.
        /// </summary>
        /// <remarks>
        ///     If the entity have fixed properties and any optimistic concurrency problem exists,
        ///     then an exception is thrown
        /// </remarks>
        /// <exception cref="DbEntityValidationException">Condition. </exception>
        /// <exception cref="Exception">Condition. </exception>
        public int Commit()
        {
            try
            {
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages =
                    from eve in ex.EntityValidationErrors
                    let entity = eve.Entry.Entity.GetType().Name
                    from ev in eve.ValidationErrors
                    select new {Entity = entity, ev.PropertyName, ev.ErrorMessage};

                var fullErrorMessage = string.Join(
                    "; ",
                    errorMessages.Select(
                        e => string.Format("[Entity: {0}, Property: {1}] {2}", e.Entity, e.PropertyName, e.ErrorMessage)));

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                logger.Error(ex, exceptionMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors, ex);
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                logger.Error(e, e.StackTrace);
                throw;
            }
        }

        /// <summary>
        ///     Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually indicates an optimistic
        ///     concurrency violation; that is, a row has been changed in the database since it was queried.
        /// </exception>
        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        public async Task<int> CommitAsync()
        {
            return await this.SaveChangesAsync();
        }

        /// <summary>
        ///     Sets the modified.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public void SetModified<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ParameterNullException("entity");
            }
            this.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Execute specific query with underlying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        ///     Dialect Query
        ///     <example>
        ///         SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        ///     </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        ///     Enumerable results
        /// </returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return this.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        ///     Execute arbitrary command into underlaying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        ///     Command to execute
        ///     <example>
        ///         SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        ///     </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        ///     The number of affected records
        /// </returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        /// <summary>
        ///     This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        ///     Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///     classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}