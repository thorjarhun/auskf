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
            var user = await UserManager.FindByIdAsync(userId);
            
            MyProfileViewModel model = new MyProfileViewModel()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Telephone = user.PhoneNumber,
                Email = user.Email, 
                DojoId = user.Profile != null ? user.Profile.DojoId : 0,
                RankId = user.Profile != null ? user.Profile.RankId : 0,
                AddressLine1 = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.AddressLine1 : string.Empty,
                AddressLine2 = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.AddressLine2 : string.Empty,
                City = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.City : string.Empty,
                State = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.State : string.Empty,
                ZipCode = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.ZipCode : string.Empty
            };

            using (var context = new DataContext())
            {
                var federationMembership = context.FederationMemberships.Include(f => f.Federation).Where(m => m.UserId == userId).FirstOrDefault();
                if (federationMembership != null)
                {
                    model.MyFederation = federationMembership.Federation.Name;
                }
            }

            //TODO: TLS - Determine AUSKF membership

            return View(model);
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(MyProfileViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = User.Identity.GetUserIdAsInt();
                var user = await UserManager.FindByIdAsync(userId);

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

                await UserManager.UpdateAsync(user);

                return this.View();
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}