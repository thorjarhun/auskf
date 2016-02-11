namespace AUSKF.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Collections;
    using Domain.Data;
    using Domain.Entities.Identity;
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;

    public class UserController : Controller
    {
        private readonly ICacheService cacheService;
        private readonly ICachingRepository<User, int> userRepository;

        public UserController(ICacheService cacheService,
            ICachingRepository<User, int> userRepository)
        {
            this.cacheService = cacheService;
            this.userRepository = userRepository;
        }

        // TODO Require login/roles etc
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pagesize = 20,
            string sortdirection = "ascending", string sortby = "Active", string query = null)
        {
            //int skip = (pagesize * (page - 1));
            //var totalUsers = this.userRepository.GetCount();
            ViewBag.CurrentBreadCrumb = "User Dashboard";
            
            //string cacheKey = User.GetType().FullName + "Profile.Dojo" + "skip:" + skip + "take:" + pagesize;
            //ICollection<User> userList;

            //if (this.cacheService.Contains(cacheKey))
            //{
            //    userList = (ICollection<User>) this.cacheService[cacheKey];
            //}
            //else
            //{
            //    userList = await GetUserList(pagesize, sortby, skip);
            //    this.cacheService.Add(cacheKey, userList);
            //}

            //var users = new SerializablePagination<User>(userList, totalUsers, page, pagesize);
            ViewBag.PageHeader = "User List ";
            return View();
        }

        private async Task<ICollection<User>> GetUserList(int pagesize, string sortby, int skip)
        {
            using (var context = new DataContext())
            {
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
                    user.DateOfBirth = (DateTime)SqlDateTime.MinValue;
                    await context.SaveChangesAsync();
                }

                return user;
            }
        }
    }
}