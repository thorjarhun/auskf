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
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/logs")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        private readonly IEntityRepository<Log, Guid> logRepository;
        private readonly ICacheService cacheService;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LogController(IEntityRepository<Log, Guid> logRepository, ICacheService cacheService)
        {
            this.logRepository = logRepository;
            this.cacheService = cacheService;
            logger.Debug("EventsController created");
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "LogsV1")]
        [ResponseType(typeof(SerializablePagination<Log>))]
        public async Task<IHttpActionResult> Get(int? pagenumber)
        {
            try
            {
                var logs = await this.cacheService.TryGetAsync<Expression<Func<Log, bool>>,
                    Func<IQueryable<Log>, IOrderedQueryable<Log>>, string,
                    IEnumerable<Log>>("Logs", x => x != null, null, "", this.logRepository.Get, null);

                var logArray = logs as Log[] ?? logs.ToArray();

                if (!logArray.Any())
                {
                    // no Events? Ok just return
                    return await Task.FromResult(this.Ok());
                }

                pagenumber = pagenumber.HasValue ? pagenumber : 0;

                return await Task.FromResult((IHttpActionResult)this.Ok(
                    new SerializablePagination<Log>(logArray.ToList(), (int)pagenumber)));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }
    }
}