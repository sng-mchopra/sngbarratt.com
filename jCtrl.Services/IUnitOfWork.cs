using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        ITweetRepository Tweets { get; } 
        IAdvertRepository Adverts { get; }
        IWishListRepository WishLists { get; }
        IWishListItemRepository WishListItems { get; }
        IOrderRepository WebOrders { get; }
        IBranchRepository Branches { get; }
        IEventRepository Events { get; }
        IPaymentCardsRepository PaymentCards { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        ICartItemRepository CartItems { get; }
        ICountriesRepository Countries { get; }
        IShippingProviderRepository ShippingProviders{ get; }
        IShippingMethodRepository ShippingMethods{ get; }
        IPackingContainerRepository PackingContainers{ get; }
        IShippingQuoteRepository ShippingQuotes{ get; }
        ICatalogueFamilyRepository CatalogueFamilies { get; }
        ICatalogueModelRepository CatalogueModels { get; }
        ICatalogueApplicationRepository CatalogueApplications { get; }
        ICatalogueAssemblyRepository CatalogueAssemblies { get; }
        ICustomerVehiclesRepository CustomerVehicles { get; }
        ICustomerEmailAddressRepository CustomerEmailAddresses { get; }
        ICustomerShippingAddressRepository CustomerShippingAddresses { get; }
        IBranchPaymentMethodRepository BranchPaymentMethods { get; }
        IVoucherRepository Vouchers{ get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IClientRepository Clients { get; }
        IUserAccountRepository UserAccounts { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
