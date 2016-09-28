using jCtrl.Services;
using jCtrl.Services.Core.Domain;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("events")]
    public class EventsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetEventById")]
        [ResponseType(typeof(EventReturnModel))]
        public async Task<IHttpActionResult> GetEventByIdAsync(Guid id)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            try
            {
                var evt = unitOfWork.Events.GetEvent(id);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetEventByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (evt != null)
                {
                    return Ok(this.TheModelFactory.Create(evt.Result));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    System.Diagnostics.Debug.WriteLine("Inner Exception:");
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("", Name = "GetEventListByDateRange")]
        [ResponseType(typeof(List<EventListReturnModel>))]
        public async Task<IHttpActionResult> GetEventListByDateRangeAsync(DateTime start, DateTime end)
        {
            if (start > end) return BadRequest("End date must be greater than Start date");
            if ((end - start).TotalDays > 90) return BadRequest("Max date range 90 days");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var query = unitOfWork.Events.GetEventsByDateRange(start, end);

            var totalCount = query.Count();

            var results = this.TheModelFactory.Create(
                await query
                .Include(e => e.Branch)
                .Include(e => e.EventDateTimes)
                .ToListAsync()
            );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetEventListByDateRangeAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(results);
        }

        [HttpGet]
        [Route("", Name = "GetEventListUpcoming")]
        [ResponseType(typeof(List<EventListReturnModel>))]
        public async Task<IHttpActionResult> GetEventListUpcomingAsync([FromUri] int limit = 3)
        {

            if (limit > 10) return BadRequest("Max result limit 10");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var query = unitOfWork.Events.GetUpcomingEvents();

            var results = this.TheModelFactory.Create(
                await query
                .Take(limit)
                .Include(e => e.Branch)
                .Include(e => e.EventDateTimes)
                .ToListAsync()
            );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetEventListUpcomingAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(results);
        }
    }
}
