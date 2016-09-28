using jCtrl.Services;
using jCtrl.Services.Core.Configuration;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Repositories;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;

namespace jCtrl.WebApi.Controllers
{
    [RoutePrefix("{siteCode:alpha:length(2)}/categories/upgrades")]
    public class CategoryUpgradePartsController : BaseApiController
    {
        private IUnitOfWork unitOfWork;

        public CategoryUpgradePartsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("products", Name = "GetUpgradeBranchProductsByBranchCode")]
        [ResponseType(typeof(jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetUpgradeBranchProductsByBranchCodeAsync([FromUri] string siteCode, [FromUri] int language = -1, [FromUri] int sortOrder = 1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
        {
            if (!Settings.PageSizes.Contains(pageSize)) { return BadRequest("Invalid pageSize value"); }

            Settings.ProductListSortOptions orderBy;
            try { orderBy = (Settings.ProductListSortOptions)Enum.Parse(typeof(Settings.ProductListSortOptions), sortOrder.ToString()); } catch { return BadRequest("Invalid sortOrder value"); }

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

                            var cust = await unitOfWork.Customers .GetCustomer((Guid)user.Customer_Id, false, false);
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
                language = await unitOfWork.Branches.GetBranchLanguageIdAsync(branchId);
            }

            jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<BranchProduct> results = await unitOfWork.Categories.GetProductsByCategoryAsync(branchId, Settings.CategoryTypes.Upgrades, null, orderBy, pageSize, pageNo, language);

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetUpgradeProductsByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetUpgradeProductsByBranchCode", new { sort = sortOrder, pageSize = pageSize, pageNo = pageNo - 1 }) : "";
            var nextLink = pageNo < results.PageCount ? urlHelper.Link("GetUpgradeProductsByBranchCode", new { sort = sortOrder, pageSize = pageSize, pageNo = pageNo + 1 }) : "";

            return Ok(new jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<ProductListReturnModel>()
            {
                PageSizes = results.PageSizes,
                PageSize = results.PageSize,
                PageCount = results.PageCount,
                PageNo = results.PageNo,
                SortedBy = results.SortedBy,
                SortOptions = results.SortOptions,
                ResultsCount = results.ResultsCount,
                Results = this.TheModelFactory.Create(results.Results.ToList(), terms, language),
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
            });

        }


        [Route("", Name = "GetUpgradeCategoriesByBranchCode")]
        [ResponseType(typeof(List<CategoryReturnModel>))]
        public async Task<IHttpActionResult> GetUpgradeCategoriesByBranchCodeAsync([FromUri] string siteCode, [FromUri] int? parent = null, [FromUri] int language = -1)
        {
            int branchId;
            try { branchId = (int)(Settings.SiteCodes)Enum.Parse(typeof(Settings.SiteCodes), siteCode.ToString()); } catch { return BadRequest("Invalid siteCode value"); }

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            if (language == -1)
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                        if (user != null)
                        {
                            if (user.Customer_Id != null)
                            {

                                var cust = await unitOfWork.Customers.GetCustomer((Guid)user.Customer_Id, false, false);
                                if (cust != null) { language = cust.Language_Id; }
                            }
                        }
                    }
                }
                if (language == -1)
                {
                    // use default language for branch
                    language = await unitOfWork.Branches.GetBranchLanguageIdAsync(branchId);
                }
            }

            List<Category> lst;

            if (parent == null) { lst = await unitOfWork.Categories.GetRootCategoriesByTypeAsync(Settings.CategoryTypes.Upgrades); }
            else { lst = await unitOfWork.Categories.GetSubCategoriesByParentAsync(Settings.CategoryTypes.Upgrades, (int)parent); }

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetUpgradeCategoriesByBranchCodeAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(this.TheModelFactory.Create(lst, language));
        }


