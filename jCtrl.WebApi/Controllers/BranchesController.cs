using jCtrl.Services;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("branches")]
    public class BranchesController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public BranchesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("1", Name = "GetSomething")]
        public IHttpActionResult Something()
        {
            return Ok("something");
        }

        [HttpGet]
        [Route("", Name = "GetBranches")]
        [ResponseType(typeof(List<BranchReturnModel>))]
        public async Task<IHttpActionResult> GetBranchesAsync([FromUri] int language = 1) // default to English
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var branches = new List<BranchReturnModel>();

            foreach (Branch b in await unitOfWork.Branches.GetBranches())
            {
                branches.Add(this.TheModelFactory.Create(b, language));
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchesAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(branches);
        }

        [Authorize(Roles = "system_admin,branches_author,branches_reviewer")]
        [HttpGet]
        [Route("{id:int}", Name = "GetBranchById")]
        [ResponseType(typeof(BranchReturnModel))]
        public async Task<IHttpActionResult> GetBranchByIdAsync([FromUri] int id, [FromUri] int language = -1)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var branch = await unitOfWork.Branches.GetBranch(id);

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

            if (branch != null)
            {
                if (language == -1)
                {
                    // use default language for branch                        
                    language = branch.Language_Id;
                }

                return Ok(this.TheModelFactory.Create(branch, language));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{siteCode:alpha:length(2)}", Name = "GetBranchByCode")]
        [ResponseType(typeof(BranchReturnModel))]
        public async Task<IHttpActionResult> GetBranchByCodeAsync([FromUri] string siteCode, [FromUri] int language = -1)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var branch = await unitOfWork.Branches.GetBranchByCode(siteCode);

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchByCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            if (branch != null)
            {
                if (language == -1)
                {
                    // use default language for branch                        
                    language = branch.Language_Id;
                }

                //return Ok(JsonConvert.SerializeObject(this.TheModelFactory.Create(branch, lang)));
                return Ok(this.TheModelFactory.Create(branch, language));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{siteCode:alpha:length(2)}/adverts", Name = "GetBranchAdvertsByBranchCode")]
        [ResponseType(typeof(List<BranchAdvertReturnModel>))]
        public async Task<IHttpActionResult> GetBranchAdvertsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int limit = 4)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var lst = new List<BranchAdvertReturnModel>();

            var query = unitOfWork.Branches.GetBranchAdvertsByCode(siteCode, true);

            lst.AddRange(
                this.TheModelFactory.Create(
                    await query
                    .Take(limit)
                    //.Include(a => a.Branch)                    
                    .ToListAsync()
                )
            );

            if (lst.Count < limit)
            {
                // get some non-priority adverts
                query = unitOfWork.Branches.GetBranchAdvertsByCode(siteCode, false);

                lst.AddRange(
                    this.TheModelFactory.Create(
                        await query
                        .Take(limit - lst.Count)
                        //.Include(a => a.Branch)                    
                        .ToListAsync()
                    )
                );
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchAdvertsByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(lst);
        }

        [Authorize(Roles = "system_admin,branches_author,branches_reviewer,customers_author,customers_reviewer")]
        [HttpGet]
        [Route("{siteCode:alpha:length(2)}/customers", Name = "GetCustomerListByBranchCode")]
        [ResponseType(typeof(PagedResultsReturnModel<CustomerListReturnModel>))]
        public async Task<IHttpActionResult> GetCustomerListByBranchCodeAsync([FromUri] string siteCode, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {

            if (pageSize > 100) return BadRequest("Max page size 100");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var query = unitOfWork.Branches.GetCustomersByBranchCode(siteCode);

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = this.TheModelFactory.Create(
                await query
                .Skip(() => pgSkip) // use lamdas to force paramatisation
                .Take(() => pgTake)
                .Include(c => c.EmailAddresses)
                .Include(c => c.PhoneNumbers.Select(p => p.PhoneNumberType))
                .ToListAsync()
            );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetCustomerListByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetCustomerListByBranchCode", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetCustomerListByBranchCode", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";


            return Ok(new PagedResultsReturnModel<CustomerListReturnModel>
            {
                PageSize = pageSize,
                PageCount = totalPages,
                PageNo = pageNo,
                ResultsCount = totalCount,
                Results = results,
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
                //LoadPageUrl = pageLink,
            });
        }

        [HttpGet]
        [Route("{siteCode:alpha:length(2)}/events", Name = "GetEventListByBranchCode")]
        [ResponseType(typeof(PagedResultsReturnModel<EventListReturnModel>))]
        public async Task<IHttpActionResult> GetEventsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");


            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var query = unitOfWork.Branches.GetEventsByBranchCode(siteCode);

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = this.TheModelFactory.Create(
                await query
                .Skip(() => pgSkip) // use lamdas to force paramatisation
                .Take(() => pgTake)
                .Include(e => e.EventDateTimes)
                .ToListAsync()
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetEventListByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetEventListByBranchCode", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetEventListByBranchCode", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<EventListReturnModel>
            {
                PageSize = pageSize,
                PageCount = totalPages,
                PageNo = pageNo,
                ResultsCount = totalCount,
                Results = results,
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
                //LoadPageUrl = pageLink,
            });
        }

        [Authorize(Roles = "system_admin,branches_author,branches_reviewer,orders_author,orders_reviewer")]
        [HttpGet]
        [Route("{siteCode:alpha:length(2)}/orders", Name = "GetWebOrderListByBranchCode")]
        [ResponseType(typeof(PagedResultsReturnModel<WebOrderListReturnModel>))]
        public async Task<IHttpActionResult> GetWebOrderListByBranchCodeAsync([FromUri] string siteCode, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {

            if (pageSize > 100) return BadRequest("Max page size 100");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var query = unitOfWork.Branches.GetWebOrdersByBranchCode(siteCode);

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = this.TheModelFactory.Create(
                await query
                    .Skip(() => pgSkip) // use lamdas to force paramatisation
                    .Take(() => pgTake)
                    .ToListAsync()
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetWebOrderListByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetWebOrderListByBranchCode", new { pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetWebOrderListByBranchCode", new { pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<WebOrderListReturnModel>
            {
                PageSize = pageSize,
                PageCount = totalPages,
                PageNo = pageNo,
                ResultsCount = totalCount,
                Results = results,
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
                //LoadPageUrl = pageLink,
            });
        }

        [HttpGet]
        [Route("{siteCode:alpha:length(2)}/products", Name = "GetProductListByBranchCode")]
        [ResponseType(typeof(PagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetProductListByBranchCodeAsync([FromUri] string siteCode, [FromUri]  int language = -1, [FromUri] int pageSize = 25, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var terms = "RN";

            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                    if (user != null)
                    {
                        if (user.Customer_Id != null)
                        {
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(),false,false);
                            if (cust != null)
                            {
                                terms = cust.TradingTerms_Code;

                                if (language == -1)
                                {
                                    language = cust.Language_Id;
                                }
                            }
                        }
                    }
                }
            }

            if (language == -1)
            {
                // use default language for branch
                var branch = await unitOfWork.Branches.GetBranchByCode(siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            var query = unitOfWork.Branches.GetProductsByBranchCode(siteCode);

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = this.TheModelFactory.Create(await query
                .Skip(() => pgSkip) // use lamdas to force paramatisation
                .Take(() => pgTake)
                .Include(p => p.Branch)
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductDetails.TextInfo)
                //.Include(p => p.ProductDetails.ProductBrand)
                //.Include(p => p.ProductDetails.ProductType)
                .Include(p => p.ProductDetails.Images)
                .Include(p => p.ProductDetails.Documents)
                .Include(p => p.ProductDetails.DiscountLevel)
                //.Include(p => p.ProductDetails.SupersessionList.Select(x => x.ReplacementProduct).Select(bp => bp.BranchProducts.Select(d => d.ProductDetails.DiscountLevel))).Where(b => b.Branch.SiteCode == siteCode)
                .Include(p => p.ProductDetails.SupersessionList.Select(x => x.ReplacementProduct).Select(bp => bp.BranchProducts.Select(t => t.ProductDetails.TextInfo))).Where(b => b.Branch.SiteCode == siteCode)
                .Include(p => p.SpecialOffers)
                .ToListAsync()
                ,
                terms,
                language
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetProductListByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetProductListByBranchCode", new { language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetProductListByBranchCode", new { language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<ProductListReturnModel>
            {
                PageSize = pageSize,
                PageCount = totalPages,
                PageNo = pageNo,
                ResultsCount = totalCount,
                Results = results,
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
                //LoadPageUrl = pageLink,
            });
        }
    }
}
