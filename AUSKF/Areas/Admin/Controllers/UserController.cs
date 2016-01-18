namespace AUSKF.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Collections;
    using Domain.Data;
    using Domain.Entities.Identity;

    public class UserController : Controller
    {

        // TODO Require login/roles etc
        [HttpGet]
        public async Task<ActionResult> Index(string page=null)
        {
            int pageNumber = 1;

            if (!string.IsNullOrWhiteSpace(page))
            {
                int.TryParse(page, out pageNumber);
            }

            SerializablePagination<User> users;

            // TODO repository, caching etc.
            using (var context = new DataContext())
            {
                var userList = await context.Users.Include(u=>u.Profile.Dojo).ToArrayAsync();
                users = new SerializablePagination<User>(userList, pageNumber);
                return View(users);
            }
        }
    }
}