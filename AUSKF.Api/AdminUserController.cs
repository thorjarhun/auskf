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
                    BaseUrl = "Admin/User",
                    SortBy = sort,
                    SortDirection = ParseSort(sortDirection)
                };

            return await Task.FromResult((IHttpActionResult) this.Ok(model));
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