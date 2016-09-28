namespace jCtrl.Infrastructure.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services.Core.Domain;
    using Services.Core.Domain.Advert;
    using Services.Core.Domain.Branch;
    using Services.Core.Domain.Catalogue;
    using Services.Core.Domain.Customer;
    using Services.Core.Domain.Globalization;
    using Services.Core.Domain.Order;
    using Services.Core.Domain.Payment;
    using Services.Core.Domain.Product;
    using Services.Core.Domain.Vehicle;
    using Services.Core.Domain.WishList;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<jCtrl.Infrastructure.jCtrlContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(jCtrl.Infrastructure.jCtrlContext context)
        {
            //  This method will be called after migrating to the latest version.


            // attach a debugger
            //if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();


            // exit if there is already some data in the database
            if (context.Branches.Any()) return;


            // turn on identity insert for every table
            #region "TURN ON IDENTITY INSERT"
            foreach (PropertyInfo pi in context.GetType().GetProperties())
            {
                var attributes = pi.GetCustomAttributes();

                try
                {
                    if (pi.Name != "Users" && pi.Name != "Roles" && pi.Name != "Claims")
                    {
                        System.Diagnostics.Debug.WriteLine("Adding IDENTITY INSERT to " + pi.Name);
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[" + pi.Name + "] ON");
                    }
                }
                catch (SqlException e)
                {

                    if (e.Number == 8106)
                    {
                        // ignore the errors for tables that don't have an identity
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                }

            }
            #endregion




            #region "Advert Types"

            // add advert types
            context.AdvertTypes.AddOrUpdate(a => a.Id,
                new AdvertType { Id = "I", Name = "Image" },
                new AdvertType { Id = "V", Name = "Video" }
            );

            #endregion

            #region "Branches"

            // add branches
            context.Branches.AddOrUpdate(b => b.Id,
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
            );

            // add tax rates
            context.BranchTaxRates.AddOrUpdate(r => new { r.Branch_Id, r.TaxRateCategory_Id },
                new BranchTaxRate { Branch_Id = 1, TaxRateCategory_Id = 1, Rate = 20.00m },
                new BranchTaxRate { Branch_Id = 1, TaxRateCategory_Id = 2, Rate = 0.00m },
                new BranchTaxRate { Branch_Id = 2, TaxRateCategory_Id = 1, Rate = 0.00m },
                new BranchTaxRate { Branch_Id = 2, TaxRateCategory_Id = 2, Rate = 0.00m },
                new BranchTaxRate { Branch_Id = 3, TaxRateCategory_Id = 1, Rate = 19.00m },
                new BranchTaxRate { Branch_Id = 3, TaxRateCategory_Id = 2, Rate = 7.00m },
                new BranchTaxRate { Branch_Id = 4, TaxRateCategory_Id = 1, Rate = 21.00m },
                new BranchTaxRate { Branch_Id = 4, TaxRateCategory_Id = 2, Rate = 6.00m },
                new BranchTaxRate { Branch_Id = 5, TaxRateCategory_Id = 1, Rate = 19.60m },
                new BranchTaxRate { Branch_Id = 5, TaxRateCategory_Id = 2, Rate = 5.50m }
                );


            // add opening times
            context.BranchOpeningTimes.AddOrUpdate(t => new { t.Branch_Id, t.Day },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Monday, OpensUtc = TimeSpan.FromHours(7.5), ClosesUtc = TimeSpan.FromHours(16.5) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Tuesday, OpensUtc = TimeSpan.FromHours(7.5), ClosesUtc = TimeSpan.FromHours(16.5) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Wednesday, OpensUtc = TimeSpan.FromHours(7.5), ClosesUtc = TimeSpan.FromHours(16.5) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Thursday, OpensUtc = TimeSpan.FromHours(7.5), ClosesUtc = TimeSpan.FromHours(16.5) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Friday, OpensUtc = TimeSpan.FromHours(7.5), ClosesUtc = TimeSpan.FromHours(16.5) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Saturday, OpensUtc = TimeSpan.FromHours(8), ClosesUtc = TimeSpan.FromHours(11) },
                new BranchOpeningTime { Branch_Id = 1, Day = DayOfWeek.Sunday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Monday, OpensUtc = TimeSpan.FromHours(12.5), ClosesUtc = TimeSpan.FromHours(23) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Tuesday, OpensUtc = TimeSpan.FromHours(12.5), ClosesUtc = TimeSpan.FromHours(23) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Wednesday, OpensUtc = TimeSpan.FromHours(12.5), ClosesUtc = TimeSpan.FromHours(23) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Thursday, OpensUtc = TimeSpan.FromHours(12.5), ClosesUtc = TimeSpan.FromHours(23) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Friday, OpensUtc = TimeSpan.FromHours(12.5), ClosesUtc = TimeSpan.FromHours(23) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Saturday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 2, Day = DayOfWeek.Sunday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Monday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Tuesday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Wednesday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Thursday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Friday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Saturday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 3, Day = DayOfWeek.Sunday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Monday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Tuesday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Wednesday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Thursday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15.5) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Friday, OpensUtc = TimeSpan.FromHours(6.5), ClosesUtc = TimeSpan.FromHours(15) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Saturday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 4, Day = DayOfWeek.Sunday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Monday, OpensUtc = TimeSpan.FromHours(7), ClosesUtc = TimeSpan.FromHours(16) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Tuesday, OpensUtc = TimeSpan.FromHours(7), ClosesUtc = TimeSpan.FromHours(16) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Wednesday, OpensUtc = TimeSpan.FromHours(7), ClosesUtc = TimeSpan.FromHours(16) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Thursday, OpensUtc = TimeSpan.FromHours(7), ClosesUtc = TimeSpan.FromHours(16) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Friday, OpensUtc = TimeSpan.FromHours(7), ClosesUtc = TimeSpan.FromHours(16) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Saturday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) },
                new BranchOpeningTime { Branch_Id = 5, Day = DayOfWeek.Sunday, OpensUtc = TimeSpan.FromHours(0), ClosesUtc = TimeSpan.FromHours(0) }
            );

            #endregion

            #region "Clients"

            // add clients
            context.Clients.AddOrUpdate(c => c.Id,
                new Client { Id = Guid.Parse("b08133c5-5ab8-47dd-837c-4a88e00fe24d"), Name = "Dev Website", Secret = jCtrl.Services.Core.Utils.Helpers.GetHash("a0d94743-e737-49c3-97b3-6ec7c74997e6"), AllowedOrigin = "http://localhost:40549,https://localhost:40549,http://sngbarratt-dev.azurewebsites.net,https://sngbarratt-dev.azurewebsites.net,http://www.sngbarratt.com,https://www.sngbarratt.com", RefreshTokenTTL = 7200, IsSecure = false, IsActive = true }, // TODO: restrict origin to sngbarratt.com
                new Client { Id = Guid.Parse("4b19d863-62b2-4715-aa33-41438838e229"), Name = "Public Website", Secret = jCtrl.Services.Core.Utils.Helpers.GetHash("5c56bddf-d443-4f0d-9284-2d79c8481673"), AllowedOrigin = "*", RefreshTokenTTL = 7200, IsSecure = false, IsActive = true }, // restrict origin to sngbarratt.com
                new Client { Id = Guid.Parse("4b19d863-5ab8-4715-837c-4148e00fe229"), Name = "Proto Website", Secret = jCtrl.Services.Core.Utils.Helpers.GetHash("5c56bddf-e737-4f0d-9284-6ec7c74997e6"), AllowedOrigin = "http://localhost:40549,https://localhost:40549,http://sngbarratt-proto.azurewebsites.net,https://sngbarratt-proto.azurewebsites.net,http://www.sngbarratt.com,https://www.sngbarratt.com", RefreshTokenTTL = 7200, IsSecure = false, IsActive = true } // TODO: restrict origin to sngbarratt.com
            );

            #endregion

            #region "Countries"

            // add countries
            context.Countries.AddOrUpdate(c => c.Code,
                new Country
                {
                    Code = "GB",
                    // Name = "United Kingdom",
                    InternationalDialingCode = "+44",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "FR",
                    // Name = "France",
                    InternationalDialingCode = "+33",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "DE",
                    // Name = "Germany",
                    InternationalDialingCode = "+49",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "NL",
                    // Name = "Netherlands",
                    InternationalDialingCode = "+31",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "US",
                    // Name = "United States",
                    InternationalDialingCode = "+1",
                    IsMemberOfEEC = false,
                    IsEuropean = false
                }
            );

            // add country titles
            context.CountryTitles.AddOrUpdate(t => t.Id,
                new CountryTitle { Id = 1, Country_Code = "GB", Language_Id = 1, Title = "United Kingdom", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Great Britian, England, Wales, Scotland", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 2, Country_Code = "GB", Language_Id = 2, Title = "Royaume-Uni", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Grande-Bretagne, Angleterre, Pays de Galles, Écosse", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 3, Country_Code = "GB", Language_Id = 3, Title = "Großbritannien", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, England, Wales, Schottland", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 4, Country_Code = "GB", Language_Id = 4, Title = "Verenigd Koningkrijk", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, groot-Brittannië, Engeland, Wales, Schotland", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 5, Country_Code = "GB", Language_Id = 5, Title = "United Kingdom", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Great Britian, England, Wales, Scotland", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 6, Country_Code = "FR", Language_Id = 1, Title = "France", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 7, Country_Code = "DE", Language_Id = 1, Title = "Germany", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 8, Country_Code = "NL", Language_Id = 1, Title = "Netherlands", AlternativeSpellings = "Holland, The Netherlands", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CountryTitle { Id = 6, Country_Code = "US", Language_Id = 1, Title = "United States", AlternativeSpellings = "US, U.S, U.S.A, America", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            #endregion

            #region "Currencies"

            // add currencies
            context.Currencies.AddOrUpdate(c => c.Code,
                new Currency { Code = "GBP", Name = "Pounds Sterling", Symbol = "£" },
                new Currency { Code = "EUR", Name = "Euro", Symbol = "€" },
                new Currency { Code = "USD", Name = "US Dollar", Symbol = "$" },
                new Currency { Code = "AUD", Name = "Australia Dollar", Symbol = "$" }
            );

            #endregion

            #region "Customer Account Types"

            // add customer account types
            context.CustomerAccountTypes.AddOrUpdate(c => c.Code,
                new CustomerAccountType { Code = "C", Name = "Competitor" },
                //new CustomerAccountType { Code = "G", Name = "Garage" },
                new CustomerAccountType { Code = "I", Name = "Internal" },
                new CustomerAccountType { Code = "R", Name = "Retail" },
                //new CustomerAccountType { Code = "S", Name = "Specialist" },
                new CustomerAccountType { Code = "T", Name = "Trade" }

            );

            #endregion

            #region "Customer Trading Levels"

            // add customer trading levels
            context.CustomerTradingLevels.AddOrUpdate(t => t.Code,
                new CustomerTradingLevel { Code = "RN", Name = "Retail" },
                new CustomerTradingLevel { Code = "1N", Name = "Level 1" },
                new CustomerTradingLevel { Code = "1Y", Name = "Level 1 with TNET" },
                new CustomerTradingLevel { Code = "2N", Name = "Level 2" },
                new CustomerTradingLevel { Code = "2Y", Name = "Level 2 with TNET" },
                new CustomerTradingLevel { Code = "3N", Name = "Level 3" },
                new CustomerTradingLevel { Code = "3Y", Name = "Level 3 with TNET" },
                new CustomerTradingLevel { Code = "4N", Name = "Level 4" },
                new CustomerTradingLevel { Code = "4Y", Name = "Level 4 with TNET" },
                new CustomerTradingLevel { Code = "5N", Name = "Level 5" },
                new CustomerTradingLevel { Code = "5Y", Name = "Level 5 with TNET" },
                new CustomerTradingLevel { Code = "6N", Name = "Level 6" },
                new CustomerTradingLevel { Code = "6Y", Name = "Level 6 with TNET" },
                new CustomerTradingLevel { Code = "CN", Name = "Cost" }
            );

            #endregion

            #region "Discount Levels"

            // add discount levels
            context.DiscountLevels.AddOrUpdate(d => d.Code,
                new DiscountLevel { Code = "#0", Retail = 100, Level2 = 100, Level3 = 100, Level4 = 100, Level5 = 100, Level6 = 100 },
                new DiscountLevel { Code = "#1", Retail = 100, Level2 = 97.5m, Level3 = 95, Level4 = 92.5m, Level5 = 90, Level6 = 85 },
                new DiscountLevel { Code = "#2", Retail = 90, Level2 = 80, Level3 = 70, Level4 = 60, Level5 = 55, Level6 = 50 }
            );

            #endregion

            #region "Languages"

            // add languages
            context.Languages.AddOrUpdate(l => l.Id,
                new Language { Id = 1, Code = "EN", Name = "English" },
                new Language { Id = 2, Code = "FR", Name = "French" },
                new Language { Id = 3, Code = "DE", Name = "German" },
                new Language { Id = 4, Code = "NL", Name = "Dutch" },
                new Language { Id = 5, Code = "US", Name = "American" }
            );

            #endregion

            #region "Interface Phrases"

            // add interface phrases
            context.InterfacePhrases.AddOrUpdate(p => p.Id,
                new InterfacePhrase
                {
                    Id = 1,
                    ClassName = "loc-ujps",
                    Description = "SNG Brand Tag Line",
                    Context = "Site Banner",
                    Translations = new List<InterfacePhraseTranslation> {
                        new InterfacePhraseTranslation { Language_Id = 1, Text = "Ultimate Jaguar Parts Specialist", CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    }
                }
            );

            #endregion            

            #region "Payment Methods"

            // add payment methods
            context.PaymentMethods.AddOrUpdate(m => m.Code,
                new PaymentMethod
                {
                    Code = "AC",
                    Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "AC", Language_Id = 1, Title = "Credit Account", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "AC", Language_Id = 2, Title = "compte créditeur", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "AC", Language_Id = 3, Title = "Kreditkonto", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "AC", Language_Id = 4, Title = "kredietrekening", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "AC", Language_Id = 5, Title = "Account", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }

                    },
                    IsActive = true
                },
                new PaymentMethod
                {
                    Code = "CC",
                    Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "CC", Language_Id = 1, Title = "Credit / Debit Card", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "CC", Language_Id = 2, Title = "carte de crédit", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "CC", Language_Id = 3, Title = "Kreditkarte", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "CC", Language_Id = 4, Title = "kredietkaart", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "CC", Language_Id = 5, Title = "Credit Card", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                    IsActive = true
                },
                new PaymentMethod
                {
                    Code = "BT",
                    Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "BT", Language_Id = 1, Title = "Bank Transfer", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "BT", Language_Id = 2, Title = "virement bancaire", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "BT", Language_Id = 3, Title = "Banküberweisung", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "BT", Language_Id = 4, Title = "overschrijving", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "BT", Language_Id = 5, Title = "Wire Transfer", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                    IsActive = true
                },
                new PaymentMethod
                {
                    Code = "ID",
                    Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "ID", Language_Id = 1, Title = "iDEAL", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "ID", Language_Id = 2, Title = "iDEAL", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "ID", Language_Id = 3, Title = "iDEAL", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "ID", Language_Id = 4, Title = "iDEAL", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "ID", Language_Id = 5, Title = "iDEAL", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                    IsActive = true
                },
                 new PaymentMethod
                 {
                     Code = "PC",
                     Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "PC", Language_Id = 1, Title = "Payment on Collection", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PC", Language_Id = 2, Title = "Payer sur la collecte", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PC", Language_Id = 3, Title = "Bezahlung bei Abholung", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PC", Language_Id = 4, Title = "Betaling bij afhaling", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PC", Language_Id = 5, Title = "Payment on Collection", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                     IsActive = true
                 },
                 new PaymentMethod
                 {
                     Code = "PP",
                     Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "PP", Language_Id = 1, Title = "PayPal", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PP", Language_Id = 2, Title = "PayPal", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PP", Language_Id = 3, Title = "PayPal", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PP", Language_Id = 4, Title = "PayPal", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "PP", Language_Id = 5, Title = "PayPal", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                     IsActive = true
                 },
                 new PaymentMethod
                 {
                     Code = "SF",
                     Titles = new List<PaymentMethodTitle>
                    {
                        new PaymentMethodTitle { PaymentMethod_Code = "SF", Language_Id = 1, Title = "SOFORT", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "SF", Language_Id = 2, Title = "SOFORT", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "SF", Language_Id = 3, Title = "SOFORT", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "SF", Language_Id = 4, Title = "SOFORT", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" },
                        new PaymentMethodTitle { PaymentMethod_Code = "SF", Language_Id = 5, Title = "SOFORT", RowVersion=1,CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername ="Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername="Seed" }
                    },
                     IsActive = true
                 }

                );

            // add branch payment methods
            context.BranchPaymentMethods.AddOrUpdate(m => m.Id,
                new BranchPaymentMethod { Id = 1, Branch_Id = 1, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 2, Branch_Id = 1, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 3, Branch_Id = 1, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 4, Branch_Id = 1, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 5, Branch_Id = 1, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 6, Branch_Id = 2, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 7, Branch_Id = 2, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 8, Branch_Id = 2, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 9, Branch_Id = 2, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 9, Branch_Id = 2, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 10, Branch_Id = 3, PaymentMethod_Code = "CC", SortOrder = 3, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 11, Branch_Id = 3, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 12, Branch_Id = 3, PaymentMethod_Code = "ID", SortOrder = 2, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 13, Branch_Id = 3, PaymentMethod_Code = "BT", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 14, Branch_Id = 3, PaymentMethod_Code = "PP", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 15, Branch_Id = 3, PaymentMethod_Code = "PC", SortOrder = 6, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 16, Branch_Id = 4, PaymentMethod_Code = "CC", SortOrder = 3, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 17, Branch_Id = 4, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 18, Branch_Id = 4, PaymentMethod_Code = "SF", SortOrder = 2, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 19, Branch_Id = 4, PaymentMethod_Code = "BT", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 20, Branch_Id = 4, PaymentMethod_Code = "PP", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 21, Branch_Id = 4, PaymentMethod_Code = "PC", SortOrder = 6, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 22, Branch_Id = 5, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 23, Branch_Id = 5, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 24, Branch_Id = 5, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 25, Branch_Id = 5, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 26, Branch_Id = 5, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true }
            );

            #endregion

            #region "Phone Number Types"

            // add phone types
            context.PhoneNumberTypes.AddOrUpdate(p => p.Id,
                new PhoneNumberType { Id = 1, Name = "Home", IsDefault = true },
                new PhoneNumberType { Id = 2, Name = "Work", IsDefault = false },
                new PhoneNumberType { Id = 3, Name = "Mobile", IsDefault = false }
            );

            #endregion

            #region "Product Brands"

            // add product brands
            context.ProductBrands.AddOrUpdate(b => b.Id,
                new ProductBrand { Id = 1, Name = "SNG Barratt", LogoFilename = "madebysng.png" },
                new ProductBrand { Id = 2, Name = "Jaguar" },
                new ProductBrand { Id = 3, Name = "Aftermarket" },
                new ProductBrand { Id = 4, Name = "Uprated" },
                new ProductBrand { Id = 5, Name = "Performance" },
                new ProductBrand { Id = 6, Name = "123IGN" },
                new ProductBrand { Id = 7, Name = "Accumate" },
                new ProductBrand { Id = 8, Name = "AE" },
                new ProductBrand { Id = 9, Name = "Aftermarket" },
                new ProductBrand { Id = 10, Name = "Airtex" },
                new ProductBrand { Id = 11, Name = "AP Caparo" },
                new ProductBrand { Id = 12, Name = "Automec" },
                new ProductBrand { Id = 13, Name = "Bell Exhausts" },
                new ProductBrand { Id = 14, Name = "Bilstein" },
                new ProductBrand { Id = 15, Name = "Boge", LogoFilename = "boge.png" },
                new ProductBrand { Id = 16, Name = "Bosch" },
                new ProductBrand { Id = 17, Name = "Brembo", LogoFilename = "brembo.png" },
                new ProductBrand { Id = 18, Name = "Budweg" },
                new ProductBrand { Id = 19, Name = "Burlen" },
                new ProductBrand { Id = 20, Name = "Castrol" },
                new ProductBrand { Id = 21, Name = "Coopercraft" },
                new ProductBrand { Id = 22, Name = "Coopers" },
                new ProductBrand { Id = 23, Name = "County" },
                new ProductBrand { Id = 24, Name = "Dayco" },
                new ProductBrand { Id = 25, Name = "Dayton", LogoFilename = "dayton.png" },
                new ProductBrand { Id = 26, Name = "Delphi" },
                new ProductBrand { Id = 27, Name = "Denso" },
                new ProductBrand { Id = 28, Name = "Dynalite" },
                new ProductBrand { Id = 29, Name = "EBC Brakes" },
                new ProductBrand { Id = 30, Name = "Eibach" },
                new ProductBrand { Id = 31, Name = "Ferodo" },
                new ProductBrand { Id = 32, Name = "Flame Thrower" },
                new ProductBrand { Id = 33, Name = "Fram" },
                new ProductBrand { Id = 34, Name = "Gaz", LogoFilename = "gazshocks.png" },
                new ProductBrand { Id = 35, Name = "Girling" },
                new ProductBrand { Id = 36, Name = "Glyco" },
                new ProductBrand { Id = 37, Name = "Goodridge" },
                new ProductBrand { Id = 38, Name = "Grant" },
                new ProductBrand { Id = 39, Name = "Greenstuff" },
                new ProductBrand { Id = 40, Name = "Hardi" },
                new ProductBrand { Id = 41, Name = "Haynes" },
                new ProductBrand { Id = 42, Name = "Hella" },
                new ProductBrand { Id = 43, Name = "Hylomar" },
                new ProductBrand { Id = 44, Name = "Ignitor" },
                new ProductBrand { Id = 45, Name = "ITG" },
                new ProductBrand { Id = 46, Name = "Jaguar Collection" },
                new ProductBrand { Id = 47, Name = "Jurid" },
                new ProductBrand { Id = 48, Name = "Kenlowe" },
                new ProductBrand { Id = 49, Name = "King Bearings " },
                new ProductBrand { Id = 50, Name = "Koni", LogoFilename = "koni.png" },
                new ProductBrand { Id = 51, Name = "Koyo" },
                new ProductBrand { Id = 52, Name = "Lemforder", LogoFilename = "lemforder.png" },
                new ProductBrand { Id = 53, Name = "Lockheed" },
                new ProductBrand { Id = 54, Name = "Lucas" },
                new ProductBrand { Id = 55, Name = "Lumention" },
                new ProductBrand { Id = 56, Name = "Mahle" },
                new ProductBrand { Id = 57, Name = "Mangoletsi" },
                new ProductBrand { Id = 58, Name = "Mann Filters" },
                new ProductBrand { Id = 59, Name = "Minilite" },
                new ProductBrand { Id = 60, Name = "Mintex" },
                new ProductBrand { Id = 61, Name = "Morris Lubricants", LogoFilename = "morris.png" },
                new ProductBrand { Id = 62, Name = "MWS" },
                new ProductBrand { Id = 63, Name = "Nardi" },
                new ProductBrand { Id = 64, Name = "NGK" },
                new ProductBrand { Id = 65, Name = "NTN" },
                new ProductBrand { Id = 66, Name = "Nural" },
                new ProductBrand { Id = 67, Name = "Paymen" },
                new ProductBrand { Id = 68, Name = "Performance" },
                new ProductBrand { Id = 69, Name = "Petronix" },
                new ProductBrand { Id = 70, Name = "Bell", LogoFilename = "bell.png" },
                new ProductBrand { Id = 71, Name = "Polybush", LogoFilename = "polybush.png" },
                new ProductBrand { Id = 72, Name = "Powerlite" },
                new ProductBrand { Id = 73, Name = "Powertune" },
                new ProductBrand { Id = 74, Name = "Quicksilver Exhausts" },
                new ProductBrand { Id = 75, Name = "Rolon" },
                new ProductBrand { Id = 76, Name = "Rossini" },
                new ProductBrand { Id = 77, Name = "S.U." },
                new ProductBrand { Id = 78, Name = "Samco" },
                new ProductBrand { Id = 79, Name = "Securon" },
                new ProductBrand { Id = 80, Name = "Smartscreen" },
                new ProductBrand { Id = 81, Name = "Sovy" },
                new ProductBrand { Id = 82, Name = "Suplex" },
                new ProductBrand { Id = 83, Name = "Thor Hammer" },
                new ProductBrand { Id = 84, Name = "Timken", LogoFilename = "timken.png" },
                new ProductBrand { Id = 85, Name = "Tourist Trophy" },
                new ProductBrand { Id = 86, Name = "Tric" },
                new ProductBrand { Id = 87, Name = "TRW" },
                new ProductBrand { Id = 88, Name = "Valeo" },
                new ProductBrand { Id = 89, Name = "Weber" },
                new ProductBrand { Id = 90, Name = "Wipac" },
                new ProductBrand { Id = 91, Name = "Champion", LogoFilename = "champion.png" },
                new ProductBrand { Id = 92, Name = "Mota-Lita" }

            );

            #endregion

            #region "Product Component Statuses"

            // add product component statuses
            context.ProductComponentStatuses.AddOrUpdate(t => t.Code,
                new ProductComponentStatus { Code = "Y", Name = "Yes" },
                new ProductComponentStatus { Code = "N", Name = "No" },
                new ProductComponentStatus { Code = "B", Name = "Both" }
            );

            #endregion

            #region "Product Statuses"

            // add product statuses
            context.ProductStatuses.AddOrUpdate(t => t.Code,
                new ProductStatus { Code = "Y", Name = "Available" },
                new ProductStatus { Code = "N", Name = "Not Available" },
                new ProductStatus { Code = "D", Name = "Depot Denied" },
                new ProductStatus { Code = "F", Name = "Frozen" },
                new ProductStatus { Code = "S", Name = "Superceded" },
                new ProductStatus { Code = "Z", Name = "Turned Down" }
            );

            #endregion

            #region "Product Types"

            // add product types
            context.ProductTypes.AddOrUpdate(t => t.Code,
                new ProductType { Code = "AC", Name = "Accessory" },
                new ProductType { Code = "AE", Name = "Aerosol" },
                new ProductType { Code = "AF", Name = "Aftermarket" },
                new ProductType { Code = "GJ", Name = "Genuine Jaguar" },
                new ProductType { Code = "GL", Name = "Glass" },
                new ProductType { Code = "LI", Name = "Literature" },
                new ProductType { Code = "ME", Name = "Merchandise" },
                new ProductType { Code = "NG", Name = "Non-Genuine" },
                new ProductType { Code = "OE", Name = "Original Equipment" },
                new ProductType { Code = "PR", Name = "Performance" },
                new ProductType { Code = "RE", Name = "Reconditioned" },
                new ProductType { Code = "RP", Name = "Reproduction" },
                new ProductType { Code = "SU", Name = "Superceded" },
                new ProductType { Code = "UP", Name = "Uprated" },
                new ProductType { Code = "U1", Name = "Used - Grade A" },
                new ProductType { Code = "U2", Name = "Used - Grade B" },
                new ProductType { Code = "U3", Name = "Used - Grade C" },
                new ProductType { Code = "WH", Name = "White Box" },
                new ProductType { Code = "ZZ", Name = "Turned Down" }
            );

            #endregion

            #region "Shipping Coverage Levels"

            // add shipping coverage levels
            context.ShippingCoverageLevels.AddOrUpdate(l => l.Code,
                new ShippingCoverageLevel { Code = "A", Name = "All" },
                new ShippingCoverageLevel { Code = "D", Name = "Domestic" },
                new ShippingCoverageLevel { Code = "E", Name = "Europe" },
                new ShippingCoverageLevel { Code = "W", Name = "Worldwide" }
            );

            #endregion

            #region "Shipping Providers"

            // shipping providers
            context.ShippingProviders.AddOrUpdate(p => p.Id,
                new ShippingProvider { Id = 1, Name = "Legacy", IsActive = false },
                new ShippingProvider { Id = 2, Name = "Collect", IsActive = true },
                new ShippingProvider { Id = 3, Name = "UPS", IsActive = true },
                new ShippingProvider { Id = 4, Name = "Royal Mail", IsActive = true },
                new ShippingProvider { Id = 5, Name = "Chronoship", IsActive = true },
                new ShippingProvider { Id = 6, Name = "USPS", IsActive = true },
                new ShippingProvider { Id = 7, Name = "DHL", IsActive = true }
            );

            #endregion

            #region "Tax Categories"

            // add tax categories
            context.TaxRateCategories.AddOrUpdate(c => c.Id,
                new TaxRateCategory { Id = 1, Name = "Tax Rate 1" },
                new TaxRateCategory { Id = 2, Name = "Tax Rate 2" }
            );

            #endregion            

            #region "Web Order Event Types"

            // add web order event types
            context.WebOrderEventTypes.AddOrUpdate(t => t.Id,
                new WebOrderEventType { Id = "WS", Name = "Web Order Submitted" },
                new WebOrderEventType { Id = "WA", Name = "Web Order Accepted" },
                new WebOrderEventType { Id = "WD", Name = "Web Order Declined" },
                new WebOrderEventType { Id = "WX", Name = "Web Order Deleted" },
                new WebOrderEventType { Id = "QC", Name = "Sales Quote Created" },
                new WebOrderEventType { Id = "QU", Name = "Sales Quote Updated" },
                new WebOrderEventType { Id = "QA", Name = "Sales Quote Accepted" },
                new WebOrderEventType { Id = "QD", Name = "Sales Quote Declined" },
                new WebOrderEventType { Id = "QE", Name = "Sales Quote Expired" },
                new WebOrderEventType { Id = "QX", Name = "Sales Quote Deleted" },
                new WebOrderEventType { Id = "OC", Name = "Sales Order Created" },
                new WebOrderEventType { Id = "OU", Name = "Sales Order Updated" },
                new WebOrderEventType { Id = "OD", Name = "Sales Order Cancelled" },
                new WebOrderEventType { Id = "OX", Name = "Sales Order Deleted" },
                new WebOrderEventType { Id = "PC", Name = "Pick Ticket Created" },
                new WebOrderEventType { Id = "KC", Name = "Pack Ticket Created" },
                new WebOrderEventType { Id = "ZZ", Name = "Failed Payment" },
                new WebOrderEventType { Id = "IC", Name = "Sales Invoice Created" },
                new WebOrderEventType { Id = "CC", Name = "Credit Note Created" }
            );

            #endregion

            #region "Web Order Item Statuses"

            // add web order item statuses
            context.WebOrderItemStatuses.AddOrUpdate(s => s.Id,
                new WebOrderItemStatus { Id = "S", Name = "Request submitted" },
                new WebOrderItemStatus { Id = "R", Name = "Rejected by company operator" }, // quote or order
                new WebOrderItemStatus { Id = "Q", Name = "Internal quote line created" },
                new WebOrderItemStatus { Id = "A", Name = "Internal quote approved by web customer" },
                new WebOrderItemStatus { Id = "D", Name = "Internal quote declined by web customer" },
                new WebOrderItemStatus { Id = "E", Name = "Internal quote expired" },
                new WebOrderItemStatus { Id = "O", Name = "Internal order line created" },
                //new WebOrderItemStatus { Id = "P", Name = "Internal order being picked" },
                new WebOrderItemStatus { Id = "I", Name = "Internal order line, partially invoiced" },
                new WebOrderItemStatus { Id = "F", Name = "Internal order line, fully invoiced" },
                new WebOrderItemStatus { Id = "X", Name = "Internal order line cancelled" },
                new WebOrderItemStatus { Id = "C", Name = "Credit raised (full or partial)" },
                new WebOrderItemStatus { Id = "?", Name = "Unknown" }
            );

            #endregion

            #region "Web Order Statuses"

            // add web order statuses
            context.WebOrderStatuses.AddOrUpdate(s => s.Id,
                new WebOrderStatus { Id = "S", Name = "Request submitted", IsOnGoing = true },
                new WebOrderStatus { Id = "R", Name = "Rejected by branch operative", IsOnGoing = false }, // quote or order
                new WebOrderStatus { Id = "Q", Name = "Internal quote created", IsOnGoing = true },
                new WebOrderStatus { Id = "K", Name = "Pending customer approval", IsOnGoing = true },
                new WebOrderStatus { Id = "D", Name = "Quote declined by customer", IsOnGoing = false },
                new WebOrderStatus { Id = "E", Name = "Quote expired", IsOnGoing = false },
                new WebOrderStatus { Id = "A", Name = "Quote approved by customer", IsOnGoing = true },
                new WebOrderStatus { Id = "O", Name = "Internal order created", IsOnGoing = true },
                new WebOrderStatus { Id = "I", Name = "In progress", IsOnGoing = true }, // pick tickets or invoiced lines
                new WebOrderStatus { Id = "X", Name = "Order cancelled by customer", IsOnGoing = false },
                new WebOrderStatus { Id = "Z", Name = "Order failed payment", IsOnGoing = true },
                new WebOrderStatus { Id = "C", Name = "Order closed by branch operative", IsOnGoing = false }, // before being completed
                new WebOrderStatus { Id = "F", Name = "Order fulfilled", IsOnGoing = false }
            );

            #endregion

            #region "Voucher Types"

            // add advert types
            context.VoucherTypes.AddOrUpdate(a => a.Id,
                new VoucherType { Id = "F", Name = "Fixed Amount" },
                new VoucherType { Id = "P", Name = "Percentage" }
            );

            #endregion

            // save progress
            SaveChanges(context);



            #region "Adverts"            

            // add adverts
            context.Adverts.AddOrUpdate(a => a.Id,
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
                    UpdatedByUsername = "Seed"
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
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("d7eab64e-85f8-4a70-b4cb-8f034d2b8d3f"),
                    AdvertType_Id = "I",
                    Title = "Advert Three",
                    Description = "Advert three has a link to a product offer",
                    LinkUrl = "~/uk/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("b36aaaa2-94bc-48fc-a36c-1cf377a9a35b"),
                    AdvertType_Id = "I",
                    Title = "Advert Four",
                    Description = "Advert four has a link to a product",
                    LinkUrl = "~/uk/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("e6fa21a6-8524-4ce7-9be4-744bfe73c9d7"),
                    AdvertType_Id = "I",
                    Title = "Advert Five",
                    Description = "Advert five has a no link",
                    ImageFilename_Desktop = "bgnine.jpg",
                    ImageFilename_Device = "bgnine_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("63546323-be85-4790-9dea-bee6c1a328fb"),
                    AdvertType_Id = "I",
                    Title = "Advert Six",
                    Description = "Advert six has a no link",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("d9b42952-9353-4bec-9809-b4a13fc15e02"),
                    AdvertType_Id = "V",
                    Title = "Video One",
                    Description = "Some descriptive information",
                    ImageFilename_Desktop = "bgtwelve.jpg",
                    ImageFilename_Device = "bgtwelve_DEV.jpg",
                    PlayerId = "eTypeS142",
                    VideoId = "-WAfJOToDNI",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("57af9bb2-7dd4-4e82-9d84-ca26445ff480"),
                    AdvertType_Id = "V",
                    Title = "Video Two",
                    Description = "Description Video 2",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    PlayerId = "eTypeThrottle",
                    VideoId = "QLDhTPkC1Hg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("e39b1d62-58b3-4440-aab7-bd70ddedad65"),
                    AdvertType_Id = "I",
                    Title = "E-Type Hillclimb Day USA",
                    Description = "Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("71ca0d04-68c1-4b2a-9499-3ce5c16a80af"),
                    AdvertType_Id = "I",
                    Title = "Advert Two USA",
                    Description = "Advert two has a link to an event USA",
                    LinkUrl = "~/us/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("0858840b-8e93-42bf-95e2-32ab9380a2b3"),
                    AdvertType_Id = "I",
                    Title = "Advert Three USA",
                    Description = "Advert three has a link to a product offer USA",
                    LinkUrl = "~/us/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("7ee3e418-61b8-44c6-8807-dc9d04acbc3a"),
                    AdvertType_Id = "I",
                    Title = "Advert Four USA",
                    Description = "Advert four has a link to a product USA",
                    LinkUrl = "~/us/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
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
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("241bf8ea-8c9d-49d1-94ed-5f234e75189a"),
                    AdvertType_Id = "I",
                    Title = "Advert Six USA",
                    Description = "Advert six has a no link USA",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("1b54fe54-3def-4623-9ebb-5367a667249d"),
                    AdvertType_Id = "V",
                    Title = "Video One USA",
                    Description = "Some descriptive information USA",
                    ImageFilename_Desktop = "bgtwelve.jpg",
                    ImageFilename_Device = "bgtwelve_DEV.jpg",
                    PlayerId = "eTypeS142",
                    VideoId = "-WAfJOToDNI",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("c54e1964-80b8-46c3-830e-13b16e3a3854"),
                    AdvertType_Id = "V",
                    Title = "Video Two USA",
                    Description = "Description Video 2 USA",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    PlayerId = "eTypeThrottle",
                    VideoId = "QLDhTPkC1Hg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("a68a2e77-02f7-4cbb-a319-53d20a9f3c82"),
                    AdvertType_Id = "I",
                    Title = "NL E-Type Hillclimb Day",
                    Description = "NL Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("fc7f9214-a704-41b9-8ae9-d0bb3ea9fd21"),
                    AdvertType_Id = "I",
                    Title = "NL Advert Two",
                    Description = "NL Advert two has a link to an event",
                    LinkUrl = "~/nl/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("9408b5a5-616c-42c8-be4e-82713070e019"),
                    AdvertType_Id = "I",
                    Title = "NL Advert Three",
                    Description = "NL Advert three has a link to a product offer",
                    LinkUrl = "~/nl/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("ce7c3284-45ad-4d4b-ad52-774d68371290"),
                    AdvertType_Id = "I",
                    Title = "NL Advert Four",
                    Description = "NL Advert four has a link to a product",
                    LinkUrl = "~/nl/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("1af25a80-3755-45b1-aac4-8ed867b461d1"),
                    AdvertType_Id = "I",
                    Title = "NL Advert Five",
                    Description = "NL Advert five has a no link",
                    ImageFilename_Desktop = "bgnine.jpg",
                    ImageFilename_Device = "bgnine_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("eb0f7352-2163-4c6c-85fd-3977e685f48e"),
                    AdvertType_Id = "I",
                    Title = "NL Advert Six",
                    Description = "NL Advert six has a no link",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("fe33b953-2379-4fe2-ab54-594c5596aada"),
                    AdvertType_Id = "V",
                    Title = "NL Video One",
                    Description = "NL Some descriptive information",
                    ImageFilename_Desktop = "bgtwelve.jpg",
                    ImageFilename_Device = "bgtwelve_DEV.jpg",
                    PlayerId = "eTypeS142",
                    VideoId = "-WAfJOToDNI",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("34933365-3d26-4aa6-bf79-dae073428799"),
                    AdvertType_Id = "V",
                    Title = "NL Video Two",
                    Description = "NL Description Video 2",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    PlayerId = "eTypeThrottle",
                    VideoId = "QLDhTPkC1Hg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("5a8ec730-51b1-42f8-a100-ce8650260dea"),
                    AdvertType_Id = "I",
                    Title = "DE E-Type Hillclimb Day",
                    Description = "DE Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("dc3f4452-adf2-42fd-b3b1-42eb0a8dbf2b"),
                    AdvertType_Id = "I",
                    Title = "DE Advert Two",
                    Description = "DE Advert two has a link to an event",
                    LinkUrl = "~/de/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("6b5c5fa6-99d5-4d63-9ca1-2fa587574db8"),
                    AdvertType_Id = "I",
                    Title = "DE Advert Three",
                    Description = "DE Advert three has a link to a product offer",
                    LinkUrl = "~/de/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("19812cf1-19d1-435d-aa55-f3ab8994cca9"),
                    AdvertType_Id = "I",
                    Title = "DE Advert Four",
                    Description = "DE Advert four has a link to a product",
                    LinkUrl = "~/de/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("a0a5a3da-24c9-4515-bfff-274813980250"),
                    AdvertType_Id = "I",
                    Title = "DE Advert Five",
                    Description = "DE Advert five has a no link",
                    ImageFilename_Desktop = "bgnine.jpg",
                    ImageFilename_Device = "bgnine_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("dc141067-0647-4a5d-84d6-ba730e471c89"),
                    AdvertType_Id = "I",
                    Title = "DE Advert Six",
                    Description = "DE Advert six has a no link",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("5ceb7008-a4af-4a10-85de-6423ce6c87f2"),
                    AdvertType_Id = "V",
                    Title = "DE Video One",
                    Description = "DE Some descriptive information",
                    ImageFilename_Desktop = "bgtwelve.jpg",
                    ImageFilename_Device = "bgtwelve_DEV.jpg",
                    PlayerId = "eTypeS142",
                    VideoId = "-WAfJOToDNI",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("4b9bebdb-c9a2-4ee0-a911-fdf740ed2103"),
                    AdvertType_Id = "V",
                    Title = "DE Video Two",
                    Description = "DE Description Video 2",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    PlayerId = "eTypeThrottle",
                    VideoId = "QLDhTPkC1Hg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("1dde9495-ec23-43db-bce4-a1f2ee32632b"),
                    AdvertType_Id = "I",
                    Title = "FR E-Type Hillclimb Day",
                    Description = "FR Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("2b203b8b-6c94-4d0c-8bb0-f7fe90c6f970"),
                    AdvertType_Id = "I",
                    Title = "FR Advert Two",
                    Description = "FR Advert two has a link to an event",
                    LinkUrl = "~/fr/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("e2cc126c-6c38-4223-b8ca-624c8bd9a6e7"),
                    AdvertType_Id = "I",
                    Title = "FR Advert Three",
                    Description = "FR Advert three has a link to a product offer",
                    LinkUrl = "~/fr/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("9646ce05-7f28-45f7-ae5b-0d2bb2a9550f"),
                    AdvertType_Id = "I",
                    Title = "FR Advert Four",
                    Description = "FR Advert four has a link to a product",
                    LinkUrl = "~/fr/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("030bbf00-a314-4d8a-9d34-58a5ae08a675"),
                    AdvertType_Id = "I",
                    Title = "FR Advert Five",
                    Description = "FR Advert five has a no link",
                    ImageFilename_Desktop = "bgnine.jpg",
                    ImageFilename_Device = "bgnine_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("cc8bf80b-77b6-4a32-b2df-a9dff2bc2301"),
                    AdvertType_Id = "I",
                    Title = "FR Advert Six",
                    Description = "FR Advert six has a no link",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("52a9e4c6-eb47-4d4c-aa97-9bfca2e5102b"),
                    AdvertType_Id = "V",
                    Title = "FR Video One",
                    Description = "FR Some descriptive information",
                    ImageFilename_Desktop = "bgtwelve.jpg",
                    ImageFilename_Device = "bgtwelve_DEV.jpg",
                    PlayerId = "eTypeS142",
                    VideoId = "-WAfJOToDNI",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("f8a92fd0-0d66-457d-be83-375cc5ebf2ed"),
                    AdvertType_Id = "V",
                    Title = "FR Video Two",
                    Description = "FR Description Video 2",
                    ImageFilename_Desktop = "bgsix.jpg",
                    ImageFilename_Device = "bgsix_DEV.jpg",
                    PlayerId = "eTypeThrottle",
                    VideoId = "QLDhTPkC1Hg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }

            );

            SaveChanges(context);

            #endregion

            #region "Events"

            // add events
            context.Events.AddOrUpdate(e => e.Id,
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
                    UpdatedByUsername = "Seed"
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
                    UpdatedByUsername = "Seed"
                },
                new ShowEvent
                {
                    Id = Guid.Parse("881fe69e-c49d-4442-b80f-26e260ad49db"),
                    Title = "Event Two UK",
                    Description = "Quo nisi malis irure deserunt ea ex anim iudicem, nisi laborum si cupidatat, eiusmod dolore aliquip eiusmod. E summis arbitror sempiternum, laborum culpa lorem laboris esse. Multos quo eiusmod, aliquip cillum cillum ad nulla. Summis quibusdam ingeniis, labore ingeniis firmissimum qui quid probant e velit velit ut an quorum cohaerescant, do excepteur firmissimum si irure iis ne veniam excepteur, iis fugiat esse irure admodum, te litteris domesticarum. Qui ab veniam sunt elit.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("881fe69e-c49d-4442-b80f-26e260ad49db"), StartsUtc = DateTime.Parse("2015-08-23 08:00"), EndsUtc = DateTime.Parse("2015-08-23 20:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("881fe69e-c49d-4442-b80f-26e260ad49db"), StartsUtc = DateTime.Parse("2015-08-24 08:00"), EndsUtc = DateTime.Parse("2015-08-24 20:00") }
                    },
                    Location = "Manchester, England",
                    ImageFilename = "ukevent.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new ShowEvent
                {
                    Id = Guid.Parse("b2c82716-2106-4f38-b546-713be3671e23"),
                    Title = "Event UK",
                    Description = "Excepteur multos tempor ab aliqua id se quis mandaremus, dolore a quamquam est varias, eu nostrud efflorescere, an se exercitation id nam admodum domesticarum a ipsum occaecat o doctrina, velit cohaerescant incurreret lorem nescius. De summis pariatur. Legam et cupidatat, possumus sunt incurreret, ita legam eiusmod incurreret, quem admodum offendit in nescius fore tamen hic nisi, laboris summis dolore fabulas quae qui e eram aut sint, cernantur fore incididunt. Tempor lorem ipsum o velit.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("b2c82716-2106-4f38-b546-713be3671e23"), StartsUtc = DateTime.Parse("2015-08-28 09:00"), EndsUtc = DateTime.Parse("2015-08-28 16:00") }
                    },
                    EventUrl = "http://www.sngbarratt.com",
                    Location = "Bridgnorth, England",
                    ImageFilename = "ukevent.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new ShowEvent
                {
                    Id = Guid.Parse("84512fd9-2f59-41dd-a704-4213a6c270fd"),
                    Title = "Event US",
                    Description = "Eiusmod nulla dolore mentitum sunt. Voluptate malis fugiat proident fore, mandaremus distinguantur ad vidisse, si aliqua ut quis, ut do illum esse illum ita fugiat senserit offendit quo non sint velit enim probant, appellat minim cernantur eiusmod quo quibusdam anim sed offendit voluptatibus. Quo senserit graviterque a ad ut instituendarum do o duis nescius ut qui dolor nam cillum, nam labore firmissimum, aute aut voluptate nam dolore, qui quae offendit mentitum ex e fugiat officia arbitror.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("84512fd9-2f59-41dd-a704-4213a6c270fd"), StartsUtc = DateTime.Parse("2015-09-11 12:00"), EndsUtc = DateTime.Parse("2015-09-11 23:00") }
                    },
                    Location = "Chicago, IL",
                    ImageFilename = "usaevent.jpg",
                    IsAttending = false,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new ShowEvent
                {
                    Id = Guid.Parse("51f73faa-c85d-400b-a4a9-4a1946d62f08"),
                    Title = "Event FR",
                    Description = "Si velit aliqua tamen commodo, laboris non fugiat. Consequat ipsum commodo si id offendit o admodum ea non fugiat esse quid ingeniis et voluptate ab fore senserit. A esse occaecat quibusdam, te aliqua ita multos, doctrina tractavissent an appellat, de veniam de varias. Possumus ut ipsum litteris qui pariatur esse incurreret proident sed iudicem cillum doctrina non nulla singulis in coniunctione ut doctrina nulla eram iis quis.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("51f73faa-c85d-400b-a4a9-4a1946d62f08"), StartsUtc = DateTime.Parse("2015-08-14 07:00"), EndsUtc = DateTime.Parse("2015-08-14 17:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("51f73faa-c85d-400b-a4a9-4a1946d62f08"), StartsUtc = DateTime.Parse("2015-08-15 07:00"), EndsUtc = DateTime.Parse("2015-08-15 17:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("51f73faa-c85d-400b-a4a9-4a1946d62f08"), StartsUtc = DateTime.Parse("2015-08-16 07:00"), EndsUtc = DateTime.Parse("2015-08-16 17:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("51f73faa-c85d-400b-a4a9-4a1946d62f08"), StartsUtc = DateTime.Parse("2015-08-17 07:00"), EndsUtc = DateTime.Parse("2015-08-17 17:00") }
                    },
                    Location = "Toulon, France",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = false,
                    IsActive = true,
                    Branch_Id = 5,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new ShowEvent
                {
                    Id = Guid.Parse("cc139381-0c56-45e6-9e6b-391350a94013"),
                    Title = "Event Two NL",
                    Description = "Come and marvel at the finest and most exclusive vehicles in the Benelux, as well as everything else related to car parts and automobilia.  SNG Barratt Holland will be at the show, and taking Pre-Orders for collection.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("cc139381-0c56-45e6-9e6b-391350a94013"), StartsUtc = DateTime.Parse("2015-09-04 07:00"), EndsUtc = DateTime.Parse("2015-09-04 17:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("cc139381-0c56-45e6-9e6b-391350a94013"), StartsUtc = DateTime.Parse("2015-09-05 07:00"), EndsUtc = DateTime.Parse("2015-09-05 17:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("cc139381-0c56-45e6-9e6b-391350a94013"), StartsUtc = DateTime.Parse("2015-09-06 07:00"), EndsUtc = DateTime.Parse("2015-09-06 17:00") }
                    },
                    Location = "Tilburg, Netherlands",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }

            );


            SaveChanges(context);

            #endregion

            #region "Shipping Methods"

            // add shipping methods
            context.ShippingMethods.AddOrUpdate(m => m.Id,
                new ShippingMethod
                {
                    Id = 1,
                    Branch_Id = 1,
                    ShippingProvider_Id = 2,
                    ShippingCoverageLevel_Id = "A",
                    Title = "Click & Collect",
                    MaxWeightKgs = 0,
                    MaxDimensionCms = 0,
                    MaxVolumeCm3 = 0,
                    CostPrice = 0,
                    Price = 0,
                    SortOrder = 1,
                    IsActive = true
                },
                new ShippingMethod
                {
                    Id = 2,
                    Branch_Id = 1,
                    ShippingProvider_Id = 3,
                    ShippingCoverageLevel_Id = "W",
                    ProviderReference = "11",
                    Title = "UPS Standard",
                    MaxWeightKgs = 0,
                    MaxDimensionCms = 0,
                    MaxVolumeCm3 = 0,
                    CostPrice = 0,
                    Price = 0,
                    SortOrder = 1,
                    IsActive = true
                },
                new ShippingMethod
                {
                    Id = 3,
                    Branch_Id = 1,
                    ShippingProvider_Id = 4,
                    ShippingCoverageLevel_Id = "D",
                    ProviderReference = "FIRST",
                    Title = "First Class Post (Uninsured)",
                    MaxWeightKgs = 1,
                    MaxDimensionCms = 45,
                    MaxVolumeCm3 = 25200,
                    CostPrice = 2.95m,
                    Price = 4.50m,
                    SortOrder = 1,
                    IsActive = true
                }
                ,
                new ShippingMethod
                {
                    Id = 4,
                    Branch_Id = 1,
                    ShippingProvider_Id = 4,
                    ShippingCoverageLevel_Id = "D",
                    ProviderReference = "FIRST",
                    Title = "First Class Post (Uninsured)",
                    MaxWeightKgs = 2,
                    MaxDimensionCms = 45,
                    MaxVolumeCm3 = 25200,
                    CostPrice = 5.20m,
                    Price = 7.50m,
                    SortOrder = 2,
                    IsActive = true
                }
                ,
                new ShippingMethod
                {
                    Id = 5,
                    Branch_Id = 1,
                    ShippingProvider_Id = 4,
                    ShippingCoverageLevel_Id = "E",
                    ProviderReference = "AIRMAIL",
                    Title = "Airmail Europe, 3-5 Days",
                    MaxWeightKgs = 2,
                    MaxDimensionCms = 45,
                    MaxVolumeCm3 = 25200,
                    CostPrice = 14.07m,
                    Price = 18m,
                    SortOrder = 5,
                    IsActive = true
                },
                new ShippingMethod
                {
                    Id = 6,
                    Branch_Id = 1,
                    ShippingProvider_Id = 3,
                    ShippingCoverageLevel_Id = "A",
                    ProviderReference = "7",
                    Title = "UPS Express",
                    MaxWeightKgs = 0,
                    MaxDimensionCms = 0,
                    MaxVolumeCm3 = 0,
                    CostPrice = 0,
                    Price = 0,
                    SortOrder = 2,
                    IsActive = true
                },
                new ShippingMethod
                {
                    Id = 7,
                    Branch_Id = 1,
                    ShippingProvider_Id = 3,
                    ShippingCoverageLevel_Id = "A",
                    ProviderReference = "65",
                    Title = "UPS Saver",
                    MaxWeightKgs = 0,
                    MaxDimensionCms = 0,
                    MaxVolumeCm3 = 0,
                    CostPrice = 0,
                    Price = 0,
                    SortOrder = 3,
                    IsActive = true
                }
            );

            SaveChanges(context);

            #endregion

            #region "Tweets"

            // add tweets
            context.Tweets.AddOrUpdate(t => t.Id,
                new Tweet { Id = 1, Text = "Tweet 1", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-1), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 2, Text = "Tweet 2", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-2), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 3, Text = "Tweet 3", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-3), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 4, Text = "Tweet 4", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-4), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 5, Text = "Tweet 5", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-5), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 6, Text = "Tweet 6", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-6), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 7, Text = "Tweet 7", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-7), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 8, Text = "Tweet 8", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-8), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 9, Text = "Tweet 9", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-9), UpdatedTimestampUtc = DateTime.UtcNow },
                new Tweet { Id = 10, Text = "Tweet 10", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-10), UpdatedTimestampUtc = DateTime.UtcNow }
            );

            SaveChanges(context);

            #endregion

            #region "Products"

            // add products
            context.Products.AddOrUpdate(p => p.Id,
                new Product
                {
                    Id = 1,
                    PartNumber = "JIM1001",
                    BasePartNumber = "JIM1001",
                    ProductType_Code = "AF",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 1",
                            ShortDescription = "All about product item number one which is unbranded",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#0",
                    QuantityBreakDiscountLevels = new List<ProductQuantityBreakDiscountLevel> {
                        new ProductQuantityBreakDiscountLevel
                        {
                            Quantity = 5,
                            MinMargin = 20.0m,
                            Retail = 98.0m,
                            Level2 = 95.0m,
                            Level3 = 92.0m,
                            Level4 = 90.0m,
                            Level5 = 85.0m,
                            Level6 = 82.5m,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        },
                        new ProductQuantityBreakDiscountLevel
                        {
                            Quantity = 10,
                            MinMargin = 18.0m,
                            Retail = 95.0m,
                            Level2 = 92.0m,
                            Level3 = 90.0m,
                            Level4 = 87.5m,
                            Level5 = 82.5m,
                            Level6 = 80.0m,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        },
                        new ProductQuantityBreakDiscountLevel
                        {
                            Quantity = 100,
                            MinMargin = 15.0m,
                            Retail = 92.0m,
                            Level2 = 90.0m,
                            Level3 = 87.5m,
                            Level4 = 82.0m,
                            Level5 = 80.0m,
                            Level6 = 75.0m,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }

                    },
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.675m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 15,
                    PackedWeightKgs = 0.8m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "Y",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    PartNumber = "JIM1002",
                    BasePartNumber = "JIM1002",
                    ProductType_Code = "OE",
                    ProductBrand = context.ProductBrands.FirstOrDefault(b => b.Id == 9), // koni
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 2",
                            ShortDescription = "All about product item number two which is branded Koni",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#1",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "Y",
                    TaxRateCategory_Id = 2,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    PartNumber = "JIM1003",
                    BasePartNumber = "JIM1001",
                    ProductType_Code = "UP",
                    ProductBrand = context.ProductBrands.FirstOrDefault(b => b.Id == 4), // boge
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 3",
                            ShortDescription = "Part Number 3 is better than part number 1 and 2. It doesn't get much better than this. Product item number three is branded Boge",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "B",
                    PartStatus_Code = "Y",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = true,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                 new Product
                 {
                     Id = 4,
                     PartNumber = "JIM1004",
                     BasePartNumber = "JIM1004",
                     ProductType_Code = "GJ",
                     ProductBrand = context.ProductBrands.FirstOrDefault(b => b.Id == 1), // jaguar
                     TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 4",
                            ShortDescription = "Part Number 4 is even better than part number 3. It REALLY doesn't get much better than this. Gen Jag",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                     DiscountLevel_Code = "#2",
                     ItemWidthCms = 10,
                     ItemHeightCms = 11,
                     ItemDepthCms = 12,
                     ItemWeightKgs = 0.5m,
                     PackedWidthCms = 12,
                     PackedHeightCms = 14,
                     PackedDepthCms = 16,
                     PackedWeightKgs = 1.2m,
                     ComponentStatus_Code = "N",
                     PartStatus_Code = "Y",
                     TaxRateCategory_Id = 1,
                     IsShipable = true,
                     IsShipableByAir = true,
                     IsPackable = true,
                     IsPackableLoose = true,
                     IsRotatable = true,
                     IsWebsiteApproved = true,
                     IsQualityAssured = false,
                     IsActive = false,
                     RowVersion = 1,
                     CreatedTimestampUtc = DateTime.UtcNow,
                     UpdatedTimestampUtc = DateTime.UtcNow
                 },
                new Product
                {
                    Id = 5,
                    PartNumber = "JIM1005",
                    BasePartNumber = "JIM1005",
                    ProductType_Code = "RE",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 5",
                            ShortDescription = "Part Number 5 is reconditioned and has a surcharge",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "Y",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new Product
                {
                    Id = 6,
                    PartNumber = "JIM1006",
                    BasePartNumber = "JIM1005",
                    ProductType_Code = "ZZ",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 6",
                            ShortDescription = "Part Number 6 is turned down",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "Z",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new Product
                {
                    Id = 7,
                    PartNumber = "JIM1007",
                    BasePartNumber = "JIM1005",
                    ProductType_Code = "PR",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 7",
                            ShortDescription = "Part Number 7 is frozen",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "F",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new Product
                {
                    Id = 8,
                    PartNumber = "JIM1008",
                    BasePartNumber = "JIM1008",
                    ProductType_Code = "NG",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 8",
                            ShortDescription = "Part Number 8 is not available",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 10,
                    ItemHeightCms = 11,
                    ItemDepthCms = 12,
                    ItemWeightKgs = 0.5m,
                    PackedWidthCms = 12,
                    PackedHeightCms = 14,
                    PackedDepthCms = 16,
                    PackedWeightKgs = 1.2m,
                    ComponentStatus_Code = "B",
                    PartStatus_Code = "N",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                 new Product
                 {
                     Id = 9,
                     PartNumber = "JIM1009",
                     BasePartNumber = "JIM1009",
                     ProductType_Code = "SU",
                     TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 9",
                            ShortDescription = "Part Number 9 is superceeded by Part No 10",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                     DiscountLevel_Code = "#2",
                     ItemWidthCms = 10,
                     ItemHeightCms = 11,
                     ItemDepthCms = 12,
                     ItemWeightKgs = 0.5m,
                     PackedWidthCms = 12,
                     PackedHeightCms = 14,
                     PackedDepthCms = 16,
                     PackedWeightKgs = 1.2m,
                     ComponentStatus_Code = "N",
                     PartStatus_Code = "S",
                     TaxRateCategory_Id = 1,
                     IsShipable = true,
                     IsShipableByAir = true,
                     IsPackable = true,
                     IsPackableLoose = true,
                     IsRotatable = true,
                     IsWebsiteApproved = true,
                     IsQualityAssured = false,
                     IsActive = true,
                     RowVersion = 1,
                     CreatedTimestampUtc = DateTime.UtcNow,
                     UpdatedTimestampUtc = DateTime.UtcNow
                 },
                new Product
                {
                    Id = 10,
                    PartNumber = "JIM1010",
                    BasePartNumber = "JIM1010",
                    ProductBrand = context.ProductBrands.FirstOrDefault(b => b.Id == 10), // lemforder
                    ProductType_Code = "OE",
                    TextInfo = new List<ProductText> {
                        new ProductText
                        {
                            Language_Id = 1,
                            ShortTitle = "Test Part Number 10",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    DiscountLevel_Code = "#2",
                    ItemWidthCms = 20,
                    ItemHeightCms = 30,
                    ItemDepthCms = 40,
                    ItemWeightKgs = 12.5m,
                    PackedWidthCms = 30,
                    PackedHeightCms = 40,
                    PackedDepthCms = 50,
                    PackedWeightKgs = 15m,
                    ComponentStatus_Code = "N",
                    PartStatus_Code = "Y",
                    TaxRateCategory_Id = 1,
                    IsShipable = true,
                    IsShipableByAir = true,
                    IsPackable = true,
                    IsPackableLoose = true,
                    IsRotatable = true,
                    IsWebsiteApproved = true,
                    IsQualityAssured = false,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                });

            SaveChanges(context);

            // add product images
            context.ProductImages.AddOrUpdate(i => i.Id,
                new ProductImage
                {
                    Id = 1,
                    Product_Id = 10,
                    Filename = "99df9ad1-ad02-44e7-a9e1-971835815ee4.jpg",
                    IsDefault = true,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                }
            );

            SaveChanges(context);


            Product prod = null;

            prod = context.Products.FirstOrDefault(p => p.Id == 2);
            if (prod != null)
            {
                prod.LinkedProducts = new List<ProductLink> {
                    new ProductLink {
                        Product_Id = 2,
                        LinkedProduct_Id = 1,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                };



                context.Products.AddOrUpdate(prod);

                SaveChanges(context);
            }

            prod = context.Products.FirstOrDefault(p => p.Id == 3);
            if (prod != null)
            {
                prod.LinkedProducts = new List<ProductLink> {
                    new ProductLink {
                        Product_Id = 3,
                        LinkedProduct_Id = 1,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                };

                prod.AlternativeProducts = new List<ProductAlternative> {
                    new ProductAlternative {
                        Product_Id = 3,
                        AlternativeProduct_Id = 2,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                };

                context.Products.AddOrUpdate(prod);

                SaveChanges(context);
            }

            prod = context.Products.FirstOrDefault(p => p.Id == 4);
            if (prod != null)
            {

                prod.AlternativeProducts = new List<ProductAlternative> {
                    new ProductAlternative {
                        Product_Id = 4,
                        AlternativeProduct_Id = 2,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    },
                    new ProductAlternative {
                        Product_Id = 4,
                        AlternativeProduct_Id = 3,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                };

                context.Products.AddOrUpdate(prod);

                SaveChanges(context);
            }

            prod = context.Products.FirstOrDefault(p => p.Id == 9);
            if (prod != null)
            {

                prod.SupersessionList = new List<ProductSupersession> {
                    new ProductSupersession {
                        Product_Id = 9,
                        ReplacementProduct_Id = 10,
                        Quantity = 1,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                };

                context.Products.AddOrUpdate(prod);

                SaveChanges(context);
            }

            #endregion

            #region "Branch Products"

            // add branch products
            context.BranchProducts.AddOrUpdate(p => p.Id,
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
                    UpdatedTimestampUtc = DateTime.UtcNow
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
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("91bf8fc7-2c80-4e9c-8ade-6368dc45314e"),
                    Branch_Id = 1,
                    Product_Id = 3,
                    RetailPrice = 13,
                    TradePrice = 0,
                    ClearancePrice = 10,
                    AvgCostPrice = 3,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 10,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    SpecialOffers = new List<BranchProductOffer>
                    {
                        new BranchProductOffer {
                            Id = Guid.Parse("44cb9ad7-b604-48d4-995f-ba786b094c29"),
                            ProductImage_Id = 1,
                            Title = "Special Offer Title 3",
                            OfferPrice = 2.50m,
                            ExpiryDate = DateTime.UtcNow.Date.AddDays(60),
                            IsActive = true,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        }
                    },
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("897bc3d1-1982-48a8-b684-4c929583d2b6"),
                    Branch_Id = 1,
                    Product_Id = 4,
                    RetailPrice = 14,
                    TradePrice = 0,
                    AvgCostPrice = 3,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 10,
                    BranchStatus_Code = "D",
                    IsActive = true,
                    SpecialOffers = new List<BranchProductOffer>
                    {
                        new BranchProductOffer {
                            Id = Guid.Parse("55cb9ad7-b604-48d4-995f-fa786b094c29"),
                            ProductImage_Id = 1,
                            Title = "Special Offer Title 4",
                            OfferPrice = 2.50m,
                            ExpiryDate = DateTime.UtcNow.Date.AddDays(-30),
                            IsActive = false,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        }
                    },
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("a8c788c9-4043-4a5f-895f-c9348a9fbbec"),
                    Branch_Id = 1,
                    Product_Id = 5,
                    RetailPrice = 15,
                    TradePrice = 0,
                    AvgCostPrice = 3,
                    Surcharge = 10,
                    MinStockLevel = 20,
                    MaxStockLevel = 50,
                    Quantity = 10,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("ec5d6ca9-d734-4866-b9e7-d16c7dbf4d02"),
                    Branch_Id = 1,
                    Product_Id = 6,
                    RetailPrice = 0,
                    TradePrice = 0,
                    AvgCostPrice = 0,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 0,
                    BranchStatus_Code = "Z",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("7bf4229f-c478-4b48-a25b-d94f83dc2bab"),
                    Branch_Id = 1,
                    Product_Id = 7,
                    RetailPrice = 50,
                    TradePrice = 20,
                    AvgCostPrice = 10,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 6,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("3be64c1c-ba58-4747-9c94-f961a69013c9"),
                    Branch_Id = 1,
                    Product_Id = 8,
                    RetailPrice = 18,
                    TradePrice = 0,
                    ClearancePrice = 14,
                    AvgCostPrice = 8,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 8,
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
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
                    UpdatedTimestampUtc = DateTime.UtcNow
                },
                new BranchProduct
                {
                    Id = Guid.Parse("6704bee0-dda9-430e-9b41-176e7c0c8663"),
                    Branch_Id = 1,
                    Product_Id = 10,
                    RetailPrice = 100,
                    TradePrice = 0,
                    AvgCostPrice = 0,
                    Surcharge = 0,
                    MinStockLevel = 0,
                    MaxStockLevel = 0,
                    Quantity = 0,
                    //SpecialOffers = context.BranchProductOffers.Where(o => o.Branch_Id == 1 && o.Product_Id == 10).ToList(),
                    //SpecialOffers = context.BranchProductOffers.Where(o => o.BranchProduct_Id.ToString() == "6704bee0-dda9-430e-9b41-176e7c0c8663").ToList(),
                    SpecialOffers = new List<BranchProductOffer>
                    {
                        new BranchProductOffer {
                            Id = Guid.Parse("55cb9ad7-b604-48d4-995f-ba786b095c29"),
                            ProductImage_Id = 1,
                            Title = "Special Offer Title 1",
                            OfferPrice = 2.00m,
                            ExpiryDate = DateTime.UtcNow.Date.AddDays(30),
                            IsActive = true,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        },
                        new BranchProductOffer
                        {
                            Id = Guid.Parse("55cb9ad7-b604-48d4-995f-ba786b094c29"),
                            ProductImage_Id = 1,
                            Title = "Special Offer Title 2",
                            OfferPrice = 2.50m,
                            ExpiryDate = DateTime.UtcNow.Date.AddDays(-30),
                            IsActive = false,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        }
                    },
                    BranchStatus_Code = "Y",
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                }
            );

            SaveChanges(context);

            //////// add branch product offers
            //////context.BranchProductOffers.AddOrUpdate(o => o.Id,
            //////    new BranchProductOffer
            //////    {
            //////        Id = Guid.Parse("55cb9ad7-b604-48d4-995f-ba786b095c29"),
            //////        BranchProduct_Id = Guid.Parse("6704bee0-dda9-430e-9b41-176e7c0c8663"),
            //////        //Branch_Id = 1,
            //////        //Product_Id = 10,
            //////        ProductImage_Id = 1,
            //////        Title = "Special Offer Title 1",
            //////        OfferPrice = 2.00m,
            //////        ExpiryDate = DateTime.UtcNow.Date.AddDays(30),
            //////        IsActive = true,
            //////        RowVersion = 1,
            //////        CreatedTimestampUtc = DateTime.UtcNow,
            //////        UpdatedTimestampUtc = DateTime.UtcNow
            //////    },
            //////    new BranchProductOffer
            //////    {
            //////        Id = Guid.Parse("55cb9ad7-b604-48d4-995f-ba786b094c29"),
            //////        BranchProduct_Id = Guid.Parse("6704bee0-dda9-430e-9b41-176e7c0c8663"),
            //////        //Branch_Id = 1,
            //////        //Product_Id = 10,
            //////        ProductImage_Id = 1,
            //////        Title = "Special Offer Title 2",
            //////        OfferPrice = 2.50m,
            //////        ExpiryDate = DateTime.UtcNow.Date.AddDays(-30),
            //////        IsActive = false,
            //////        RowVersion = 1,
            //////        CreatedTimestampUtc = DateTime.UtcNow,
            //////        UpdatedTimestampUtc = DateTime.UtcNow
            //////    }
            //////    ,
            //////    new BranchProductOffer
            //////    {
            //////        Id = Guid.Parse("44cb9ad7-b604-48d4-995f-ba786b094c29"),
            //////        BranchProduct_Id = Guid.Parse("91bf8fc7-2c80-4e9c-8ade-6368dc45314e"),
            //////        //Branch_Id = 1,
            //////        //Product_Id = 3,
            //////        ProductImage_Id = 1,
            //////        Title = "Special Offer Title 3",
            //////        OfferPrice = 2.50m,
            //////        ExpiryDate = DateTime.UtcNow.Date.AddDays(60),
            //////        IsActive = true,
            //////        RowVersion = 1,
            //////        CreatedTimestampUtc = DateTime.UtcNow,
            //////        UpdatedTimestampUtc = DateTime.UtcNow
            //////    }
            //////    , new BranchProductOffer
            //////    {
            //////        Id = Guid.Parse("55cb9ad7-b604-48d4-995f-fa786b094c29"),
            //////        BranchProduct_Id = Guid.Parse("897bc3d1-1982-48a8-b684-4c929583d2b6"),
            //////        //Branch_Id = 1,
            //////        //Product_Id = 4,
            //////        ProductImage_Id = 1,
            //////        Title = "Special Offer Title 4",
            //////        OfferPrice = 2.50m,
            //////        ExpiryDate = DateTime.UtcNow.Date.AddDays(-30),
            //////        IsActive = false,
            //////        RowVersion = 1,
            //////        CreatedTimestampUtc = DateTime.UtcNow,
            //////        UpdatedTimestampUtc = DateTime.UtcNow
            //////    }
            //////);

            SaveChanges(context);

            #endregion

            #region "Categories"

            #region "Category Types" 

            // add category types
            context.CategoryTypes.AddOrUpdate(c => c.Id,
                new CategoryType { Id = 1, Name = "Accessories" },
                new CategoryType { Id = 2, Name = "Service Parts" },
                new CategoryType { Id = 3, Name = "Upgrades" }
            );

            #endregion

            // save progress
            SaveChanges(context);

            #region "Accessories"

            // add  Accessory categories
            context.Categories.AddOrUpdate(c => c.Id,
                new Category
                {
                    Id = 1,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 1,
                            Language_Id = 1,
                            Title = "Car Care",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 1,
                            Language_Id = 1,
                            Intro = "Everything required to keep your Growler in tip top condition",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    ImageFilename = "carcare.png",
                    SortOrder = 1000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 2,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 2,
                            Language_Id = 1,
                            Title = "Car Covers",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 3,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 3,
                            Language_Id = 1,
                            Title = "Battery Conditioner",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 4,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 4,
                            Language_Id = 1,
                            Title = "Paint Touch-Up",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 5,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 5,
                            Language_Id = 1,
                            Title = "Safety & Security",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 6,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 6,
                            Language_Id = 1,
                            Title = "Nuts & Bolts",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 7,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 7,
                            Language_Id = 1,
                            Title = "Clips & Ties",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1006,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 8,
                    Parent_Id = 1,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 8,
                            Language_Id = 1,
                            Title = "Bulbs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1007,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 9,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 9,
                            Language_Id = 1,
                            Title = "Car Cleaning",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 2000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 10,
                    Parent_Id = 9,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 10,
                            Language_Id = 1,
                            Title = "Interior",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 11,
                    Parent_Id = 9,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 11,
                            Language_Id = 1,
                            Title = "Exterior",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 12,
                    Parent_Id = 9,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 12,
                            Language_Id = 1,
                            Title = "Wheels",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 13,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 13,
                            Language_Id = 1,
                            Title = "Car Interior Styling",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 3000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 14,
                    Parent_Id = 13,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 14,
                            Language_Id = 1,
                            Title = "Car Mats",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 15,
                    Parent_Id = 13,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 15,
                            Language_Id = 1,
                            Title = "Gear Knobs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 16,
                    Parent_Id = 13,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 16,
                            Language_Id = 1,
                            Title = "Steering Wheels",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 17,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 17,
                            Language_Id = 1,
                            Title = "Car Exterior Styling",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 4000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 18,
                    Parent_Id = 17,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 18,
                            Language_Id = 1,
                            Title = "Badges & Motifs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 19,
                    Parent_Id = 17,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 19,
                            Language_Id = 1,
                            Title = "Chrome",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 20,
                    Parent_Id = 17,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 20,
                            Language_Id = 1,
                            Title = "Wheels",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 21,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 21,
                            Language_Id = 1,
                            Title = "Lubricants & Fluids",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 5000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 22,
                    Parent_Id = 21,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 22,
                            Language_Id = 1,
                            Title = "Anti-Freeze",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 23,
                    Parent_Id = 21,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 23,
                            Language_Id = 1,
                            Title = "Brake Fluid",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 24,
                    Parent_Id = 21,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 24,
                            Language_Id = 1,
                            Title = "Driveline Oils",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 25,
                    Parent_Id = 21,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 25,
                            Language_Id = 1,
                            Title = "Engine Oil",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 26,
                    Parent_Id = 21,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 26,
                            Language_Id = 1,
                            Title = "Gearbox Oil",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 5005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 27,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 27,
                            Language_Id = 1,
                            Title = "Gifts, Toys & Models",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 6000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 28,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 28,
                            Language_Id = 1,
                            Title = "Cufflinks",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 29,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 29,
                            Language_Id = 1,
                            Title = "Caps & Hats",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 30,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 30,
                            Language_Id = 1,
                            Title = "Key Rings",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 31,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 31,
                            Language_Id = 1,
                            Title = "Models",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 32,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 32,
                            Language_Id = 1,
                            Title = "Mugs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 33,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 33,
                            Language_Id = 1,
                            Title = "Toys",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6006,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 34,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 34,
                            Language_Id = 1,
                            Title = "Umbrellas",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6007,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                }
                ,
                new Category
                {
                    Id = 35,
                    Parent_Id = 27,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 35,
                            Language_Id = 1,
                            Title = "Vouchers",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 6008,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 36,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 28,
                            Language_Id = 1,
                            Title = "Jaguar Merchandise",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 7000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 37,
                    Parent_Id = 36,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 37,
                            Language_Id = 1,
                            Title = "Caps & Hats",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 7001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 38,
                    Parent_Id = 36,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 38,
                            Language_Id = 1,
                            Title = "Shirts",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 7002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 39,
                    Parent_Id = 36,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 39,
                            Language_Id = 1,
                            Title = "T-Shirts",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 7003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 40,
                    Parent_Id = 36,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 40,
                            Language_Id = 1,
                            Title = "Coats & Jackets",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 7004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 41,
                    Parent_Id = 36,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 41,
                            Language_Id = 1,
                            Title = "Bags & Luggage",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 7005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 42,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 42,
                            Language_Id = 1,
                            Title = "Literature & DVDs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 42,
                            Language_Id = 1,
                            Intro = "Everything for your library",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 8000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 43,
                    Parent_Id = 42,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 43,
                            Language_Id = 1,
                            Title = "Books",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 43,
                            Language_Id = 1,
                            Intro = "Essential reading...",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 8001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 44,
                    Parent_Id = 42,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 44,
                            Language_Id = 1,
                            Title = "DVDs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 8002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 45,
                    Parent_Id = 42,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 45,
                            Language_Id = 1,
                            Title = "Owner's Manuals",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 8003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 46,
                    Parent_Id = 42,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 46,
                            Language_Id = 1,
                            Title = "Worksop Manuals",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 8004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 47,
                    Parent_Id = 42,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 47,
                            Language_Id = 1,
                            Title = "Parts Manuals",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 8005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 48,
                    Parent_Id = null,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 48,
                            Language_Id = 1,
                            Title = "Tools",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 9000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 49,
                    Parent_Id = 48,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 49,
                            Language_Id = 1,
                            Title = "Spanners",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 9001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 50,
                    Parent_Id = 48,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 50,
                            Language_Id = 1,
                            Title = "Jacks",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 9002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 51,
                    Parent_Id = 48,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 51,
                            Language_Id = 1,
                            Title = "Tool Rolls",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 9003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 52,
                    Parent_Id = 48,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 52,
                            Language_Id = 1,
                            Title = "Engine Tools",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 9004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                },
                new Category
                {
                    Id = 53,
                    Parent_Id = 48,
                    CategoryType_Id = 1,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 53,
                            Language_Id = 1,
                            Title = "Carburettor Tools",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 9005,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"

                }
            );


            #endregion

            // save progress
            SaveChanges(context);

            #region "Service Parts"

            // add service categories
            context.Categories.AddOrUpdate(c => c.Id,
                new Category
                {
                    Id = 54,
                    Parent_Id = null,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 54,
                            Language_Id = 1,
                            Title = "XK120 / XK140 / XK150",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 54,
                            Language_Id = 1,
                            Intro = "Some text all about the Classic XK range and what parts require serving, what breaks etc",
                            More = "Even more text about the Classic XK range and what parts require serving, what breaks etc",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 10000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 55,
                    Parent_Id = 54,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 55,
                            Language_Id = 1,
                            Title = "Filters",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 1000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 56,
                    Parent_Id = 54,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 56,
                            Language_Id = 1,
                            Title = "Air Filters",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 56,
                            Language_Id = 1,
                            Intro = "Air filters are great at filtering air.",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, QuantityOfFit = "3", FromBreakPoint = "VIN 12345", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, QuantityOfFit = "6", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "1 Set", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 57,
                    Parent_Id = 54,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 57,
                            Language_Id = 1,
                            Title = "Oil Filters",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 57,
                            Language_Id = 1,
                            Intro = "Oil filters are great at filtering oil.",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 7, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, QuantityOfFit = "3", FromBreakPoint = "VIN 12345", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, QuantityOfFit = "6", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "2", ToBreakPoint = "VIN 99999L", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1002,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 58,
                    Parent_Id = 54,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 58,
                            Language_Id = 1,
                            Title = "Pollen Filters",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 58,
                            Language_Id = 1,
                            Intro = "Pollen filters are great at filtering pollen.",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 3, QuantityOfFit = "6", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1003,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
                ,
                new Category
                {
                    Id = 59,
                    Parent_Id = 54,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 59,
                            Language_Id = 1,
                            Title = "Fuel Filters",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 59,
                            Language_Id = 1,
                            Intro = "Fuel filters are great at filtering fuel.",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 5, QuantityOfFit = "3", ToBreakPoint = "VIN 12345", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 6, QuantityOfFit = "6", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 7, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1004,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
                ,
                new Category
                {
                    Id = 60,
                    Parent_Id = null,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 60,
                            Language_Id = 1,
                            Title = "Brakes",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Introductions = new List<CategoryIntroduction> {
                        new CategoryIntroduction {
                            Category_Id = 60,
                            Language_Id = 1,
                            Intro = "Don't underestimate the stopping power of our <a>Four Pot Caliper Conversion Kit</a>.",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    SortOrder = 20000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 61,
                    Parent_Id = 60,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 61,
                            Language_Id = 1,
                            Title = "Front Brakes",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 2, QuantityOfFit = "3", FromBreakPoint = "VIN 12345", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 3, QuantityOfFit = "3", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }

                    },
                    SortOrder = 2000,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 62,
                    Parent_Id = 60,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 62,
                            Language_Id = 1,
                            Title = "Rear Brakes",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "9 Inches", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Category
                {
                    Id = 63,
                    Parent_Id = 60,
                    CategoryType_Id = 2,
                    Titles = new List<CategoryTitle> {
                        new CategoryTitle {
                            Category_Id = 63,
                            Language_Id = 1,
                            Title = "Brake Discs",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        },
                        new CategoryTitle {
                            Category_Id = 63,
                            Language_Id = 5,
                            Title = "Brake Rotors",
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            CreatedByUsername = "Seed",
                            UpdatedTimestampUtc = DateTime.UtcNow,
                            UpdatedByUsername = "Seed"
                        }
                    },
                    Products = new List<CategoryProduct>
                    {
                        new CategoryProduct  { Product_Id = 1, QuantityOfFit = "1", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new CategoryProduct  { Product_Id = 4, QuantityOfFit = "9 Inches", IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2001,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            #endregion

            // save progress
            SaveChanges(context);

            #endregion

            #region "Find Parts"

            #region "Families"

            // add catalogue model families
            context.CatalogueFamilies.AddOrUpdate(f => f.Id,
                new CatalogueFamily { Id = 1, StartYear = "1935", EndYear = "1961", SortOrder = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 2, StartYear = "1948", EndYear = "1961", SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 3, StartYear = "1961", EndYear = "1975", SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 4, StartYear = "1955", EndYear = "1970", SortOrder = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 5, StartYear = "1968", EndYear = "1992", SortOrder = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 6, StartYear = "1968", EndYear = "1992", SortOrder = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 7, StartYear = "1988", EndYear = "1996", SortOrder = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 8, StartYear = "1986", EndYear = "1994", SortOrder = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 9, StartYear = "1994", EndYear = "1997", SortOrder = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 10, StartYear = "1996", EndYear = "2006", SortOrder = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 11, StartYear = "1998", EndYear = "2002", SortOrder = 11, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 12, StartYear = "1999", EndYear = "2009", SortOrder = 12, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 13, StartYear = "2003", EndYear = "2007", SortOrder = 13, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 14, StartYear = "2006", EndYear = "2014", SortOrder = 14, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 15, StartYear = "2008", SortOrder = 15, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 16, StartYear = "2009", SortOrder = 16, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 17, StartYear = "2013", SortOrder = 17, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamily { Id = 18, StartYear = "2015", SortOrder = 18, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            // add catalogue model family titles
            context.CatalogueFamilyTitles.AddOrUpdate(t => t.Id,
                new CatalogueFamilyTitle { Id = 1, Family_Id = 1, Language_Id = 1, Title = "Early Saloons", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 2, Family_Id = 1, Language_Id = 2, Title = "Early Saloons FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 3, Family_Id = 1, Language_Id = 3, Title = "Early Saloons DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 4, Family_Id = 1, Language_Id = 4, Title = "Early Saloons NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 5, Family_Id = 2, Language_Id = 1, Title = "Classic XK", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 6, Family_Id = 2, Language_Id = 2, Title = "Classic XK FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 7, Family_Id = 2, Language_Id = 3, Title = "Classic XK DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 8, Family_Id = 2, Language_Id = 4, Title = "Classic XK NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 9, Family_Id = 3, Language_Id = 1, Title = "E-Type / XKE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 10, Family_Id = 3, Language_Id = 2, Title = "E-Type FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 11, Family_Id = 3, Language_Id = 3, Title = "E-Type DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 12, Family_Id = 3, Language_Id = 4, Title = "E-Type NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 13, Family_Id = 4, Language_Id = 1, Title = "Classic Saloons", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 14, Family_Id = 4, Language_Id = 2, Title = "Classic Saloons FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 15, Family_Id = 4, Language_Id = 3, Title = "Classic Saloons DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 16, Family_Id = 4, Language_Id = 4, Title = "Classic Saloons NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 17, Family_Id = 5, Language_Id = 1, Title = "XJ Series 1-3", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 18, Family_Id = 5, Language_Id = 2, Title = "XJ Series 1-3 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 19, Family_Id = 5, Language_Id = 3, Title = "XJ Series 1-3 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 20, Family_Id = 5, Language_Id = 4, Title = "XJ Series 1-3 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 21, Family_Id = 6, Language_Id = 1, Title = "Limousine", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 22, Family_Id = 6, Language_Id = 2, Title = "Limousine FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 23, Family_Id = 6, Language_Id = 3, Title = "Limousine DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 24, Family_Id = 6, Language_Id = 4, Title = "Limousine NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 25, Family_Id = 7, Language_Id = 1, Title = "XJS", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 26, Family_Id = 7, Language_Id = 2, Title = "XJS FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 27, Family_Id = 7, Language_Id = 3, Title = "XJS DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 28, Family_Id = 7, Language_Id = 4, Title = "XJS NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 29, Family_Id = 8, Language_Id = 1, Title = "XJ40", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 30, Family_Id = 8, Language_Id = 2, Title = "XJ40 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 31, Family_Id = 8, Language_Id = 3, Title = "XJ40 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 32, Family_Id = 8, Language_Id = 4, Title = "XJ40 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 33, Family_Id = 9, Language_Id = 1, Title = "X300", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 34, Family_Id = 9, Language_Id = 2, Title = "X300 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 35, Family_Id = 9, Language_Id = 3, Title = "X300 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 36, Family_Id = 9, Language_Id = 4, Title = "X300 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 37, Family_Id = 10, Language_Id = 1, Title = "XK8 / XKR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 38, Family_Id = 10, Language_Id = 2, Title = "XK8 / XKR FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 39, Family_Id = 10, Language_Id = 3, Title = "XK8 / XKR DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 40, Family_Id = 10, Language_Id = 4, Title = "XK8 / XKR NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 41, Family_Id = 11, Language_Id = 1, Title = "XJ8 / X308", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 42, Family_Id = 11, Language_Id = 2, Title = "XJ8 / X308 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 43, Family_Id = 11, Language_Id = 3, Title = "XJ8 / X308 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 44, Family_Id = 11, Language_Id = 4, Title = "XJ8 / X308 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 45, Family_Id = 12, Language_Id = 1, Title = "S-Type & X-Type", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 46, Family_Id = 12, Language_Id = 2, Title = "S-Type & X-Type FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 47, Family_Id = 12, Language_Id = 3, Title = "S-Type & X-Type DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 48, Family_Id = 12, Language_Id = 4, Title = "S-Type & X-Type NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 49, Family_Id = 13, Language_Id = 1, Title = "XJ8 / XJR / X350", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 50, Family_Id = 13, Language_Id = 2, Title = "XJ8 / XJR / X350 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 51, Family_Id = 13, Language_Id = 3, Title = "XJ8 / XJR / X350 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 52, Family_Id = 13, Language_Id = 4, Title = "XJ8 / XJR / X350 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 53, Family_Id = 14, Language_Id = 1, Title = "XK", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 54, Family_Id = 14, Language_Id = 2, Title = "XK FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 55, Family_Id = 14, Language_Id = 3, Title = "XK DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 56, Family_Id = 14, Language_Id = 4, Title = "XK NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 57, Family_Id = 15, Language_Id = 1, Title = "XF", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 58, Family_Id = 15, Language_Id = 2, Title = "XF FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 59, Family_Id = 15, Language_Id = 3, Title = "XF DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 60, Family_Id = 15, Language_Id = 4, Title = "XF NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 61, Family_Id = 16, Language_Id = 1, Title = "XJ", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 62, Family_Id = 16, Language_Id = 2, Title = "XJ FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 63, Family_Id = 16, Language_Id = 3, Title = "XJ DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 64, Family_Id = 16, Language_Id = 4, Title = "XJ NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 65, Family_Id = 17, Language_Id = 1, Title = "F-Type", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 66, Family_Id = 17, Language_Id = 2, Title = "F-Type FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 67, Family_Id = 17, Language_Id = 3, Title = "F-Type DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 68, Family_Id = 17, Language_Id = 4, Title = "F-Type NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 69, Family_Id = 18, Language_Id = 1, Title = "XE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 70, Family_Id = 18, Language_Id = 2, Title = "XE FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 71, Family_Id = 18, Language_Id = 3, Title = "XE DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueFamilyTitle { Id = 72, Family_Id = 18, Language_Id = 4, Title = "XE NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            #endregion

            #region "Models"

            // add catalogue models
            context.CatalogueModels.AddOrUpdate(m => m.Id,
                new CatalogueModel { Id = 1, Family_Id = 3, StartYear = "1961", EndYear = "1964", SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModel { Id = 2, Family_Id = 3, StartYear = "1964", EndYear = "1968", SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModel { Id = 3, Family_Id = 3, StartYear = "1969", EndYear = "1971", SortOrder = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModel { Id = 4, Family_Id = 3, StartYear = "1971", EndYear = "1973", SortOrder = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModel { Id = 5, Family_Id = 3, StartYear = "1971", EndYear = "1975", SortOrder = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            // add catalogue model titles
            context.CatalogueModelTitles.AddOrUpdate(t => t.Id,
                new CatalogueModelTitle { Id = 1, Model_Id = 1, Language_Id = 1, Title = "E-Type Series 1 3.8", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 2, Model_Id = 1, Language_Id = 2, Title = "E-Type Series 1 3.8 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 3, Model_Id = 1, Language_Id = 3, Title = "E-Type Series 1 3.8 De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 4, Model_Id = 1, Language_Id = 4, Title = "E-Type Series 1 3.8 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 5, Model_Id = 2, Language_Id = 1, Title = "E-Type Series 1 4.2", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 6, Model_Id = 2, Language_Id = 2, Title = "E-Type Series 1 4.2 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 7, Model_Id = 2, Language_Id = 3, Title = "E-Type Series 1 4.2 De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 8, Model_Id = 2, Language_Id = 4, Title = "E-Type Series 1 4.2 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 9, Model_Id = 3, Language_Id = 1, Title = "E-Type Series 2 4.2", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 10, Model_Id = 3, Language_Id = 2, Title = "E-Type Series 2 4.2 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 11, Model_Id = 3, Language_Id = 3, Title = "E-Type Series 2 4.2 De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 12, Model_Id = 3, Language_Id = 4, Title = "E-Type Series 2 4.2 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 13, Model_Id = 4, Language_Id = 1, Title = "E-Type Series 3 V12 2+2", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 14, Model_Id = 4, Language_Id = 2, Title = "E-Type Series 3 V12 2+2 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 15, Model_Id = 4, Language_Id = 3, Title = "E-Type Series 3 V12 2+2 De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 16, Model_Id = 4, Language_Id = 4, Title = "E-Type Series 3 V12 2+2 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 17, Model_Id = 5, Language_Id = 1, Title = "E-Type Series 3 V12 Roadster", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 18, Model_Id = 5, Language_Id = 2, Title = "E-Type Series 3 V12 Roadster FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 19, Model_Id = 5, Language_Id = 3, Title = "E-Type Series 3 V12 Roadster De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueModelTitle { Id = 20, Model_Id = 5, Language_Id = 4, Title = "E-Type Series 3 V12 Roadster NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            #endregion

            #region "Categories"

            // add catalogue categories
            context.CatalogueCategories.AddOrUpdate(c => c.Id,
                new CatalogueCategory { Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 10, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 11, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 12, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 13, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 14, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 15, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 16, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 17, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 18, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 19, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 20, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 21, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 22, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 23, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 24, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 25, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 26, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 27, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 28, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 29, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 30, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 31, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 32, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 33, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 34, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 35, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 36, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 37, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategory { Id = 38, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            // add catalogue category titles
            context.CatalogueCategoryTitles.AddOrUpdate(t => t.Id,
                new CatalogueCategoryTitle { Id = 1, Category_Id = 1, Language_Id = 1, Title = "Accessories", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 2, Category_Id = 2, Language_Id = 1, Title = "Automatic Transmission", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 3, Category_Id = 3, Language_Id = 1, Title = "Body", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 4, Category_Id = 4, Language_Id = 1, Title = "Body Fittings", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 5, Category_Id = 5, Language_Id = 1, Title = "Body Shell", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 6, Category_Id = 6, Language_Id = 1, Title = "Body Shell & Panels", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 7, Category_Id = 7, Language_Id = 1, Title = "Body Shell, Panels & Frame", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 8, Category_Id = 8, Language_Id = 1, Title = "Brakes", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 9, Category_Id = 9, Language_Id = 1, Title = "Clutch", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 10, Category_Id = 10, Language_Id = 1, Title = "Clutch & Gearbox", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 11, Category_Id = 11, Language_Id = 1, Title = "Cooling System", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 12, Category_Id = 12, Language_Id = 1, Title = "Electrical Equipment", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 13, Category_Id = 13, Language_Id = 1, Title = "Engine", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 14, Category_Id = 14, Language_Id = 1, Title = "Exhaust System", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 15, Category_Id = 15, Language_Id = 1, Title = "Front Frame", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 16, Category_Id = 16, Language_Id = 1, Title = "Fuel System", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 17, Category_Id = 17, Language_Id = 1, Title = "Gearbox", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 18, Category_Id = 18, Language_Id = 1, Title = "Heater", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 19, Category_Id = 19, Language_Id = 1, Title = "Heating & Air Conditioning", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 20, Category_Id = 20, Language_Id = 1, Title = "Lighting", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 21, Category_Id = 21, Language_Id = 1, Title = "Optional Extras", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 22, Category_Id = 22, Language_Id = 1, Title = "Propshaft & Final Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 23, Category_Id = 23, Language_Id = 1, Title = "Propshaft & Rear Axle", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 24, Category_Id = 24, Language_Id = 1, Title = "Road Wheels", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 25, Category_Id = 25, Language_Id = 1, Title = "Service Parts", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 26, Category_Id = 26, Language_Id = 1, Title = "Steering", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 27, Category_Id = 27, Language_Id = 1, Title = "Steering & Suspension", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 28, Category_Id = 28, Language_Id = 1, Title = "Suspension", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 29, Category_Id = 29, Language_Id = 1, Title = "Tool Kit", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 30, Category_Id = 30, Language_Id = 1, Title = "Upgrades", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 31, Category_Id = 31, Language_Id = 1, Title = "Brake Controls", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 32, Category_Id = 32, Language_Id = 1, Title = "Brake Pipes - Hydraulic - LHD", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 33, Category_Id = 33, Language_Id = 1, Title = "Brake Pipes - Hydraulic - RHD", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 34, Category_Id = 34, Language_Id = 1, Title = "Brake Servo", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 35, Category_Id = 35, Language_Id = 1, Title = "Brake Servo - Reservac Tank", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 36, Category_Id = 36, Language_Id = 1, Title = "Handbrake Controls", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 37, Category_Id = 37, Language_Id = 1, Title = "Front Brakes", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 38, Category_Id = 38, Language_Id = 1, Title = "Rear Brakes", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 39, Category_Id = 1, Language_Id = 2, Title = "Accessories FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 40, Category_Id = 2, Language_Id = 2, Title = "Automatic Transmission FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 41, Category_Id = 3, Language_Id = 2, Title = "Body FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 42, Category_Id = 4, Language_Id = 2, Title = "Body Fittings FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 43, Category_Id = 5, Language_Id = 2, Title = "Body Shell FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 44, Category_Id = 6, Language_Id = 2, Title = "Body Shell & Panels FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 45, Category_Id = 7, Language_Id = 2, Title = "Body Shell, Panels & Frame FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 46, Category_Id = 8, Language_Id = 2, Title = "Brakes FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 47, Category_Id = 9, Language_Id = 2, Title = "Clutch FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 48, Category_Id = 10, Language_Id = 2, Title = "Clutch & Gearbox FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 49, Category_Id = 11, Language_Id = 2, Title = "Cooling System FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 50, Category_Id = 12, Language_Id = 2, Title = "Electrical Equipment FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 51, Category_Id = 13, Language_Id = 2, Title = "Engine FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 52, Category_Id = 14, Language_Id = 2, Title = "Exhaust System FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 53, Category_Id = 15, Language_Id = 2, Title = "Front Frame FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 54, Category_Id = 16, Language_Id = 2, Title = "Fuel System FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 55, Category_Id = 17, Language_Id = 2, Title = "Gearbox FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 56, Category_Id = 18, Language_Id = 2, Title = "Heater FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 57, Category_Id = 19, Language_Id = 2, Title = "Heating & Air Conditioning FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 58, Category_Id = 20, Language_Id = 2, Title = "Lighting FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 59, Category_Id = 21, Language_Id = 2, Title = "Optional Extras FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 60, Category_Id = 22, Language_Id = 2, Title = "Propshaft & Final Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 61, Category_Id = 23, Language_Id = 2, Title = "Propshaft & Rear Axle FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 62, Category_Id = 24, Language_Id = 2, Title = "Road Wheels FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 63, Category_Id = 25, Language_Id = 2, Title = "Service Parts FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 64, Category_Id = 26, Language_Id = 2, Title = "Steering FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 65, Category_Id = 27, Language_Id = 2, Title = "Steering & Suspension FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 66, Category_Id = 28, Language_Id = 2, Title = "Suspension FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 67, Category_Id = 29, Language_Id = 2, Title = "Tool Kit FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 68, Category_Id = 30, Language_Id = 2, Title = "Upgrades FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 69, Category_Id = 31, Language_Id = 2, Title = "Brake Controls FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 70, Category_Id = 32, Language_Id = 2, Title = "Brake Pipes - Hydraulic - LHD FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 71, Category_Id = 33, Language_Id = 2, Title = "Brake Pipes - Hydraulic - RHD FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 72, Category_Id = 34, Language_Id = 2, Title = "Brake Servo FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 73, Category_Id = 35, Language_Id = 2, Title = "Brake Servo - Reservac Tank FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 74, Category_Id = 36, Language_Id = 2, Title = "Handbrake Controls FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 75, Category_Id = 37, Language_Id = 2, Title = "Front Brakes FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 76, Category_Id = 38, Language_Id = 2, Title = "Rear Brakes FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed FR", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 77, Category_Id = 1, Language_Id = 3, Title = "Accessories DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 78, Category_Id = 2, Language_Id = 3, Title = "Automatic Transmission DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 79, Category_Id = 3, Language_Id = 3, Title = "Body DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 80, Category_Id = 4, Language_Id = 3, Title = "Body Fittings DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 81, Category_Id = 5, Language_Id = 3, Title = "Body Shell DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 82, Category_Id = 6, Language_Id = 3, Title = "Body Shell & Panels DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 83, Category_Id = 7, Language_Id = 3, Title = "Body Shell, Panels & Frame DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 84, Category_Id = 8, Language_Id = 3, Title = "Brakes DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 85, Category_Id = 9, Language_Id = 3, Title = "Clutch DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 86, Category_Id = 10, Language_Id = 3, Title = "Clutch & Gearbox DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 87, Category_Id = 11, Language_Id = 3, Title = "Cooling System DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 88, Category_Id = 12, Language_Id = 3, Title = "Electrical Equipment DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 89, Category_Id = 13, Language_Id = 3, Title = "Engine DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 90, Category_Id = 14, Language_Id = 3, Title = "Exhaust System DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 91, Category_Id = 15, Language_Id = 3, Title = "Front Frame DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 92, Category_Id = 16, Language_Id = 3, Title = "Fuel System DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 93, Category_Id = 17, Language_Id = 3, Title = "Gearbox DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 94, Category_Id = 18, Language_Id = 3, Title = "Heater DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 95, Category_Id = 19, Language_Id = 3, Title = "Heating & Air Conditioning DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 96, Category_Id = 20, Language_Id = 3, Title = "Lighting DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 97, Category_Id = 21, Language_Id = 3, Title = "Optional Extras DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 98, Category_Id = 22, Language_Id = 3, Title = "Propshaft & Final Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 99, Category_Id = 23, Language_Id = 3, Title = "Propshaft & Rear Axle DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 100, Category_Id = 24, Language_Id = 3, Title = "Road Wheels DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 101, Category_Id = 25, Language_Id = 3, Title = "Service Parts DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 102, Category_Id = 26, Language_Id = 3, Title = "Steering DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 103, Category_Id = 27, Language_Id = 3, Title = "Steering & Suspension DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 104, Category_Id = 28, Language_Id = 3, Title = "Suspension DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 105, Category_Id = 29, Language_Id = 3, Title = "Tool Kit DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 106, Category_Id = 30, Language_Id = 3, Title = "Upgrades DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 107, Category_Id = 31, Language_Id = 3, Title = "Brake Controls DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 108, Category_Id = 32, Language_Id = 3, Title = "Brake Pipes - Hydraulic - LHD DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 109, Category_Id = 33, Language_Id = 3, Title = "Brake Pipes - Hydraulic - RHD DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 110, Category_Id = 34, Language_Id = 3, Title = "Brake Servo DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 111, Category_Id = 35, Language_Id = 3, Title = "Brake Servo - Reservac Tank DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 112, Category_Id = 36, Language_Id = 3, Title = "Handbrake Controls DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 113, Category_Id = 37, Language_Id = 3, Title = "Front Brakes DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 114, Category_Id = 38, Language_Id = 3, Title = "Rear Brakes DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed DE", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 115, Category_Id = 1, Language_Id = 4, Title = "Accessories NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 116, Category_Id = 2, Language_Id = 4, Title = "Automatic Transmission NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 117, Category_Id = 3, Language_Id = 4, Title = "Body NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 118, Category_Id = 4, Language_Id = 4, Title = "Body Fittings NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 119, Category_Id = 5, Language_Id = 4, Title = "Body Shell NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 120, Category_Id = 6, Language_Id = 4, Title = "Body Shell & Panels NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 121, Category_Id = 7, Language_Id = 4, Title = "Body Shell, Panels & Frame NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 122, Category_Id = 8, Language_Id = 4, Title = "Brakes NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 123, Category_Id = 9, Language_Id = 4, Title = "Clutch NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 124, Category_Id = 10, Language_Id = 4, Title = "Clutch & Gearbox NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 125, Category_Id = 11, Language_Id = 4, Title = "Cooling System NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 126, Category_Id = 12, Language_Id = 4, Title = "Electrical Equipment NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 127, Category_Id = 13, Language_Id = 4, Title = "Engine NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 128, Category_Id = 14, Language_Id = 4, Title = "Exhaust System NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 129, Category_Id = 15, Language_Id = 4, Title = "Front Frame NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 130, Category_Id = 16, Language_Id = 4, Title = "Fuel System NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 131, Category_Id = 17, Language_Id = 4, Title = "Gearbox NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 132, Category_Id = 18, Language_Id = 4, Title = "Heater NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 133, Category_Id = 19, Language_Id = 4, Title = "Heating & Air Conditioning NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 134, Category_Id = 20, Language_Id = 4, Title = "Lighting NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 135, Category_Id = 21, Language_Id = 4, Title = "Optional Extras NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 136, Category_Id = 22, Language_Id = 4, Title = "Propshaft & Final Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 137, Category_Id = 23, Language_Id = 4, Title = "Propshaft & Rear Axle NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 138, Category_Id = 24, Language_Id = 4, Title = "Road Wheels NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 139, Category_Id = 25, Language_Id = 4, Title = "Service Parts NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 140, Category_Id = 26, Language_Id = 4, Title = "Steering NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 141, Category_Id = 27, Language_Id = 4, Title = "Steering & Suspension NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 142, Category_Id = 28, Language_Id = 4, Title = "Suspension NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 143, Category_Id = 29, Language_Id = 4, Title = "Tool Kit NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 144, Category_Id = 30, Language_Id = 4, Title = "Upgrades NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 145, Category_Id = 31, Language_Id = 4, Title = "Brake Controls NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 146, Category_Id = 32, Language_Id = 4, Title = "Brake Pipes - Hydraulic - LHD NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 147, Category_Id = 33, Language_Id = 4, Title = "Brake Pipes - Hydraulic - RHD NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 148, Category_Id = 34, Language_Id = 4, Title = "Brake Servo NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 149, Category_Id = 35, Language_Id = 4, Title = "Brake Servo - Reservac Tank NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 150, Category_Id = 36, Language_Id = 4, Title = "Handbrake Controls NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 151, Category_Id = 37, Language_Id = 4, Title = "Front Brakes NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueCategoryTitle { Id = 152, Category_Id = 38, Language_Id = 4, Title = "Rear Brakes NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            // same assembly could be linked to multiple application paths
            context.CatalogueAssemblies.AddOrUpdate(a => a.Id,
                // E-Type S1 4.2 > Brakes > Brake Controls
                new CatalogueAssembly { Id = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Handbrake Controls
                new CatalogueAssembly { Id = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Brake Pipes - Hydraulic - LHD
                new CatalogueAssembly { Id = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Brake Pipes - Hydraulic - RHD
                new CatalogueAssembly { Id = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Brake Servo
                new CatalogueAssembly { Id = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Brake Servo - Reservac Tank
                new CatalogueAssembly { Id = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Front Brakes
                new CatalogueAssembly { Id = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Rear Brakes
                new CatalogueAssembly { Id = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                // E-Type S1 4.2 > Brakes > Multiple Depths
                new CatalogueAssembly { Id = 9, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );


            SaveChanges(context);

            #endregion

            #region "Assemblies"

            context.CatalogueAssemblyTitles.AddOrUpdate(t => t.Id,
                new CatalogueAssemblyTitle { Id = 1, Assembly_Id = 7, Language_Id = 1, Title = "Front Brake Assembly", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 2, Assembly_Id = 8, Language_Id = 1, Title = "Rear Brake Assembly", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 3, Assembly_Id = 7, Language_Id = 2, Title = "Front Brake Assembly FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 4, Assembly_Id = 8, Language_Id = 2, Title = "Rear Brake Assembly FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 5, Assembly_Id = 7, Language_Id = 3, Title = "Front Brake Assembly DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 6, Assembly_Id = 8, Language_Id = 3, Title = "Rear Brake Assembly DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 7, Assembly_Id = 7, Language_Id = 4, Title = "Front Brake Assembly NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 8, Assembly_Id = 8, Language_Id = 4, Title = "Rear Brake Assembly NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyTitle { Id = 9, Assembly_Id = 9, Language_Id = 1, Title = "Multiple Depths", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                );

            SaveChanges(context);

            context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                new CatalogueAssemblyNode { Id = 1, Assembly_Id = 7, Parent = null, SortOrder = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNode { Id = 2, Assembly_Id = 7, Parent = null, SortOrder = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNode { Id = 3, Assembly_Id = 7, Parent = null, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNode { Id = 4, Assembly_Id = 7, Parent = null, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNode { Id = 5, Assembly_Id = 7, Parent = null, SortOrder = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNode { Id = 6, Assembly_Id = 7, Parent = null, SortOrder = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
            );

            SaveChanges(context);

            var parentNode = context.CatalogueAssemblyNodes.FirstOrDefault(a => a.Id == 2);

            if (parentNode != null)
            {
                prod = context.Products.FirstOrDefault(p => p.PartNumber == "JIM1008");

                if (prod != null)
                {
                    parentNode.Products = new List<CatalogueAssemblyNodeProduct>();
                    parentNode.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "TEST PARTNO", QuantityOfFit = "1PR", ProductDetails = null, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
                    parentNode.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "JIM1008", QuantityOfFit = "1PR", ProductDetails = prod, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });

                    SaveChanges(context);
                }

                context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                    new CatalogueAssemblyNode { Id = 7, Assembly_Id = 7, Parent = parentNode, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                    new CatalogueAssemblyNode { Id = 8, Assembly_Id = 7, Parent = parentNode, SortOrder = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                    new CatalogueAssemblyNode { Id = 9, Assembly_Id = 7, Parent = parentNode, SortOrder = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                );

                SaveChanges(context);

                var node = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 7);
                if (node != null)
                {
                    node.Products = new List<CatalogueAssemblyNodeProduct>();
                    node.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "TEST PARTNO", QuantityOfFit = "1PR", ProductDetails = null, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });

                    SaveChanges(context);
                }

                node = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 8);
                if (node != null)
                {
                    node.Products = new List<CatalogueAssemblyNodeProduct>();

                    if (prod != null)
                    {
                        node.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "JIM1008", QuantityOfFit = "1PR", ProductDetails = prod, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
                    }

                    SaveChanges(context);
                }

                node = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 9);
                if (node != null)
                {
                    node.Products = new List<CatalogueAssemblyNodeProduct>();
                    node.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "TEST PARTNO", QuantityOfFit = "2PR", ProductDetails = null, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });

                    SaveChanges(context);
                }

            }



            context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                new CatalogueAssemblyNode { Id = 10, Assembly_Id = 7, Parent = null, SortOrder = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
             );

            SaveChanges(context);

            parentNode = context.CatalogueAssemblyNodes.FirstOrDefault(a => a.Id == 10);

            if (parentNode != null)
            {


                context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                    new CatalogueAssemblyNode { Id = 11, Assembly_Id = 7, Parent = parentNode, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                );

                SaveChanges(context);

                parentNode = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 11);
                if (parentNode != null)
                {
                    context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                        new CatalogueAssemblyNode { Id = 12, Assembly_Id = 7, Parent = parentNode, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );

                    SaveChanges(context);
                }

                parentNode = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 12);
                if (parentNode != null)
                {
                    context.CatalogueAssemblyNodes.AddOrUpdate(n => n.Id,
                        new CatalogueAssemblyNode { Id = 13, Assembly_Id = 7, Parent = parentNode, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );

                    SaveChanges(context);
                }

                var node = context.CatalogueAssemblyNodes.FirstOrDefault(n => n.Id == 13);
                if (node != null)
                {
                    node.Products = new List<CatalogueAssemblyNodeProduct>();

                    prod = context.Products.FirstOrDefault(p => p.Id == 3);

                    if (prod != null)
                    {
                        node.Products.Add(new CatalogueAssemblyNodeProduct { PartNumber = "JIM1008", QuantityOfFit = "1PR", ProductDetails = prod, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
                    }

                    SaveChanges(context);
                }

            }

            context.CatalogueAssemblyNodeTitles.AddOrUpdate(t => t.Id,
                new CatalogueAssemblyNodeTitle { Id = 1, Node_Id = 1, Language_Id = 1, Title = "Brake Caliper - Four Pot Conversion", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 2, Node_Id = 2, Language_Id = 1, Title = "Brake Disc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 3, Node_Id = 3, Language_Id = 1, Title = "Brake Caliper Assembly - LH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 4, Node_Id = 4, Language_Id = 1, Title = "Brake Caliper Assembly - RH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 5, Node_Id = 5, Language_Id = 1, Title = "Brake Caliper Piston Retraction Tool", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 6, Node_Id = 6, Language_Id = 1, Title = "Front Wheel Cylinder Repair Kit", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 7, Node_Id = 1, Language_Id = 2, Title = "Brake Caliper - Four Pot Conversion FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 8, Node_Id = 2, Language_Id = 2, Title = "Brake Disc FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 9, Node_Id = 3, Language_Id = 2, Title = "Brake Caliper Assembly - LH FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 10, Node_Id = 4, Language_Id = 2, Title = "Brake Caliper Assembly - RH FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 11, Node_Id = 5, Language_Id = 2, Title = "Brake Caliper Piston Retraction Tool FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 12, Node_Id = 6, Language_Id = 2, Title = "Front Wheel Cylinder Repair Kit FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 13, Node_Id = 1, Language_Id = 3, Title = "Brake Caliper - Four Pot Conversion DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 14, Node_Id = 2, Language_Id = 3, Title = "Brake Disc DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 15, Node_Id = 3, Language_Id = 3, Title = "Brake Caliper Assembly - LH DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 16, Node_Id = 4, Language_Id = 3, Title = "Brake Caliper Assembly - RH DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 17, Node_Id = 5, Language_Id = 3, Title = "Brake Caliper Piston Retraction Tool DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 18, Node_Id = 6, Language_Id = 3, Title = "Front Wheel Cylinder Repair Kit DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 19, Node_Id = 1, Language_Id = 4, Title = "Brake Caliper - Four Pot Conversion NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 20, Node_Id = 2, Language_Id = 4, Title = "Brake Disc NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 21, Node_Id = 3, Language_Id = 4, Title = "Brake Caliper Assembly - LH NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 22, Node_Id = 4, Language_Id = 4, Title = "Brake Caliper Assembly - RH NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 23, Node_Id = 5, Language_Id = 4, Title = "Brake Caliper Piston Retraction Tool NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 24, Node_Id = 6, Language_Id = 4, Title = "Front Wheel Cylinder Repair Kit NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 25, Node_Id = 7, Language_Id = 1, Title = "Disc for Front Brake Calipers", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 26, Node_Id = 8, Language_Id = 1, Title = "Bolt, securing Discs to Front Hubs", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 27, Node_Id = 9, Language_Id = 1, Title = "Nut, Self-Locking, on Bolts", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 28, Node_Id = 7, Language_Id = 2, Title = "Disc for Front Brake Calipers FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 29, Node_Id = 8, Language_Id = 2, Title = "Bolt, securing Discs to Front Hubs FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 30, Node_Id = 9, Language_Id = 2, Title = "Nut, Self-Locking, on Bolts FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 31, Node_Id = 7, Language_Id = 3, Title = "Disc for Front Brake Calipers De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 32, Node_Id = 8, Language_Id = 3, Title = "Bolt, securing Discs to Front Hubs De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 33, Node_Id = 9, Language_Id = 3, Title = "Nut, Self-Locking, on Bolts De", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 34, Node_Id = 7, Language_Id = 4, Title = "Disc for Front Brake Calipers NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 35, Node_Id = 8, Language_Id = 4, Title = "Bolt, securing Discs to Front Hubs NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 36, Node_Id = 9, Language_Id = 4, Title = "Nut, Self-Locking, on Bolts NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 37, Node_Id = 10, Language_Id = 1, Title = "Multi Depth 1", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 38, Node_Id = 11, Language_Id = 1, Title = "Multi Depth 2", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 39, Node_Id = 12, Language_Id = 1, Title = "Multi Depth 3", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new CatalogueAssemblyNodeTitle { Id = 40, Node_Id = 13, Language_Id = 1, Title = "Multi Depth 4", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                );

            SaveChanges(context);

            #endregion

            var model = context.CatalogueModels.FirstOrDefault(m => m.Id == 2);
            var category = context.CatalogueCategories.FirstOrDefault(c => c.Id == 8);

            if (model != null && category != null)
            {
                var section = new CatalogueCategory();

                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 31);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Brake Controls
                        new CatalogueApplication { Id = 1, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 1, SortOrder = 3, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 36);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Handbrake Controls
                        new CatalogueApplication { Id = 2, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 2, SortOrder = 8, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 32);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Brake Pipes - Hydraulic - LHD
                        new CatalogueApplication { Id = 3, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 3, SortOrder = 4, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 33);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Brake Pipes - Hydraulic - RHD
                        new CatalogueApplication { Id = 4, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 4, SortOrder = 5, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 34);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Brake Servo
                        new CatalogueApplication { Id = 5, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 5, SortOrder = 6, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 35);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Brake Servo - Reservac Tank
                        new CatalogueApplication { Id = 6, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 6, SortOrder = 7, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 37);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Front Brakes
                        new CatalogueApplication { Id = 7, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 7, SortOrder = 1, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }
                section = context.CatalogueCategories.FirstOrDefault(c => c.Id == 38);
                if (section != null)
                {
                    context.CatalogueApplications.AddOrUpdate(a => a.Id,
                        // E-Type S1 4.2 > Brakes > Rear Brakes
                        new CatalogueApplication { Id = 8, Model = model, Category = category, Section = section, SubSection = section, Assembly_Id = 8, SortOrder = 2, IsActive = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed NL", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    );
                }

            }

            SaveChanges(context);

            #endregion


            // save progress
            SaveChanges(context);


            #region "Vehicles"

            #region "Marques"

            // add marques
            context.VehicleMarques.AddOrUpdate(m => m.Id,
                new VehicleMarque
                {
                    Id = 1,
                    Titles = new List<VehicleMarqueTitle> {
                        new VehicleMarqueTitle { Language_Id = 1, Title = "Jaguar", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 2, Title = "Jaguar FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 3, Title = "Jaguar DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 4, Title = "Jaguar NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleMarque
                {
                    Id = 2,
                    Titles = new List<VehicleMarqueTitle> {
                        new VehicleMarqueTitle { Language_Id = 1, Title = "Daimler", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 2, Title = "Daimler FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 3, Title = "Daimler DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleMarqueTitle { Language_Id = 4, Title = "Daimler NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Model Ranges"

            // add model ranges
            context.VehicleRanges.AddOrUpdate(r => r.Id,
                new VehicleRange
                {
                    Id = 1,
                    Titles = new List<VehicleRangeTitle>
                    {
                        new VehicleRangeTitle { Language_Id = 1, Title = "E-Type / XKE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleRangeTitle { Language_Id = 2, Title = "E-Type FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleRangeTitle { Language_Id = 3, Title = "E-Type DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleRangeTitle { Language_Id = 4, Title = "E-Type NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Models"

            // add models
            context.VehicleModels.AddOrUpdate(m => m.Id,
                new VehicleModel
                {
                    Id = 1,
                    Titles = new List<VehicleModelTitle>
                    {
                        new VehicleModelTitle { Language_Id = 1, Title = "E-Type", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelTitle { Language_Id = 2, Title = "E-Type FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelTitle { Language_Id = 3, Title = "E-Type DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelTitle { Language_Id = 4, Title = "E-Type NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Model Variants"            

            // add model variants
            context.VehicleModelVariants.AddOrUpdate(m => m.Id,
                new VehicleModelVariant
                {
                    Id = 1,
                    Titles = new List<VehicleModelVariantTitle> {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series 1", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series 1 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series 1 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series 1 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 2,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series 1.5", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series 1.5 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series 1.5 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series 1.5 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 3,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series 2", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series 2 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series 2 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series 2 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 4,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series 3", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series 3 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series 3 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series 3 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 5,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series I", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series I FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series I DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series I NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 6,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series II", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series II FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series II DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series II NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleModelVariant
                {
                    Id = 7,
                    Titles = new List<VehicleModelVariantTitle>
                    {
                        new VehicleModelVariantTitle { Language_Id = 1, Title = "Series III", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 2, Title = "Series III FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 3, Title = "Series III DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleModelVariantTitle { Language_Id = 4, Title = "Series III NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);


            #endregion

            #region "Body"

            // add steering variants
            context.VehicleBodyVariants.AddOrUpdate(v => v.Id,
                new VehicleBodyVariant
                {
                    Id = 1,
                    Titles = new List<VehicleBodyVariantTitle>
                    {
                        new VehicleBodyVariantTitle { Language_Id = 1, Title = "Saloon", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 2, Title = "Saloon FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 3, Title = "Saloon DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 4, Title = "Saloon NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleBodyVariant
                {
                    Id = 2,
                    Titles = new List<VehicleBodyVariantTitle>
                    {
                        new VehicleBodyVariantTitle { Language_Id = 1, Title = "Estate", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 2, Title = "Estate FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 3, Title = "Estate DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 4, Title = "Estate NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleBodyVariant
                {
                    Id = 3,
                    Titles = new List<VehicleBodyVariantTitle>
                    {
                        new VehicleBodyVariantTitle { Language_Id = 1, Title = "Fixed Head Coupe", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 2, Title = "Fixed Head Coupe FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 3, Title = "Fixed Head Coupe DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 4, Title = "Fixed Head Coupe NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleBodyVariant
                {
                    Id = 4,
                    Titles = new List<VehicleBodyVariantTitle>
                    {
                        new VehicleBodyVariantTitle { Language_Id = 1, Title = "Drop Head Coupe", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 2, Title = "Drop Head Coupe FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 3, Title = "Drop Head Coupe DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleBodyVariantTitle { Language_Id = 4, Title = "Drop Head Coupe NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Engine Types"

            // add engine types
            context.VehicleEngineTypeVariants.AddOrUpdate(m => m.Id,
                new VehicleEngineTypeVariant
                {
                    Id = 1,
                    Titles = new List<VehicleEngineTypeVariantTitle>
                    {
                        new VehicleEngineTypeVariantTitle { Language_Id = 1, Title = "Petrol", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 2, Title = "Petrol FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 3, Title = "Petrol DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 4, Title = "Petrol NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleEngineTypeVariant
                {
                    Id = 2,
                    Titles = new List<VehicleEngineTypeVariantTitle>
                    {
                        new VehicleEngineTypeVariantTitle { Language_Id = 1, Title = "Diesel", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 2, Title = "Diesel FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 3, Title = "Diesel DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineTypeVariantTitle { Language_Id = 4, Title = "Diesel NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Engines"

            // add engine variants
            context.VehicleEngineVariants.AddOrUpdate(m => m.Id,
                new VehicleEngineVariant
                {
                    Id = 1,
                    Titles = new List<VehicleEngineVariantTitle>
                    {
                        new VehicleEngineVariantTitle { Language_Id = 1, Title = "3.8 litre", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineVariantTitle {  Language_Id = 2, Title = "3.8 litre FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineVariantTitle {  Language_Id = 3, Title = "3.8 litre DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleEngineVariantTitle {  Language_Id = 4, Title = "3.8 litre NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleEngineVariant
                {
                    Id = 2,
                    Titles = new List<VehicleEngineVariantTitle>
                    {
                        new VehicleEngineVariantTitle { Language_Id = 1, Title = "4.2 litre", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle {  Language_Id = 2, Title = "4.2 litre FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle {  Language_Id = 3, Title = "4.2 litre DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle {  Language_Id = 4, Title = "4.2 litre NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleEngineVariant
                {
                    Id = 3,
                    Titles = new List<VehicleEngineVariantTitle>
                    {
                        new VehicleEngineVariantTitle {  Language_Id = 1, Title = "V12", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle { Language_Id = 2, Title = "V12 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle {  Language_Id = 3, Title = "V12 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle { Language_Id = 4, Title = "V12 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleEngineVariant
                {
                    Id = 4,
                    Titles = new List<VehicleEngineVariantTitle>
                    {
                new VehicleEngineVariantTitle {  Language_Id = 1, Title = "3.0 litre V6", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle { Language_Id = 2, Title = "3.0 litre V6 FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle { Language_Id = 3, Title = "3.0 litre V6 DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                new VehicleEngineVariantTitle {  Language_Id = 4, Title = "3.0 litre V6 NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion            

            #region "Transmission"

            // add transmission variants
            context.VehicleTransmissionVariants.AddOrUpdate(v => v.Id,
                new VehicleTransmissionVariant
                {
                    Id = 1,
                    Titles = new List<VehicleTransmissionVariantTitle>
                    {
                        new VehicleTransmissionVariantTitle { Language_Id = 1, Title = "Manual", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle { Language_Id = 2, Title = "Manual FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle { Language_Id = 3, Title = "Manual DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle { Language_Id = 4, Title = "Manual NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleTransmissionVariant
                {
                    Id = 2,
                    Titles = new List<VehicleTransmissionVariantTitle>
                    {
                        new VehicleTransmissionVariantTitle { Language_Id = 1, Title = "Automatic", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle {  Language_Id = 2, Title = "Automatic FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle { Language_Id = 3, Title = "Automatic DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTransmissionVariantTitle { Id = 8, Transmission_Id = 2, Language_Id = 4, Title = "Automatic NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);


            #endregion

            #region "Drivetrain"

            // add drivetrain variants
            context.VehicleDrivetrainVariants.AddOrUpdate(v => v.Id,
                new VehicleDrivetrainVariant
                {
                    Id = 1,
                    Titles = new List<VehicleDrivetrainVariantTitle> {
                        new VehicleDrivetrainVariantTitle { Language_Id = 1, Title = "Front Wheel Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 2, Title = "Front Wheel Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 3, Title = "Front Wheel Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 4, Title = "Front Wheel Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleDrivetrainVariant
                {
                    Id = 2,
                    Titles = new List<VehicleDrivetrainVariantTitle>
                    {
                        new VehicleDrivetrainVariantTitle { Language_Id = 1, Title = "Rear Wheel Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 2, Title = "Rear Wheel Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 3, Title = "Rear Wheel Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 4, Title = "Rear Wheel Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleDrivetrainVariant
                {
                    Id = 3,
                    Titles = new List<VehicleDrivetrainVariantTitle>
                    {
                        new VehicleDrivetrainVariantTitle { Language_Id = 1, Title = "All Wheel Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 2, Title = "All Wheel Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 3, Title = "All Wheel Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleDrivetrainVariantTitle { Language_Id = 4, Title = "All Wheel Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Steering"

            // add steering variants
            context.VehicleSteeringVariants.AddOrUpdate(v => v.Id,
                new VehicleSteeringVariant
                {
                    Id = 1,
                    Titles = new List<VehicleSteeringVariantTitle>
                    {
                        new VehicleSteeringVariantTitle { Language_Id = 1, Title = "Left Hand Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 2, Title = "Left Hand Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 3, Title = "Left Hand Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 4, Title = "Left Hand Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleSteeringVariant
                {
                    Id = 2,
                    Titles = new List<VehicleSteeringVariantTitle>
                    {
                        new VehicleSteeringVariantTitle { Language_Id = 1, Title = "Right Hand Drive", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 2, Title = "Right Hand Drive FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 3, Title = "Right Hand Drive DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleSteeringVariantTitle {  Language_Id = 4, Title = "Right Hand Drive NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            #region "Trim Levels"

            // add trim level variants
            context.VehicleTrimLevelVariants.AddOrUpdate(v => v.Id,
                new VehicleTrimLevelVariant
                {
                    Id = 1,
                    Titles = new List<VehicleTrimLevelVariantTitle>
                    {
                        new VehicleTrimLevelVariantTitle { Language_Id = 1, Title = "Prestige", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 2, Title = "Prestige FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 3, Title = "Prestige DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 4, Title = "Prestige NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleTrimLevelVariant
                {
                    Id = 2,
                    Titles = new List<VehicleTrimLevelVariantTitle>
                    {
                        new VehicleTrimLevelVariantTitle {  Language_Id = 1, Title = "R-Sport", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle { Language_Id = 2, Title = "R-Sport FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle { Language_Id = 3, Title = "R-Sport DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle { Language_Id = 4, Title = "R-Sport NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleTrimLevelVariant
                {
                    Id = 3,
                    Titles = new List<VehicleTrimLevelVariantTitle>
                    {
                        new VehicleTrimLevelVariantTitle { Language_Id = 1, Title = "Portfolio", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 2, Title = "Portfolio FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 3, Title = "Portfolio DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 4, Title = "Portfolio NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 3,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new VehicleTrimLevelVariant
                {
                    Id = 4,
                    Titles = new List<VehicleTrimLevelVariantTitle>
                    {
                        new VehicleTrimLevelVariantTitle {  Language_Id = 1, Title = "S", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 2, Title = "S FR", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle {  Language_Id = 3, Title = "S DE", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new VehicleTrimLevelVariantTitle { Language_Id = 4, Title = "S NL", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4,
                    IsActive = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            );

            SaveChanges(context);

            #endregion

            context.Vehicles.AddOrUpdate(v => v.Id,
                new Vehicle
                {
                    Id = 1,
                    Marque_Id = 1, // Jaguar
                    Range_Id = 1, // E-Type
                    Model_Id = 1, // E-Type
                    ModelVariant_Id = 1, // Series 1
                    EngineType_Id = 1, // Petrol
                    Engine_Id = 2, // 4.2 litre
                    Body_Id = 3, // FHC
                    Transmission_Id = 1, // manual
                    Steering_Id = 2, // RHD
                    SortOrder = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                });

            // save progress
            SaveChanges(context);

            #endregion



            #region "User Accounts"

            var manager = new UserManager<UserAccount>(new UserStore<UserAccount>(new jCtrlContext()));

            var user = new UserAccount
            {
                UserName = "admin@sngbarratt.com",
                Email = "admin@sngbarratt.com",
                EmailConfirmed = true,
                CreatedTimestampUtc = new DateTime(1991, 1, 1, 0, 0, 0),
                UpdatedTimestampUtc = DateTime.UtcNow
            };

            manager.Create(user, "Test1234!");

            #endregion

            #region "Roles"

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new jCtrlContext()));

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "system_admin" });

                roleManager.Create(new IdentityRole { Name = "internal_user" });

                roleManager.Create(new IdentityRole { Name = "admin_area" });

                roleManager.Create(new IdentityRole { Name = "accounts_author" });
                roleManager.Create(new IdentityRole { Name = "accounts_reviewer" });

                roleManager.Create(new IdentityRole { Name = "banners_author_bau" });
                roleManager.Create(new IdentityRole { Name = "banners_author_snd" });
                roleManager.Create(new IdentityRole { Name = "banners_author_snf" });
                roleManager.Create(new IdentityRole { Name = "banners_author_sng" });
                roleManager.Create(new IdentityRole { Name = "banners_author_snh" });
                roleManager.Create(new IdentityRole { Name = "banners_reviewer_bau" });
                roleManager.Create(new IdentityRole { Name = "banners_reviewer_snd" });
                roleManager.Create(new IdentityRole { Name = "banners_reviewer_snf" });
                roleManager.Create(new IdentityRole { Name = "banners_reviewer_sng" });
                roleManager.Create(new IdentityRole { Name = "banners_reviewer_snh" });

                roleManager.Create(new IdentityRole { Name = "categories_author" });
                roleManager.Create(new IdentityRole { Name = "categories_reviewer" });

                roleManager.Create(new IdentityRole { Name = "clearance_author_bau" });
                roleManager.Create(new IdentityRole { Name = "clearance_author_snd" });
                roleManager.Create(new IdentityRole { Name = "clearance_author_snf" });
                roleManager.Create(new IdentityRole { Name = "clearance_author_sng" });
                roleManager.Create(new IdentityRole { Name = "clearance_author_snh" });
                roleManager.Create(new IdentityRole { Name = "clearance_reviewer_bau" });
                roleManager.Create(new IdentityRole { Name = "clearance_reviewer_snd" });
                roleManager.Create(new IdentityRole { Name = "clearance_reviewer_snf" });
                roleManager.Create(new IdentityRole { Name = "clearance_reviewer_sng" });
                roleManager.Create(new IdentityRole { Name = "clearance_reviewer_snh" });


                roleManager.Create(new IdentityRole { Name = "customer_author" });
                roleManager.Create(new IdentityRole { Name = "customer_reviewer" });

                roleManager.Create(new IdentityRole { Name = "downloads_author" });
                roleManager.Create(new IdentityRole { Name = "downloads_reviewer" });

                roleManager.Create(new IdentityRole { Name = "events_author" });
                roleManager.Create(new IdentityRole { Name = "events_reviewer" });

                roleManager.Create(new IdentityRole { Name = "hotspots_author" });
                roleManager.Create(new IdentityRole { Name = "hotspots_reviewer" });

                roleManager.Create(new IdentityRole { Name = "offers_author_bau" });
                roleManager.Create(new IdentityRole { Name = "offers_author_snd" });
                roleManager.Create(new IdentityRole { Name = "offers_author_snf" });
                roleManager.Create(new IdentityRole { Name = "offers_author_sng" });
                roleManager.Create(new IdentityRole { Name = "offers_author_snh" });
                roleManager.Create(new IdentityRole { Name = "offers_reviewer_bau" });
                roleManager.Create(new IdentityRole { Name = "offers_reviewer_snd" });
                roleManager.Create(new IdentityRole { Name = "offers_reviewer_snf" });
                roleManager.Create(new IdentityRole { Name = "offers_reviewer_sng" });
                roleManager.Create(new IdentityRole { Name = "offers_reviewer_snh" });

                roleManager.Create(new IdentityRole { Name = "orders_processor_bau" });
                roleManager.Create(new IdentityRole { Name = "orders_processor_snd" });
                roleManager.Create(new IdentityRole { Name = "orders_processor_snf" });
                roleManager.Create(new IdentityRole { Name = "orders_processor_sng" });
                roleManager.Create(new IdentityRole { Name = "orders_processor_snh" });

                roleManager.Create(new IdentityRole { Name = "products_author" });
                roleManager.Create(new IdentityRole { Name = "products_reviewer" });

                roleManager.Create(new IdentityRole { Name = "subscribers_author" });
                roleManager.Create(new IdentityRole { Name = "subscribers_reviewer" });

                user = manager.FindByName("admin@sngbarratt.com");
                if (user != null)
                {
                    manager.AddToRoles(user.Id, new string[] { "system_admin", "admin_area", "internal_user" });
                }

            }

            SaveChanges(context);

            #endregion

            #region "Customers"

            context.Customers.AddOrUpdate(c => c.Id,
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
                });

            // save progress
            SaveChanges(context);


            var cust = context.Customers.FirstOrDefault();
            if (cust != null)
            {

                // link customer with user account

                user = manager.FindByName("admin@sngbarratt.com");

                if (user != null)
                {
                    user.Customer_Id = cust.Id;

                    manager.Update(user);
                }


                #region "Payment Card"

                var card = new PaymentCard
                {
                    Customer_Id = cust.Id,
                    CardNumber = "4111111111111111",
                    ExpiryDate = DateTime.Now.AddMonths(12),
                    IsDefault = true,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                };

                var key = ConfigurationManager.AppSettings["Encryption_Key"];
                var salt = ConfigurationManager.AppSettings["Encryption_Salt"];

                // encrypt card details
                card.Encrypt(key, salt);

                // add payment card
                context.PaymentCards.AddOrUpdate(c => c.Id, card);

                #endregion

                // save progress
                SaveChanges(context);


                // get some products
                var prod10 = context.BranchProducts.Where(p => p.Product_Id == 10).FirstOrDefault();
                var prod8 = context.BranchProducts.Where(p => p.Product_Id == 8).FirstOrDefault();
                var prod5 = context.BranchProducts.Where(p => p.Product_Id == 5).FirstOrDefault();

                #region "Cart Items"

                // add cart items
                context.CartItems.AddOrUpdate(i => i.Id,
                    new CartItem
                    {
                        Id = Guid.Parse("f1f1f790-1d13-4416-b079-5f41bcedc4ab"),
                        Customer_Id = cust.Id,
                        CustomerLevel_Id = cust.TradingTerms_Code,
                        Branch_Id = cust.Branch_Id,
                        BranchProduct_Id = prod10.Id,
                        PartNumber = "JIM1010",
                        PartTitle = "Test Part Number 10",
                        DiscountCode = "#2",
                        ExpiresUtc = DateTime.UtcNow.AddDays(30),
                        IsExpired = false,
                        IsCheckedOut = false,
                        RetailPrice = 100.00m,
                        UnitPrice = 90.00m,
                        Surcharge = 0.00m,
                        QuantityRequired = 2,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        CreatedByUsername = "Seed",
                        UpdatedTimestampUtc = DateTime.UtcNow,
                        UpdatedByUsername = "Seed"
                    },
                    new CartItem
                    {
                        Id = Guid.Parse("1294782a-4dd7-4445-a451-590069eaa3aa"),
                        Customer_Id = cust.Id,
                        CustomerLevel_Id = cust.TradingTerms_Code,
                        Branch_Id = cust.Branch_Id,
                        BranchProduct_Id = prod8.Id,
                        PartNumber = "JIM1008",
                        PartTitle = "Test Part Number 8",
                        DiscountCode = "#2",
                        ExpiresUtc = DateTime.UtcNow.AddDays(30),
                        IsExpired = false,
                        IsCheckedOut = false,
                        RetailPrice = 100.00m,
                        UnitPrice = 90.00m,
                        Surcharge = 0.00m,
                        QuantityRequired = 1,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        CreatedByUsername = "Seed",
                        UpdatedTimestampUtc = DateTime.UtcNow,
                        UpdatedByUsername = "Seed"
                    },
                    new CartItem
                    {
                        Id = Guid.Parse("e04027e6-70af-45cc-9e16-deec23671c1a"),
                        Customer_Id = cust.Id,
                        CustomerLevel_Id = cust.TradingTerms_Code,
                        Branch_Id = cust.Branch_Id,
                        BranchProduct_Id = prod5.Id,
                        PartNumber = "JIM1005",
                        PartTitle = "Test Part Number 5",
                        DiscountCode = "#2",
                        ExpiresUtc = DateTime.UtcNow.AddDays(30),
                        IsExpired = false,
                        IsCheckedOut = false,
                        RetailPrice = 100.00m,
                        UnitPrice = 80.00m,
                        Surcharge = 50.00m,
                        QuantityRequired = 1,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        CreatedByUsername = "Seed",
                        UpdatedTimestampUtc = DateTime.UtcNow,
                        UpdatedByUsername = "Seed"
                    }
                );

                #endregion

                #region "Web Orders"

                // add web orders
                context.WebOrders.AddOrUpdate(o => o.Id,
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
                                BranchProduct_Id = prod10.Id,
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
            );

                #endregion

                #region "Wish Lists"

                // add wish lists
                context.WishLists.AddOrUpdate(l => l.Id,
                    new WishList
                    {
                        Id = Guid.Parse("069441bd-29eb-4ed4-88c6-8068c7bb0488"),
                        Customer_Id = cust.Id,
                        DisplayName = "Christmas List",
                        Items = new List<WishListItem> {
                            new WishListItem {
                                Id = Guid.Parse("9cf0e670-d3e5-4564-bb34-d5d7d60b4c4e"),
                                Customer_Id = cust.Id,
                                Product_Id = 10,
                                PartNumber = "JIM1010",
                                PartTitle = "Test Part Number 10",
                                Quantity = 12,
                                RowVersion = 1,
                                CreatedTimestampUtc = DateTime.UtcNow,
                                CreatedByUsername = "Seed",
                                UpdatedTimestampUtc = DateTime.UtcNow,
                                UpdatedByUsername = "Seed"
                            }
                        },
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        CreatedByUsername = "Seed",
                        UpdatedTimestampUtc = DateTime.UtcNow,
                        UpdatedByUsername = "Seed"
                    }
                );

                #endregion


                // save progress
                SaveChanges(context);
            }

            #endregion




            // turn off identity insert for every table       
            #region "TURN OFF IDENTITY INSERT"         
            foreach (PropertyInfo pi in context.GetType().GetProperties())
            {
                // var attributes = pi.GetCustomAttributes();

                try
                {
                    if (pi.Name != "Users" && pi.Name != "Roles" && pi.Name != "Claims")
                    {
                        System.Diagnostics.Debug.WriteLine("Adding IDENTITY INSERT to " + pi.Name);
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[" + pi.Name + "] OFF");
                    }
                }
                catch (SqlException e)
                {

                    if (e.Number == 8106)
                    {
                        // ignore the errors for tables that don't have an identity
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                }
            }

            #endregion


        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" + sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
