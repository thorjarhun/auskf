namespace AUSKF.Domain.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.Windsor;

    public interface IInversionOfControlContainer
    {
        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="classType">Type of the class.</param>
        /// <exception cref="System.ArgumentNullException">
        /// key
        /// or
        /// classType
        /// </exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent(string key, Type classType);

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <exception cref="System.ArgumentNullException">
        /// serviceType
        /// or
        /// classType
        /// or
        /// key
        /// </exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent(string key, Type serviceType, Type classType);

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <exception cref="System.ArgumentNullException">
        /// classType
        /// or
        /// key
        /// </exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle(string key, Type classType, LifestyleType lifestyle);

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <exception cref="System.ArgumentNullException">
        /// serviceType
        /// or
        /// classType
        /// or
        /// key
        /// </exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle(string key, Type serviceType, Type classType, LifestyleType lifestyle);

        /// <summary>
        ///   Adds the component.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <remarks>
        ///   This method is thread-safe.
        /// </remarks>
        void AddComponent<T>()
            where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent<T>(string key)
            where T : class;

        /// <summary>
        ///   Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="lifestyle"> The lifestyle. </param>
        void AddComponentWithLifestyle<T>(LifestyleType lifestyle)
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle<T>(string key, LifestyleType lifestyle)
            where T : class;

        /// <summary>
        ///   Adds the component.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TU"> The type of the U. </typeparam>
        /// <remarks>
        ///   This method is thread-safe.
        /// </remarks>
        void AddComponent<T, TU>()
            where TU : class, T
            where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent<T, TU>(string key)
            where TU : class, T
            where T : class;

        /// <summary>
        ///   Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TU"> The type of the U. </typeparam>
        /// <param name="lifestyle"> The lifestyle. </param>
        /// <remarks>
        ///   This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle<T, TU>(LifestyleType lifestyle)
            where TU : class, T
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle<T, TU>(string key, LifestyleType lifestyle)
            where T : class
            where TU : T;

        /// <summary>
        /// Adds the component with dependency (dependency can be a constructor parameter or public property).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <param name="dependencyPropertyName">Name of the dependency property.</param>
        /// <param name="dependencyPropertyValue">The dependency property value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// dependencyPropertyValue
        /// or key or key
        /// </exception>
        void AddComponentWithDependency<T, TU>(
            string key, LifestyleType lifestyle, string dependencyPropertyName,
            object dependencyPropertyValue)
            where T : class
            where TU : T;

        /// <summary>
        /// Resolves an object with the specified key and specified service
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// service
        /// or
        /// key
        /// </exception>
        object Resolve(string key, Type service);

        /// <summary>
        /// Resolves an object with the specified key and specified service asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// service
        /// or
        /// key
        /// </exception>
        Task<object> ResolveAsync(string key, Type service);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        /// <exception cref="Castle.MicroKernel.ComponentNotFoundException">
        /// key
        /// or
        /// key
        /// </exception>
        /// <exception cref="ArgumentNullException">The value of 'key' cannot be null.</exception>
        /// <exception cref="ComponentNotFoundException">key</exception>
        /// <remarks>
        /// This method is dangerous! Be sure you know what you are doing if you use it!
        /// </remarks>
        object Resolve(string key);

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">service</exception>
        object Resolve(Type service);

        /// <summary>
        ///   Releases the specified instance.
        /// </summary>
        /// <param name="instanceParam"> The instance. </param>
        void Release(object instanceParam);

        /// <summary>
        /// Releases the specified instance asynchronously.
        /// </summary>
        /// <param name="instanceParam">The instance parameter.</param>
        /// <returns></returns>
        Task ReleaseAsync(object instanceParam);

        /// <summary>
        /// Resolves an object specified type T.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves an object specified type T asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ResolveAsync<T>();

        /// <summary>
        /// Resolves an object with the specified key and type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        T Resolve<T>(string key);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <exception cref="System.ArgumentNullException">
        /// serviceType
        /// or
        /// classType
        /// or
        /// key
        /// </exception>
        /// <exception cref="ArgumentNullException">The value of 'serviceType' cannot be null.</exception>
        void Replace(string key, Type serviceType, Type classType, LifestyleType lifestyle = LifestyleType.Singleton);

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        bool ContainsKey(string key);

        /// <summary>
        ///   Gets the Windsor container.
        /// </summary>
        WindsorContainer WindsorContainer { get; }
    }
}