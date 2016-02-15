namespace AUSKF.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;
    using Domain.Collections;
    using Domain.Entities;
    using Domain.Entities.Identity;
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/dojos")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DojoController : ApiController
    {   
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<Dojo, int> dojoRepository;
        private readonly ICacheService cacheService;
        private readonly List<Tuple<string, string>> states = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Alabama", "AK"),
            new Tuple<string, string>("Alaska", "AK"),
            new Tuple<string, string>("Arizona", "AZ"),
            new Tuple<string, string>("Arkansas", "AR"),
            new Tuple<string, string>("California", "CA"),
            new Tuple<string, string>("Colorado", "CO"),
            new Tuple<string, string>("Connecticut", "CT"),
            new Tuple<string, string>("Delaware", "DE"),
            new Tuple<string, string>("Florida", "FL"),
            new Tuple<string, string>("Georgia", "GA"),
            new Tuple<string, string>("Hawaii", "HI"),
            new Tuple<string, string>("Idaho", "ID"),
            new Tuple<string, string>("Illinois", "IL"),
            new Tuple<string, string>("Indiana", "IN"),
            new Tuple<string, string>("Iowa", "IA"),
            new Tuple<string, string>("Kansas", "KS"),
            new Tuple<string, string>("Kentucky", "KY"),
            new Tuple<string, string>("Lousiana", "LA"),
            new Tuple<string, string>("Maine", "ME"),
            new Tuple<string, string>("Maryland", "MD"),
            new Tuple<string, string>("Massachusetts", "MA"),
            new Tuple<string, string>("Michigan", "MI"),
            new Tuple<string, string>("Minnesota", "MN"),
            new Tuple<string, string>("Mississippi", "MS"),
            new Tuple<string, string>("Missouri", "MO"), 
            new Tuple<string, string>("Montana", "MT"),
            new Tuple<string, string>("Nebraska", "NE"),
            new Tuple<string, string>("Nevada", "NV"),
            new Tuple<string, string>("New Hampshire", "NH"),
            new Tuple<string, string>("New Jersey", "NJ"),
            new Tuple<string, string>("New Mexico", "NM"),
            new Tuple<string, string>("New York", "NY"),
            new Tuple<string, string>("North Carolina", "NC"),
            new Tuple<string, string>("North Dakota", "ND"),
            new Tuple<string, string>("Ohio", "OH"),
            new Tuple<string, string>("Oklahoma", "OK"),
            new Tuple<string, string>("Oregon", "OR"),
            new Tuple<string, string>("Pennsylvania", "PA"),
            new Tuple<string, string>("Rhode Island", "RI"),
            new Tuple<string, string>("South Carolina", "SC"),
            new Tuple<string, string>("South Dakota", "SD"),
            new Tuple<string, string>("Tennessee", "TN"),
            new Tuple<string, string>("Texas", "TX"),
            new Tuple<string, string>("Utah", "UT"),
            new Tuple<string, string>("Vermont", "VT"),
            new Tuple<string, string>("Virginia", "VA"),
            new Tuple<string, string>("Washington", "WA"),
            new Tuple<string, string>("West Virginia", "WV"),
            new Tuple<string, string>("Wisconsin", "WI"),
            new Tuple<string, string>("Wyoming", "WY"), 
        };

        public DojoController(IEntityRepository<Dojo, int> dojoRepository, ICacheService cacheService)
        {
            this.dojoRepository = dojoRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "DojosV1")]
        [ResponseType(typeof(SerializablePagination<Dojo>))]
        public async Task<IHttpActionResult> Get(int? pagenumber, [FromUri]int? federationId = null, [FromUri]string state = "")
        {
            try
            {
                var dojos = this.cacheService.TryGet<Expression<Func<Dojo, bool>>,
                                                     Func<IQueryable<Dojo>, IOrderedQueryable<Dojo>>,
                                                     string,
                                                     IEnumerable<Dojo>
                                                    >("Dojos", (x => x != null), null, "Address", this.dojoRepository.Get, null);

                if (dojos != null)
                {
                    if (federationId != null)
                    {
                        dojos = dojos.Where(d => d.FederationId == federationId);
                    }

                    if (!string.IsNullOrEmpty(state))
                    {
                        dojos = dojos.Where<Dojo>(d => d.Address != null && d.Address.State == state);
                    }

                    var dojoArray = dojos as Dojo[] ?? dojos.ToArray();

                    if (!dojoArray.Any())
                    {
                        // no dojos? Ok just return
                        return await Task.FromResult(this.Ok());
                    }

                    pagenumber = pagenumber.HasValue ? pagenumber : 0;

                    var result = await Task.FromResult((IHttpActionResult)
                        this.Ok(new SerializablePagination<Dojo>(dojoArray.ToList(), (int)pagenumber)));

                    return result;
                }
                return await Task.FromResult((IHttpActionResult)this.NotFound());
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("states", Name = "DojoStatesV1")]
        [ResponseType(typeof(SerializablePagination<Dojo>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var dojos = this.cacheService.TryGet<Expression<Func<Dojo, bool>>,
                                                     Func<IQueryable<Dojo>, IOrderedQueryable<Dojo>>,
                                                     string,
                                                     IEnumerable<Dojo>
                                                    >("Dojos", (x => x != null), null, "Address", this.dojoRepository.Get, null);

                if (dojos != null)
                {


                    var dojoArray = dojos as Dojo[] ?? dojos.ToArray();

                    if (!dojoArray.Any())
                    {
                        // no dojos? Ok just return
                        return await Task.FromResult(this.Ok());
                    }

                    var dojoStates = dojos.Where(d => d.Address != null).Select(d => d.Address.State).Distinct().ToList();
                    var statesList = states;

                    if (dojoStates.Count > 0)
                    {
                        statesList = states.Where(s => dojoStates.Contains(s.Item2)).ToList();
                    }

                    return await Task.FromResult((IHttpActionResult)this.Ok(statesList));
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