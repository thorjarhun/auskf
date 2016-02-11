namespace AUSKF.Api
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain.Collections;
    using Domain.Data;
    using Domain.Entities.Identity;
    using Domain.Interfaces;
    using Domain.Models;
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;

    [RoutePrefix("api/v1/admin/users")]
    public class AdminUserController : ApiController
    {
        
        private readonly ICacheService cacheService;
        private readonly ICachingRepository<User, int> userRepository;

        public AdminUserController(ICacheService cacheService,
            ICachingRepository<User, int> userRepository)
        {
            this.cacheService = cacheService;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("{page}", Name = "AdminUsersV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get(int page = 1, int pagesize = 20,
            string sortdirection = "ascending", string sortby = "Active", string query = null)
        {
            int skip = (pagesize * (page - 1));
            var totalUsers = this.userRepository.GetCount();
            
            string cacheKey = User.GetType().FullName + "Profile.Dojo" + "skip:" + skip + "take:" + pagesize;
            ICollection<User> userList;

            if (this.cacheService.Contains(cacheKey))
            {
                userList = (ICollection<User>) this.cacheService[cacheKey];
            }
            else
            {
                userList = await GetUserList(pagesize, sortby, skip);
                this.cacheService.Add(cacheKey, userList);
            }

            var users = new SerializablePagination<User>(userList, totalUsers, page, pagesize)
            {
                BaseUrl = "users",
                SortBy = sortby,
                SortDirection = ParseSort(sortdirection)
            };
            return await Task.FromResult((IHttpActionResult)this.Ok(users));
        }

        private async Task<ICollection<User>> GetUserList(int pagesize, string sortby, int skip)
        {
            using (var context = new DataContext())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var userList = await (from x in context.Users
                    .Include(u => u.Profile.Dojo)
                                      orderby "Active", sortby
                                      select x)
                    .Skip(skip)
                    .Take(pagesize)
                    .ToArrayAsync();
                return userList;
            }
        }


        [HttpGet]
        [Route("", Name = "AdminUsersV1-a")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> PostSearch([FromUri]SearchValues searchValues)
        {
            int totalItemCount;
            int skip = (searchValues.Page - 1) * searchValues.PageSize;
            List<User> userList;

            // TODO repository, caching etc.
            using (var context = new DataContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                totalItemCount = context.Users.Count();

                userList = await (from x in context.Users
                                         .Include(u => u.Profile)
                                  orderby searchValues.OrderBy 
                                  select x
                   ).Skip(skip).Take(searchValues.PageSize).ToListAsync();
            }


            var model = new SerializablePagination<User>(
                userList.ToList(),
                totalItemCount,
                searchValues.Page,
                searchValues.PageSize)
            {
                BaseUrl = "User",
                SortBy = searchValues.OrderBy,
                SortDirection =searchValues.SortDirection
            };

            return await Task.FromResult((IHttpActionResult)this.Ok(model));
        }

        private SortDirection ParseSort(string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortDirection))
            {
                return SortDirection.Descending;
            }
            return sortDirection.ToUpperInvariant().StartsWith("ASC") ?
                    SortDirection.Ascending : SortDirection.Descending;

        }
    }


}