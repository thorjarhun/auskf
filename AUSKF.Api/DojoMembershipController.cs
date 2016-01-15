namespace AUSKF.Api.Controllers
{
    using System; 
    using System.Web.Http;
    using System.Web.Http.Cors;  
    using Domain.Entities; 
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/dojomembership")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DojoMembershipController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger(); 
        private readonly ICacheService cacheService; 
        private readonly IEntityRepository<Dojo, int> dojoMembershipRepository;
         
        public DojoMembershipController(IEntityRepository<Dojo, int> dojoMembershipRepository, ICacheService cacheService)
        {
            this.dojoMembershipRepository = dojoMembershipRepository;
            this.cacheService = cacheService;  
        }
         
    }
}