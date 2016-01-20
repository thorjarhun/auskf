namespace AUSKF.Controllers
{
    using Domain.Entities;
    using AUSKF.Domain.Models;
    using Domain.Providers.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Domain.Data;


    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Collections;
    using Domain.Data;
    using Domain.Entities.Identity;


    [Authorize]
    public class MyProfileController : Controller
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

        public MyProfileController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: MyProfile
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserIdAsInt();
            var user = await UserManager.FindByIdAsync(userId);
              
            MyProfileViewModel model = new MyProfileViewModel()
            {
                FirstName = user.Profile.FirstName,
                MiddleName = user.Profile.MiddleName,
                LastName = user.Profile.LastName,
                DateOfBirth = user.Profile.BirthDay,
                Gender = user.Profile.Gender,
                DojoId = user.Profile.DojoId,
                RankId = user.Profile.RankId,
                AddressLine1 = user.Profile.Address != null ? user.Profile.Address.AddressLine1 : string.Empty,
                AddressLine2 = user.Profile.Address != null ? user.Profile.Address.AddressLine2 : string.Empty,
                City = user.Profile.Address != null ? user.Profile.Address.City : string.Empty,
                State = user.Profile.Address != null ? user.Profile.Address.State : string.Empty,
                ZipCode = user.Profile.Address != null ? user.Profile.Address.ZipCode : string.Empty
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
                
                return this.View();
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
    }
}