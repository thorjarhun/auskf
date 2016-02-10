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

    [RoutePrefix("api/v1/admin/user")]
    public class AdminUserController : ApiController
    {
        [HttpGet]
        [Route("{page}/{sort}", Name = "AdminUsersV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get(string page = null,
            string sort = "Id", string sortDirection = "descending")
        {
            int pageNumber = 1;

            if (!string.IsNullOrWhiteSpace(page))
            {
                int.TryParse(page, out pageNumber);
            }

            int totalItemCount;
            int pageSize = 20;
            int skip = (pageNumber - 1) * pageSize;
            List<User> userList;

            // TODO repository, caching etc.
            using (var context = new DataContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                totalItemCount = context.Users.Count();

                userList = await (from x in context.Users
                                         .Include(u => u.Profile)
                                  orderby sort
                                  select x
                   ).Skip(skip).Take(pageSize).ToListAsync();
            }

            
                var model = new SerializablePagination<User>(
                    userList.ToList(),
                    totalItemCount,
                    pageNumber,
                    pageSize)
                {
                    BaseUrl = "User",
                    SortBy = sort,
                    SortDirection = ParseSort(sortDirection)
                };

            return await Task.FromResult((IHttpActionResult) this.Ok(model));
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