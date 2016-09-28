using jCtrl.Services;
using jCtrl.Services.Core.Domain.Catalogue;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using System.Data.Entity;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("{siteCode:alpha:length(2)}/catalogues")]
    public class CataloguesController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public CataloguesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("", Name = "GetCatalogueFamilies")]
        [ResponseType(typeof(List<CatalogueFamilyReturnModel>))]
        public async Task<IHttpActionResult> GetCatalogueFamiliesAsync([FromUri] string siteCode, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
        {
            if (pageSize > 100) return BadRequest("Max page size 100");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var terms = "RN";

            var identity = User.Identity;
            if (identity != null)
            {
                if (identity.IsAuthenticated)
                {
                    // get the linked customer account
                    var user = await this.AppUserManager.FindByNameAsync(identity.Name);

                    if (user != null)
                    {
                        var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(),false,false);
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

            if (language == -1)
            {
                // use default language for branch
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<CatalogueFamily> query = unitOfWork.CatalogueFamilies.GetCatalogueFamilies();

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = new List<CatalogueFamilyReturnModel>();
            foreach (CatalogueFamily f in await query
                  .Skip(() => pgSkip) // use lamdas to force paramatisation
                .Take(() => pgTake)
                .Include(f => f.Titles)
                .Include(f => f.Models)
                .Include(f => f.Models.Select(m => m.Titles))
                .ToListAsync())
            {
                results.Add(this.TheModelFactory.Create(f, terms, language));
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchCatalogueFamiliesAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchCatalogueFamilies", new { language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchCatalogueFamilies", new { language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<CatalogueFamilyReturnModel>
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

        [Route("{id:int}", Name = "GetCatalogueFamilyById")]
        [ResponseType(typeof(CatalogueFamilyReturnModel))]
        public async Task<IHttpActionResult> GetCatalogueFamilyByIdAsync([FromUri] string siteCode, [FromUri] int id, [FromUri] int language = -1)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var terms = "RN";

            var identity = User.Identity;
            if (identity != null)
            {
                if (identity.IsAuthenticated)
                {
                    // get the linked customer account
                    var user = await this.AppUserManager.FindByNameAsync(identity.Name);

                    if (user != null)
                    {
                        var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(),false,false);
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

            if (language == -1)
            {
                // use default language for branch
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            try
            {
                var family = await unitOfWork.CatalogueFamilies.GetCatalogueFamily(id);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetBranchCatalogueFamilyByIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (family != null)
                {
                    return Ok(this.TheModelFactory.Create(family, terms, language));
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

        [Route("{id:int}/models", Name = "GetCatalogueModelsByFamilyId")]
        [ResponseType(typeof(List<CatalogueModelReturnModel>))]
        public async Task<IHttpActionResult> GetCatalogueModelsByFamilyIdAsync([FromUri] string siteCode, [FromUri] int id, [FromUri] int language = -1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
        {

            if (pageSize > 100) return BadRequest("Max page size 100");

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var terms = "RN";

            var identity = User.Identity;
            if (identity != null)
            {
                if (identity.IsAuthenticated)
                {
                    // get the linked customer account
                    var user = await this.AppUserManager.FindByNameAsync(identity.Name);

                    if (user != null)
                    {
                        var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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

            if (language == -1)
            {
                // use default language for branch
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            IQueryable<CatalogueModel> query = unitOfWork.CatalogueModels.GetCatalogueModelsByFamily(id);

            var totalCount = query.Count();

            var pgSkip = (int)(pageSize * (pageNo - 1));
            var pgTake = (int)(pageSize);

            var results = new List<CatalogueModelReturnModel>();
            foreach (CatalogueModel m in await query
                .Skip(() => pgSkip) // use lamdas to force paramatisation
                .Take(() => pgTake)
                .Include(m => m.Titles)
                .ToListAsync())
            {
                results.Add(this.TheModelFactory.Create(m, terms, language));
            }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetBranchCatalogueModelsByFamilyIdAsync took " + tmr.ElapsedMilliseconds + " ms");

            var totalPages = (int)Math.Ceiling((double)(totalCount / pageSize));
            if (totalPages < 1) { totalPages = 1; }

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetBranchCatalogueModelsByFamilyId", new { id = id, language = language, pageNo = pageNo - 1, pageSize = pageSize }) : "";
            var nextLink = pageNo < totalPages ? urlHelper.Link("GetBranchCatalogueModelsByFamilyId", new { id = id, language = language, pageNo = pageNo + 1, pageSize = pageSize }) : "";

            return Ok(new PagedResultsReturnModel<CatalogueModelReturnModel>
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

        [Route("{familyId:int}/models/{modelId:int}", Name = "GetCatalogueModelByFamilyIdModelId")]
        [ResponseType(typeof(List<CatalogueModelReturnModel>))]
        public async Task<IHttpActionResult> GetCatalogueModelByFamilyIdModelIdAsync([FromUri] string siteCode, [FromUri] int familyId, [FromUri] int modelId, [FromUri] int language = -1)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var terms = "RN";

            var identity = User.Identity;
            if (identity != null)
            {
                if (identity.IsAuthenticated)
                {
                    // get the linked customer account
                    var user = await this.AppUserManager.FindByNameAsync(identity.Name);

                    if (user != null)
                    {
                        var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
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

            if (language == -1)
            {
                // use default language for branch
                var branch = await unitOfWork.Branches.SingleOrDefault(b => b.SiteCode == siteCode);
                if (branch != null) { language = branch.Language_Id; }
            }

            try
            {
                var model = await unitOfWork.CatalogueModels.GetCatalogueModelByFamilyIdModelId(familyId, modelId);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetBranchCatalogueModelByFamilyIdModelIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (model != null)
                {
                    return Ok(this.TheModelFactory.Create(model, terms, language));
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

        [Route("{familyId:int}/models/{modelId:int}/assemblies/{assemblyId:int}", Name = "GetCatalogueAssemblyByFamilyIdModelIdAssemblyId")]
        [ResponseType(typeof(CatalogueAssemblyReturnModel))]
        public async Task<IHttpActionResult> GetCatalogueAssemblyByFamilyIdModelIdAssemblyIdAsync([FromUri] string siteCode, [FromUri] int familyId, [FromUri] int modelId, [FromUri] int assemblyId, [FromUri] int language = -1)
        {
            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            var branch = await unitOfWork.Branches.GetBranchByCode(siteCode);

            if (branch == null) { return BadRequest("Branch not found"); }

            var terms = "RN";

            //// if user is signed in get trading level
            var identity = User.Identity;
            if (identity != null)
            {
                if (identity.IsAuthenticated)
                {
                    // get the linked customer account
                    var user = await this.AppUserManager.FindByNameAsync(identity.Name);

                    if (user != null)
                    {
                        var cust = await unitOfWork.Customers.GetCustomer(user.Customer_Id.GetValueOrDefault(), false, false);
                        {
                            terms = cust.TradingTerms_Code;

                            // use default language for customer if language not provided
                            if (language == -1)
                            {
                                language = cust.Language_Id;
                            }
                        }
                    }
                }
            }

            // use default language for branch if language not provided
            if (language == -1) { language = branch.Language_Id; }

            var apps = unitOfWork.CatalogueApplications.GetCatalogueApplicationsByAssemblyIdFamilyId(assemblyId, familyId, modelId);

            if (!apps.Any())
            {
                // unknown assembly
                // invalid family, model, assembly combo
                // inactive path element
                return NotFound();
            }

            try
            {
                var assembly = await unitOfWork.CatalogueAssemblies.GetCatalogueAssembly(assemblyId);

                tmr.Stop();
                System.Diagnostics.Debug.WriteLine("GetBranchCatalogueAssemblyByFamilyIdModelIdAssemblyIdAsync took " + tmr.ElapsedMilliseconds + " ms");

                if (assembly != null)
                {
                    return Ok(this.TheModelFactory.Create(familyId, modelId, assembly, terms, language));
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
