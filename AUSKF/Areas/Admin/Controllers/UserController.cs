namespace AUSKF.Areas.Admin.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlTypes;
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
                ViewBag.PageHeader = "User List ";
                return View(users);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int userId)
        {
            var user = await GetUser(userId);
            ViewBag.PageHeader = "Details " + user.UserName;
            
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int userId)
        {
            var user = await GetUser(userId);
            ViewBag.PageHeader = "Editing " + user.UserName;

            return View(user);
        }

        private async Task<User> GetUser(int userId)
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
                    where u.Id == userId
                    select u)
                    .FirstOrDefaultAsync();
                user.Promotions.Sort();

                if (user.DateOfBirth == null)
                {
                    user.DateOfBirth = (DateTime) SqlDateTime.MinValue;
                    await context.SaveChangesAsync();
                }

                return user;
            }
        }
    }
}