using jCtrl.Services;
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
    [RoutePrefix("admin/categories")]
    public class Admin_CategoriesController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public Admin_CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("products/recount", Name = "RecountCategoryProducts")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> RecountCategoryProductsAsync()
        {

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            await unitOfWork.Categories.RecountCategoryProductsAsync();

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("RecountCategoryProductsAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(true);

        }

    }
}
