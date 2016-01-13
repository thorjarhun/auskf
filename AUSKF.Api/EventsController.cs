namespace AUSKF.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;
    using Domain;
    using Domain.Attributes;
    using Domain.Collections;
    using Domain.Entities;
    using Domain.Repositories.Interfaces;
    using Domain.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/events")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventsController : ApiController
    {
        private readonly ICacheService cacheService;
        private readonly IEntityRepository<Event, Guid> eventRepository;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public EventsController(IEntityRepository<Event, Guid> eventRepository, ICacheService cacheService)
        {
            logger.Debug("EventsController created");
            this.eventRepository = eventRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "EventsV1")]
        [ResponseType(typeof (SerializablePagination<Event>))]
        public async Task<IHttpActionResult> Get(int? pagenumber)
        {
            try
            {
                var events = await this.cacheService.TryGetAsync<Expression<Func<Event, bool>>,
                    Func<IQueryable<Event>, IOrderedQueryable<Event>>, string,
                    IEnumerable<Event>>("Events", x => x != null, null, "", this.eventRepository.Get, null);

                var eventArray = events as Event[] ?? events.ToArray();

                if (!eventArray.Any())
                {
                    // no Events? Ok just return
                    return await Task.FromResult(this.Ok());
                }

                pagenumber = pagenumber.HasValue ? pagenumber : 0;

                return await Task.FromResult((IHttpActionResult) this.Ok(
                    new SerializablePagination<Event>(eventArray.ToList(), (int) pagenumber)));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{EventId}", Name = "GetEventById")]
        [ResponseType(typeof (Event))]
        public async Task<IHttpActionResult> Get(string eventId)
        {
            // Todo pass email or Eventname rather than Event id?
            try
            {
                Guid id;
                if (Guid.TryParse(eventId, out id))
                {
                    var Event = await this.eventRepository.GetAsync
                        (x => x.Id == id, includeProperties: "");

                    if (Event != null)
                    {
                        return this.Ok(Event);
                    }
                }
                else
                {
                    return this.BadRequest(Common.UnableToFindEvent);
                }
            }

            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
            return this.NotFound();
        }


        [Route("")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Post([FromBody] Event Event)
        {
            try
            {
                await this.eventRepository.InsertAsync(Event);
                return this.CreatedAtRoute("GetEventById", new {EventId = Event.Id}, Event);
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{EventId}")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Put(string eventId, [FromBody] Event Event)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(eventId, out id))
                {
                    await this.eventRepository.UpdateAsync(Event, id);
                    return this.StatusCode(HttpStatusCode.NoContent);
                }
                return this.BadRequest(Common.UnableToFindEvent);
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{EventId}")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Delete(string eventId, [FromBody] Event Event)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(eventId, out id))
                {
                    await this.eventRepository.DeleteAsync(Event);
                    return this.StatusCode(HttpStatusCode.NoContent);
                }
                logger.Warn(Common.UnableToFindEvent + " passed in parameter was " + id);
                // don't let them know that an error has occured on delete.
                return this.Ok();
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                // don't let them know that an error has occured on delete.
                return this.Ok();
            }
        }
    }
}