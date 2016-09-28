using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Customer;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class CustomerVehiclesRepositoryTests
    {
        private IList<Customer> customers;
        private ICustomerVehiclesRepository customerVehicleRepository;
        private Guid custId;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<ICustomerVehiclesRepository> mockcustomerVehicleRepository = new Mock<ICustomerVehiclesRepository>();

            mockcustomerVehicleRepository.Setup(mr => mr.GetCustomerVehicles(It.IsAny<Guid>())).ReturnsAsync(customers.Where(c => c.Id == custId).SelectMany(v => v.Vehicles));

            customerVehicleRepository = mockcustomerVehicleRepository.Object;
        }

        [Test]
        public void Get_CustomerVehicles()
        {
            var customerVehicles = customerVehicleRepository.GetCustomerVehicles(custId).Result;

            Assert.AreEqual(1, customerVehicles.Count());
        }

        public void InitialiseTestData()
        {
            custId = Guid.Parse("e1aac15a-4dd9-40e0-96f4-2c51a19477eb");

            customers = new List<Customer>
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
                    UpdatedByUsername = "Seed"
                }
            };
        }
    }
}
