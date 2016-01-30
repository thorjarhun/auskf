namespace AUSKF.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Collections;
    using Domain.Data;
    using Domain.Entities.Identity;

    public class UserController : Controller
    {

        // TODO Require login/roles etc
        [HttpGet]
        public async Task<ActionResult> Index(string page = null, string sort = "Id")
        {
            int pageNumber = 1;

            if (!string.IsNullOrWhiteSpace(page))
            {
                int.TryParse(page, out pageNumber);
            }
            ViewBag.CurrentBreadCrumb = "User Dashboard";

            // TODO repository, caching etc.
            using (var context = new DataContext())
            {
                var userList = await (from x in context.Users
                                          .Include(u => u.Profile.Dojo)
                                      orderby sort
                                      select x
                    ).ToArrayAsync();


                var users = new SerializablePagination<User>(userList, pageNumber);
                return View(users);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int userId)
        {
            using (var context = new DataContext())
            {
                var user = await (from u in context.Users
                    .Include(u => u.Promotions)
                                  where u.Id == userId
                                  select u)
                    //.Include(u => u.Promotions)
                    //.Include(u => u.Roles)
                    //.Include(u => u.Claims)
                    //.Include(u => u.Logins)
                    .FirstOrDefaultAsync();

                //var promotions = await (from p in context.Promotions
                //                        where p.UserId == user.Id
                //                        select p).ToListAsync();

                return View(user);
            }
        }
    }
}