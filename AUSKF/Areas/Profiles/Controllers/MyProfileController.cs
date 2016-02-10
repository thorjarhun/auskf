namespace AUSKF.Areas.Profiles.Controllers
{
    using Domain.Entities;
    using Models;
    using Domain.Providers.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Domain.Data;
    using System.Data.Entity;
    using Domain.Providers.Interfaces;
    using NLog;
    using Domain.Entities.Identity;

    [Authorize]
    public class MyProfileController : Controller
    {
        private static readonly Logger logger = LogManager.GetLogger("MyProfileController");
        private ApplicationUserManager userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProfileController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>  
        public MyProfileController(IApplicationUserManager userManager)
        {
            if (logger != null)
            {
                logger.Info("MyProfileController created.");
            }
            this.UserManager = (ApplicationUserManager)userManager; 
        }

        // GET: MyProfile 
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserIdAsInt();  
            return View(await GetUserProfileModel(userId));
        }

        // EDIT: MyProfile 
        public async Task<ActionResult> Edit()
        {
            var userId = User.Identity.GetUserIdAsInt();  
            return  View(await GetUserProfileModel(userId));
        }

        private async Task<MyProfileViewModel> GetUserProfileModel(int userId)
        {
            MyProfileViewModel model;
            using (var context = new DataContext())
            {
                model = await (from x in context.Users.Include(u => u.Profile)
                                                  .Include(u => u.Profile.Address)
                                                  .Include(u => u.Profile.Federation)
                                                  .Include(u => u.Profile.Dojo)
                               where x.Id == userId
                               select new MyProfileViewModel()
                               {
                                   FirstName = x.FirstName,
                                   MiddleName = x.MiddleName,
                                   LastName = x.LastName,
                                   DateOfBirth = x.DateOfBirth,
                                   Gender = x.Gender,
                                   //Telephone = x.PhoneNumber,
                                   Email = x.Email,
                                   DojoId = x.Profile != null ? x.Profile.DojoId : 0,
                                   Dojo = x.Profile != null ? x.Profile.Dojo.DojoName : "",
                                   MyFederation = x.Profile != null ? x.Profile.Federation.Name : "",
                                   RankId = x.Profile != null ? x.Profile.RankId : 0,
                                   AddressLine1 = x.Profile != null && x.Profile.Address != null ? x.Profile.Address.AddressLine1 : string.Empty,
                                   AddressLine2 = x.Profile != null && x.Profile.Address != null ? x.Profile.Address.AddressLine2 : string.Empty,
                                   City = x.Profile != null && x.Profile.Address != null ? x.Profile.Address.City : string.Empty,
                                   State = x.Profile != null && x.Profile.Address != null ? x.Profile.Address.State : string.Empty,
                                   ZipCode = x.Profile != null && x.Profile.Address != null ? x.Profile.Address.ZipCode : string.Empty
                               }).FirstOrDefaultAsync();

                //var federationMembership = context.FederationMemberships.Include(f => f.Federation).Where(m => m.UserId == user.Id).FirstOrDefault();
                //if (federationMembership != null)
                //{
                //    model.MyFederation = federationMembership.Federation.Name;
                //} 
            }
            //TODO: TLS - Determine AUSKF membership

            return model;
        }

        // POST: /MyProfile/UpdateProfile
        /// <summary>
        /// Update users profile information
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">Always.</exception>
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(MyProfileViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = User.Identity.GetUserIdAsInt();
               
                using (var context = new DataContext())
                {
                    var user = (from x in context.Users.Include(u => u.Profile)
                                where x.Id == userId
                                select x).FirstOrDefault();

                    if (user != null)
                    {
                        user.FirstName = model.FirstName;
                        user.MiddleName = model.MiddleName;
                        user.LastName = model.LastName;
                        user.DateOfBirth = model.DateOfBirth;
                        user.Gender = model.Gender;
                        user.PhoneNumber = model.Telephone;
                        user.Email = model.Email;
                        user.Profile.DojoId = model.DojoId;
                        user.Profile.RankId = model.RankId;

                        if (!string.IsNullOrEmpty(model.AddressLine1) || !string.IsNullOrEmpty(model.AddressLine2) ||
                            !string.IsNullOrEmpty(model.City) || !string.IsNullOrEmpty(model.ZipCode) || !string.IsNullOrEmpty(model.State))
                        {
                            if (user.Profile.Address == null)
                            {
                                user.Profile.Address = new Address();
                            }

                            user.Profile.Address.AddressLine1 = model.AddressLine1;
                            user.Profile.Address.AddressLine2 = model.AddressLine2;
                            user.Profile.Address.City = model.City;
                            user.Profile.Address.State = model.State;
                            user.Profile.Address.ZipCode = model.ZipCode;
                        }

                        context.SaveChanges();
                        ViewBag.EditResult = "Profile Saved";
                    }  
                } 
            }

            // If we got this far, something failed, redisplay form
            return this.View("Index", model);
        }
    }
}