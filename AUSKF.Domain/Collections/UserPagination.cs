namespace AUSKF.Domain.Collections
{
    using System.Collections.Generic;
    using Entities.Identity;
    using Interfaces;

    public class UserPagination : SerializablePagination<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPagination"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="totalNumberOfItems">The total number of items.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="activeCount">The active count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        public UserPagination(ICollection<User> dataSource, int totalNumberOfItems, int pageNumber,
            int pageSize, int activeCount, SortDirection sortDirection = SortDirection.Descending)
            : base(dataSource,totalNumberOfItems, pageNumber, pageSize, sortDirection)
        {
            this.ActiveCount = activeCount;
        }

        /// <summary>
        /// Gets or sets the active count.
        /// </summary>
        /// <value>
        /// The active count.
        /// </value>
        public int ActiveCount { get; set; }
    }
}