using AutoMapper;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Catalogue;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Domain.Payment;
using jCtrl.Services.Core.Domain.Product;
using jCtrl.Services.Core.Domain.Shipping;
using jCtrl.Services.Core.Domain.Vehicle;
using jCtrl.Services.Core.Domain.WishList;
using jCtrl.WebApi.App_Start;
using jCtrl.WebApi.Infrastructure;
using jCtrl.WebApi.Models.Return;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace jCtrl.WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;
        private string _CdnHost;

        public ModelFactory()
        {
            _CdnHost = ConfigurationManager.AppSettings["Cdn_Host"];
        }

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
            _CdnHost = ConfigurationManager.AppSettings["Cdn_Host"];
        }

        // advert
        public AdvertReturnModel Create(Advert advert)
        {
            return new AdvertReturnModel
            {
                Id = advert.Id,
                BranchId = advert.Branch_Id,
                TypeId = advert.AdvertType_Id,
                Title = advert.Title,
                Description = advert.Description,
                ImageFilename_Desktop = advert.ImageFilename_Desktop,
                ImageFilename_Device = advert.ImageFilename_Device,
                LinkUrl = advert.LinkUrl,
                PlayerId = advert.PlayerId,
                VideoId = advert.VideoId,
                Priority = advert.IsPriority,
                ExpiresUtc = advert.ExpiresUtc,
                Active = advert.IsActive
            };
        }

        // address lookup
        public AddressLookupReturnModel Create(jCtrl.Services.External.AddressChecker.VerifiedAddress address)
        {
            var s = address.AVC.Substring(address.AVC.LastIndexOf("-"));

            var addr = new AddressLookupReturnModel
            {
                AddressLine1 = address.Address1,
                AddressLine2 = address.Address2,
                TownCity = address.Locality,
                CountyState = address.AdministrativeArea,
                PostalCode = address.PostalCode,
                Country = address.CountryName,
                CountryCode = address.ISO3166_2,
                QualityIndex = address.AQI,
                MatchScore = Int16.Parse(s)
            };

            if (addr.AddressLine2 == addr.TownCity) addr.AddressLine2 = null;

            return addr;
        }

        // branch
        public BranchReturnModel Create(Branch branch, int languageId)
        {
            var b = new BranchReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetBranchByCode", new { }) : null,
                BranchId = branch.Id,
                BranchCode = branch.SiteCode,
                Name = branch.Name,
                Address = new AddressReturnModel
                {
                    AddressLine1 = branch.AddressLine1,
                    AddressLine2 = branch.AddressLine2,
                    TownCity = branch.TownCity,
                    CountyState = branch.CountyState,
                    PostalCode = branch.PostalCode,
                    Country = branch.CountryName,
                    CountryCode = branch.Country_Code
                },
                Phone = branch.PhoneNumber,
                Email = branch.EmailAddress,
                CurrenyCode = branch.Currency_Code,
                FlagUrl = branch.FlagFilename,
                Latitude = branch.Latitude,
                Longitude = branch.Longitude
            };

            if (branch.Currency != null) { b.CurrencySymbol = branch.Currency.Symbol; }

            if (branch.TaxRates != null) { b.TaxRates = this.Create(branch.TaxRates); }

            if (branch.OpeningTimes != null) { b.OpeningTimes = this.Create(branch.OpeningTimes); }

            if (branch.Introductions != null) { b.Introduction = this.Create(branch.Introductions.ToList(), languageId); }

            return b;
        }

        // branch tax rates
        public List<TaxRateReturnModel> Create(ICollection<BranchTaxRate> rates)
        {
            var lst = new List<TaxRateReturnModel>();

            foreach (BranchTaxRate taxRate in rates)
            {
                lst.Add(new TaxRateReturnModel()
                {
                    TaxCategory = taxRate.TaxRateCategory_Id,
                    Rate = taxRate.Rate
                });
            }

            return lst;
        }

        // branch opening times
        public List<OpeningTimeReturnModel> Create(ICollection<BranchOpeningTime> times)
        {
            var lst = new List<OpeningTimeReturnModel>();

            foreach (BranchOpeningTime time in times)
            {
                lst.Add(new OpeningTimeReturnModel()
                {
                    Day = time.Day,
                    OpensUtc = time.OpensUtc,
                    ClosesUtc = time.ClosesUtc
                });
            }

            return lst;
        }

        // branch advert list
        public List<BranchAdvertReturnModel> Create(List<Advert> adverts)
        {
            var lst = new List<BranchAdvertReturnModel>();

            foreach (Advert advert in adverts)
            {
                lst.Add(
                    new BranchAdvertReturnModel()
                    {
                        Id = advert.Id,
                        Type = advert.AdvertType_Id,
                        Title = advert.Title,
                        Description = advert.Description,
                        ImageUrl_Desktop = _CdnHost + "/adverts/" + advert.ImageFilename_Desktop,
                        ImageUrl_Device = _CdnHost + "/adverts/" + advert.ImageFilename_Device,
                        LinkUrl = advert.LinkUrl,
                        PlayerId = advert.PlayerId,
                        VideoId = advert.VideoId
                    }
                );
            }

            return lst;
        }



        // cart
        public CartReturnModel Create(Customer customer, List<CartItem> items)
        {

            var cart = new CartReturnModel()
            {
                Site = customer.Branch.SiteCode,
                CartTotal = 0,
                ExpressCheckout = false,
                ItemCount = 0,
                Items = new List<CartItemReturnModel>()
            };

            if (customer.ShippingMethod_Id != null && customer.DefaultShippingAddress != null)
            {
                cart.ExpressCheckout = true;
            }

            if (items.Any())
            {
                foreach (CartItem item in items)
                {
                    cart.CartTotal += item.LineTotal();
                    cart.ItemCount++;
                    cart.Items.Add(this.Create(item));
                }
            }

            return cart;
        }

        // cart item
        public CartItemReturnModel Create(CartItem item)
        {
            var itm = new CartItemReturnModel
            {
                Id = item.Id,
                ProductId = item.BranchProduct_Id,
                PartNumber = item.PartNumber,
                Title = item.PartTitle,
                Status = "NAO",
                RetailPrice = item.RetailPrice,
                UnitPrice = item.UnitPrice,
                Surcharge = item.Surcharge,
                Quantity = item.QuantityRequired,
                LineTotal = item.LineTotal_ExcludingSurcharge(),
                CreatedTimestamp = item.CreatedTimestampUtc
            };

            if (item.BranchProduct != null)
            {

                // set current stock status
                itm.Status = item.BranchProduct.StockStatus();

                if (item.BranchProduct.ProductDetails != null)
                {

                    if (item.BranchProduct.ProductDetails.Images != null)
                    {
                        if (item.BranchProduct.ProductDetails.DefaultProductImage != null)
                        {
                            itm.ImageUrl = this.Create(item.BranchProduct.ProductDetails.DefaultProductImage).ImageUrl_Thumbnail;
                        }
                    }
                    // use placeholder image
                    if (itm.ImageUrl == null) { itm.ImageUrl = this.Create(new ProductImage()).ImageUrl_Thumbnail; }
                }

                if (item.BranchProduct.Branch != null)
                {
                    if (item.BranchProduct.Branch.TaxRates != null)
                    {
                        // calc price inc tax
                        var tax = item.BranchProduct.Branch.TaxRates.Single(r => r.TaxRateCategory_Id == item.BranchProduct.ProductDetails.TaxRateCategory_Id);

                        itm.TaxAmount = Math.Round(itm.UnitPrice * (tax.Rate / 100), 2);
                        itm.TaxPrice = itm.UnitPrice + itm.TaxAmount;
                    }
                }
            }

            return itm;
        }

        #region "Catalogue - Find Parts"

        // catalogue family
        public CatalogueFamilyReturnModel Create(CatalogueFamily family, string tradingLevel, int languageId)
        {
            var fam = new CatalogueFamilyReturnModel()
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueFamilyById", new { id = family.Id, language = languageId }) : null,
                FamilyId = family.Id,
                StartYear = family.StartYear,
                EndYear = family.EndYear
            };

            if (family.Titles != null)
            {
                if (family.Titles.Any())
                {

                    var title = family.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = family.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        fam.Title = title.Title;
                    }

                }
            }

            if (family.Models != null)
            {
                fam.Models = new List<CatalogueModelReturnModel>();

                foreach (CatalogueModel m in family.Models)
                {
                    fam.Models.Add(this.Create(m, tradingLevel, languageId));
                }
            }

            return fam;
        }

        // catalogue model
        public CatalogueModelReturnModel Create(CatalogueModel model, string tradingLevel, int languageId)
        {
            var mdl = new CatalogueModelReturnModel()
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueModelByFamilyIdModelId", new { familyId = model.Family_Id, modelId = model.Id, language = languageId }) : null,
                ModelId = model.Id,
                StartYear = model.StartYear,
                EndYear = model.EndYear
            };

            if (model.Titles != null)
            {
                if (model.Titles.Any())
                {

                    var title = model.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = model.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        mdl.Title = title.Title;
                    }

                }
            }

            if (model.Applications != null)
            {
                mdl.Categories = this.Create(model.Applications, model.Family_Id, model.Id, tradingLevel, languageId);
            }



            return mdl;
        }

        // catalogue category
        public CatalogueCategoryTreeReturnModel Create(CatalogueCategory category, string tradingLevel, int languageId)
        {
            var cat = new CatalogueCategoryTreeReturnModel { CategoryId = category.Id };

            if (category.Titles != null)
            {
                if (category.Titles.Any())
                {
                    var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        cat.Title = title.Title;
                    }

                }
            }

            return cat;
        }

        // catalogue application path - catalogue category tree
        public ICollection<CatalogueCategoryTreeReturnModel> Create(ICollection<CatalogueApplication> applications, int familyId, int modelId, string tradingLevel, int languageId)
        {
            var lst = new List<CatalogueCategoryTreeReturnModel>();

            foreach (CatalogueCategory cat in applications.Select(a => a.Category).Distinct())
            {
                var category = this.Create(cat, tradingLevel, languageId);
                category.ModelId = modelId;
                category.SubCategories = new List<CatalogueCategoryTreeReturnModel>();


                foreach (CatalogueCategory sec in applications.Where(a => a.Category_Id == category.CategoryId).Select(a => a.Section).Distinct())
                {
                    if (sec.Id == category.CategoryId)
                    {
                        // same as parent
                        var assemblyId = applications.Where(a => a.Category_Id == category.CategoryId
                            && a.Section_Id == category.CategoryId
                            && a.SubSection_Id == category.CategoryId)
                            .Select(a => a.Assembly_Id)
                            .FirstOrDefault();

                        category.AssemblyUrl = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueAssemblyByFamilyIdModelIdAssemblyId", new { familyId = familyId, modelId = modelId, assemblyId = assemblyId, language = languageId }) : null;

                    }
                    else
                    {

                        var section = this.Create(sec, tradingLevel, languageId);
                        section.ModelId = modelId;
                        section.SubCategories = new List<CatalogueCategoryTreeReturnModel>();

                        foreach (CatalogueCategory sub in applications.Where(a => a.Category_Id == category.CategoryId && a.Section_Id == section.CategoryId).Select(a => a.SubSection).Distinct())
                        {
                            if (sub.Id == section.CategoryId)
                            {
                                // same as parent
                                var assemblyId = applications.Where(a => a.Category_Id == category.CategoryId
                                    && a.Section_Id == section.CategoryId
                                    && a.SubSection_Id == section.CategoryId)
                                    .Select(a => a.Assembly_Id)
                                    .FirstOrDefault();

                                section.AssemblyUrl = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueAssemblyByFamilyIdModelIdAssemblyId", new { familyId = familyId, modelId = modelId, assemblyId = assemblyId, language = languageId }) : null;

                            }
                            else
                            {

                                var subSection = this.Create(sub, tradingLevel, languageId);
                                subSection.ModelId = modelId;

                                var assemblyId = applications.Where(a => a.Category_Id == category.CategoryId
                                    && a.Section_Id == section.CategoryId
                                    && a.SubSection_Id == subSection.CategoryId)
                                    .Select(a => a.Assembly_Id)
                                    .FirstOrDefault();

                                subSection.AssemblyUrl = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueAssemblyByFamilyIdModelIdAssemblyId", new { familyId = familyId, modelId = modelId, assemblyId = assemblyId, language = languageId }) : null;

                                section.SubCategories.Add(subSection);
                            }
                        }

                        category.SubCategories.Add(section);
                    }

                }

                lst.Add(category);
            }

            return lst;
        }

        // catalogue assembly
        public CatalogueAssemblyReturnModel Create(int familyId, int modelId, CatalogueAssembly assembly, string tradingLevel, int languageId)
        {
            var ass = new CatalogueAssemblyReturnModel()
            {
                AssemblyId = assembly.Id,
                Url = _UrlHelper != null ? _UrlHelper.Link("GetCatalogueAssemblyByFamilyIdModelIdAssemblyId", new { familyId = familyId, modelId = modelId, assemblyId = assembly.Id, language = languageId }) : null
            };

            if (assembly.Titles != null)
            {
                if (assembly.Titles.Any())
                {
                    var title = assembly.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = assembly.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        ass.Title = title.Title;
                    }

                }
            }

            if (assembly.Nodes != null)
            {
                if (assembly.Nodes.Any())
                {
                    ass.Nodes = new List<CatalogueAssemblyNodeReturnModel>();

                    foreach (CatalogueAssemblyNode n in assembly.Nodes)
                    {
                        ass.Nodes.Add(this.Create(n, tradingLevel, languageId));
                    }
                }
            }

            if (assembly.Illustration != null)
            {
                ass.Illustration = this.Create(assembly.Illustration);
            }

            return ass;
        }

        // catalogue illustration        
        public CatalogueIllustrationReturnModel Create(CatalogueAssemblyIllustration illustration)
        {
            return new CatalogueIllustrationReturnModel()
            {
                ImageUrl_Thumbnail = _CdnHost + "/illustrations/" + illustration.Filename_Thb,
                ImageUrl_Small = _CdnHost + "/illustrations/" + illustration.Filename_Sml,
                ImageUrl_Medium = _CdnHost + "/illustrations/" + illustration.Filename_Med,
                ImageUrl_Large = _CdnHost + "/illustrations/" + illustration.Filename_Lrg,
                ImageUrl_Full = _CdnHost + "/illustrations/" + illustration.Filename
            };

        }

        // catalogue node
        public CatalogueAssemblyNodeReturnModel Create(CatalogueAssemblyNode node, string tradingLevel, int languageId)
        {
            var nd = new CatalogueAssemblyNodeReturnModel { AssemblyNodeId = node.Id, AnnotationRef = node.AnnotationRef };

            if (node.Titles != null)
            {
                if (node.Titles.Any())
                {
                    var title = node.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = node.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        nd.Title = title.Title;
                    }
                }
            }

            if (node.Children != null)
            {
                if (node.Children.Any())
                {
                    nd.Children = new List<CatalogueAssemblyNodeReturnModel>();
                    foreach (CatalogueAssemblyNode n in node.Children)
                    {
                        nd.Children.Add(this.Create(n, tradingLevel, languageId));
                    }
                }
            }

            if (node.Products != null)
            {
                if (node.Products.Any())
                {
                    nd.Products = new List<CatalogueProductReturnModel>();

                    foreach (CatalogueAssemblyNodeProduct p in node.Products)
                    {
                        nd.Products.Add(this.Create(p, tradingLevel, languageId));
                    }
                }
            }

            return nd;
        }

        // catalogue product
        CatalogueProductReturnModel Create(CatalogueAssemblyNodeProduct product, string tradingLevel, int languageId)
        {
            var prod = new CatalogueProductReturnModel()
            {
                CatalogueProductId = product.Id,
                PartNumber = product.PartNumber,
                QuantityOfFit = product.QuantityOfFit,
                FromBreakPoint = product.FromBreakPoint,
                ToBreakPoint = product.ToBreakPoint
            };

            if (product.ProductDetails != null)
            {
                if (product.ProductDetails.BranchProducts != null)
                {
                    var lst = new List<BranchProduct> { product.ProductDetails.BranchProducts.FirstOrDefault() };

                    prod.Product = this.Create(lst, tradingLevel, languageId).FirstOrDefault();
                }
            }

            return prod;
        }

        #endregion

        // category
        public CategoryReturnModel Create(Category category, int languageId)
        {

            var cat = new CategoryReturnModel { CategoryId = category.Id, ProductCount = category.DistinctProductCount };

            if (category.Titles != null)
            {

                var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                if (title == null && languageId != 1)
                {
                    // default to English if translation not available
                    title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
                }

                if (title != null)
                {
                    cat.Title = title.Title;
                }
            }

            if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

            if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

            if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

            return cat;
        }

        // category with products
        public CategoryWithProductsReturnModel Create(Category category, string terms, int languageId)
        {

            var cat = new CategoryWithProductsReturnModel { CategoryId = category.Id, ProductCount = category.DistinctProductCount };

            if (category.Titles != null)
            {

                var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                if (title == null && languageId != 1)
                {
                    // default to English if translation not available
                    title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
                }

                if (title != null)
                {
                    cat.Title = title.Title;
                }
            }

            if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

            if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

            if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

            if (category.Products != null)
            {
                cat.Products = this.Create(category.Products.ToList(), terms, languageId);
                cat.ProductCount = cat.Products.Distinct().Count();
            }

            return cat;
        }

        // category list
        public List<CategoryReturnModel> Create(List<Category> categories, int languageId)
        {

            var lst = new List<CategoryReturnModel>();

            try
            {

                foreach (Category category in categories)
                {

                    var url = "";

                    switch (category.CategoryType_Id)
                    {
                        case 1: // accessories
                            url = "GetAccessoryCategoryBranchProductsByBranchCodeCategoryId";
                            break;
                        case 2: // service parts
                            url = "GetServicePartsCategoryBranchProductsByBranchCodeCategoryId";
                            break;
                        case 3:
                            url = "GetUpgradeCategoryBranchProductsByBranchCodeCategoryId";
                            break;
                    }

                    var cat = new CategoryReturnModel
                    {
                        CategoryId = category.Id,
                        ProductCount = category.DistinctProductCount,
                        ProductsUrl = _UrlHelper != null && !string.IsNullOrEmpty(url) ? _UrlHelper.Link(url, new { id = category.Id, language = languageId }) : null
                    };

                    if (category.Titles != null)
                    {

                        var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                        if (title == null && languageId != 1)
                        {
                            // default to English if translation not available
                            title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
                        }

                        if (title != null)
                        {
                            cat.Title = title.Title;
                        }

                    }

                    if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

                    if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

                    if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

                    lst.Add(cat);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("ERROR");
                System.Diagnostics.Trace.WriteLine(e.Message);

                while (e.InnerException != null)
                {
                    System.Diagnostics.Trace.WriteLine("");
                    System.Diagnostics.Trace.WriteLine("INNER EXCEPTION");
                    System.Diagnostics.Trace.WriteLine(e.InnerException.Message);

                    e = e.InnerException;
                }

            }

            if (!lst.Any()) { return null; }

            return lst;
        }

        // category product list
        public List<CategoryProductReturnModel> Create(List<CategoryProduct> products, string terms, int languageId)
        {
            var lst = new List<CategoryProductReturnModel>();

            foreach (CategoryProduct product in products)
            {
                var prod = new CategoryProductReturnModel()
                {
                    QuantityOfFit = product.QuantityOfFit,
                    FromBreakPoint = product.FromBreakPoint,
                    ToBreakPoint = product.ToBreakPoint,
                };

                if (product.ProductDetails != null)
                {
                    if (product.ProductDetails.BranchProducts != null)
                    {
                        if (product.ProductDetails.BranchProducts.Any())
                        {
                            prod.Product = this.Create(product.ProductDetails.BranchProducts.FirstOrDefault(), terms, languageId);
                        }
                    }

                }

                lst.Add(prod);
            }

            return lst;
        }


        #region "Old category"

        //        // category - accessory
        //        public List<CategoryReturnModel> Create(List<AccessoryCategory> categories, int languageId)
        //        {

        //            var lst = new List<CategoryReturnModel>();

        //            foreach(AccessoryCategory category in categories)
        //            {

        //                var cat = new CategoryReturnModel { CategoryId = category.Id, ProductsUrl = _UrlHelper != null ? _UrlHelper.Link("GetAccessoryCategoryProductsByBranchCodeCategoryId", new { id = category.Id, language = languageId }) };

        //                if (category.Titles != null) {

        //                    var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                    if (title == null && languageId != 1)
        //                    {
        //                        // default to English if translation not available
        //                        title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                    }

        //                    if (title != null)
        //                    {
        //                        cat.Title = title.Title;
        //                    }

        //                }

        //                if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //                if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //                if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

        //                lst.Add(cat);
        //            }

        //            return lst;
        //        }
        //        public CategoryWithProductsReturnModel Create(AccessoryCategory category, string terms, int languageId)
        //        {

        //                var cat = new CategoryWithProductsReturnModel { CategoryId = category.Id };

        //                if (category.Titles != null) {

        //                    var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                    if (title == null && languageId != 1)
        //                    {
        //                        // default to English if translation not available
        //                        title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                    }

        //                    if (title != null)
        //                    {
        //                        cat.Title = title.Title;
        //                    }
        //                }

        //                if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //                if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //                if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

        //                if (category.Products != null) {
        //                    cat.Products = this.Create(category.Products.ToList(), terms, languageId);
        //                    cat.ProductCount = cat.Products.Count;
        //                }

        //            return cat;
        //        }

        //        public CategoryIntroductionReturnModel Create(List<AccessoryCategoryIntroduction> introductions, int languageId)
        //        {
        //            var intro = new CategoryIntroductionReturnModel();

        //            if (introductions != null)
        //            {
        //                AccessoryCategoryIntroduction introduction = introductions.FirstOrDefault(i => i.Language_Id == languageId);

        //                if (introduction == null && languageId != 1)
        //                {
        //                    // default to English if translation not available
        //                    introduction = introductions.FirstOrDefault(i => i.Language_Id == 1);
        //                }

        //                if (introduction != null)
        //                {
        //                    intro.Text = introduction.Intro;
        //                    intro.More = introduction.More;
        //                }
        //            }

        //            return intro;
        //        }
        //        public List<CategoryProductReturnModel> Create(List<AccessoryCategoryProduct> products, string terms, int languageId)
        //        {
        //            var lst = new List<CategoryProductReturnModel>();

        //            foreach (AccessoryCategoryProduct product in products)
        //            {
        //                var prod = new CategoryProductReturnModel()
        //                {
        //                    QuantityOfFit = product.QuantityOfFit,
        //                    FromBreakPoint = product.FromBreakPoint,
        //                    ToBreakPoint = product.ToBreakPoint,
        //                };

        //                if (product.ProductDetails != null)
        //                {
        //                    if (product.ProductDetails.BranchProducts != null)
        //                    {
        //                        if (product.ProductDetails.BranchProducts.Any())
        //                        {
        //                            prod.Product = this.Create(product.ProductDetails.BranchProducts.FirstOrDefault(), terms, languageId);
        //                        }
        //                    }

        //                }

        //                lst.Add(prod);
        //            }

        //            return lst;
        //        }


        //        // category - service
        //        public List<CategoryReturnModel> Create(List<ServiceCategory> categories, int languageId)
        //        {

        //            var lst = new List<CategoryReturnModel>();

        //            foreach (ServiceCategory category in categories)
        //            {

        //                var cat = new CategoryReturnModel { CategoryId = category.Id };

        //                if (category.Titles != null) {

        //                    var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                    if (title == null && languageId != 1)
        //                    {
        //                        // default to English if translation not available
        //                        title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                    }

        //                    if (title != null)
        //                    {
        //                        cat.Title = title.Title;
        //                    }
        //                }

        //                if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //                if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //                if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

        //                lst.Add(cat);
        //            }

        //            return lst;
        //        }
        //        public CategoryWithProductsReturnModel Create(ServiceCategory category, string terms, int languageId)
        //        {

        //            var cat = new CategoryWithProductsReturnModel { CategoryId = category.Id };

        //            if (category.Titles != null)
        //            {

        //                var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                if (title == null && languageId != 1)
        //                {
        //                    // default to English if translation not available
        //                    title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                }

        //                if (title != null)
        //                {
        //                    cat.Title = title.Title;
        //                }
        //            }

        //            if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //            if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //            if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }


        //            if (category.Products != null) {
        //                cat.Products = this.Create(category.Products.ToList(), terms, languageId);
        //                cat.ProductCount = cat.Products.Count;
        //            }

        //            return cat;
        //        }

        //        public CategoryIntroductionReturnModel Create(List<ServiceCategoryIntroduction> introductions, int languageId)
        //        {
        //            var intro = new CategoryIntroductionReturnModel();

        //            if (introductions != null)
        //            {
        //                ServiceCategoryIntroduction introduction = introductions.FirstOrDefault(i => i.Language_Id == languageId);

        //                if (introduction == null && languageId != 1)
        //                {
        //                    // default to English if translation not available
        //                    introduction = introductions.FirstOrDefault(i => i.Language_Id == 1);
        //                }

        //                if (introduction != null)
        //                {
        //                    intro.Text = introduction.Intro;
        //                    intro.More = introduction.More;
        //                }
        //            }

        //            return intro;
        //        }
        //        public List<CategoryProductReturnModel> Create(List<ServiceCategoryProduct> products, string terms, int languageId)
        //        { 
        //             var lst = new List<CategoryProductReturnModel>();

        //            foreach (ServiceCategoryProduct product in products)
        //            {
        //                var prod = new CategoryProductReturnModel()
        //                {
        //                    QuantityOfFit = product.QuantityOfFit,
        //                    FromBreakPoint = product.FromBreakPoint,
        //                    ToBreakPoint = product.ToBreakPoint,
        //                };

        //                if (product.ProductDetails != null)
        //                {
        //                    if (product.ProductDetails.BranchProducts != null)
        //                    {
        //                        if (product.ProductDetails.BranchProducts.Any())
        //                        {
        //                            prod.Product = this.Create(product.ProductDetails.BranchProducts.FirstOrDefault(), terms, languageId);
        //                        }
        //}

        //                }

        //                lst.Add(prod);
        //            }

        //            return lst;
        //        }


        //        // category - upgrade
        //        public List<CategoryReturnModel> Create(List<UpgradeCategory> categories, int languageId)
        //        {

        //            var lst = new List<CategoryReturnModel>();

        //            foreach (UpgradeCategory category in categories)
        //            {

        //                var cat = new CategoryReturnModel { CategoryId = category.Id };

        //                if (category.Titles != null)
        //                {

        //                    var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                    if (title == null && languageId != 1)
        //                    {
        //                        // default to English if translation not available
        //                        title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                    }

        //                    if (title != null)
        //                    {
        //                        cat.Title = title.Title;
        //                    }
        //                }

        //                if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //                if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //                if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

        //                lst.Add(cat);
        //            }

        //            return lst;
        //        }
        //        public CategoryWithProductsReturnModel Create(UpgradeCategory category, string terms, int languageId)
        //        {

        //            var cat = new CategoryWithProductsReturnModel { CategoryId = category.Id };

        //            if (category.Titles != null)
        //            {

        //                var title = category.Titles.FirstOrDefault(t => t.Language_Id == languageId);

        //                if (title == null && languageId != 1)
        //                {
        //                    // default to English if translation not available
        //                    title = category.Titles.FirstOrDefault(t => t.Language_Id == 1);
        //                }

        //                if (title != null)
        //                {
        //                    cat.Title = title.Title;
        //                }
        //            }

        //            if (category.Introductions != null) { cat.Introduction = this.Create(category.Introductions.ToList(), languageId); }

        //            if (category.ImageFilename != null) { cat.ImageUrl = _CdnHost + "/categories/" + category.ImageFilename; }

        //            if (category.Children != null) { cat.SubCategories = this.Create(category.Children.ToList(), languageId); }

        //            if (category.Products != null) {
        //                cat.Products = this.Create(category.Products.ToList(), terms, languageId);
        //                cat.ProductCount = cat.Products.Count;
        //            }


        //            return cat;
        //        }

        //        public CategoryIntroductionReturnModel Create(List<UpgradeCategoryIntroduction> introductions, int languageId)
        //        {
        //            var intro = new CategoryIntroductionReturnModel();

        //            if (introductions != null)
        //            {
        //                UpgradeCategoryIntroduction introduction = introductions.FirstOrDefault(i => i.Language_Id == languageId);

        //                if (introduction == null && languageId != 1)
        //                {
        //                    // default to English if translation not available
        //                    introduction = introductions.FirstOrDefault(i => i.Language_Id == 1);
        //                }

        //                if (introduction != null)
        //                {
        //                    intro.Text = introduction.Intro;
        //                    intro.More = introduction.More;
        //                }
        //            }

        //            return intro;
        //        }
        //        public List<CategoryProductReturnModel> Create(List<UpgradeCategoryProduct> products, string terms, int languageId)
        //        {
        //            var lst = new List<CategoryProductReturnModel>();

        //            foreach (UpgradeCategoryProduct product in products)
        //            {
        //                var prod = new CategoryProductReturnModel()
        //                {
        //                    QuantityOfFit = product.QuantityOfFit,
        //                    FromBreakPoint = product.FromBreakPoint,
        //                    ToBreakPoint = product.ToBreakPoint,
        //                };

        //                if (product.ProductDetails != null)
        //                {
        //                    if (product.ProductDetails.BranchProducts != null)
        //                    {
        //                        if (product.ProductDetails.BranchProducts.Any())
        //                        {
        //                            prod.Product = this.Create(product.ProductDetails.BranchProducts.FirstOrDefault(), terms, languageId);
        //                        }
        //                    }

        //                }

        //                lst.Add(prod);
        //            }

        //            return lst;
        //        }


        #endregion


        // contact
        public ContactReturnModel Create(Contact contact)
        {
            return new ContactReturnModel()
            {
                Name = contact.Name,
                AddressLine1 = contact.AddressLine1,
                AddressLine2 = contact.AddressLine2,
                TownCity = contact.TownCity,
                CountyState = contact.CountyState,
                PostalCode = contact.PostalCode,
                Country = contact.CountryName,
                CountryCode = contact.Country_Code,
                PhoneNumber = contact.PhoneNumber
            };
        }


        // customer
        public CustomerReturnModel Create(Customer customer)
        {
            var cust = new CustomerReturnModel
            {
                //Url = _UrlHelper != null ? _UrlHelper.Link("GetCustomerById", new { id = customer.Id }) : null,
                Url = _UrlHelper != null ? _UrlHelper.Link("GetCustomerById", new { }) : null,
                Id = customer.Id,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                CompanyName = customer.CompanyName,
                CompanyTaxNo = customer.CompanyTaxNo,
                Address = new AddressReturnModel
                {
                    AddressLine1 = customer.AddressLine1,
                    AddressLine2 = customer.AddressLine2,
                    TownCity = customer.TownCity,
                    CountyState = customer.CountyState,
                    PostalCode = customer.PostalCode,
                    Country = customer.CountryName,
                    CountryCode = customer.Country_Code
                }
            };

            if (customer.Branch != null)
            {
                cust.SiteCode = customer.Branch.SiteCode;
            }

            if (customer.EmailAddresses != null)
            {
                if (customer.DefaultEmailAddress != null)
                {
                    cust.EmailAddress = customer.DefaultEmailAddress.Address;
                }
            }

            if (customer.Language != null)
            {
                cust.LanguageCode = customer.Language.Code;
            }

            if (customer.PhoneNumbers != null)
            {
                if (customer.DefaultPhoneNumber != null)
                {
                    cust.PhoneNumber = customer.DefaultPhoneNumber.FullNumber();
                }
            }

            if (customer.ShippingAddresses != null)
            {
                if (customer.DefaultShippingAddress != null)
                {
                    cust.ShippingAddress = this.Create(customer.DefaultShippingAddress);
                }
            }

            if (customer.ShippingMethod != null)
            {
                cust.ShippingMethod = customer.ShippingMethod.Title; // TODO: transalations
            }

            if (customer.PaymentMethod != null)
            {

                if (customer.PaymentMethod.Titles != null)
                {

                    var title = customer.PaymentMethod.Titles.FirstOrDefault(t => t.Language_Id == customer.Language_Id);

                    if (title == null && customer.Language_Id != 1)
                    {
                        // default to English if translation not available
                        title = customer.PaymentMethod.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        cust.PaymentMethod = title.Title;
                    }

                }

            }

            if (customer.PaymentCards != null)
            {
                if (customer.DefaultPaymentCard != null)
                {
                    cust.PaymentCard = this.Create(customer.DefaultPaymentCard);
                }
            }

            if (customer.Vehicles != null)
            {
                if (customer.DefaultVehicle != null)
                {
                    cust.Vehicle = this.Create(customer.DefaultVehicle, customer.Language_Id);
                }
            }

            return cust;
        }

        // customer list
        public List<CustomerListReturnModel> Create(List<Customer> customers)
        {
            var lst = new List<CustomerListReturnModel>();

            foreach (Customer customer in customers)
            {

                var cust = new CustomerListReturnModel
                {
                    Url = _UrlHelper != null ? _UrlHelper.Link("GetCustomerById", new { id = customer.Id }) : null,
                    FullName = (customer.Title + ' ' + customer.FirstName + " " + customer.LastName).Replace("  ", " ").Trim(),
                    CompanyName = customer.CompanyName,
                    DefaultBranchId = customer.Branch_Id
                };

                if (customer.EmailAddresses != null)
                {
                    if (customer.DefaultEmailAddress != null)
                    {
                        cust.EmailAddress = customer.DefaultEmailAddress.Address;
                    }
                }

                if (customer.PhoneNumbers != null)
                {
                    if (customer.DefaultPhoneNumber != null)
                    {
                        cust.PhoneNumber = customer.DefaultPhoneNumber.FullNumber();
                    }
                }

                lst.Add(cust);
            }

            return lst;
        }

        // customer email address
        public CustomerEmailAddressReturnModel Create(CustomerEmailAddress custEmail)
        {
            var email = new CustomerEmailAddressReturnModel()
            {
                Id = custEmail.Id,
                EmailAddress = custEmail.Address,
                Marketing = custEmail.IsMarketing,
                Billing = custEmail.IsBilling,
                Verified = custEmail.IsVerified,
                Default = custEmail.IsDefault
            };


            return email;
        }

        // customer email address list
        public List<CustomerEmailAddressReturnModel> Create(List<CustomerEmailAddress> emailAddresses)
        {
            var lst = new List<CustomerEmailAddressReturnModel>();

            foreach (CustomerEmailAddress emailAddress in emailAddresses)
            {
                lst.Add(this.Create(emailAddress));
            }

            return lst;
        }

        // customer phone number
        public CustomerPhoneNumberReturnModel Create(CustomerPhoneNumber phoneNumber)
        {
            var phoneNo = new CustomerPhoneNumberReturnModel()
            {
                Id = phoneNumber.Id,
                InternationalCode = phoneNumber.InternationalCode,
                AreaCode = phoneNumber.AreaCode,
                Number = phoneNumber.Number,
                FullNumber = phoneNumber.FullNumber(),
                Default = phoneNumber.IsDefault
            };

            if (phoneNumber.PhoneNumberType != null)
            {
                phoneNo.Type = phoneNumber.PhoneNumberType.Name;
            }

            return phoneNo;
        }

        // customer phone number list
        public List<PhoneNumberReturnModel> Create(List<CustomerPhoneNumber> phoneNumbers)
        {
            var lst = new List<PhoneNumberReturnModel>();

            foreach (CustomerPhoneNumber phoneNumber in phoneNumbers)
            {
                lst.Add(this.Create(phoneNumber));
            }

            return lst;
        }

        // customer shipping address
        public ShippingAddressReturnModel Create(CustomerShippingAddress address)
        {
            return new ShippingAddressReturnModel
            {
                Id = address.Id,
                DisplayName = address.DisplayName,
                Name = address.Name,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                TownCity = address.TownCity,
                CountyState = address.CountyState,
                PostalCode = address.PostalCode,
                Country = address.CountryName,
                CountryCode = address.Country_Code,
                PhoneNumber = address.PhoneNumber,
                Default = address.IsDefault
            };
        }

        // customer shipping  address list
        public List<ShippingAddressReturnModel> Create(List<CustomerShippingAddress> addresses)
        {
            var lst = new List<ShippingAddressReturnModel>();

            foreach (CustomerShippingAddress address in addresses)
            {
                lst.Add(this.Create(address));
            }

            return lst;
        }

        // customer vehicle        
        public CustomerVehicleReturnModel Create(CustomerVehicle custVehicle, int languageId)
        {
            var custCar = new CustomerVehicleReturnModel()
            {
                DisplayName = custVehicle.DisplayName,
                ModelYear = custVehicle.ModelYear,
                EngineNumber = custVehicle.EngineNumber,
                ChassisNumber = custVehicle.ChassisNumber,
                VIN = custVehicle.VIN,
                Notes = custVehicle.Notes,
                Default = custVehicle.IsDefault
            };

            if (custVehicle.Vehicle != null)
            {
                custCar.VehicleDetails = this.Create(custVehicle.Vehicle, languageId);
            }

            return custCar;
        }

        // customer vehicle list           
        public List<CustomerVehicleReturnModel> Create(List<CustomerVehicle> custVehicles, int languageId)
        {
            var custCars = new List<CustomerVehicleReturnModel>();

            foreach (CustomerVehicle custVehicle in custVehicles)
            {
                custCars.Add(this.Create(custVehicle, languageId));
            }

            return custCars;
        }

        // event
        public EventReturnModel Create(ShowEvent evt)
        {
            var ev = new EventReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetEventById", new { id = evt.Id }) : null,
                Id = evt.Id,
                Title = evt.Title,
                Description = evt.Description,
                EventUrl = evt.EventUrl,
                Location = evt.Location,
                MapUrl = evt.MapUrl,
                ImageUrl = _CdnHost + "/events/" + evt.ImageFilename,
                Attending = evt.IsAttending
            };

            if (evt.Branch != null)
            {
                ev.BranchCode = evt.Branch.SiteCode;
            }

            if (evt.EventDateTimes != null)
            {
                if (evt.EventDateTimes.Any())
                {
                    ev.DateTimes = Create(evt.EventDateTimes.ToList());
                }
            }

            return ev;
        }

        // event list
        public List<EventListReturnModel> Create(List<ShowEvent> events)
        {
            var lst = new List<EventListReturnModel>();

            foreach (ShowEvent evt in events)
            {
                var ev = new EventListReturnModel
                {
                    Url = _UrlHelper != null ? _UrlHelper.Link("GetEventById", new { id = evt.Id }) : null,
                    Title = evt.Title,
                    Location = evt.Location,
                    Attending = evt.IsAttending
                };

                if (evt.Branch != null)
                {
                    ev.BranchCode = evt.Branch.SiteCode;
                }

                if (evt.EventDateTimes != null)
                {
                    ev.StartsUtc = evt.EventDateTimes.FirstOrDefault().StartsUtc;
                    ev.EndsUtc = evt.EventDateTimes.LastOrDefault().EndsUtc;
                }

                lst.Add(ev);
            }

            return lst;
        }

        // event date time
        public EventDateTimeReturnModel Create(ShowEventDateTime time)
        {
            return new EventDateTimeReturnModel
            {
                StartsUtc = time.StartsUtc,
                EndsUtc = time.EndsUtc
            };

        }

        // event date time list
        public List<EventDateTimeReturnModel> Create(List<ShowEventDateTime> times)
        {
            var lst = new List<EventDateTimeReturnModel>();

            foreach (ShowEventDateTime time in times)
            {
                lst.Add(
                    new EventDateTimeReturnModel
                    {
                        StartsUtc = time.StartsUtc,
                        EndsUtc = time.EndsUtc
                    }
                );
            }

            return lst;

        }

        // introduction
        public IntroductionReturnModel Create(List<BranchIntroduction> introductions, int languageId)
        {
            var intro = new IntroductionReturnModel();

            if (introductions != null)
            {
                BranchIntroduction introduction = introductions.FirstOrDefault(i => i.Language_Id == languageId);

                if (introduction == null && languageId != 1)
                {
                    // default to English if translation not available
                    introduction = introductions.FirstOrDefault(i => i.Language_Id == 1);
                }

                if (introduction != null)
                {
                    intro.Text = introduction.Intro;
                    intro.More = introduction.More;
                }
            }

            return intro;
        }
        public IntroductionReturnModel Create(List<CategoryIntroduction> introductions, int languageId)
        {
            var intro = new IntroductionReturnModel();

            if (introductions != null)
            {
                CategoryIntroduction introduction = introductions.FirstOrDefault(i => i.Language_Id == languageId);

                if (introduction == null && languageId != 1)
                {
                    // default to English if translation not available
                    introduction = introductions.FirstOrDefault(i => i.Language_Id == 1);
                }

                if (introduction != null)
                {
                    intro.Text = introduction.Intro;
                    intro.More = introduction.More;
                }
            }

            return intro;
        }


        // payment card
        public PaymentCardReturnModel Create(PaymentCard paymentCard)
        {
            // TODO: consider storing some of this info so that the details don't need to be decrypted first
            paymentCard.Decrypt(ConfigurationManager.AppSettings["Encryption_Key"], ConfigurationManager.AppSettings["Encryption_Salt"]);

            var card = new PaymentCardReturnModel()
            {
                Id = paymentCard.Id,
                DisplayName = paymentCard.DisplayName,
                CardNumber = paymentCard.CardNumberMasked(),
                ExpiryMonth = paymentCard.ExpiryDate.Value.Month,
                ExpiryYear = paymentCard.ExpiryDate.Value.Year,
                Default = paymentCard.IsDefault
            };

            paymentCard.Reset();

            return card;
        }

        // payment card list
        public List<PaymentCardReturnModel> Create(List<PaymentCard> cards)
        {
            var lst = new List<PaymentCardReturnModel>();

            foreach (PaymentCard card in cards)
            {
                lst.Add(this.Create(card));
            }

            return lst;
        }

        // payment method
        public PaymentMethodReturnModel Create(PaymentMethod method, int languageId)
        {
            var mth = new PaymentMethodReturnModel()
            {
                Code = method.Code
            };

            PaymentMethodTitle title = method.Titles.FirstOrDefault(i => i.Language_Id == languageId);

            if (title == null && languageId != 1)
            {
                // default to English if translation not available
                title = method.Titles.FirstOrDefault(i => i.Language_Id == 1);
            }

            if (title != null)
            {
                mth.Title = title.Title;
            }

            return mth;
        }

        ////// payment methods list
        ////public List<PaymentMethodReturnModel> Create(List<PaymentMethod> methods, int languageId)
        ////{
        ////    var lst = new List<PaymentMethodReturnModel>();

        ////    foreach (PaymentMethod method in methods)
        ////    {
        ////        lst.Add(this.Create(method, languageId));
        ////    }

        ////    return lst;
        ////}

        // payment options
        public PaymentOptionsReturnModel Create(List<BranchPaymentMethod> methods, Customer cust)
        {
            var options = new PaymentOptionsReturnModel()
            {
                CustomerId = cust.Id,
                Default = cust.PaymentMethod_Code,
                Methods = new List<PaymentMethodReturnModel>()
            };

            var mtdOk = true;
            foreach (BranchPaymentMethod method in methods)
            {

                mtdOk = true;

                if (method.PaymentMethod_Code == "AC") // Account
                {
                    // only add account as an option if the customer has credit facility and isn't on stop
                    mtdOk = false;
                    if (cust.IsCreditAccount == true && cust.IsOnStop == false) { mtdOk = true; }
                }

                if (mtdOk == true)
                {
                    options.Methods.Add(this.Create(method.PaymentMethod, cust.Language_Id));
                }
            }


            return options;
        }

        // phone number
        //public PhoneNumberReturnModel Create(PhoneNumber phone)
        //{
        //    return new PhoneNumberReturnModel
        //    {
        //        InternationalCode = phone.InternationalCode,
        //        AreaCode = phone.AreaCode,
        //        Number = phone.Number
        //    };

        //}



        // product 
        public ProductReturnModel Create(BranchProduct product, string tradingLevel, int languageId)
        {
            var prod = new ProductReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetBranchProductByBranchCodeProductGuid", new { id = product.Id }) : null,
                Id = product.Id,
                RetailPrice = product.RetailPrice,
                UnitPrice = product.UnitPrice(tradingLevel),
                Surcharge = product.Surcharge,
                Status = product.StockStatus()

            };

            if (product.ProductDetails != null)
            {

                prod.PartNumber = product.ProductDetails.PartNumber;

                // calc price inc tax
                if (product.Branch != null)
                {
                    if (product.Branch.TaxRates != null)
                    {
                        var tax = product.Branch.TaxRates.Single(r => r.TaxRateCategory_Id == product.ProductDetails.TaxRateCategory_Id);
                        var taxAmnt = Math.Round(prod.UnitPrice * (tax.Rate / 100), 2);

                        prod.TaxPrice = prod.UnitPrice + taxAmnt;
                    }


                }

                if (product.ProductDetails.ProductBrand != null) { prod.Brand = this.Create(product.ProductDetails.ProductBrand); }

                if (product.ProductDetails.TextInfo != null)
                {
                    ProductText info = product.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == languageId);

                    if (info == null && languageId != 1)
                    {
                        // default to English if translation not available
                        info = product.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == 1);
                    }

                    if (info != null)
                    {
                        prod.Title = info.ShortTitle;
                        //prod.LongTitle = info.LongTitle;
                        prod.Description = info.ShortDescription;
                        if (!string.IsNullOrWhiteSpace(info.LongDescription)) { prod.Description = info.LongDescription; }

                        prod.Information = info.SalesNotes;
                    }

                }

                prod.Applications = product.ProductDetails.ApplicationList;

                if (product.ProductDetails.ItemWidthCms > 0 && product.ProductDetails.ItemHeightCms > 0 && product.ProductDetails.ItemDepthCms > 0)
                {
                    prod.Measurements = new MeasurementsReturnModel
                    {
                        WidthCms = product.ProductDetails.ItemWidthCms,
                        HeightCms = product.ProductDetails.ItemHeightCms,
                        DepthCms = product.ProductDetails.ItemDepthCms,
                        WeightKgs = product.ProductDetails.ItemWeightKgs
                    };
                }

                if (product.ProductDetails.QuantityBreakDiscountLevels != null)
                {
                    if (product.ProductDetails.QuantityBreakDiscountLevels.Any())
                    {
                        prod.QuantityBreakPrices = this.Create(product.ProductDetails.QuantityBreakDiscountLevels.ToList(), tradingLevel, product.RetailPrice, product.AvgCostPrice);
                    }
                }

                prod.Images = new List<ProductImageReturnModel>();
                if (product.ProductDetails.Images != null)
                {
                    if (product.ProductDetails.Images.Any())
                    {
                        foreach (ProductImage img in product.ProductDetails.Images)
                        {
                            prod.Images.Add(this.Create(img));
                        }
                    }
                }
                // if no product images exist, add default image
                if (!prod.Images.Any()) { prod.Images.Add(this.Create(new ProductImage())); }

                // TODO: product documents by language               
                if (product.ProductDetails.Documents != null)
                {
                    if (product.ProductDetails.Documents.Any())
                    {
                        prod.Documents = new List<ProductDocumentReturnModel>();
                        foreach (ProductDocument doc in product.ProductDetails.Documents)
                        {
                            prod.Documents.Add(this.Create(doc));
                        }
                    }
                }

                if (product.SpecialOffers != null)
                {
                    if (product.SpecialOffers.Any())
                    {
                        var offer = product.SpecialOffers.Where(o => o.IsActive == true).OrderBy(o => o.ExpiryDate).FirstOrDefault();

                        if (offer != null) { prod.SpecialOffer = this.Create(offer); }
                    }
                }

                if (product.ProductDetails.SupersessionList != null)
                {
                    if (product.ProductDetails.SupersessionList.Any())
                    {
                        prod.Supersession = new List<ProductSupersessionReturnModel>();

                        foreach (ProductSupersession s in product.ProductDetails.SupersessionList)
                        {
                            var replacementProd = s.ReplacementProduct.BranchProducts.Where(b => b.Branch_Id == product.Branch_Id).FirstOrDefault();

                            prod.Supersession.Add(this.Create(s.Quantity, replacementProd, languageId));
                        }
                    }
                }

                if (product.ProductDetails.AlternativeProducts != null)
                {
                    if (product.ProductDetails.AlternativeProducts.Any())
                    {

                        var alts = new List<ProductListReturnModel>();

                        foreach (ProductAlternative alt in product.ProductDetails.AlternativeProducts)
                        {
                            alts.AddRange(this.Create(new List<BranchProduct> { alt.AlternativeBranchProduct(product.Branch_Id) }, tradingLevel, languageId));
                        }

                        prod.Alternatives = alts;
                    }
                }

                if (product.ProductDetails.LinkedProducts != null)
                {
                    if (product.ProductDetails.LinkedProducts.Any())
                    {

                        var lnks = new List<ProductListReturnModel>();

                        foreach (ProductLink lnk in product.ProductDetails.LinkedProducts)
                        {
                            lnks.AddRange(this.Create(new List<BranchProduct> { lnk.LinkedBranchProduct(product.Branch_Id) }, tradingLevel, languageId));
                        }

                        prod.Associations = lnks;
                    }
                }
            }



            return prod;
        }

        // product list
        public List<ProductListReturnModel> Create(List<BranchProduct> products, string tradingLevel, int languageId)
        {

            var prods = new List<ProductListReturnModel>();
            var prod = new ProductListReturnModel();

            foreach (BranchProduct product in products)
            {

                if (product != null)
                {

                    prod = new ProductListReturnModel()
                    {
                        Url = _UrlHelper != null ? _UrlHelper.Link("GetBranchProductByBranchCodeProductGuid", new { id = product.Id }) : null,
                        Id = product.Id,
                        RetailPrice = product.RetailPrice,
                        UnitPrice = (product.ProductDetails != null ?
                            (product.ProductDetails.DiscountLevel != null ? product.UnitPrice(tradingLevel) : product.RetailPrice)
                        : product.RetailPrice),
                        Surcharge = product.Surcharge,
                        Status = product.StockStatus()
                    };


                    if (product.ProductDetails != null)
                    {

                        prod.PartNumber = product.ProductDetails.PartNumber;

                        // calc price inc tax
                        if (product.Branch != null)
                        {
                            if (product.Branch.TaxRates != null)
                            {
                                var tax = product.Branch.TaxRates.Single(r => r.TaxRateCategory_Id == product.ProductDetails.TaxRateCategory_Id);
                                var taxAmnt = Math.Round(prod.UnitPrice * (tax.Rate / 100), 2);

                                prod.TaxPrice = prod.UnitPrice + taxAmnt;
                            }


                        }

                        if (product.ProductDetails.TextInfo != null)
                        {

                            var txtInfo = product.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == languageId);

                            if (txtInfo == null && languageId != 1)
                            {
                                // default to English if translation not available
                                txtInfo = product.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == 1);
                            }

                            if (txtInfo != null)
                            {
                                prod.Title = txtInfo.ShortTitle;
                                prod.Description = txtInfo.ShortDescription;
                            }

                        }

                        if (product.ProductDetails.Images != null)
                        {
                            if (product.ProductDetails.DefaultProductImage != null)
                            {
                                prod.Image = this.Create(product.ProductDetails.DefaultProductImage);
                            }
                        }
                        // use placeholder image
                        if (prod.Image == null) { prod.Image = this.Create(new ProductImage()); }

                        if (product.ProductDetails.SupersessionList != null)
                        {
                            var ss = new List<ProductSupersessionReturnModel>();

                            foreach (ProductSupersession s in product.ProductDetails.SupersessionList)
                            {
                                var replacementProd = s.ReplacementProduct.BranchProducts.Where(b => b.Branch_Id == product.Branch_Id).FirstOrDefault();

                                ss.Add(this.Create(s.Quantity, replacementProd, languageId));
                            }

                            prod.Supersession = ss;
                        }
                    }

                    if (product.SpecialOffers != null)
                    {
                        if (product.SpecialOffers.Any())
                        {
                            var offer = product.SpecialOffers.Where(o => o.IsActive == true && o.ExpiryDate > DateTime.UtcNow).OrderBy(o => o.ExpiryDate).FirstOrDefault();

                            if (offer != null)
                            {
                                prod.SpecialOffer = this.Create(offer);
                            }
                        }
                    }

                    prods.Add(prod);
                }
            }

            return prods;
        }

        // product brand
        public ProductBrandReturnModel Create(ProductBrand brand)
        {
            return new ProductBrandReturnModel()
            {
                Name = brand.Name,
                LogoUrl = _CdnHost + "/products/brands/" + brand.LogoFilename
            };

        }

        // product documents
        public ProductDocumentReturnModel Create(ProductDocument document)
        {
            return new ProductDocumentReturnModel()
            {
                Title = document.Title,
                DocumentUrl = _CdnHost + "/products/documents/" + document.Filename
            };

        }

        // product images
        public ProductImageReturnModel Create(ProductImage image)
        {
            // TODO: should these paths come out of a configurartion setting?
            return new ProductImageReturnModel()
            {
                ImageUrl_Thumbnail = _CdnHost + "/products/75/" + image.Filename,
                ImageUrl_XXSmall = _CdnHost + "/products/100/" + image.Filename,
                ImageUrl_XSmall = _CdnHost + "/products/125/" + image.Filename,
                ImageUrl_Small = _CdnHost + "/products/250/" + image.Filename,
                ImageUrl_Medium = _CdnHost + "/products/500/" + image.Filename,
                ImageUrl_Large = _CdnHost + "/products/750/" + image.Filename,
                ImageUrl_XLarge = _CdnHost + "/products/1000/" + image.Filename,
                ImageUrl_XXLarge = _CdnHost + "/products/1500/" + image.Filename,
                Default = image.IsDefault
            };

        }

        // product offer
        public ProductOfferReturnModel Create(BranchProductOffer offer)
        {
            var prodOffer = new ProductOfferReturnModel()
            {
                ProductId = offer.BranchProduct_Id,
                Title = offer.Title,
                OfferPrice = offer.OfferPrice,
                OfferExpires = offer.ExpiryDate
            };

            if (offer.Image != null)
            {
                prodOffer.Image = this.Create(offer.Image);
            };

            return prodOffer;
        }

        // product quantity break prices
        public List<ProductQuantityBreakPriceReturnModel> Create(List<ProductQuantityBreakDiscountLevel> qtyBreaks, string tradingLevel, decimal retailPrice, decimal costPrice)
        {
            var lst = new List<ProductQuantityBreakPriceReturnModel>();

            decimal curPrice = retailPrice;
            foreach (ProductQuantityBreakDiscountLevel qtyBreak in qtyBreaks.OrderBy(p => p.Quantity))
            {
                curPrice = qtyBreak.UnitPrice(tradingLevel, retailPrice, costPrice);

                if (curPrice > 0)
                {
                    if (lst.Any())
                    {
                        if (curPrice < lst.Last().UnitPrice)
                        {
                            // cheaper than last item
                            lst.Add(new ProductQuantityBreakPriceReturnModel { MinQuantity = qtyBreak.Quantity, UnitPrice = curPrice });
                        }

                    }
                    else
                    {
                        // no items in list
                        lst.Add(new ProductQuantityBreakPriceReturnModel { MinQuantity = qtyBreak.Quantity, UnitPrice = curPrice });
                    }
                }
            }



            return lst;
        }

        // product supersession
        public ProductSupersessionReturnModel Create(int quantity, BranchProduct replacementProduct, int languageId)
        {
            var ss = new ProductSupersessionReturnModel
            {
                Quantity = quantity,
                ProductId = replacementProduct.Id
            };

            if (replacementProduct.ProductDetails != null)
            {

                ss.PartNumber = replacementProduct.ProductDetails.PartNumber;

                if (replacementProduct.ProductDetails.TextInfo != null)
                {
                    ProductText info = replacementProduct.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == languageId);

                    if (info == null && languageId != 1)
                    {
                        // default to English if translation not available
                        info = replacementProduct.ProductDetails.TextInfo.FirstOrDefault(i => i.Language_Id == 1);
                    }

                    if (info != null)
                    {
                        ss.Title = info.ShortTitle;

                    }
                }

            }

            return ss;
        }

        // shipping options
        public ShippingOptionsReturnModel Create(Contact recipient, IEnumerable<ShippingQuote> quotes)
        {
            var options = new ShippingOptionsReturnModel()
            {
                Recipient = this.Create(recipient),
                Quotes = new List<ShippingQuoteReturnModel>()
            };

            foreach (var qte in quotes)
            {
                options.Quotes.Add(this.Create(qte));

                // get package info out of 1st quote
                if (options.PackageCount == 0) { options.PackageCount = qte.PackagesCount; }
                if (options.EstimatedWeightKgs == 0) { options.EstimatedWeightKgs = qte.EstimatedWeightKgs; }
            }

            return options;
        }

        // shipping provider
        public ShippingProviderReturnModel Create(ShippingProvider provider)
        {
            return new ShippingProviderReturnModel
            {
                Id = provider.Id,
                Name = provider.Name,
                LogoUrl = _CdnHost + "/shipping/providers/" + provider.LogoFilename
            };
        }

        // shipping quote
        public ShippingQuoteReturnModel Create(ShippingQuote quote)
        {
            return new ShippingQuoteReturnModel
            {
                Id = quote.Id,
                Provider = this.Create(quote.ServiceProvider),
                //ProviderServiceReference = quote.ServiceReference,
                Title = quote.ServiceDescription,
                Price = quote.Price
            };
        }

        // user account
        public UserReturnModel Create(UserAccount appUser)
        {
            UserReturnModel user = new UserReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetUserById", new { id = appUser.Id }) : null,
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                CreatedTimestamp = appUser.CreatedTimestampUtc,
                Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };

            if (appUser.CustomerAccount != null)
            {
                user.BranchId = appUser.CustomerAccount.Branch_Id;
                user.LanguageId = appUser.CustomerAccount.Language_Id;
            }

            return user;
        }

        //// linked user list
        public List<LinkedUserReturnModel> Create(IEnumerable<UserAccount> users)
        {
            var lst = new List<LinkedUserReturnModel>();
            foreach (var user in users)
            {
                lst.Add(new LinkedUserReturnModel
                {
                    Id = user.Id,
                    Email = user.UserName,
                    Confirmed = user.EmailConfirmed
                });
            }

            return lst;
        }

        //// user role
        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetRoleById", new { id = appRole.Id }) : null,
                Id = appRole.Id,
                Name = appRole.Name
            };
        }


        // vehicle        
        public VehicleReturnModel Create(Vehicle vehicle, int languageId)
        {
            var car = new VehicleReturnModel();

            if (vehicle.Marque != null)
            {
                if (vehicle.Marque.Titles != null)
                {

                    var title = vehicle.Marque.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Marque.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Marque = title.Title;
                    }
                }
            }

            if (vehicle.Range != null)
            {
                if (vehicle.Range.Titles != null)
                {

                    var title = vehicle.Range.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Range.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Range = title.Title;
                    }
                }
            }

            if (vehicle.Model != null)
            {
                if (vehicle.Model.Titles != null)
                {

                    var title = vehicle.Model.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Model.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Model = title.Title;
                    }
                }
            }

            if (vehicle.ModelVariant != null)
            {
                if (vehicle.ModelVariant.Titles != null)
                {

                    var title = vehicle.ModelVariant.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.ModelVariant.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.ModelVariant = title.Title;
                    }
                }
            }

            if (vehicle.Engine != null)
            {
                if (vehicle.Engine.Titles != null)
                {

                    var title = vehicle.Engine.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Engine.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Engine = title.Title;
                    }
                }
            }

            if (vehicle.Transmission != null)
            {
                if (vehicle.Transmission.Titles != null)
                {

                    var title = vehicle.Transmission.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Transmission.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Transmission = title.Title;
                    }
                }
            }

            if (vehicle.Drivetrain != null)
            {
                if (vehicle.Drivetrain.Titles != null)
                {

                    var title = vehicle.Drivetrain.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Drivetrain.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.Drive = title.Title;
                    }
                }
            }

            if (vehicle.Body != null)
            {
                if (vehicle.Body.Titles != null)
                {

                    var title = vehicle.Body.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.Body.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.BodyStyle = title.Title;
                    }
                }
            }

            if (vehicle.TrimLevel != null)
            {
                if (vehicle.TrimLevel.Titles != null)
                {

                    var title = vehicle.TrimLevel.Titles.FirstOrDefault(t => t.Language_Id == languageId);

                    if (title == null && languageId != 1)
                    {
                        // default to English if translation not available
                        title = vehicle.TrimLevel.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        car.TrimLevel = title.Title;
                    }
                }
            }


            return car;
        }



        // web order
        public WebOrderReturnModel Create(WebOrder order)
        {
            var ord = new WebOrderReturnModel
            {
                Url = _UrlHelper != null ? _UrlHelper.Link("GetWebOrderById", new { id = order.Id }) : null,
                Id = order.Id,
                OrderNo = order.OrderNo,
                OrderDate = order.OrderDate,
                BranchId = order.Branch.Id,
                BranchName = order.Branch.Name,
                CustomerId = order.Customer_Id,
                CustomerTaxNo = order.CustomerTaxNo,
                CustomerOrderRef = order.CustomerOrderRef,
                InternalQuoteNo = order.InternalQuoteNo,
                InternalQuoteDate = order.InternalQuoteDate,
                InternalOrderNo = order.InternalOrderNo,
                InternalOrderDate = order.InternalOrderDate,
                BillingName = order.BillingName,
                BillingAddressLine1 = order.BillingAddressLine1,
                BillingAddressLine2 = order.BillingAddressLine2,
                BillingTownCity = order.BillingTownCity,
                BillingCountyState = order.BillingCountyState,
                BillingPostalCode = order.BillingPostalCode,
                BillingCountry = order.BillingCountryName,
                BillingCountryCode = order.BillingCountryCode,
                DeliveryName = order.DeliveryName,
                DeliveryAddressLine1 = order.DeliveryAddressLine1,
                DeliveryAddressLine2 = order.DeliveryAddressLine2,
                DeliveryTownCity = order.DeliveryTownCity,
                DeliveryCountyState = order.DeliveryCountyState,
                DeliveryPostalCode = order.DeliveryPostalCode,
                DeliveryCountry = order.DeliveryCountryName,
                DeliveryCountryCode = order.DeliveryCountryCode,
                DeliveryContactNumber = order.DeliveryContactNumber,
                ShippingMethod = order.ShippingMethodName,
                EstimatedWeight = order.EstimatedShippingWeightKgs,
                ShippingCharge = order.ShippingCharge,
                ShippingTaxRate = order.ShippingTaxRate,
                GoodsAtRate1 = order.GoodsAtRate1,
                GoodsTaxRate1 = order.GoodsTaxRate1,
                GoodsAtRate2 = order.GoodsAtRate2,
                GoodsTaxRate2 = order.GoodsTaxRate2,
                GrandTotal = order.GrandTotal,
                Status = order.WebOrderStatus_Id

            };

            if (order.PaymentMethod != null)
            {

                if (order.PaymentMethod.Titles != null)
                {

                    var title = order.PaymentMethod.Titles.FirstOrDefault(t => t.Language_Id == order.Language_Id);

                    if (title == null && order.Language_Id != 1)
                    {
                        // default to English if translation not available
                        title = order.PaymentMethod.Titles.FirstOrDefault(t => t.Language_Id == 1);
                    }

                    if (title != null)
                    {
                        ord.PaymentMethod = title.Title;
                    }

                }
            }

            if (order.Items != null)
            {
                ord.Items = this.Create(order.Items);
            }

            if (order.OrderEvents != null)
            {
                ord.OrderEvents = this.Create(order.OrderEvents);
            }

            return ord;
        }

        // web order list
        public List<WebOrderListReturnModel> Create(List<WebOrder> orders)
        {
            var lst = new List<WebOrderListReturnModel>();

            foreach (WebOrder order in orders)
            {

                var ord = new WebOrderListReturnModel
                {
                    Url = _UrlHelper != null ? _UrlHelper.Link("GetWebOrderById", new { id = order.Id }) : null,
                    Id = order.Id,
                    OrderNo = order.OrderNo,
                    OrderDate = order.OrderDate,
                    BranchId = order.Branch_Id,
                    CustomerId = order.Customer_Id,
                    CustomerTaxNo = order.CustomerTaxNo,
                    CustomerOrderRef = order.CustomerOrderRef,
                    DeliveryName = order.DeliveryName,
                    DeliveryAddressLine1 = order.DeliveryAddressLine1,
                    DeliveryAddressLine2 = order.DeliveryAddressLine2,
                    DeliveryTownCity = order.DeliveryTownCity,
                    DeliveryCountyState = order.DeliveryCountyState,
                    DeliveryPostalCode = order.DeliveryPostalCode,
                    DeliveryCountry = order.DeliveryCountryName,
                    DeliveryCountryCode = order.DeliveryCountryCode,
                    DeliveryContactNumber = order.DeliveryContactNumber,
                    ShippingMethod = order.ShippingMethodName,
                    ShippingCharge = order.ShippingCharge,
                    ShippingTaxRate = order.ShippingTaxRate,
                    GoodsAtRate1 = order.GoodsAtRate1,
                    GoodsTaxRate1 = order.GoodsTaxRate1,
                    GoodsAtRate2 = order.GoodsAtRate2,
                    GoodsTaxRate2 = order.GoodsTaxRate2,
                    GrandTotal = order.GrandTotal,
                    Status = order.WebOrderStatus_Id
                };

                lst.Add(ord);
            }

            return lst;
        }

        // web order items
        public ICollection<WebOrderItemReturnModel> Create(ICollection<WebOrderItem> items)
        {
            var ordItems = new List<WebOrderItemReturnModel>();
            var ordItem = new WebOrderItemReturnModel();

            foreach (WebOrderItem itm in items)
            {
                ordItem = new WebOrderItemReturnModel()
                {

                    Id = itm.Id,
                    LineNo = itm.LineNo,
                    LineStatus = itm.WebOrderItemStatus_Id,
                    ProductId = itm.Product_Id,
                    PartNumber = itm.PartNumber,
                    PartTitle = itm.PartTitle,
                    DiscountCode = itm.DiscountCode,
                    TaxRateCategory = itm.TaxRateCategory_Id,
                    PackedHeightCms = itm.PackedHeightCms,
                    PackedWidthCms = itm.PackedWidthCms,
                    PackedDepthCms = itm.PackedDepthCms,
                    PackedWeightKgs = itm.PackedWeightKgs,
                    PackedWeightKgs_Volumetric = itm.PackedWeightKgs_Volumetric(),
                    RetailPrice = itm.RetailPrice,
                    UnitPrice = itm.UnitPrice,
                    Surcharge = itm.Surcharge,
                    QuantityRequired = itm.QuantityRequired,
                    QuantityAllocated = itm.QuantityAllocated,
                    QuantityBackOrdered = itm.QuantityBackOrdered,
                    QuantityPicked = itm.QuantityPicked,
                    QuantityPacked = itm.QuantityPacked,
                    QuantityInvoiced = itm.QuantityInvoiced,
                    QuantityCredited = itm.QuantityCredited

                };


                ordItems.Add(ordItem);

            }

            return ordItems;
        }

        // web order events
        public ICollection<WebOrderEventReturnModel> Create(ICollection<WebOrderEvent> events)
        {
            var ordEvents = new List<WebOrderEventReturnModel>();
            var ordEvent = new WebOrderEventReturnModel();

            foreach (WebOrderEvent itm in events)
            {
                ordEvent = new WebOrderEventReturnModel()
                {

                    EventType = itm.EventType.Name,
                    Timestamp = itm.CreatedTimestampUtc

                };

                if (itm.Notes != null)
                {
                    ordEvent.Notes = this.Create(itm.Notes);
                }


                ordEvents.Add(ordEvent);

            }

            return ordEvents;
        }

        // web order event note
        public ICollection<WebOrderEventNoteReturnModel> Create(ICollection<WebOrderEventNote> notes)
        {
            var ordNotes = new List<WebOrderEventNoteReturnModel>();
            var ordNote = new WebOrderEventNoteReturnModel();

            foreach (WebOrderEventNote itm in notes)
            {
                ordNote = new WebOrderEventNoteReturnModel()
                {

                    Id = itm.Id,
                    Message = itm.Message

                };


                ordNotes.Add(ordNote);

            }

            return ordNotes;
        }

        // wish list
        public WishListReturnModel Create(Customer customer, WishList list)
        {

            var lst = new WishListReturnModel()
            {
                Id = list.Id,
                DisplayName = list.DisplayName,
                Site = customer.Branch.SiteCode,
                ListTotal = 0,
                ItemCount = 0,
                Items = new List<WishListItemReturnModel>()
            };

            if (list.Items != null)
            {
                lst.Items = this.Create(customer, list.Items.ToList());
            }

            foreach (var item in lst.Items)
            {
                lst.ItemCount++;
                lst.ListTotal += item.LineTotal;
            }

            return lst;
        }

        // wish list items
        public List<WishListItemReturnModel> Create(Customer customer, List<WishListItem> items)
        {

            var lst = new List<WishListItemReturnModel>();

            if (items.Any())
            {
                var itm = new WishListItemReturnModel();
                foreach (WishListItem item in items)
                {
                    lst.Add(this.Create(customer, item));
                }
            }

            return lst;
        }

        // wish list item
        public WishListItemReturnModel Create(Customer customer, WishListItem item)
        {
            var itm = new WishListItemReturnModel
            {
                Id = item.Id,
                PartNumber = item.PartNumber,
                Title = item.PartTitle,
                Quantity = item.Quantity,
                Status = "NAO",
                CreatedTimestamp = item.CreatedTimestampUtc
            };

            if (item.Product != null)
            {

                if (item.Product.Images != null)
                {
                    if (item.Product.DefaultProductImage != null)
                    {
                        itm.ImageUrl = this.Create(item.Product.DefaultProductImage).ImageUrl_Thumbnail;
                    }
                }
                // use placeholder image
                if (itm.ImageUrl == null) { itm.ImageUrl = this.Create(new ProductImage()).ImageUrl_Thumbnail; }


                if (item.Product.BranchProducts != null)
                {
                    var prod = item.Product.BranchProducts.FirstOrDefault(p => p.Branch_Id == customer.Branch_Id);
                    if (prod != null)
                    {
                        itm.ProductId = prod.Id;
                        itm.RetailPrice = prod.RetailPrice;
                        itm.UnitPrice = prod.UnitPrice(customer.TradingTerms_Code);
                        itm.Surcharge = prod.Surcharge;
                        itm.LineTotal = itm.UnitPrice * itm.Quantity;

                        // set current stock status
                        itm.Status = prod.StockStatus();


                    }
                }
            }


            if (customer.Branch != null)
            {
                if (customer.Branch.TaxRates != null)
                {
                    // calc price inc tax
                    var tax = customer.Branch.TaxRates.Single(r => r.TaxRateCategory_Id == item.Product.TaxRateCategory_Id);

                    itm.TaxAmount = Math.Round(itm.UnitPrice * (tax.Rate / 100), 2);
                    itm.TaxPrice = itm.UnitPrice + itm.TaxAmount;
                }
            }


            return itm;
        }

        public TweetReturnModel Create(Tweet tweet)
        {
            return AutoMapper.Mapper.Map<TweetReturnModel>(tweet);
        }


    }
}