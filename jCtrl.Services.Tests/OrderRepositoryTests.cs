using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Branch;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private IList<WebOrder> orders;
        private IList<Customer> customers;
        private IList<Branch> branches;

        private IOrderRepository orderRepository;
        private Guid orderId;
        private Guid customerId;
        private bool isOngoing;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(mr => mr.GetOrder(It.IsAny<Guid>())).ReturnsAsync(orders.Where(o => o.Id == orderId).Single());

            IQueryable<WebOrder> customerOrders = orders.Where(o => o.Customer_Id == customerId).AsQueryable();
            mockOrderRepository.Setup(mr => mr.GetOrderByCustomerId(It.IsAny<Guid>())).Returns(customerOrders);

            IQueryable<WebOrder> currentCustomerOrders = orders.Where(o => o.Customer_Id == customerId).Where(o => o.Status.IsOnGoing == isOngoing).AsQueryable();
            mockOrderRepository.Setup(mr => mr.GetCurrentOrdersByCustomerId(It.IsAny<Guid>(), It.IsAny<bool>())).Returns(currentCustomerOrders);

            orderRepository = mockOrderRepository.Object;
        }

        [Test]
        public void Get_Order()
        {
            var order = orderRepository.GetOrder(orderId).Result;

            Assert.AreEqual(99999, order.OrderNo);
        }

        [Test]
        public void Get_OrderByCustomerId()
        {
            var customerOrders = orderRepository.GetOrderByCustomerId(customerId);

            Assert.AreEqual("Mr James Griffin", customerOrders.First().BillingName);
        }

        [Test]
        public void Get_CurrentOrdersByCustomerId()
        {
            var currentOrders = orderRepository.GetCurrentOrdersByCustomerId(customerId, isOngoing);

            Assert.AreEqual(1, currentOrders.Count());
        }


        public void InitialiseTestData()
        {
            orderId = Guid.Parse("b1238cd5-477c-4ce2-9131-fc1f3d7eab03");
            customerId = Guid.Parse("e1aac15a-4dd9-40e0-96f4-2c51a19477eb");
            isOngoing = true; 

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
                        UpdatedByUsername = "Seed",
                        Status = new WebOrderStatus() { IsOnGoing = true }

                    }
            };
        }
    }
}
