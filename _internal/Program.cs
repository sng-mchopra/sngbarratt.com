using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Globalization;
using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Domain.Product;
using System;
using System.Collections.Generic;

namespace _internal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Internal Test App - SNG Barratt Group";
            Console.WriteLine("Internal Test App");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Simple Types");
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Simple Types 
            #region Currency
            Console.WriteLine("Currency Instances");
            IList<Currency> currencies = new List<Currency>();

            currencies.Add(new Currency()
            {
                Code = "GBR",
                Name = "Pounds Sterling",
                Symbol = "£"
            });

            currencies.Add(new Currency()
            {
                Code = "EUR",
                Name = "Euro",
                Symbol = "€"
            });

            currencies.Add(new Currency()
            {
                Code = "USD",
                Name = "US Dollar",
                Symbol = "$"
            });

            currencies.Add(new Currency()
            {
                Code = "AUD",
                Name = "Australian Dollar",
                Symbol = "$"
            });

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var item in currencies)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name} Symbol: {item.Symbol}");
            }
            Console.WriteLine();
            #endregion

            #region Advert Type
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Advert Type Instances");
            IList<VoucherType> advertTypes = new List<VoucherType>();
            advertTypes.Add(new VoucherType()
            {
                Id = "I",
                Name = "Image"
            });

            advertTypes.Add(new VoucherType()
            {
                Id = "V",
                Name = "Video"
            });

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var item in advertTypes)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name}");
            }
            Console.WriteLine();
            #endregion

            #region Tax Rate Category
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tax Rate Category Instances");

            IList<TaxRateCategory> taxRateCategories = new List<TaxRateCategory>();
            taxRateCategories.Add(new TaxRateCategory()
            {
                Id = 1,
                Name = "Tax Rate 1"
            });

            taxRateCategories.Add(new TaxRateCategory()
            {
                Id = 1,
                Name = "Tax Rate 2"
            });

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var item in taxRateCategories)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name}");
            }
            Console.WriteLine();
            #endregion

            #region Customer Account Type Instances
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Customer Account Type Instances");

            List<CustomerAccountType> customerAccountTypes = new List<CustomerAccountType>();
            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "C",
                Name = "Competitor"
            });

            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "G",
                Name = "Garage"
            });

            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "I",
                Name = "Internal"
            });

            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "R",
                Name = "Retail"
            });

            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "S",
                Name = "Specialist"
            });

            customerAccountTypes.Add(new CustomerAccountType()
            {
                Code = "T",
                Name = "Trade"
            });

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var item in customerAccountTypes)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Discount Level Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Discount Level Instances");

            IList<DiscountLevel> discountLevels = new List<DiscountLevel>();

            discountLevels.Add(new DiscountLevel()
            {
                Code = "#0",
                Retail = 100,
                Level1 = 100,
                Level2 = 100,
                Level3 = 100,
                Level4 = 100,
                Level5 = 100,
                Level6 = 100
            });
            discountLevels.Add(new DiscountLevel()
            {
                Code = "#1",
                Retail = 100,
                Level2 = 97.5m,
                Level3 = 95,
                Level4 = 92.5m,
                Level5 = 90,
                Level6 = 85
            });
            discountLevels.Add(new DiscountLevel()
            {
                Code = "#2",
                Retail = 90,
                Level2 = 80,
                Level3 = 70,
                Level4 = 60,
                Level5 = 55,
                Level6 = 50
            });

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var item in discountLevels)
            {
                Console.WriteLine($"Code: {item.Code} Retail: {item.Retail} Level1: {item.Level1} Level2: {item.Level2} Level3: {item.Level3} Level4: {item.Level4} Level5: {item.Level5} Level6: {item.Level6}");
            }
            Console.WriteLine();
            #endregion

            #region Language Instances
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Language Instances");

            IList<Language> languages = new List<Language>();
            languages.Add(new Language()
            {
                Id = 1,
                Code = "EN",
                Name = "English"
            });
            languages.Add(new Language()
            {
                Id = 2,
                Code = "FR",
                Name = "French"
            });
            languages.Add(new Language()
            {
                Id = 3,
                Code = "DE",
                Name = "German"
            });
            languages.Add(new Language()
            {
                Id = 4,
                Code = "NL",
                Name = "Dutch"
            });
            languages.Add(new Language()
            {
                Id = 5,
                Code = "US",
                Name = "American"
            });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in languages)
            {
                Console.WriteLine($"Id: {item.Id} Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Phone Number Type Instances
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Phone Number Type Instances");

            IList<PhoneNumberType> numberTypes = new List<PhoneNumberType>();

            numberTypes.Add(new PhoneNumberType()
            {
                Id = 1,
                Name = "Home",
                IsDefault = true
            });

            numberTypes.Add(new PhoneNumberType()
            {
                Id = 2,
                Name = "Mobile",
                IsDefault = false
            });

            numberTypes.Add(new PhoneNumberType()
            {
                Id = 3,
                Name = "Work",
                IsDefault = false
            });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in numberTypes)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} IsDefault: {item.IsDefault}");
            }

            Console.WriteLine();
            #endregion

            #region Product Brand Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Product Brand Instances");

            IList<ProductBrand> productBrands = new List<ProductBrand>();
            productBrands.Add(new ProductBrand { Id = 1, Name = "SNG Barratt", LogoFilename = "madebysng.png" });
            productBrands.Add(new ProductBrand { Id = 2, Name = "Jaguar" });
            productBrands.Add(new ProductBrand { Id = 3, Name = "Aftermarket" });
            productBrands.Add(new ProductBrand { Id = 4, Name = "Uprated" });
            productBrands.Add(new ProductBrand { Id = 4, Name = "Uprated" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in productBrands)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} LogoFilename: {item.LogoFilename}");
            }

            Console.WriteLine();
            #endregion

            #region Product Component Status Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Product Component Status Instances ");

            IList<ProductComponentStatus> componentStatuses = new List<ProductComponentStatus>();
            componentStatuses.Add(new ProductComponentStatus { Code = "Y", Name = "Yes" });
            componentStatuses.Add(new ProductComponentStatus { Code = "N", Name = "No" });
            componentStatuses.Add(new ProductComponentStatus { Code = "B", Name = "Both" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in componentStatuses)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Product Status Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Product Status Instances ");

            IList<ProductStatus> productStatuses = new List<ProductStatus>();
            productStatuses.Add(new ProductStatus { Code = "Y", Name = "Available" });
            productStatuses.Add(new ProductStatus { Code = "N", Name = "Not Available" });
            productStatuses.Add(new ProductStatus { Code = "D", Name = "Depot Denied" });
            productStatuses.Add(new ProductStatus { Code = "F", Name = "Frozen" });
            productStatuses.Add(new ProductStatus { Code = "S", Name = "Superceded" });
            productStatuses.Add(new ProductStatus { Code = "Z", Name = "Turned Down" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in productStatuses)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Product Type Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Product Type Instances ");

            IList<ProductType> productTypes = new List<ProductType>();
            productTypes.Add(new ProductType { Code = "AC", Name = "Accessory" });
            productTypes.Add(new ProductType { Code = "AE", Name = "Aerosol" });
            productTypes.Add(new ProductType { Code = "AF", Name = "Aftermarket" });
            productTypes.Add(new ProductType { Code = "GJ", Name = "Genuine Jaguar" });
            productTypes.Add(new ProductType { Code = "GL", Name = "Glass" });
            productTypes.Add(new ProductType { Code = "LI", Name = "Literature" });
            productTypes.Add(new ProductType { Code = "ME", Name = "Merchandise" });
            productTypes.Add(new ProductType { Code = "NG", Name = "Non-Genuine" });
            productTypes.Add(new ProductType { Code = "OE", Name = "Original Equipment" });
            productTypes.Add(new ProductType { Code = "PR", Name = "Performance" });
            productTypes.Add(new ProductType { Code = "RE", Name = "Reconditioned" });
            productTypes.Add(new ProductType { Code = "RP", Name = "Reproduction" });
            productTypes.Add(new ProductType { Code = "SU", Name = "Superceded" });
            productTypes.Add(new ProductType { Code = "UP", Name = "Uprated" });
            productTypes.Add(new ProductType { Code = "U1", Name = "Used - Grade A" });
            productTypes.Add(new ProductType { Code = "U2", Name = "Used - Grade B" });
            productTypes.Add(new ProductType { Code = "U3", Name = "Used - Grade C" });
            productTypes.Add(new ProductType { Code = "WH", Name = "White Box" });
            productTypes.Add(new ProductType { Code = "ZZ", Name = "Turned Down" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in productTypes)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Shipping Coverage Level Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Shipping Coverage Level Instances ");

            IList<ShippingCoverageLevel> shippingCoverageLevels = new List<ShippingCoverageLevel>();
            shippingCoverageLevels.Add(new ShippingCoverageLevel { Code = "A", Name = "All" });
            shippingCoverageLevels.Add(new ShippingCoverageLevel { Code = "D", Name = "Domestic" });
            shippingCoverageLevels.Add(new ShippingCoverageLevel { Code = "E", Name = "Europe" });
            shippingCoverageLevels.Add(new ShippingCoverageLevel { Code = "W", Name = "Worldwide" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in shippingCoverageLevels)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Shipping Provider Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Shipping Provider Instances ");

            IList<ShippingProvider> shippingProviders = new List<ShippingProvider>();
            shippingProviders.Add(new ShippingProvider { Id = 1, Name = "Legacy", IsActive = false });
            shippingProviders.Add(new ShippingProvider { Id = 2, Name = "Collect", IsActive = true });
            shippingProviders.Add(new ShippingProvider { Id = 3, Name = "UPS", IsActive = true });
            shippingProviders.Add(new ShippingProvider { Id = 4, Name = "Royal Mail", IsActive = true });
            shippingProviders.Add(new ShippingProvider { Id = 5, Name = "Chronoship", IsActive = true });
            shippingProviders.Add(new ShippingProvider { Id = 6, Name = "USPS", IsActive = true });
            shippingProviders.Add(new ShippingProvider { Id = 7, Name = "DHL", IsActive = true });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in shippingProviders)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} IsActive: {item.IsActive}");
            }

            Console.WriteLine();
            #endregion

            #region Customer Trading Level Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Customer Trading Level Instances ");

            IList<CustomerTradingLevel> tradingLevels = new List<CustomerTradingLevel>();

            tradingLevels.Add(new CustomerTradingLevel { Code = "RN", Name = "Retail" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "1N", Name = "Level 1" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "1Y", Name = "Level 1 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "2N", Name = "Level 2" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "2Y", Name = "Level 2 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "3N", Name = "Level 3" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "3Y", Name = "Level 3 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "4N", Name = "Level 4" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "4Y", Name = "Level 4 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "5N", Name = "Level 5" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "5Y", Name = "Level 5 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "6N", Name = "Level 6" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "6Y", Name = "Level 6 with TNET" });
            tradingLevels.Add(new CustomerTradingLevel { Code = "CN", Name = "Cost" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in tradingLevels)
            {
                Console.WriteLine($"Code: {item.Code} Name: {item.Name}");
            }

            Console.WriteLine();
            #endregion

            #region Tweet Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tweet Instances ");

            IList<Tweet> tweets = new List<Tweet>();

            tweets.Add(new Tweet { Id = 1, Text = "Tweet 1", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-1), UpdatedTimestampUtc = DateTime.UtcNow });
            tweets.Add(new Tweet { Id = 2, Text = "Tweet 2", CreatedTimestampUtc = DateTime.UtcNow.AddHours(-2), UpdatedTimestampUtc = DateTime.UtcNow });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in tweets)
            {
                Console.WriteLine($"Id: {item.Id} Text: {item.Text} CreatedTimestampUtc: {item.CreatedTimestampUtc} UpdatedTimestampUtc: {item.UpdatedTimestampUtc} ");
            }

            Console.WriteLine();
            #endregion

            #region Voucher Type Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voucher Type Instances ");

            IList<VoucherType> voucherTypes = new List<VoucherType>();

            voucherTypes.Add(new VoucherType { Id = "F", Name = "Fixed Amount" });
            voucherTypes.Add(new VoucherType { Id = "P", Name = "Percentage" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in voucherTypes)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} ");
            }

            Console.WriteLine();
            #endregion

            #region Web Order Event Type Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Web Order Event Type Instances ");

            IList<WebOrderEventType> webOrderEventTypes = new List<WebOrderEventType>();

            webOrderEventTypes.Add(new WebOrderEventType { Id = "WS", Name = "Web Order Submitted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "WA", Name = "Web Order Accepted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "WD", Name = "Web Order Declined" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "WX", Name = "Web Order Deleted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QC", Name = "Sales Quote Created" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QU", Name = "Sales Quote Updated" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QA", Name = "Sales Quote Accepted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QD", Name = "Sales Quote Declined" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QE", Name = "Sales Quote Expired" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "QX", Name = "Sales Quote Deleted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "OC", Name = "Sales Order Created" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "OU", Name = "Sales Order Updated" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "OD", Name = "Sales Order Cancelled" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "OX", Name = "Sales Order Deleted" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "PC", Name = "Pick Ticket Created" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "KC", Name = "Pack Ticket Created" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "ZZ", Name = "Failed Payment" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "IC", Name = "Sales Invoice Created" });
            webOrderEventTypes.Add(new WebOrderEventType { Id = "CC", Name = "Credit Note Created" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in webOrderEventTypes)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} ");
            }

            Console.WriteLine();
            #endregion

            #region Web Order Item Status Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Web Order Item Status Instances ");

            IList<WebOrderItemStatus> webOrderItemStatus = new List<WebOrderItemStatus>();

            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "S", Name = "Request submitted" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "R", Name = "Rejected by company operator" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "Q", Name = "Internal quote line created" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "A", Name = "Internal quote approved by web customer" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "D", Name = "Internal quote declined by web customer" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "E", Name = "Internal quote expired" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "O", Name = "Internal order line created" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "I", Name = "Internal order line, partially invoiced" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "F", Name = "Internal order line, fully invoiced" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "X", Name = "Internal order line cancelled" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "C", Name = "Credit raised (full or partial)" });
            webOrderItemStatus.Add(new WebOrderItemStatus { Id = "?", Name = "Unknown" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in webOrderItemStatus)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} ");
            }

            Console.WriteLine();
            #endregion

            #region Web Order Status Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Web Order Status Instances ");

            IList<WebOrderStatus> webOrderStatus = new List<WebOrderStatus>();

            webOrderStatus.Add(new WebOrderStatus { Id = "S", Name = "Request submitted", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "R", Name = "Rejected by branch operative", IsOnGoing = false });
            webOrderStatus.Add(new WebOrderStatus { Id = "Q", Name = "Internal quote created", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "K", Name = "Pending customer approval", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "D", Name = "Quote declined by customer", IsOnGoing = false });
            webOrderStatus.Add(new WebOrderStatus { Id = "E", Name = "Quote expired", IsOnGoing = false });
            webOrderStatus.Add(new WebOrderStatus { Id = "A", Name = "Quote approved by customer", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "O", Name = "Internal order created", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "I", Name = "In progress", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "X", Name = "Order cancelled by customer", IsOnGoing = false });
            webOrderStatus.Add(new WebOrderStatus { Id = "Z", Name = "Order failed payment", IsOnGoing = true });
            webOrderStatus.Add(new WebOrderStatus { Id = "C", Name = "Order closed by branch operative", IsOnGoing = false });
            webOrderStatus.Add(new WebOrderStatus { Id = "F", Name = "Order fulfilled", IsOnGoing = false });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in webOrderStatus)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} ");
            }

            Console.WriteLine();
            #endregion

            #region Category Type Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Category Type Instances ");

            IList<CategoryType> categoryTypes = new List<CategoryType>();

            categoryTypes.Add(new CategoryType { Id = 1, Name = "Accessories" });
            categoryTypes.Add(new CategoryType { Id = 2, Name = "Service Parts" });
            categoryTypes.Add(new CategoryType { Id = 3, Name = "Upgrades" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in categoryTypes)
            {
                Console.WriteLine($"Id: {item.Id} Name: {item.Name} ");
            }

            Console.WriteLine();
            #endregion

            //Complex Types - TODO: TO complete 
            #region Country Type Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Country Type Instances ");

            //First create some country titles:
            IList<CountryTitle> titles = new List<CountryTitle>();

            titles.Add(new CountryTitle
            {
                Id = 1,
                Country_Code = "GB",
                Language_Id = 1,
                Title = "United Kingdom",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Great Britian, England, Wales, Scotland",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            titles.Add(new CountryTitle
            {
                Id = 2,
                Country_Code = "GB",
                Language_Id = 2,
                Title = "Royaume-Uni",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Grande-Bretagne, Angleterre, Pays de Galles, Écosse",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            IList<CountryTitle> titlesForUS = new List<CountryTitle>();

            titlesForUS.Add(new CountryTitle { Id = 6, Country_Code = "US", Language_Id = 1, Title = "United States", AlternativeSpellings = "US, U.S, U.S.A, America", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });


            IList<Country> countries = new List<Country>();

            countries.Add(new Country
            {
                Code = "GB",
                Titles = titles,
                InternationalDialingCode = "+44",
                IsMemberOfEEC = true,
                IsEuropean = true
            });

            countries.Add(new Country
            {
                Code = "FR",
                Titles = null,
                InternationalDialingCode = "+33",
                IsMemberOfEEC = true,
                IsEuropean = true
            });

            countries.Add(new Country
            {
                Code = "DE",
                Titles = null,
                InternationalDialingCode = "+49",
                IsMemberOfEEC = true,
                IsEuropean = true
            });

            countries.Add(new Country
            {
                Code = "NL",
                Titles = null,
                InternationalDialingCode = "+31",
                IsMemberOfEEC = true,
                IsEuropean = true
            });

            countries.Add(new Country
            {
                Code = "US",
                Titles = titlesForUS,
                InternationalDialingCode = "+1",
                IsMemberOfEEC = false,
                IsEuropean = false
            });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in countries)
            {
                Console.WriteLine($"Code: {item.Code} Titles: {item.Titles?.Count} International Dialing Code: {item.InternationalDialingCode} IsMemberOfEEc: {item.IsMemberOfEEC} IsEuropean: {item.IsEuropean}");
            }

            Console.WriteLine();
            #endregion

            #region Country Titles Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Country Titles Instances ");

            IList<CountryTitle> countyTitles = new List<CountryTitle>();

            countyTitles.Add(new CountryTitle
            {
                Id = 1,
                Country_Code = "GB",
                Language_Id = 1,
                Title = "United Kingdom",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Great Britian, England, Wales, Scotland",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            countyTitles.Add(new CountryTitle
            {
                Id = 2,
                Country_Code = "GB",
                Language_Id = 2,
                Title = "Royaume-Uni",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Grande-Bretagne, Angleterre, Pays de Galles, Écosse",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            countyTitles.Add(new CountryTitle
            {
                Id = 3,
                Country_Code = "GB",
                Language_Id = 3,
                Title = "Großbritannien",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, England, Wales, Schottland",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            countyTitles.Add(new CountryTitle
            {
                Id = 4,
                Country_Code = "GB",
                Language_Id = 4,
                Title = "Verenigd Koningkrijk",
                AlternativeSpellings = "UK, U.K, U.K., GB, G.B, groot-Brittannië, Engeland, Wales, Schotland",
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });

            countyTitles.Add(new CountryTitle { Id = 5, Country_Code = "GB", Language_Id = 5, Title = "United Kingdom", AlternativeSpellings = "UK, U.K, U.K., GB, G.B, Great Britian, England, Wales, Scotland", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
            countyTitles.Add(new CountryTitle { Id = 6, Country_Code = "FR", Language_Id = 1, Title = "France", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
            countyTitles.Add(new CountryTitle { Id = 7, Country_Code = "DE", Language_Id = 1, Title = "Germany", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
            countyTitles.Add(new CountryTitle { Id = 8, Country_Code = "NL", Language_Id = 1, Title = "Netherlands", AlternativeSpellings = "Holland, The Netherlands", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });
            countyTitles.Add(new CountryTitle { Id = 6, Country_Code = "US", Language_Id = 1, Title = "United States", AlternativeSpellings = "US, U.S, U.S.A, America", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" });

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in countyTitles)
            {
                Console.WriteLine($"Id: {item.Id} Country_Code: {item.Country_Code} Language_Id: {item.Language_Id} Title: {item.Title} Alternative Spellings: {item.AlternativeSpellings}");
            }

            Console.WriteLine();
            #endregion

            #region Contact Instances 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Contact Instances ");

            IList<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact
            {
                Name = "Manish Chopra",
                PhoneNumber = "0121 345 333",
                EmailAddress = "manishchopra@microsoft.com",
                AddressLine1 = "10 Harley Drive",
                AddressLine2 = "Bridgnorth",
                TownCity = "London",
                CountyState = "Buckhinghamshire",
                CountryName = "England",
                Country_Code = "GB",
                PostalCode = "WV22 4AV",
                IsVerifiedAddress = true,
                RowVersion = 1,
                CreatedTimestampUtc = DateTime.UtcNow,
                CreatedByUsername = "Seed",
                UpdatedTimestampUtc = DateTime.UtcNow,
                UpdatedByUsername = "Seed"
            });


            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in contacts)
            {
                Console.WriteLine($@"
                    Name: {item.Name} 
                    Phone: {item.PhoneNumber} 
                    Email: {item.EmailAddress}
                    AddressLine1: {item.AddressLine1}
                    PostalCode: {item.PostalCode}
                    Country Code: {item.Country_Code}");
            }

            Console.WriteLine();
            #endregion

            #region Customer Shipping Address Instances
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Customer Shipping Address Instances ");

            IList<CustomerShippingAddress> shippingAddresses = new List<CustomerShippingAddress> {
                            new CustomerShippingAddress { DisplayName = "Mum and Dad's", Name = "C Griffin", AddressLine1 = "6 Barnard Avenue", TownCity = "Street", CountyState = "Somerset", PostalCode = "BA16 0RW", CountryName = "United Kingdom", Country_Code = "GB", IsVerifiedAddress = false, IsDefault = false, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                            new CustomerShippingAddress { DisplayName = "Work", Name = "James Griffin", AddressLine1 = "SNG Barratt", AddressLine2 = "The Heritage Building", TownCity = "Bridgnorth", CountyState = "Shropshire", PostalCode = "WV15 6AP", CountryName = "United Kingdom", Country_Code = "GB", IsVerifiedAddress = true, IsDefault = true, RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                        };

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in shippingAddresses)
            {
                Console.WriteLine($@"
                    Display Name: {item.DisplayName} 
                    Name: {item.Name} 
                    AddressLine1: {item.AddressLine1}
                    AddressLine2: {item.AddressLine2}
                    TownCity: {item.TownCity}
                    CountyState: {item.CountyState}
                    PostalCode: {item.PostalCode}
                    CountryName: {item.CountryName}
                    Country_Code: {item.Country_Code}");
            }

            Console.WriteLine();


            #endregion






            Console.ReadKey();
        }
    }
}