        [Route("{id:int}", Name = "GetUpgradeCategoryByBranchCodeCategoryId")]
        [ResponseType(typeof(CategoryReturnModel))]
        public async Task<IHttpActionResult> GetUpgradeCategoryByBranchCodeCategoryIdAsync([FromUri] string siteCode, [FromUri] int id, [FromUri] int language = -1)
        {
            int branchId;
            try { branchId = (int)(Settings.SiteCodes)Enum.Parse(typeof(Settings.SiteCodes), siteCode.ToString()); } catch { return BadRequest("Invalid siteCode value"); }

            var tmr = new System.Diagnostics.Stopwatch();
            tmr.Start();

            if (language == -1)
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

                        if (user != null)
                        {
                            if (user.Customer_Id != null)
                            {

                                var cust = await unitOfWork.Customers.GetCustomer((Guid)user.Customer_Id, false, false);
                                if (cust != null) { language = cust.Language_Id; }
                            }
                        }
                    }
                }
                if (language == -1)
                {
                    // use default language for branch
                    language = await unitOfWork.Branches.GetBranchLanguageIdAsync(branchId);
                }
            }

            Category cat = await unitOfWork.Categories.GetCategoryAsync(Settings.CategoryTypes.Upgrades, id);

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetUpgradeCategoriesByBranchCodeCategoryIdAsync took " + tmr.ElapsedMilliseconds + " ms");

            return Ok(this.TheModelFactory.Create(cat, language));
        }


        [Route("{id:int}/products", Name = "GetUpgradeCategoryBranchProductsByBranchCodeCategoryId")]
        [ResponseType(typeof(jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<ProductListReturnModel>))]
        public async Task<IHttpActionResult> GetUpgradeCategoryBranchProductsByBranchCodeCategoryIdAsync([FromUri] string siteCode, [FromUri] int id, [FromUri] int language = -1, [FromUri] int sortOrder = 1, [FromUri] int pageSize = 10, [FromUri] int pageNo = 1)
        {
            if (!Settings.PageSizes.Contains(pageSize)) { return BadRequest("Invalid pageSize value"); }

            Settings.ProductListSortOptions orderBy;
            try { orderBy = (Settings.ProductListSortOptions)Enum.Parse(typeof(Settings.ProductListSortOptions), sortOrder.ToString()); } catch { return BadRequest("Invalid sortOrder value"); }

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

                            var cust = await unitOfWork.Customers.GetCustomer((Guid)user.Customer_Id, false,false);
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
                language = await unitOfWork.Branches.GetBranchLanguageIdAsync(branchId);
            }

            jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<BranchProduct> results = await unitOfWork.Categories.GetProductsByCategoryAsync(branchId, Settings.CategoryTypes.Upgrades, id, orderBy, pageSize, pageNo, language);

            tmr.Stop();
            System.Diagnostics.Debug.WriteLine("GetUpgradeCategoryProductsByBranchCodeCategoryId took " + tmr.ElapsedMilliseconds + " ms");

            var urlHelper = new UrlHelper(Request);
            var prevLink = pageNo > 1 ? urlHelper.Link("GetUpgradeProductsByBranchCode", new { sort = sortOrder, pageSize = pageSize, pageNo = pageNo - 1 }) : "";
            var nextLink = pageNo < results.PageCount ? urlHelper.Link("GetUpgradeProductsByBranchCode", new { sort = sortOrder, pageSize = pageSize, pageNo = pageNo + 1 }) : "";

            return Ok(new jCtrl.Services.Core.Repositories.SortedPagedResultsReturnModel<ProductListReturnModel>()
            {
                PageSizes = results.PageSizes,
                PageSize = results.PageSize,
                PageCount = results.PageCount,
                PageNo = results.PageNo,
                SortedBy = results.SortedBy,
                SortOptions = results.SortOptions,
                ResultsCount = results.ResultsCount,
                Results = this.TheModelFactory.Create(results.Results.ToList(), terms, language),
                PrevPageUrl = prevLink,
                NextPageUrl = nextLink
            });
        }
    }
}
