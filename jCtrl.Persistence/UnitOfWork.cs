using EFSecondLevelCache;
using jCtrl.Infrastructure.Repositories;
using jCtrl.Services;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace jCtrl.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly jCtrlContext _context;

        public UnitOfWork(jCtrlContext context)
        {
            _context = context;

            Adverts = new AdvertRepository(context);
            Tweets = new TweetRepository(context);
            Categories = new CategoryRepository(context);
            Branches = new BranchRepository(context);
            Events = new EventRepository(context);
            WebOrders = new OrderRepository(context);
            Products = new ProductRepository(context);
            WishLists = new WishListRepository(context);
            WishListItems = new WishListItemRepository(context);
            PaymentCards = new PaymentCardsRepository(context);
            CartItems = new CartItemRepository(context);
            Countries = new CountriesRepository(context);
            ShippingProviders = new ShippingProviderRepository(context);
            ShippingMethods = new ShippingMethodRepository(context);
            PackingContainers = new PackingContainerRepository(context);
            ShippingQuotes = new ShippingQuoteRepository(context);
            CatalogueFamilies = new CatalogueFamilyRepository(context);
            CatalogueModels = new CatalogueModelRepository(context);
            CatalogueApplications = new CatalogueApplicationRepository(context);
            CatalogueAssemblies = new CatalogueAssemblyRepository(context);
            Customers = new CustomerRepository(context);
            CustomerVehicles = new CustomerVehiclesRepository(context);
            CustomerEmailAddresses = new CustomerEmailAddressRepository(context);
            CustomerShippingAddresses = new CustomerShippingAddressRepository(context);
            BranchPaymentMethods = new BranchPaymentMethodRepository(context);
            Vouchers = new VoucherRepository(context);
            RefreshTokens = new RefreshTokenRepository(context);
            Clients = new ClientRepository(context);
            UserAccounts = new UserAccountRepository(context);
        }

        public ICategoryRepository Categories { get; private set; }
        public ITweetRepository Tweets { get; private set; }
        public IAdvertRepository Adverts { get; private set; }
        public IWishListRepository WishLists { get; private set; }
        public IWishListItemRepository WishListItems { get; private set; }
        public IOrderRepository WebOrders { get; private set; }
        public IBranchRepository Branches { get; private set; }
        public IEventRepository Events { get; private set; }
        public IPaymentCardsRepository PaymentCards { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public ICartItemRepository CartItems { get; private set; }
        public ICountriesRepository Countries { get; private set; }
        public IShippingProviderRepository ShippingProviders { get; private set; }
        public IShippingMethodRepository ShippingMethods { get; private set; }
        public IPackingContainerRepository PackingContainers { get; private set; }
        public IShippingQuoteRepository ShippingQuotes { get; private set; }
        public ICatalogueFamilyRepository CatalogueFamilies { get; private set; }
        public ICatalogueModelRepository CatalogueModels { get; private set; }
        public ICatalogueApplicationRepository CatalogueApplications { get; private set; }
        public ICatalogueAssemblyRepository CatalogueAssemblies { get; private set; }
        public ICustomerVehiclesRepository CustomerVehicles { get; private set; }
        public ICustomerEmailAddressRepository CustomerEmailAddresses { get; private set; }
        public ICustomerShippingAddressRepository CustomerShippingAddresses { get; private set; }
        public IBranchPaymentMethodRepository BranchPaymentMethods { get;private set; }
        public IVoucherRepository Vouchers { get;private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }
        public IClientRepository Clients { get; private set; }
        public IUserAccountRepository UserAccounts { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
