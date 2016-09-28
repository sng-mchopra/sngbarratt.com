using EFSecondLevelCache;
using jCtrl.Services;
using jCtrl.Services.Core.Configuration;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("{siteCode:alpha:length(2)}/products")]
    public class ProductsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetBranchProductByBranchCodeProductGuid")]
        [ResponseType(typeof(ProductReturnModel))]
        public async Task<IHttpActionResult> GetBranchProductByBranchCodeProductGuidAsync([FromUri] string siteCode, [FromUri] Guid id, [FromUri] int language = -1)
        {
            int branchId;
            try { branchId = (int)(Settings.SiteCodes)Enum.Parse(typeof(Settings.SiteCodes), siteCode.ToString()); } catch { return BadRequest("Invalid siteCode value"); }

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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), true,true);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.Id == branchId);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetBranchProducts(branchId, id);

            try
            {
                var prod = await query
                    .Include(p => p.Branch.TaxRates)
                    .Include(p => p.ProductDetails)
                    .Include(p => p.ProductDetails.TextInfo)
                    .Include(p => p.ProductDetails.ProductBrand)
                    .Include(p => p.ProductDetails.ProductType)
                    .Include(p => p.ProductDetails.Images)
                    .Include(p => p.ProductDetails.Documents)
                    .Include(p => p.ProductDetails.DiscountLevel)
                    .Include(p => p.ProductDetails.QuantityBreakDiscountLevels)
                    //.Include(p => p.ProductDetails.SupersessionList
                    //    .Select(x => x.ReplacementProduct)
                    //    .Select(rp => rp.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.TextInfo)
                    //    )
                    //)
                    //.Include(p => p.ProductDetails.SupersessionList
                    //    .Select(x => x.ReplacementProduct)
                    //    .Select(rp => rp.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.DiscountLevel)
                    //    )
                    //)
                    //.Include(p => p.ProductDetails.AlternativeProducts
                    //    .Select(x => x.AlternativeProduct)
                    //    .Select(ap => ap.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.TextInfo)
                    //    )
                    //)
                    //.Include(p => p.ProductDetails.AlternativeProducts
                    //    .Select(x => x.AlternativeProduct)
                    //    .Select(ap => ap.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.DiscountLevel)
                    //    )
                    //)
                    //.Include(p => p.ProductDetails.LinkedProducts
                    //    .Select(x => x.LinkedProduct)
                    //    .Select(lp => lp.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.TextInfo)
                    //    )
                    //)
                    //.Include(p => p.ProductDetails.LinkedProducts
                    //    .Select(x => x.LinkedProduct)
                    //    .Select(lp => lp.BranchProducts //.Where(bp => bp.Branch_Id == branchId)
                    //        .Select(bp => bp.ProductDetails.DiscountLevel)
                    //    )
                    //)       

                    //.Include(p => p.SpecialOffers)
                    .Cacheable()
                    .SingleOrDefaultAsync();

                if (prod != null)
                {
                    // get additional info - more db hits but quicker than adding as includes above
                    // caching and asnotracking causes an issue when the associated products are already in the cache
                    prod.ProductDetails.AlternativeProducts = await unitOfWork.Products.GetAlternativeProducts(prod.ProductDetails.Id);

                    prod.ProductDetails.LinkedProducts = await unitOfWork.Products.GetLinkedProducts(prod.ProductDetails.Id);

                    prod.ProductDetails.SupersessionList = await unitOfWork.Products.GetSupersessionProducts(prod.ProductDetails.Id);

                    prod.SpecialOffers = await unitOfWork.Products.GetProductOffers(id);

                    tmr.Stop();
                    System.Diagnostics.Debug.WriteLine("GetBranchProductByBranchCodeProductGuid took " + tmr.ElapsedMilliseconds + " ms");

                    return Ok(this.TheModelFactory.Create(prod, terms, language));
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
        [Route("", Name = "GetBranchProductsByBranchCodePartNumber")]
        [ResponseType(typeof(SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetBranchProductsByBranchCodePartNumberAsync([FromUri] string siteCode, [FromUri] string partNumber, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetBranchProductsByBranchCodePartNumber(siteCode, partNumber);

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
                .ToListAsync(),
                terms,
                language
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchProductsByBranchCodePartNumber took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchProductsByBranchCodePartNumber", new { partNumber = partNumber, language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchProductsByBranchCodePartNumber", new { partNumber = partNumber, language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";


            return Ok(new PagedResultsReturnModel<ProductListReturnModel>()
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
        [Route("", Name = "GetBranchProductsByBranchCodePhrase")]
        [ResponseType(typeof(SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetProductsByBranchCodePhraseAsync([FromUri] string siteCode, [FromUri] string phrase, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetProductsByBranchCodePhrase(siteCode, language, phrase);

            // TODO: rank results by relevance

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
                .ToListAsync(),
                terms,
                language
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchProductsByBranchCodePhrase took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchProductsByBranchCodePhrase", new { phrase = phrase, language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchProductsByBranchCodePhrase", new { phrase = phrase, language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<ProductListReturnModel>()
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
        [Route("clearance", Name = "GetBranchClearanceProductsByBranchCode")]
        [ResponseType(typeof(SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetBranchClearanceProductsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetBranchClearanceProductsByBranchCode(siteCode);

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
                .ToListAsync(),
                terms,
                language
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchClearanceProductsByBranchCode took " + tmr.ElapsedMilliseconds + " ms");


            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchClearanceProductsByBranchCode", new { language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchClearanceProductsByBranchCode", new { language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<ProductListReturnModel>()
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
        [Route("clearance/random", Name = "GetRandomBranchClearanceProductsByBranchCode")]
        [ResponseType(typeof(List<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetRandomBranchClearanceProductsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int limit = 10, [FromUri] int language = -1)
        {

            if (limit > 100) return BadRequest("Max results 100");

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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetRandomBranchClearanceProductsByBranchCode(siteCode);

            var results = this.TheModelFactory.Create(await query
                .Take(limit)
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
            System.Diagnostics.Debug.WriteLine("GetRandomBranchClearanceProductsByBranchCode took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(results);
        }

        [HttpGet]
        [Route("offers", Name = "GetBranchSpecialOfferProductsByBranchCode")]
        [ResponseType(typeof(SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetBranchSpecialOfferProductsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetBranchSpecialOfferProductsByBranchCode(siteCode);

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
                .ToListAsync(),
                terms,
                language
                );

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchSpecialOfferProductsByBranchCode took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchSpecialOfferProductsByBranchCode", new { language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchSpecialOfferProductsByBranchCode", new { language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<ProductListReturnModel>()
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
        [Route("offers/random", Name = "GetRandomSpecialOfferProductsByBranchCode")]
        [ResponseType(typeof(List<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetRandomSpecialOfferProductsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int limit = 10, [FromUri] int language = -1)
        {
            if (limit > 100) return BadRequest("Max results 100");

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
                            var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<BranchProduct> query = unitOfWork.Products.GetRandomSpecialOfferProductsByBranchCode(siteCode);

            var results = this.TheModelFactory.Create(await query
                .Take(limit)
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
            System.Diagnostics.Debug.WriteLine("GetRandomSpecialOfferProductsByBranchCode took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(results);
        }
    }
}
