namespace AUSKF.Api
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlTypes;
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
        [ResponseType(typeof(UserPagination))]
        public async Task<IHttpActionResult> Get(int page = 1, int pagesize = 20, string sortdirection = "ascending",
            string sortby = "Active", bool onlyShowActive = false, string query = null)
        {
            int skip = (pagesize * (page - 1));

            var totalUsers = this.userRepository.GetCount();
            var activeCount = this.cacheService.TryGet("ActiveUserCount", GetActiveCount, null);

            string cacheKey = User.GetType().FullName  + ":skip:" + 
                skip + ":take:" + pagesize + ":active:" + onlyShowActive;
            
            ICollection<User> userList;

            if (this.cacheService.Contains(cacheKey))
            {
                userList = (ICollection<User>)this.cacheService[cacheKey];
            }
            else
            {
                userList = await GetUserList(pagesize, sortby, skip, onlyShowActive);
                this.cacheService.Add(cacheKey, userList);
            }

            var users = new UserPagination(userList, totalUsers, page, pagesize, 
                activeCount, ParseSort(sortdirection))
            {
                BaseUrl = "users",
                SortBy = sortby
            };
            return await Task.FromResult((IHttpActionResult)this.Ok(users));
        }

        [HttpGet]
        [Route("", Name = "AdminUsersV1-a")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> PostSearch([FromUri]SearchValues searchValues)
        {
            // create a default searchValues, this should probably just be a strut
            if (searchValues == null)
            {
                searchValues = new SearchValues
                {
                    OrderBy = "Id",
                    Page = 1,
                    PageSize = 20,
                    SortDirection = SortDirection.Ascending
                };
            }


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
                SortDirection = searchValues.SortDirection
            };

            return await Task.FromResult((IHttpActionResult)this.Ok(model));
        }

        [HttpGet]
        [Route("{id}", Name = "AdminUserDetails")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetForId(int id)
        {
            using (var context = new DataContext())
            {
                var user = await (from u in context.Users
                    .Include(u => u.Promotions)
                    .Include(u => u.Profile)
                    .Include(u => u.Roles)
                    .Include(u => u.Claims)
                    .Include(u => u.Logins)
                    .Include(u => u.Profile.Federation)
                    .Include(u => u.Profile.Dojo)
                    .Include(u => u.Profile.Events)
                                  where u.Id == id
                                  select u)
                    .FirstOrDefaultAsync();
                user.Promotions.Sort();

                if (user.DateOfBirth == null)
                {
                    user.DateOfBirth = (DateTime)SqlDateTime.MinValue;
                    await context.SaveChangesAsync();
                }

                return await Task.FromResult((IHttpActionResult)this.Ok(user));
            }
        }

        private int GetActiveCount()
        {
            return (from u in ((DataContext)this.userRepository.Context).Users
                    where u.Active
                    select u).Count();
        }

        private async Task<ICollection<User>> GetUserList(int pagesize, string sortby, int skip, bool onlyShowActive)
        {
            using (var context = new DataContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                User[] userList;

                if (onlyShowActive)
                {
                    userList = await (from x in context.Users
                                      where x.Active
                                      orderby "Active", sortby
                                      select x)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToArrayAsync();
                }
                else
                {
                    userList = await (from x in context.Users
                                      orderby "Active", sortby
                                      select x)
                    .Skip(skip)
                    .Take(pagesize)
                    .ToArrayAsync();
                }
                return userList;
            }
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