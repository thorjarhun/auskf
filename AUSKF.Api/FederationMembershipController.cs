namespace AUSKF.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;
    using NLog;
    using Domain.Services.Interfaces;
    using Domain.Entities;
    using Domain.Collections;
    using Domain;
    using Domain.Providers.Interfaces;
    using Domain.Repositories.Interfaces;
    using Domain.Providers.Identity;

    [RoutePrefix("api/v1/federationmembership")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FederationMembershipController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ICacheService cacheService;
        private IApplicationSignInManager signInManager;
        private IApplicationUserManager userManager;
        private readonly IEntityRepository<FederationMembership, Guid> federationMembershipRepository;
        private readonly IEntityRepository<DojoMembership, Guid> dojoMembershipRepository;

        //public IApplicationUserManager UserManager
        //{
        //    get { return this.userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        //    private set { this.userManager = value; }
        //}


        public FederationMembershipController(IEntityRepository<FederationMembership, Guid> federationMembershipRepository, 
                                              IEntityRepository<DojoMembership, Guid> dojoMembershipRepository, 
                                              ICacheService cacheService, 
                                              IApplicationUserManager userManager, 
                                              IApplicationSignInManager signInManager)
        {
            this.federationMembershipRepository = federationMembershipRepository;
            this.dojoMembershipRepository = dojoMembershipRepository;
            this.cacheService = cacheService;
            //this.UserManager = (ApplicationUserManager)userManager;
            this.signInManager = signInManager;
        }
       
        [HttpGet]
        [Route("{userId}", Name = "GetUserCurrentMembershipV1")]
        [ResponseType(typeof(FederationMembership))]
        public async Task<IHttpActionResult> Get(string userId)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(userId, out id))
                {
                    var federationMembership = await this.federationMembershipRepository.GetAsync
                        (x => x.UserId == id && x.MembershipYear == DateTime.Now.Year, includeProperties: "User,Federation");

                    if (federationMembership != null)
                    {
                        return this.Ok(federationMembership);
                    }
                }
                else
                {
                    return this.BadRequest(Common.UnableToDetermineId);
                }
            }

            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
            return this.NotFound();
        }

        
        [HttpGet]
        [Route("getmembershipyears/{federationId}", Name = "GetMembershipYearsV1")]
        [ResponseType(typeof(FederationMembership))]
        public async Task<IHttpActionResult> GetMembershipYears(string federationId)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(federationId, out id))
                {
                    var federationMemberships = await this.federationMembershipRepository.GetAsync(x => x.FederationId == id);

                    if (federationMemberships != null)
                    {
                        var years = federationMemberships.Select(m => m.MembershipYear).Distinct().ToList();
                        if (!years.Contains(DateTime.Now.Year))
                        {
                            years.Add(DateTime.Now.Year);
                        }
                        return this.Ok(years.ToArray());
                    }
                }
                else
                {
                    return this.BadRequest(Common.UnableToDetermineId);
                }
            }

            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
            return this.Ok(DateTime.Now.Year);
        }


        [HttpGet]
        [Route("paged/{pagenumber}", Name = "FederationMembershipsV1")]
        [ResponseType(typeof(SerializablePagination<FederationMembership>))]
        public async Task<IHttpActionResult> GetFederationMemberships(int? pagenumber, [FromUri]Guid? federationId = null, [FromUri]Guid? dojoId = null, [FromUri]int? membershipYear = null)
        {
            try
            {
                var federationMemberships = this.cacheService.TryGet<Expression<Func<FederationMembership, bool>>,
                                                     Func<IQueryable<FederationMembership>, IOrderedQueryable<FederationMembership>>,
                                                     string,
                                                     IEnumerable<FederationMembership>
                                                    >("FederationMemberships", (x => x != null), null, "User,Federation", this.federationMembershipRepository.Get, null);
                 
                if (federationMemberships != null)
                {
                    if (federationId != null)
                    {
                        federationMemberships = federationMemberships.Where<FederationMembership>(f => f.FederationId == federationId);
                    }
                      
                    if (dojoId != null)
                    {
                        var dojoMemberships = this.cacheService.TryGet<Expression<Func<DojoMembership, bool>>,
                                                     Func<IQueryable<DojoMembership>, IOrderedQueryable<DojoMembership>>,
                                                     string,
                                                     IEnumerable<DojoMembership>
                                                    >("DojoMemberships", (x => x != null), null, "", this.dojoMembershipRepository.Get, null);

                        federationMemberships = federationMemberships.Where<FederationMembership>(f => dojoMemberships.Any(d => d.UserId == f.UserId && d.DojoId == dojoId));
                    }

                    if (membershipYear != null)
                    {
                        federationMemberships = federationMemberships.Where<FederationMembership>(f => f.MembershipYear == membershipYear);
                    }

                    var federationMembershipArray = federationMemberships as FederationMembership[] ?? federationMemberships.ToArray();

                    if (!federationMembershipArray.Any())
                    {
                        // no memberships? Ok just return
                        return await Task.FromResult(this.Ok());
                    }

                    pagenumber = pagenumber.HasValue ? pagenumber : 0;

                    return await Task.FromResult((IHttpActionResult)
                        this.Ok(new SerializablePagination<FederationMembership>(federationMembershipArray.ToList(), (int)pagenumber)));
                }
                return await Task.FromResult((IHttpActionResult)this.NotFound());
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }
    }
}