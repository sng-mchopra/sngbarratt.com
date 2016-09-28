using EFSecondLevelCache;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Catalogue;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Globalization;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Domain.Payment;
using jCtrl.Services.Core.Domain.Product;
using jCtrl.Services.Core.Domain.Shipping;
using jCtrl.Services.Core.Domain.Vehicle;
using jCtrl.Services.Core.Domain.Voucher;
using jCtrl.Services.Core.Domain.WishList;
using jCtrl.Services.Core.Utils.CustomAttributes;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace jCtrl.Infrastructure
{
    public class jCtrlContext : IdentityDbContext<UserAccount>
    {
        public jCtrlContext() : base("name=jCtrlContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //TODO: Comment out to stop writing out sql queries
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public static jCtrlContext Create()
        {
            return new jCtrlContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Diagnostics.Debug.WriteLine("OnModelCreating...");

            // remove cascade delete
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // apply decimal precision
            foreach (Type classType in from t in Assembly.GetAssembly(typeof(DecimalPrecisionAttribute)).GetTypes()
                                       where t.IsClass && (t.Namespace == "jCtrl.Services.Core.Domain")
                                       select t)
            {
                foreach (var propAttr in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null).Select(
                       p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))
                {

                    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(classType).Invoke(modelBuilder, null);
                    var param = Expression.Parameter(classType, "c");
                    var property = Expression.Property(param, propAttr.prop.Name);
                    var lambdaExpression = Expression.Lambda(property, true,
                                                                             new ParameterExpression[]
                                                                                 {param});
                    DecimalPropertyConfiguration decimalConfig;
                    if (propAttr.prop.PropertyType.IsGenericType && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                    else
                    {
                        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[6];
                        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }

                    decimalConfig.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                }
            }


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Advert> Adverts { get; set; }
        public virtual DbSet<AdvertType> AdvertTypes { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public DbSet<BranchIntroduction> BranchIntroductions { get; set; }
        public DbSet<BranchOpeningTime> BranchOpeningTimes { get; set; }
        public DbSet<BranchPaymentMethod> BranchPaymentMethods { get; set; }
        public DbSet<BranchProduct> BranchProducts { get; set; }
        public DbSet<BranchProductOffer> BranchProductOffers { get; set; }
        public DbSet<BranchTaxRate> BranchTaxRates { get; set; }
        public virtual DbSet<CatalogueApplication> CatalogueApplications { get; set; }
        public DbSet<CatalogueAssembly> CatalogueAssemblies { get; set; }
        public DbSet<CatalogueAssemblyIllustration> CatalogueAssemblyIllustrations { get; set; }
        public DbSet<CatalogueAssemblyNode> CatalogueAssemblyNodes { get; set; }
        public DbSet<CatalogueAssemblyNodeProduct> CatalogueAssemblyNodeProducts { get; set; }
        public DbSet<CatalogueAssemblyNodeTitle> CatalogueAssemblyNodeTitles { get; set; }
        public DbSet<CatalogueAssemblyTitle> CatalogueAssemblyTitles { get; set; }
        public DbSet<CatalogueCategory> CatalogueCategories { get; set; }
        public DbSet<CatalogueCategoryIntroduction> CatalogueCategoryIntroductions { get; set; }
        public DbSet<CatalogueCategoryTitle> CatalogueCategoryTitles { get; set; }
        public DbSet<CatalogueFamily> CatalogueFamilies { get; set; }
        public DbSet<CatalogueFamilyTitle> CatalogueFamilyTitles { get; set; }
        public DbSet<CatalogueModel> CatalogueModels { get; set; }
        public DbSet<CatalogueModelTitle> CatalogueModelTitles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryIntroduction> CategoryIntroductions { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<CategoryTitle> CategoryTitles { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<TaxRateCategory> TaxRateCategories { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccountType> CustomerAccountTypes { get; set; }
        public DbSet<CustomerEmailAddress> CustomerEmailAddresses { get; set; }
        public DbSet<CustomerPhoneNumber> CustomerPhoneNumbers { get; set; }
        public DbSet<CustomerShippingAddress> CustomerShippingAddresses { get; set; }
        public DbSet<CustomerTradingLevel> CustomerTradingLevels { get; set; }
        public DbSet<CustomerVehicle> CustomerVehicles { get; set; }
        public virtual DbSet<ShowEvent> Events { get; set; }
        public DbSet<ShowEventDateTime> EventDateTimes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTitle> CountryTitles { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Locale> Locales { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<WebOrder> WebOrders { get; set; }
        public DbSet<WebOrderEvent> WebOrderEvents { get; set; }
        public DbSet<WebOrderEventNote> WebOrderEventNotes { get; set; }
        public DbSet<WebOrderEventType> WebOrderEventTypes { get; set; }
        public DbSet<WebOrderItem> WebOrderItems { get; set; }
        public DbSet<WebOrderItemStatus> WebOrderItemStatuses { get; set; }
        public DbSet<WebOrderStatus> WebOrderStatuses { get; set; }
        public DbSet<PackageManifestItem> PackageManifestItems { get; set; }
        public DbSet<PackingContainer> PackingContainers { get; set; }
        public virtual DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodTitle> PaymentMethodTitles { get; set; }
        public DbSet<InterfacePhrase> InterfacePhrases { get; set; }
        public DbSet<InterfacePhraseTranslation> InterfacePhraseTranslations { get; set; }
        public DbSet<DiscountLevel> DiscountLevels { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<ProductAlternative> ProductAlternatives { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductComponentStatus> ProductComponentStatuses { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductLink> ProductLinks { get; set; }
        public DbSet<ProductQuantityBreakDiscountLevel> ProductQuantityBreakDiscountLevels { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductSupersession> ProductSupersessions { get; set; }
        public DbSet<ProductText> ProductTexts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ShippingCoverageLevel> ShippingCoverageLevels { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<ShippingProvider> ShippingProviders { get; set; }
        public DbSet<ShippingQuote> ShippingQuotes { get; set; }
        public DbSet<ShippingQuotePackage> ShippingQuotePackages { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBodyVariant> VehicleBodyVariants { get; set; }
        public DbSet<VehicleBodyVariantTitle> VehicleBodyVariantTitles { get; set; }
        public DbSet<VehicleDrivetrainVariant> VehicleDrivetrainVariants { get; set; }
        public DbSet<VehicleDrivetrainVariantTitle> VehicleDrivetrainVariantTitles { get; set; }
        public DbSet<VehicleEngineTypeVariant> VehicleEngineTypeVariants { get; set; }
        public DbSet<VehicleEngineTypeVariantTitle> VehicleEngineTypeVariantTitles { get; set; }
        public DbSet<VehicleEngineVariant> VehicleEngineVariants { get; set; }
        public DbSet<VehicleEngineVariantTitle> VehicleEngineVariantTitles { get; set; }
        public DbSet<VehicleMarque> VehicleMarques { get; set; }
        public DbSet<VehicleMarqueTitle> VehicleMarqueTitles { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleModelTitle> VehicleModelTitles { get; set; }
        public DbSet<VehicleModelVariant> VehicleModelVariants { get; set; }
        public DbSet<VehicleModelVariantTitle> VehicleModelVariantTitles { get; set; }
        public DbSet<VehicleRange> VehicleRanges { get; set; }
        public DbSet<VehicleRangeTitle> VehicleRangeTitles { get; set; }
        public DbSet<VehicleSteeringVariant> VehicleSteeringVariants { get; set; }
        public DbSet<VehicleSteeringVariantTitle> VehicleSteeringVariantTitles { get; set; }
        public DbSet<VehicleTransmissionVariant> VehicleTransmissionVariants { get; set; }
        public DbSet<VehicleTransmissionVariantTitle> VehicleTransmissionVariantTitles { get; set; }
        public DbSet<VehicleTrimLevelVariant> VehicleTrimLevelVariants { get; set; }
        public DbSet<VehicleTrimLevelVariantTitle> VehicleTrimLevelVariantTitles { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherRedemption> VoucherRedemptions { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
    }
}
