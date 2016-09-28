using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jCtrl.Shipping.Chronoship;
using jCtrl.Shipping.UPS;
using jCtrl.Shipping.USPS;
using System.Diagnostics;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Shipping;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain;

namespace jCtrl.Shipping
{
    public class Rates
    {

        public static async Task<List<ShippingQuote>> GetShippingOptions(Branch branch, Customer customer, Contact recipient, Country deliveryCountry, List<ShippingProvider> providers, List<ShippingMethod> methods, List<PackingContainer> containers, List<CartItem> cart, string reference)
        {
            var lst = new List<ShippingQuote>();


            #region "Calc Weight / Pack Boxes"

            // pack items in boxes and calculate total weight                                                
            var packages = new List<ShippingQuotePackage>();
            var packages_UPS = new List<ShippingQuotePackage>();
            decimal totalWeight = 0m;
            decimal totalWeight_UPS = 0m;

            if (branch.BranchCode == "SNF")
            {
                // Chronoship just uses the weight, so no need to pack items for FR

                // calc weight from items alone
                totalWeight = Utilities.CalcCartWeight_Kgs(cart);
            }
            else if (containers.Any())
            {
                // pack the items

                packages = jCtrl.Shipping.Utilities.PackCartItems(cart, containers, false);
                if (packages.Any())
                {
                    // get total weight from packages
                    foreach (var pkg in packages)
                    {
                        totalWeight += pkg.WeightKgs;
                    }
                }
                else
                {
                    // calc weight from items alone
                    totalWeight = jCtrl.Shipping.Utilities.CalcCartWeight_Kgs(cart);
                }

                // pack items with UPS boxes available
                packages_UPS = jCtrl.Shipping.Utilities.PackCartItems(cart, containers, true);
                if (packages_UPS.Any())
                {
                    // get total weight from packages
                    foreach (var pkg in packages_UPS)
                    {
                        totalWeight_UPS += pkg.WeightKgs;
                    }
                }
                else
                {
                    // calc weight from items alone
                    totalWeight_UPS = jCtrl.Shipping.Utilities.CalcCartWeight_Kgs(cart);
                }

            }
            else
            {
                // no active boxes

                // calc weight from items alone
                totalWeight = jCtrl.Shipping.Utilities.CalcCartWeight_Kgs(cart);
                totalWeight_UPS = jCtrl.Shipping.Utilities.CalcCartWeight_Kgs(cart);
            }

            #endregion


            ShippingProvider provider = null;

            #region "UPS"

            // get UPS options
            if (branch.BranchCode == "BAU" ||
                branch.BranchCode == "SND" ||
                branch.BranchCode == "SNG" ||
                branch.BranchCode == "SNH")
            {
                provider = providers.Where(p => p.Name == "UPS").FirstOrDefault();
                if (provider != null)
                {
                    // get UPS methods
                    var methods_ups = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id)
                        .ToList();

                    if (methods_ups.Any())
                    {
                        lst.AddRange(await jCtrl.Shipping.Rates.GetShippingOptions_UPS(branch, customer, recipient, provider, methods_ups, packages_UPS, totalWeight_UPS, reference));
                    }
                }
            }

            #endregion

            #region "USPS"

            // get USPS options
            if (branch.BranchCode == "BAU")
            {
                provider = providers.Where(p => p.Name == "USPS").FirstOrDefault();
                if (provider != null)
                {
                    // get USPS methods
                    var methods_usps = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id)
                        .ToList();

                    if (methods_usps.Any())
                    {
                        // TODO: add USPS as a shipping service
                        lst.AddRange(await jCtrl.Shipping.Rates.GetShippingOptions_USPS(branch, customer, recipient, provider, methods_usps, packages, totalWeight, reference));
                    }
                }
            }

            #endregion

            #region "DHL"

