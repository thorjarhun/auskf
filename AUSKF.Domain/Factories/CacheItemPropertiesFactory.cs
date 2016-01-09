namespace AUSKF.Domain.Factories
{
    using System;
    using System.Configuration;
    using System.Web.Caching;
    using Helpers;
    using Helpers.Interfaces;
    using Interfaces;
    public sealed class CacheItemPropertiesFactory : ICacheItemPropertiesFactory
    {
        private readonly int slidingExpiration;
        private readonly int absoluteExpiration;
        private readonly ICacheItemProperties defaultItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItemPropertiesFactory" /> class.
        /// </summary>
        public CacheItemPropertiesFactory()
        {
            string key;
            if ((key = ConfigurationManager.AppSettings[Constants.DefaultSlidingExpiration]) != null)
            {
                if (!int.TryParse(key, out this.slidingExpiration))
                {
                    this.slidingExpiration = 1;
                }
            }

            if ((key = ConfigurationManager.AppSettings[Constants.DefaultAbsoluteExpiration]) != null)
            {
                if (!int.TryParse(key, out this.absoluteExpiration))
                {
                    this.absoluteExpiration = 10;
                }
            }
            this.defaultItem = this.Build();
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public ICacheItemProperties Build()
        {
            var sliding = TimeSpan.FromMinutes(this.slidingExpiration);
            // if there is a sliding expiration then absolute expiration must be set to DateTime.MaxValue
            var absolute = sliding > TimeSpan.Zero ? DateTime.MaxValue : DateTime.Now + TimeSpan.FromMinutes(this.absoluteExpiration);

            CacheItemProperties cip = new CacheItemProperties
            {
                SlidingExpiration = sliding,
                AbsoluteExpiration = absolute,
                CachePriority = CacheItemPriority.Normal
            };

            return cip;
        }

        /// <summary>
        /// Builds the specified sliding expiration.
        /// </summary>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        /// <param name="cacheItemPriority">The cache item priority.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public ICacheItemProperties Build(int slidingExpiration, int absoluteExpiration, CacheItemPriority cacheItemPriority,
            CacheDependency dependency, Delegate callback)
        {
            if (slidingExpiration < 0)
            {
                slidingExpiration = 0;
            }

            if (absoluteExpiration < 0)
            {
                absoluteExpiration = 0;
            }

            CacheItemProperties cip = new CacheItemProperties
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration),
                AbsoluteExpiration = absoluteExpiration > 0 ? DateTime.Now + TimeSpan.FromMinutes(absoluteExpiration) : DateTime.MaxValue,
                CachePriority = CacheItemPriority.Normal
            };

            return cip;
        }

        /// <summary>
        /// Gets the default cacheItemProperties object.
        /// This will be created with the values passed in via the
        /// environment service or with a sliding of 1 and absolute of 10.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public ICacheItemProperties Default
        {
            get { return this.defaultItem; }
        }

    }
}