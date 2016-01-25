namespace AUSKF.Areas.Profiles.Controllers
{ 
    using Models;
    using Domain.Providers.Identity;
    using Microsoft.AspNet.Identity.Owin; 
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc; 

    public class FederationRegistrationController : Controller
    {
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

        public FederationRegistrationController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: Profiles/FederationRegistration
        [ValidateAntiForgeryToken]
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
                DojoId = user.Profile.DojoId,
                RankId = user.Profile.RankId,
                AddressLine1 = user.Profile.Address != null ? user.Profile.Address.AddressLine1 : string.Empty,
                AddressLine2 = user.Profile.Address != null ? user.Profile.Address.AddressLine2 : string.Empty,
                City = user.Profile.Address != null ? user.Profile.Address.City : string.Empty,
                State = user.Profile.Address != null ? user.Profile.Address.State : string.Empty,
                ZipCode = user.Profile.Address != null ? user.Profile.Address.ZipCode : string.Empty
            };

            return View();
        }
    }
}