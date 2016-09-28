using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Order;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class BranchRepositoryTests
    {
        private IList<Branch> branches;
        private IList<Customer> customers;
        private IList<Advert> adverts;
        private IList<ShowEvent> events;
        private IList<WebOrder> orders;
        private IList<BranchProduct> branchProducts;

        private IBranchRepository branchRepository;
        private ICustomerRepository customerRepository;
        private IAdvertRepository advertRepository;
        private IEventRepository eventRepository;
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;

        private int branchCode;
        private string siteCode;
        private bool isPriority;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IBranchRepository> mockBranchRepository = new Mock<IBranchRepository>();
            Mock<ICustomerRepository> mockCustomerRepository = new Mock<ICustomerRepository>();
            Mock<IAdvertRepository> mockAdvertRepository = new Mock<IAdvertRepository>();
            Mock<IEventRepository> mockEventRepository = new Mock<IEventRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            mockBranchRepository.Setup(r => r.GetBranches()).ReturnsAsync(branches);

            Func<int, Branch> branch = (int branchCode) => branches.Where(b => b.Id == branchCode).Single();
            mockBranchRepository.Setup(r => r.GetBranch(It.IsAny<int>())).ReturnsAsync(branch.Invoke(branchCode));

            Func<string, Branch> branchSiteCode = (string siteCode) => branches.Where(b => b.SiteCode == siteCode).Single();
            mockBranchRepository.Setup(r => r.GetBranchByCode(It.IsAny<string>())).ReturnsAsync(branchSiteCode.Invoke(siteCode));

            Func<int, int> branchLanguage = (int branchCode) => branches.Where(b => b.Id == branchCode).Select(l => l.Language_Id).Single();
            mockBranchRepository.Setup(r => r.GetBranchLanguageIdAsync(It.IsAny<int>())).ReturnsAsync(branchLanguage.Invoke(branchCode));

            IQueryable<Customer> customerList = customers.Where(c => c.Branch.SiteCode == "UK").AsQueryable();
            mockBranchRepository.Setup(b => b.GetCustomersByBranchCode(It.IsAny<string>())).Returns(customerList);

            IQueryable<Advert> branchAds = adverts
                .Where(a => a.Branch.SiteCode == siteCode)
                .Where(a => a.IsPriority == isPriority)
                .AsQueryable();

            mockBranchRepository.Setup(b => b.GetBranchAdvertsByCode(It.IsAny<string>(), It.IsAny<bool>())).Returns(branchAds);

            IQueryable<ShowEvent> eventsByBranch = events
                .Where(a => a.Branch.SiteCode == siteCode)
                .AsQueryable();

            mockBranchRepository.Setup(b => b.GetEventsByBranchCode(It.IsAny<string>())).Returns(eventsByBranch);

            IQueryable<WebOrder> webOrders = orders
                .Where(a => a.Branch.SiteCode == siteCode)
                .AsQueryable();

            mockBranchRepository.Setup(b => b.GetWebOrdersByBranchCode(It.IsAny<string>())).Returns(webOrders);

            IQueryable<BranchProduct> products = branchProducts
                .Where(a => a.Branch.SiteCode == siteCode)
                .AsQueryable();

            mockBranchRepository.Setup(b => b.GetProductsByBranchCode(It.IsAny<string>())).Returns(products);

            branchRepository = mockBranchRepository.Object;
            customerRepository = mockCustomerRepository.Object;
            advertRepository = mockAdvertRepository.Object;
            eventRepository = mockEventRepository.Object;
            orderRepository = mockOrderRepository.Object;
            productRepository = mockProductRepository.Object;
        }

        [Test]
        public void Get_Branches()
        {
            var branches = branchRepository.GetBranches().Result;

            Assert.AreEqual(5, branches.Count());
            Assert.IsNotNull(branches);
        }

        [Test]
        public void Get_Branch()
        {
            var branch = branchRepository.GetBranch(branchCode).Result;

            Assert.AreEqual("SNG", branch.BranchCode);
        }

        [Test]
        public void Get_BranchByCode()
        {
            var branch = branchRepository.GetBranchByCode(siteCode).Result;

            Assert.AreEqual("UK", branch.SiteCode);
        }

        [Test]
        public void Get_BranchLanguageId()
        {
            int branch = branchRepository.GetBranchLanguageIdAsync(branchCode).Result;

            Assert.AreEqual(1, branch);
        }

        [Test]
        public void Get_CustomersByBranchCode()
        {
            var customers = branchRepository.GetCustomersByBranchCode(siteCode);

            Assert.AreEqual(1, customers.Count());
        }

        [Test]
        public void Get_BranchAdvertsByCode()
        {
            var adverts = branchRepository.GetBranchAdvertsByCode(siteCode, isPriority);

            Assert.AreEqual(1, adverts.Count());
        }

        [Test]
        public void Get_EventsByBranchCode()
        {
            var events = branchRepository.GetEventsByBranchCode(siteCode);

            Assert.AreEqual(0, events.Count());
        }

        [Test]
        public void Get_WebOrdersByBranchCode()
        {
            var orders = branchRepository.GetWebOrdersByBranchCode(siteCode);

            Assert.AreEqual(1, orders.Count());
        }

        [Test]
        public void Get_ProductsByBranchCode()
        {
            var products = branchRepository.GetProductsByBranchCode(siteCode);

            Assert.AreEqual(3, products.Count());
        }

        public void InitialiseTestData()
        {
            branchCode = 1;
            siteCode = "UK";
            isPriority = true;

            branches = new List<Branch>(){
                new Branch
                {
                    Id = 1,
                    SiteCode = "UK",
                    BranchCode = "SNG",
                    Name = "SNG Barratt UK",
                    AddressLine1 = "The Heritage Building",
                    AddressLine2 = "Stourbridge Road",
                    TownCity = "Bridgnorth",
                    CountyState = "Shropshire",
                    PostalCode = "WV15 6AP",
                    CountryName = "United Kingdom",
                    Country_Code = "GB",
                    PhoneNumber = "+44 (0)1746 765 432",
                    EmailAddress = "sales.uk@sngbarratt.com",
                    Currency_Code = "GBP",
                    Language_Id = 1,
                    FlagFilename = "../img/flags/GB.png",
                    Latitude = 52.528213m,
                    Longitude = -2.403453m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 2,
                    SiteCode = "US",
                    BranchCode = "BAU",
                    Name = "SNG Barratt USA",
                    AddressLine1 = "92 Londonderry Turnpike",
                    AddressLine2 = "",
                    TownCity = "Manchester",
                    CountyState = "New Hampshire",
                    PostalCode = "03104",
                    CountryName = "United States",
                    Country_Code = "US",
                    PhoneNumber = "+1 800 452 4787",
                    EmailAddress = "sales.usa@sngbarratt.com",
                    Currency_Code = "USD",
                    Language_Id = 1,
                    FlagFilename = "../img/flags/US.png",
                    Latitude = 43.004570m,
                    Longitude = -71.393640m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 3,
                    SiteCode = "NL",
                    BranchCode = "SNH",
                    Name = "SNG Barratt NL",
                    AddressLine1 = "Laarakkerweg 12",
                    AddressLine2 = "",
                    TownCity = "Oisterwijk",
                    CountyState = "",
                    PostalCode = "5061 JR",
                    CountryName = "Netherlands",
                    Country_Code = "NL",
                    PhoneNumber = "+31 13-5211552",
                    EmailAddress = "sales.nl@sngbarratt.com",
                    Currency_Code = "EUR",
                    Language_Id = 4,
                    FlagFilename = "../img/flags/NL.png",
                    Latitude = 51.585647m,
                    Longitude = 5.204178m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 4,
                    SiteCode = "DE",
                    BranchCode = "SND",
                    Name = "SNG Barratt DE",
                    AddressLine1 = "Laarakkerweg 12",
                    AddressLine2 = "",
                    TownCity = "Oisterwijk",
                    CountyState = "",
                    PostalCode = "5061 JR",
                    CountryName = "Netherlands",
                    Country_Code = "NL",
                    PhoneNumber = "+31 13-5211552",
                    EmailAddress = "sales.de@sngbarratt.com",
                    Currency_Code = "EUR",
                    Language_Id = 3,
                    FlagFilename = "../img/flags/DE.png",
                    Latitude = 51.585647m,
                    Longitude = 5.204178m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "DE Branch Intro Text. Trades out of Holland but charges German tax rates. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "DE Branch Intro Text. Trades out of Holland but charges German tax rates. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "DE Branch Intro Text. Trades out of Holland but charges German tax rates. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "DE Branch Intro Text. Trades out of Holland but charges German tax rates. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "DE Branch Intro Text. Trades out of Holland but charges German tax rates. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 5,
                    SiteCode = "FR",
                    BranchCode = "SNF",
                    Name = "SNG Barratt FR",
                    AddressLine1 = "62 Chemin du Bois d'Alier",
                    AddressLine2 = "Zone Des Berthilliers",
                    TownCity = "Charnay-lès-Mâcon",
                    CountyState = "",
                    PostalCode = "71850",
                    CountryName = "France",
                    Country_Code = "FR",
                    PhoneNumber = "+33 385 201 420",
                    EmailAddress = "france@sngbarratt.com",
                    Currency_Code = "EUR",
                    Language_Id = 2,
                    FlagFilename = "../img/flags/FR.png",
                    Latitude = 46.290474m,
                    Longitude = 4.785418m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "FR Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "FR Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "FR Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "FR Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "FR Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            };

            var branch = branches.First();

            customers = new List<Customer>()
            {
                new Customer
                {
                    Id = Guid.Parse("e1aac15a-4dd9-40e0-96f4-2c51a19477eb"),
                    Title = "Mr",
                    FirstName = "James",
                    LastName = "Griffin",
                    AddressLine1 = "22 Beech Road",
                    TownCity = "Bridgnorth",
                    CountyState = "Shropshire",
                    PostalCode = "WV16 4PJ",
                    CountryName = "United Kingdom",
                    Country_Code = "GB",
                    IsVerifiedAddress = true,
                    CustomerAccountType_Code = "R",
                    TradingTerms_Code = "2N",
                    Branch_Id = 1,
                    Language_Id = 1,
                    PaymentMethod_Code = "CC",
                    EmailAddresses = new List<CustomerEmailAddress> {
                        new CustomerEmailAddress { Address = "admin@sngbarratt.com", IsMarketing = true, IsBilling = true, IsDefault = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    PhoneNumbers = new List<CustomerPhoneNumber> {
                            new CustomerPhoneNumber { InternationalCode = "+44", AreaCode = "01746", Number = "765984", PhoneNumberType_Id = 1, IsDefault = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                            new CustomerPhoneNumber { InternationalCode = "+44", AreaCode = "01746", Number = "765432", PhoneNumberType_Id = 2, IsDefault = false, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                            new CustomerPhoneNumber { InternationalCode = "+44", AreaCode = "07919", Number = "493723", PhoneNumberType_Id = 3, IsDefault = false, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                            new CustomerPhoneNumber { InternationalCode = "+44", AreaCode = "01458", Number = "443020", PhoneNumberType_Id = 1, IsDefault = false, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                        },
                    ShippingAddresses = new List<CustomerShippingAddress> {
                            new CustomerShippingAddress { DisplayName = "Mum and Dad's", Name = "C Griffin", AddressLine1 = "6 Barnard Avenue", TownCity = "Street", CountyState = "Somerset", PostalCode = "BA16 0RW", CountryName = "United Kingdom", Country_Code = "GB", IsVerifiedAddress = false, IsDefault = false, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                            new CustomerShippingAddress { DisplayName = "Work", Name = "James Griffin", AddressLine1 = "SNG Barratt", AddressLine2 = "The Heritage Building", TownCity = "Bridgnorth", CountyState = "Shropshire", PostalCode = "WV15 6AP", CountryName = "United Kingdom", Country_Code = "GB", IsVerifiedAddress = true, IsDefault = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                        },
                    Vehicles = new List<CustomerVehicle>
                    {
                        new CustomerVehicle {
                            DisplayName = "My Car",
                            Vehicle_Id = 1,
                            RegistrationNumber = "50EE",
                            ModelYear = 1949,
                            IsDefault = true,
                            Notes = "Some helpful notes",
                            IsActive = true,
                            SortOrder = 1,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    IsPaperlessBilling = true,
                    IsMarketingSubscriber = true,
                    IsCreditAccount = true,
                    IsOnStop = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branch
                }
            };


            adverts = new List<Advert>
            {
                new Advert
                {
                    Id = Guid.Parse("a87f6f18-6c67-4b92-9b91-7d6c7a2d8b66"),
                    AdvertType_Id = "I",
                    Title = "E-Type Hillclimb Day",
                    Description = "Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branch
                },
                new Advert
                {
                    Id = Guid.Parse("6330da08-73be-4542-afdc-c8ec02699208"),
                    AdvertType_Id = "I",
                    Title = "Advert Two",
                    Description = "Advert two has a link to an event",
                    LinkUrl = "~/uk/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branch
                },
                new Advert
                {
                    Id = Guid.Parse("6cc446dc-bb13-4899-91cf-f3237d7cac71"),
                    AdvertType_Id = "I",
                    Title = "Advert Five USA",
                    Description = "Advert five has a no link USA",
                    ImageFilename_Desktop = "bgnine.jpg",
                    ImageFilename_Device = "bgnine_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branches.Single(b => b.Id == 2)
                }
            };

            events = new List<ShowEvent>
            {
                new ShowEvent
                {
                    Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"),
                    Title = "Interclassics and Topmobiel",
                    Description = "Come and marvel at the finest and most exclusive vehicles in the Benelux, as well as everything else related to car parts and automobilia.  SNG Barratt Holland will be at the show, and taking Pre-Orders for collection.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"), StartsUtc = DateTime.Parse("2015-06-06 08:00"), EndsUtc = DateTime.Parse("2015-06-06 20:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"), StartsUtc = DateTime.Parse("2015-06-07 08:00"), EndsUtc = DateTime.Parse("2015-06-07 20:00") }
                    },
                    EventUrl = "http://www.ic-tm.nl/uk/",
                    Location = "MECC Maastrict, Netherlands",
                    MapUrl = "https://www.google.co.uk/maps/place/MECC+Maastricht/@50.8383184,5.7114246,15z/data=!4m2!3m1!1s0x47c0e984c10bc78d:0x11310fc6c0fade4c?hl=en",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branches.Single(b => b.Id == 3)
                },
                new ShowEvent
                {
                    Id = Guid.Parse("80a79f21-7530-47e8-b768-800d1a19a3c6"),
                    Title = "Event DE",
                    Description = "Quo nisi malis irure deserunt ea ex anim iudicem, nisi laborum si cupidatat, eiusmod dolore aliquip eiusmod. E summis arbitror sempiternum, laborum culpa lorem laboris esse. Multos quo eiusmod, aliquip cillum cillum ad nulla. Summis quibusdam ingeniis, labore ingeniis firmissimum qui quid probant e velit velit ut an quorum cohaerescant, do excepteur firmissimum si irure iis ne veniam excepteur, iis fugiat esse irure admodum, te litteris domesticarum. Qui ab veniam sunt elit.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("80a79f21-7530-47e8-b768-800d1a19a3c6"), StartsUtc = DateTime.Parse("2015-08-18 08:00"), EndsUtc = DateTime.Parse("2015-08-18 20:00") }
                    },
                    Location = "Wolfsburg, Germany",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branches.Single(b => b.Id == 4)
                }
            };

            var cust = customers.First();

            orders = new List<WebOrder>
            {
                new WebOrder
                    {
                        Id = Guid.Parse("b1238cd5-477c-4ce2-9131-fc1f3d7eab03"),
                        OrderNo = 99999,
                        OrderDate = DateTime.Now.Date,
                        Language_Id = 1,
                        Branch_Id = 1,
                        Customer_Id = cust.Id,
                        CustomerConfirmationRequired = false,
                        BillingName = (cust.Title + " " + cust.FirstName + " " + cust.LastName).Replace("  ", " ").Trim(),
                        BillingAddressLine1 = cust.AddressLine1,
                        BillingAddressLine2 = cust.AddressLine2,
                        BillingTownCity = cust.TownCity,
                        BillingCountyState = cust.CountyState,
                        BillingPostalCode = cust.PostalCode,
                        BillingCountryName = cust.CountryName,
                        BillingCountryCode = cust.Country_Code,
                        DeliveryName = cust.DefaultShippingAddress.Name,
                        DeliveryAddressLine1 = cust.DefaultShippingAddress.AddressLine1,
                        DeliveryAddressLine2 = cust.DefaultShippingAddress.AddressLine2,
                        DeliveryTownCity = cust.DefaultShippingAddress.TownCity,
                        DeliveryCountyState = cust.DefaultShippingAddress.CountyState,
                        DeliveryPostalCode = cust.DefaultShippingAddress.PostalCode,
                        DeliveryCountryName = cust.DefaultShippingAddress.CountryName,
                        DeliveryCountryCode = cust.DefaultShippingAddress.Country_Code,
                        DeliveryContactNumber = (cust.DefaultPhoneNumber.InternationalCode + " " + cust.DefaultPhoneNumber.AreaCode + " " + cust.DefaultPhoneNumber.Number).Replace("  ", " ").Trim(),
                        Branch = branch,
                        Items = new List<WebOrderItem> {
                            new WebOrderItem
                            {
                                //Id = 1,
                                Branch_Id = 1,
                                Customer_Id = cust.Id,
                                CustomerLevel_Id = cust.TradingTerms_Code,
                                WebOrder_Id = Guid.Parse( "b1238cd5-477c-4ce2-9131-fc1f3d7eab03"),
                                LineNo = 1,
                                WebOrderItemStatus_Id = "S",
                                PartNumber = "SBS1000",
                                PartTitle = "Test Part Number",
                                DiscountCode = "#0",
                                TaxRateCategory_Id = 1,
                                RetailPrice = 12.00m,
                                UnitPrice = 10.00m,
                                Surcharge = 0.00m,
                                PackedWidthCms = 0.00m,
                                PackedHeightCms = 0.00m,
                                PackedDepthCms = 0.00m,
                                PackedWeightKgs = 2.00m,
                                QuantityRequired = 1,
                                QuantityAllocated = 0,
                                QuantityBackOrdered = 0,
                                QuantityPicked = 0,
                                QuantityPacked = 0,
                                QuantityInvoiced = 0,
                                QuantityCredited = 0,
                                RowVersion = 1,
                                CreatedTimestampUtc = DateTime.UtcNow,
                                CreatedByUsername = "Seed",
                                UpdatedTimestampUtc = DateTime.UtcNow,
                                UpdatedByUsername = "Seed"
                            },
                             new WebOrderItem
                            {
                                //Id = 2,
                                Branch_Id = 1,
                                Customer_Id = cust.Id,
                                CustomerLevel_Id = cust.TradingTerms_Code,
                                WebOrder_Id = Guid.Parse( "b1238cd5-477c-4ce2-9131-fc1f3d7eab03"),
                                LineNo = 2,
                                WebOrderItemStatus_Id = "S",
                                PartNumber = "JIM1010",
                                PartTitle = "Test Part Number 10",
                                DiscountCode = "#2",
                                TaxRateCategory_Id = 1,
                                RetailPrice = 100.00m,
                                UnitPrice = 87.50m,
                                Surcharge = 0.00m,
                                PackedWidthCms = 0.00m,
                                PackedHeightCms = 0.00m,
                                PackedDepthCms = 0.00m,
                                PackedWeightKgs = 2.00m,
                                QuantityRequired = 1,
                                QuantityAllocated = 0,
                                QuantityBackOrdered = 0,
                                QuantityPicked = 0,
                                QuantityPacked = 0,
                                QuantityInvoiced = 0,
                                QuantityCredited = 0,
                                RowVersion = 1,
                                CreatedTimestampUtc = DateTime.UtcNow,
                                CreatedByUsername = "Seed",
                                UpdatedTimestampUtc = DateTime.UtcNow,
                                UpdatedByUsername = "Seed"
                            }
                        },
                        EstimatedShippingWeightKgs = 2.125m,
                        EstimatedShippingCost = 4.75m,
                        ShippingMethodName = "UPS Standard",
                        ShippingCharge = 7.50m,
                        ShippingTaxRate = 20.00m,
                        GoodsAtRate1 = 10.00m,
                        GoodsAtRate2 = 0.00m,
                        GoodsTaxRate1 = 20.00m,
                        GoodsTaxRate2 = 0.00m,
                        GrandTotal = 21.00m,

                        OrderEvents = new List<WebOrderEvent> {
                            new WebOrderEvent
                            {
                                Id = 1,
                                WebOrder_Id = Guid.Parse( "b1238cd5-477c-4ce2-9131-fc1f3d7eab03"),
                                WebOrderEventType_Id = "WS",
                                RowVersion = 1,
                                CreatedTimestampUtc = DateTime.UtcNow,
                                CreatedByUsername = "Seed",
                                UpdatedTimestampUtc = DateTime.UtcNow,
                                UpdatedByUsername = "Seed"
                            }
                        },
                        PaymentMethod_Code = "AC",
                        WebOrderStatus_Id = "S",
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        CreatedByUsername = "Seed",
                        UpdatedTimestampUtc = DateTime.UtcNow,
                        UpdatedByUsername = "Seed"

                    }
            };

            branchProducts = new List<BranchProduct>
            {
                new BranchProduct
                {
                    Id = Guid.Parse("a19692e3-e1d3-405a-b00c-745a72eb4290"),
                    Branch_Id = 1,
                    Product_Id = 1,
                    RetailPrice = 10,
                    TradePrice = 0,
                    AvgCostPrice = 3,
                    Surcharge = 0,
                    MinStockLevel = 1,
                    MaxStockLevel = 20,
                    Quantity = 12,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    Branch = branch
                },
                new BranchProduct
                {
                    Id = Guid.Parse("e9ccdca7-e277-4ee8-984b-b112a7357825"),
                    Branch_Id = 1,
                    Product_Id = 2,
                    RetailPrice = 12,
                    TradePrice = 0,
                    AvgCostPrice = 3,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 10,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    Branch = branch
                },
                new BranchProduct
                {
                    Id = Guid.Parse("8efbe693-e695-4613-99fb-9046fce8dd1e"),
                    Branch_Id = 1,
                    Product_Id = 9,
                    RetailPrice = 10,
                    TradePrice = 0,
                    AvgCostPrice = 0,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 0,
                    BranchStatus_Code = "S",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    Branch = branch
                }
            };
           

            
        }
    }
}