            // get DHL options
            if (branch.BranchCode == "DHL")
            {
                provider = providers.Where(p => p.Name == "DHL").FirstOrDefault();
                if (provider != null)
                {
                    // get DHL methods
                    var methods_dhl = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id)
                        .ToList();

                    if (methods_dhl.Any())
                    {
                        // TODO: add DHL as a shipping service
                        // lst.AddRange(await jCtrl.Shipping.Rates.GetShippingOptions_DHL(branch, customer, recipient, provider, methods_dhl, packages, totalWeight, reference));
                    }
                }
            }

            #endregion

            #region "Chronoship"

            // get Chronoship options
            if (branch.BranchCode == "SNF")
            {
                provider = providers.Where(p => p.Name == "Chronoship").FirstOrDefault();
                if (provider != null)
                {
                    // get Chronoship methods
                    var methods_chrono = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id)
                        .ToList();

                    if (methods_chrono.Any())
                    {
                        lst.AddRange(await jCtrl.Shipping.Rates.GetShippingOptions_Chronoship(branch, customer, recipient, provider, methods_chrono, totalWeight, reference));
                    }
                }
            }

            #endregion

            #region "Royal Mail"

            // get Royal Mail options
            if (branch.BranchCode == "SNG")
            {
                // royal mail
                provider = providers.Where(p => p.Name == "Royal Mail").FirstOrDefault();
                if (provider != null)
                {
                    // get RM methods
                    var methods_RM = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id)
                        .ToList();

                    if (methods_RM.Any())
                    {
                        lst.AddRange(jCtrl.Shipping.Rates.GetShippingOptions_RoyalMail(branch, customer, recipient, deliveryCountry, provider, methods_RM, packages, totalWeight, reference));
                    }
                }
            }

            #endregion

            return lst;
        }



        #region "UPS"

        private static async Task<List<ShippingQuote>> GetShippingOptions_UPS(Branch branch, Customer customer, Contact recipient, ShippingProvider provider, List<ShippingMethod> methods, List<ShippingQuotePackage> packages, decimal totalWeightKgs, string reference)
        {
            var lst = new List<ShippingQuote>();

            // get access details
            var key = "";
            var account = "";

            switch (branch.BranchCode)
            {
                case "BAU":
                    key = Properties.Settings.Default.UPS_AccessKey_BAU;
                    account = Properties.Settings.Default.UPS_AccountNo_BAU;
                    break;
                case "SNG":
                    key = Properties.Settings.Default.UPS_AccessKey_SNG;
                    account = Properties.Settings.Default.UPS_AccountNo_SNG;
                    break;
                case "SND":
                    key = Properties.Settings.Default.UPS_AccessKey_SND;
                    account = Properties.Settings.Default.UPS_AccountNo_SND;
                    break;
                case "SNH":
                    key = Properties.Settings.Default.UPS_AccessKey_SNH;
                    account = Properties.Settings.Default.UPS_AccountNo_SNH;
                    break;
                default:
                    throw new NotImplementedException();
            }


            // create sender
            var sender = new Contact()
            {
                Name = branch.Name,
                AddressLine1 = branch.AddressLine1,
                AddressLine2 = branch.AddressLine2,
                TownCity = branch.TownCity,
                CountyState = branch.CountyState,
                PostalCode = branch.PostalCode,
                Country = branch.Country,
                Country_Code = branch.Country_Code,
                IsVerifiedAddress = true,
                PhoneNumber = branch.PhoneNumber
            };

            // rely on UPS to determine if address is residential or not
            var isResidential = false;
            int packageCount = 1;

            var options = new List<UpsShippingOption>();

            if (packages.Any())
            {
                // get options by package details

                packageCount = packages.Count;

                // get options
                options = await UpsRates.GetShippingOptions(key, account, sender, recipient, isResidential, packages, reference);
            }
            else
            {
                // items couldn't be packed, get options by weight alone

                // get options 
                options = await UpsRates.GetShippingOptions(key, account, sender, recipient, isResidential, totalWeightKgs, reference);

            }


            foreach (var opt in options)
            {

                // get corresponding shipping method
                var mtd = methods
                    .Where(m => m.ShippingProvider_Id == provider.Id
                        && m.ProviderReference == opt.ServiceCode
                    )
                    .FirstOrDefault();

                if (mtd != null)
                {
                    // only add options where the method can be found

                    lst.Add(new ShippingQuote()
                    {
                        Branch = branch,
                        Branch_Id = branch.Id,
                        Customer = customer,
                        Customer_Id = customer.Id,
                        RecipientName = recipient.Name,
                        RecipientAddressLine1 = recipient.AddressLine1,
                        RecipientAddressLine2 = recipient.AddressLine2,
                        RecipientTownCity = recipient.TownCity,
                        RecipientCountyState = recipient.CountyState,
                        RecipientPostalCode = recipient.PostalCode,
                        RecipientCountryName = recipient.CountryName,
                        RecipientCountryCode = recipient.Country_Code,
                        RecipientPhoneNumber = recipient.PhoneNumber,
                        Packages = packages,
                        PackagesCount = (short)packageCount,
                        EstimatedWeightKgs = totalWeightKgs,
                        ServiceProvider = provider,
                        ServiceProvider_Id = provider.Id,
                        ServiceReference = opt.ServiceCode,
                        ServiceDescription = opt.ServiceName, // FormatUpsDescription(branch.BranchCode, opt.ServiceCode, opt.ServiceName),                        
                        ShippingMethod = mtd,
                        ShippingMethod_Id = mtd.Id,
                        Price = CalcShippingCharge_UPS(branch, recipient.Country_Code, opt.PublishedRate, opt.DiscountedRate),
                        CostPrice = opt.DiscountedRate,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    });



                }

            }

            return lst;
        }

        private static decimal CalcShippingCharge_UPS(Branch Branch, string RecipientCountryCode, decimal PublishedRate, decimal CostPrice)
        {
            // default to published rate
            var result = PublishedRate;

            if (CostPrice > 0)
            {
                var percentageExtra = 0m;
                var minExtraValue = 0m;
                var fixedAmountLimit = 0m;

                var isDomestic = false;
                if (RecipientCountryCode == Branch.Country_Code)
                {
                    isDomestic = true;
                }

                switch (Branch.BranchCode)
                {
                    case "BAU":

                        if (isDomestic)
                        {
                            percentageExtra = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_BAU / 100;
                            minExtraValue = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_BAU;
                            fixedAmountLimit = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_BAU;
                        }
                        else
                        {
                            percentageExtra = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_BAU / 100;
                            minExtraValue = Properties.Settings.Default.UPS_International_Margin_Fixed_BAU;
                            fixedAmountLimit = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_BAU;
                        }

                        break;

                    case "SND":

                        if (isDomestic)
                        {
                            percentageExtra = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SND / 100;
                            minExtraValue = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_SND;
                            fixedAmountLimit = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SND;
                        }
                        else
                        {
                            percentageExtra = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SND / 100;
                            minExtraValue = Properties.Settings.Default.UPS_International_Margin_Fixed_SND;
                            fixedAmountLimit = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SND;
                        }

                        break;

                    case "SNF":

                        throw new NotImplementedException();

                    case "SNG":

                        if (isDomestic)
                        {
                            percentageExtra = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SNG / 100;
                            minExtraValue = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_SNG;
                            fixedAmountLimit = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SNG;
                        }
                        else
                        {
                            percentageExtra = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SNG / 100;
                            minExtraValue = Properties.Settings.Default.UPS_International_Margin_Fixed_SNG;
                            fixedAmountLimit = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SNG;
                        }

                        break;

                    case "SNH":

                        if (isDomestic)
                        {
                            percentageExtra = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SNH / 100;
                            minExtraValue = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_SNH;
                            fixedAmountLimit = Properties.Settings.Default.UPS_Domestic_Margin_Fixed_Limit_SNH;
                        }
                        else
                        {
                            percentageExtra = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SNH / 100;
                            minExtraValue = Properties.Settings.Default.UPS_International_Margin_Fixed_SNH;
                            fixedAmountLimit = Properties.Settings.Default.UPS_International_Margin_Fixed_Limit_SNH;
                        }

                        break;
                }

                if (percentageExtra > 0)
                {

                    // calc percentage extra
                    var amount = CostPrice * percentageExtra;

                    // apply minimum margin amount
                    if (CostPrice <= fixedAmountLimit && amount < minExtraValue) amount = minExtraValue;

                    // set new price
                    result = CostPrice + amount;

                }

            }


            return result;
        }

        #endregion

        #region "USPS"

        private static async Task<List<ShippingQuote>> GetShippingOptions_USPS(Branch branch, Customer customer, Contact recipient, ShippingProvider provider, List<ShippingMethod> methods, List<ShippingQuotePackage> packages, decimal totalWeightKgs, string reference)
        {
            // limit access to US Branch
            if (branch.BranchCode != "BAU") { throw new NotImplementedException(); }

            var lst = new List<ShippingQuote>();

            // get access details
            var userId = Properties.Settings.Default.USPS_UserId;
            var userPwd = Properties.Settings.Default.USPS_UserPwd;

            // create sender
            var senderAddress = new USPS.Address
            {
                Name = branch.Name,
                City = branch.TownCity,
                State = branch.CountyState,
                ZipCode = branch.PostalCode,
                CountryCode = branch.Country_Code,
                CountryName = branch.CountryName
            };

            if (!string.IsNullOrWhiteSpace(branch.AddressLine1) && !string.IsNullOrWhiteSpace(branch.AddressLine2))
            {
                senderAddress.AppartmentBuilding = branch.AddressLine1;
                senderAddress.StreetAddress = branch.AddressLine2;
            }
            else if (!string.IsNullOrWhiteSpace(branch.AddressLine1))
            {
                senderAddress.StreetAddress = branch.AddressLine1;
            }

            // create recipient
            var recipientAddress = new USPS.Address
            {
                Name = recipient.Name,
                City = recipient.TownCity,
                State = recipient.CountyState,
                ZipCode = recipient.PostalCode,
                CountryCode = recipient.Country_Code,
                CountryName = recipient.CountryName
            };

            if (!string.IsNullOrWhiteSpace(recipient.AddressLine1) && !string.IsNullOrWhiteSpace(recipient.AddressLine2))
            {
                recipientAddress.AppartmentBuilding = recipient.AddressLine1;
                recipientAddress.StreetAddress = recipient.AddressLine2;
            }
            else if (!string.IsNullOrWhiteSpace(recipient.AddressLine1))
            {
                recipientAddress.StreetAddress = recipient.AddressLine1;
            }




            int packageCount = 1;
            USPS.RatesResponse rates = null;

            if (packages.Any())
            {
                // get options by package details

                packageCount = packages.Count;

                var pkgs = new List<USPS.Package>();

                foreach (var pkg in packages)
                {

                    decimal value = 0;

                    foreach (var item in pkg.Manifest)
                    {
                        // add value to total
                        //value += item.UnitPrice * item.Quantity;
                    }

                    pkgs.Add(new USPS.Package(
                        jCtrl.Services.Core.Utils.Calculator.ConvertLength_Cms_to_Ins(pkg.WidthCms),
                        jCtrl.Services.Core.Utils.Calculator.ConvertLength_Cms_to_Ins(pkg.HeightCms),
                        jCtrl.Services.Core.Utils.Calculator.ConvertLength_Cms_to_Ins(pkg.DepthCms),
                        jCtrl.Services.Core.Utils.Calculator.ConvertWeight_Kgs_to_Lbs(pkg.WeightKgs),
                        value,
                        PackageType.Own_Packaging
                        ));
                }

                // get options
                rates = await USPS.ShippingApi.GetRates(userId, userPwd, senderAddress, recipientAddress, pkgs);
            }
            else
            {
                // items couldn't be packed, get options by weight alone

                throw new NotImplementedException("USPS Rates by Weight without Package is not supported.");

                // get options 
                //rates = await USPS.ShippingApi.GetRates(userId, userPwd, senderAddress, recipient, totalWeightKgs);

            }


            if (rates != null)
            {
                foreach (var opt in rates.ServiceOptions)
                {
                    // calculate shipping amount to charge customer
                    var custPrice = opt.DiscountedRate * 1.2m;

                    Debug.WriteLine("Ref: " + opt.ProviderRef);
                    Debug.WriteLine("Name: " + opt.Name);
                    if (!string.IsNullOrEmpty(opt.DeliveryCommitment))
                    {
                        Debug.WriteLine("Del: " + opt.DeliveryCommitment);
                    }
                    Debug.WriteLine("Cust: " + custPrice);
                    Debug.WriteLine("RRP: " + opt.PublishedRate);
                    Debug.WriteLine("Cost: " + opt.DiscountedRate);
                    Debug.WriteLine("");


                    // get corresponding shipping method
                    var mtd = methods
                        .Where(m => m.ShippingProvider_Id == provider.Id
                            && m.ProviderReference == opt.ProviderRef
                        )
                        .FirstOrDefault();

                    if (mtd != null)
                    {
                        // only add options where the method can be found

                        lst.Add(new ShippingQuote()
                        {
                            Branch = branch,
                            Branch_Id = branch.Id,
                            Customer = customer,
                            Customer_Id = customer.Id,
                            RecipientName = recipient.Name,
                            RecipientAddressLine1 = recipient.AddressLine1,
                            RecipientAddressLine2 = recipient.AddressLine2,
                            RecipientTownCity = recipient.TownCity,
                            RecipientCountyState = recipient.CountyState,
                            RecipientPostalCode = recipient.PostalCode,
                            RecipientCountryName = recipient.CountryName,
                            RecipientCountryCode = recipient.Country_Code,
                            RecipientPhoneNumber = recipient.PhoneNumber,
                            Packages = packages,
                            PackagesCount = (short)packageCount,
                            EstimatedWeightKgs = totalWeightKgs,
                            ServiceProvider = provider,
                            ServiceProvider_Id = provider.Id,
                            ServiceReference = opt.ProviderRef,
                            ServiceDescription = opt.Name + (!string.IsNullOrEmpty(opt.DeliveryCommitment) ? " - " + opt.DeliveryCommitment : ""),
                            ShippingMethod = mtd,
                            ShippingMethod_Id = mtd.Id,
                            Price = custPrice,
                            CostPrice = opt.DiscountedRate,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        });



                    }

                }

            }


            return lst;
        }

        #endregion

        #region "Royal Mail"

        private static List<ShippingQuote> GetShippingOptions_RoyalMail(Branch branch, Customer customer, Contact recipient, Country deliveryCountry, ShippingProvider provider, List<ShippingMethod> options, List<ShippingQuotePackage> packages, decimal totalWeightKgs, string reference)
        {
            // limit access to UK Branch
            if (branch.BranchCode != "SNG") { throw new NotImplementedException(); }


            var lst = new List<ShippingQuote>();

            // filter options based on destination
            var methods = new List<ShippingMethod>();
            if (branch.Country_Code == deliveryCountry.Code)
            {
                // get domestic options
                methods = options
                    .Where(m => m.ShippingCoverageLevel_Id == "D" || m.ShippingCoverageLevel_Id == "A")
                    .ToList();
            }
            else
            {
                // get international options

                if (deliveryCountry != null)
                {
                    if (deliveryCountry.IsEuropean == true)
                    {
                        // get european options
                        methods = options
                            .Where(m => m.ShippingCoverageLevel_Id == "E" || m.ShippingCoverageLevel_Id == "A")
                            .ToList();
                    }
                    else
                    {
                        // get Rest of World options
                        methods = options
                            .Where(m => m.ShippingCoverageLevel_Id == "W" || m.ShippingCoverageLevel_Id == "A")
                            .ToList();
                    }
                }
            }


            if (methods.Any())
            {


                // create sender
                var sender = new Contact()
                {
                    Name = branch.Name,
                    AddressLine1 = branch.AddressLine1,
                    AddressLine2 = branch.AddressLine2,
                    TownCity = branch.TownCity,
                    CountyState = branch.CountyState,
                    PostalCode = branch.PostalCode,
                    Country = branch.Country,
                    Country_Code = branch.Country_Code,
                    IsVerifiedAddress = true,
                    PhoneNumber = branch.PhoneNumber
                };


                // get suitable options by packages or weight
                if (packages.Any())
                {
                    // get options by package details

                    // NOTE: Each package must be able to go via the same service for it to be available as an option

                    var packageCount = packages.Count;

                    // get distinct service codes
                    var services = methods.Select(m => m.ProviderReference).Distinct();
                    foreach (var service in services)
                    {
                        var suitablePackageCount = 0;
                        var totalPrice = 0m;
                        var totalCostPrice = 0m;

                        // check each package is suitable
                        foreach (var pkg in packages)
                        {
                            // get largest dimension
                            var maxDim = pkg.WidthCms;
                            if (pkg.HeightCms > maxDim) { maxDim = pkg.HeightCms; }
                            if (pkg.DepthCms > maxDim) { maxDim = pkg.DepthCms; }

                            var mtd = methods
                                .Where(m => m.ProviderReference == service
                                    && (m.MaxWeightKgs == 0 || m.MaxWeightKgs >= pkg.WeightKgs)
                                    && (m.MaxVolumeCm3 == 0 || m.MaxVolumeCm3 >= pkg.Volume_Cm3())
                                    && (m.MaxDimensionCms == 0 || m.MaxDimensionCms >= maxDim)
                                )
                                .OrderBy(m => m.SortOrder)
                                .FirstOrDefault();

                            if (mtd != null)
                            {
                                // service is suitable for current package
                                suitablePackageCount += 1;
                                totalCostPrice += mtd.CostPrice;
                                totalPrice += mtd.Price;
                            }
                        }

                        if (suitablePackageCount == packageCount)
                        {
                            // service is ok for all packages

                            // get reference to method with largest weight and dimension limits
                            var mtd = methods
                                .Where(m => m.ProviderReference == service)
                                .OrderBy(m => m.SortOrder)
                                .LastOrDefault();

                            if (mtd != null)
                            {
                                lst.Add(new ShippingQuote()
                                {
                                    Branch = branch,
                                    Branch_Id = branch.Id,
                                    Customer = customer,
                                    Customer_Id = customer.Id,
                                    RecipientName = recipient.Name,
                                    RecipientAddressLine1 = recipient.AddressLine1,
                                    RecipientAddressLine2 = recipient.AddressLine2,
                                    RecipientTownCity = recipient.TownCity,
                                    RecipientCountyState = recipient.CountyState,
                                    RecipientPostalCode = recipient.PostalCode,
                                    RecipientCountryName = recipient.CountryName,
                                    RecipientCountryCode = recipient.Country_Code,
                                    RecipientPhoneNumber = recipient.PhoneNumber,
                                    Packages = packages,
                                    PackagesCount = (short)suitablePackageCount,
                                    EstimatedWeightKgs = totalWeightKgs,
                                    ServiceProvider = provider,
                                    ServiceProvider_Id = provider.Id,
                                    ServiceReference = mtd.ProviderReference,
                                    ServiceDescription = mtd.Title,
                                    ShippingMethod = mtd,
                                    ShippingMethod_Id = mtd.Id,
                                    Price = totalPrice,
                                    CostPrice = totalCostPrice,
                                    RowVersion = 1,
                                    CreatedTimestampUtc = DateTime.UtcNow,
                                    UpdatedTimestampUtc = DateTime.UtcNow
                                });
                            }
                        }
                    }

                }
                else
                {
                    // get options by weight alone

                    foreach (var mtd in methods.Where(m => m.MaxWeightKgs == 0 || m.MaxWeightKgs >= totalWeightKgs))
                    {
                        lst.Add(new ShippingQuote()
                        {
                            Branch = branch,
                            Branch_Id = branch.Id,
                            Customer = customer,
                            Customer_Id = customer.Id,
                            RecipientName = recipient.Name,
                            RecipientAddressLine1 = recipient.AddressLine1,
                            RecipientAddressLine2 = recipient.AddressLine2,
                            RecipientTownCity = recipient.TownCity,
                            RecipientCountyState = recipient.CountyState,
                            RecipientPostalCode = recipient.PostalCode,
                            RecipientCountryName = recipient.CountryName,
                            RecipientCountryCode = recipient.Country_Code,
                            RecipientPhoneNumber = recipient.PhoneNumber,
                            Packages = packages,
                            PackagesCount = 1,
                            EstimatedWeightKgs = totalWeightKgs,
                            ServiceProvider = provider,
                            ServiceProvider_Id = provider.Id,
                            ServiceReference = mtd.ProviderReference,
                            ServiceDescription = mtd.Title,
                            ShippingMethod = mtd,
                            ShippingMethod_Id = mtd.Id,
                            Price = mtd.Price,
                            CostPrice = mtd.CostPrice,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        });
                    }



                }
            }

            return lst;
        }

        #endregion

        #region "Chronoship"

        private static async Task<List<ShippingQuote>> GetShippingOptions_Chronoship(Branch branch, Customer customer, Contact recipient, ShippingProvider provider, List<ShippingMethod> methods, decimal totalWeightKgs, string reference)
        {

            // limit access to FR Branch
            if (branch.BranchCode != "SNF") { throw new NotImplementedException(); }


            var lst = new List<ShippingQuote>();

            // get access details
            var userId = Properties.Settings.Default.Chronoship_UserId;
            var userPwd = Properties.Settings.Default.Chronoship_UserPwd;

            // create sender
            var sender = new Contact()
            {
                Name = branch.Name,
                AddressLine1 = branch.AddressLine1,
                AddressLine2 = branch.AddressLine2,
                TownCity = branch.TownCity,
                CountyState = branch.CountyState,
                PostalCode = branch.PostalCode,
                Country = branch.Country,
                Country_Code = branch.Country_Code,
                IsVerifiedAddress = true,
                PhoneNumber = branch.PhoneNumber
            };

            short packageCount = 1;

            // get options 
            var options = await ChronoshipRates.GetShippingOptions(userId, userPwd, sender, recipient, ChronpostShipmentType.Merchandise, totalWeightKgs);


            foreach (var opt in options)
            {
                // get corresponding shipping method
                var mtd = methods
                    .Where(m => m.ShippingProvider_Id == provider.Id
                        && m.ProviderReference == opt.ServiceCode
                    )
                    .FirstOrDefault();

                if (mtd != null)
                {
                    // only add options where the method can be found

                    lst.Add(
                    new ShippingQuote()
                    {
                        Branch = branch,
                        Customer = customer,
                        RecipientName = recipient.Name,
                        RecipientAddressLine1 = recipient.AddressLine1,
                        RecipientAddressLine2 = recipient.AddressLine2,
                        RecipientTownCity = recipient.TownCity,
                        RecipientCountyState = recipient.CountyState,
                        RecipientPostalCode = recipient.PostalCode,
                        RecipientCountryName = recipient.CountryName,
                        RecipientCountryCode = recipient.Country_Code,
                        RecipientPhoneNumber = recipient.PhoneNumber,
                        //Packages = packages,
                        PackagesCount = packageCount,
                        EstimatedWeightKgs = totalWeightKgs,
                        ServiceProvider = provider,
                        ServiceProvider_Id = provider.Id,
                        ServiceReference = opt.ServiceCode,
                        ServiceDescription = opt.ServiceName,
                        Price = opt.Price,
                        CostPrice = opt.Price,
                        RowVersion = 1,
                        CreatedTimestampUtc = DateTime.UtcNow,
                        UpdatedTimestampUtc = DateTime.UtcNow
                    }
                );
                }
            }

            return lst;
        }

        #endregion


    }
}
