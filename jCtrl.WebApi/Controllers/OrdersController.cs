using jCtrl.Services;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace jCtrl.WebApi.Controllers
{
    [Authorize(Roles = "system_admin,orders_author,orders_reviewer")]
    [RoutePrefix("orders")]
    public class OrdersController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetWebOrderById")]
        [ResponseType(typeof(WebOrderReturnModel))]
        public async Task<IHttpActionResult> GetWebOrderByIdAsync([FromUri] Guid id)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            try
            {
                var order = await unitOfWork.WebOrders.GetOrder(id);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetWebOrderByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (order != null)
                {
                    return Ok(this.TheModelFactory.Create(order));
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
    }
}
