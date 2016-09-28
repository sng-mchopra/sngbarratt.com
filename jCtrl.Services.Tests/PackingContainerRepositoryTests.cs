using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Branch;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class PackingContainerRepositoryTests
    {
        private IList<PackingContainer> packingContainers;
        private IList<Branch> branches;
        private IPackingContainerRepository packingContainerRepository;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IPackingContainerRepository> mockContainerRepository = new Mock<IPackingContainerRepository>();

            packingContainerRepository = mockContainerRepository.Object;
        }

        public void InitialiseTestData()
        {
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

            packingContainers = new List<PackingContainer>
           {
               new PackingContainer
               {
                   Id = 1,
                   ExternalDepthCms = 34,
                   ExternalHeightCms = 33,
                   ExternalVolumeCm3 = 1,
                   ExternalWidthCms = 11,
                   InternalDepthCms = 111,
                   InternalHeightCms = 3,
                   InternalVolumeCm3 = 12, 
                   InternalWidthCms = 333,
                   IsActive = true, 
                   IsUpsOnly = true,
                   MaxWeightKgs = 34, 
                   Name = "Packing Container 1",
                   SortOrder = 1, 
                   UnitPrice = 200,
                   UnitWeightKgs = 23, 
                   UPS_Package_Ref = "UPS111",
                   Branch = branches.Single(b => b.Id == 1) 
               },
                new PackingContainer
               {
                   Id = 2,
                   ExternalDepthCms = 3,
                   ExternalHeightCms = 3,
                   ExternalVolumeCm3 = 1,
                   ExternalWidthCms = 1,
                   InternalDepthCms = 11,
                   InternalHeightCms = 3,
                   InternalVolumeCm3 = 2,
                   InternalWidthCms = 33,
                   IsActive = false,
                   IsUpsOnly = false,
                   MaxWeightKgs = 34,
                   Name = "Packing Container 2",
                   SortOrder = 1,
                   UnitPrice = 250,
                   UnitWeightKgs = 23,
                   UPS_Package_Ref = "UPS222",
                   Branch = branches.Single(b => b.Id == 2)
               }
           };
        }
    }
}
