namespace AUSKF.Areas.Profiles.Controllers
{
    using Models;
    using Domain.Providers.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using NLog;
    using Domain.Providers.Interfaces;

    public class FederationRegistrationController : Controller
    { 
        private static readonly Logger logger = LogManager.GetLogger("FederationRegistrationController");
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
        /// Initializes a new instance of the <see cref="FederationRegistrationController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>  
        public FederationRegistrationController(IApplicationUserManager userManager)
        {
            if (logger != null)
            {
                logger.Info("MyProfileController created.");
            }
            this.UserManager = (ApplicationUserManager)userManager;
        }

        // GET: Profiles/FederationRegistration 
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
                //Dojo = user.Profile != null && user.Profile.Dojo != null ? user.Profile.Dojo.DojoName : string.Empty,
                //Rank = user.Profile != null && user.Profile.Rank != null ?  user.Profile.Rank.RankName : string.Empty,
                AddressLine1 = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.AddressLine1 : string.Empty,
                AddressLine2 = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.AddressLine2 : string.Empty,
                City = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.City : string.Empty,
                State = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.State : string.Empty,
                ZipCode = user.Profile != null && user.Profile.Address != null ? user.Profile.Address.ZipCode : string.Empty
            };

            return View(model);
        }
    }
}