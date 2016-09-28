using jCtrl.Services;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.WebApi.Models.Return;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("adverts")]
    public class AdvertsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public AdvertsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetAdvertById")]
        [ResponseType(typeof(AdvertReturnModel))]
        public async Task<IHttpActionResult> GetAdvertByIdAsync(Guid id)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            try
            {
                var advert = await unitOfWork.Adverts.GetAdvert(id);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetAdvertByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (advert != null)
                {
                    return Ok(this.TheModelFactory.Create(advert));
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
