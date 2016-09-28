using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace jCtrl.Shipping.USPS
{
    internal class Rates
    {

        private const string PRODUCTION_URL = "https://secure.shippingapis.com/ShippingAPI.dll";
        private const string USPS_TRACKING_SERVICE_ID = "106";

        private readonly string _userId;
        private readonly string _userPwd;


        public Rates(string userId, string password)
        {
            _userId = userId;
            _userPwd = password;
        }

        public async Task<RatesResponse> GetRates(Address shipFrom, Address shipTo, Package package, DateTime? shipDate = null)
        {
            var packages = new List<Package> { package };

            if (!shipFrom.IsUnitedStatesAddress() || !shipTo.IsUnitedStatesAddress())
            {
                // international
                return await GetInternationalServices(shipFrom, shipTo, packages, shipDate);
            }

            return await GetDomesticServices(shipFrom, shipTo, packages, shipDate);
        }

        public async Task<RatesResponse> GetRates(Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate = null)
        {
            if (!shipFrom.IsUnitedStatesAddress() || !shipTo.IsUnitedStatesAddress())
            {
                // international
                return await GetInternationalServices(shipFrom, shipTo, packages, shipDate);
            }

            return await GetDomesticServices(shipFrom, shipTo, packages, shipDate);
        }

        public async Task<RatesResponse> GetRates(string service, Address shipFrom, Address shipTo, Package package, DateTime? shipDate = null)
        {
            var packages = new List<Package> { package };

            if (!shipFrom.IsUnitedStatesAddress() || !shipTo.IsUnitedStatesAddress())
            {
                // international
                return await GetInternationalServices(service, shipFrom, shipTo, packages, shipDate);
            }

            return await GetDomesticServices(service, shipFrom, shipTo, packages, shipDate);
        }

        public async Task<RatesResponse> GetRates(string service, Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate = null)
        {
            if (!shipFrom.IsUnitedStatesAddress() || !shipTo.IsUnitedStatesAddress())
            {
                // international
                return await GetInternationalServices(service, shipFrom, shipTo, packages, shipDate);
            }

            return await GetDomesticServices(service, shipFrom, shipTo, packages, shipDate);
        }


        #region "Domestic"

        private async Task<RatesResponse> GetDomesticServices(string service, Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate)
        {
            var response = await GetDomesticServices(shipFrom, shipTo, packages, shipDate);

            if (!response.Errors.Any())
            {
                // no errors

                // search results for the wanted service
                response.ServiceOptions = response.ServiceOptions.Where(o => o.ProviderRef == service).ToList();
            }

            return response;
        }

        private async Task<RatesResponse> GetDomesticServices(Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate)
        {

            var result = new RatesResponse { ServiceOptions = new List<ShippingOption>(), Errors = new List<UspsError>() };

            var services = new string[] {                
                // "RETAIL GROUND", // Seth says there is no usps ground                
                "PLUS"
            };

            foreach (var service in services)
            {

                var sb = new StringBuilder();

                var settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.OmitXmlDeclaration = true;
                settings.NewLineHandling = NewLineHandling.None;

                using (var writer = XmlWriter.Create(sb, settings))
                {
                    writer.WriteStartElement("RateV4Request");
                    writer.WriteAttributeString("USERID", _userId);

                    // get all additional services
                    writer.WriteElementString("Revision", "2");

                    var i = 0;
                    foreach (var package in packages)
                    {

                        var container = "PACKAGE"; //package.Container;

                        // Container must be VARIABLE when service is  PLUS
                        if (service == "PLUS" || service == "RETAIL GROUND") { container = "VARIABLE"; }


                        var size = "REGULAR";

                        if (package.IsLargePackage)
                        {
                            size = "LARGE";
                            // Container must be RECTANGULAR or NONRECTANGULAR when SIZE is LARGE
                            container = "RECTANGULAR";
                            if (!package.IsRectangular) { container = "NONRECTANGULAR"; }
                        }

                        writer.WriteStartElement("Package");
                        writer.WriteAttributeString("ID", i.ToString());

                        writer.WriteElementString("Service", service);
                        //writer.WriteElementString("FirstClassMailType", "PARCEL");

                        writer.WriteElementString("ZipOrigination", shipFrom.ZipCode);
                        writer.WriteElementString("ZipDestination", shipTo.ZipCode);
                        writer.WriteElementString("Pounds", package.PoundsAndOunces.Pounds.ToString());
                        writer.WriteElementString("Ounces", package.PoundsAndOunces.Ounces.ToString());

                        writer.WriteElementString("Container", container);
                        writer.WriteElementString("Size", size);
                        writer.WriteElementString("Width", package.RoundedWidth.ToString());
                        writer.WriteElementString("Length", package.RoundedLength.ToString());
                        writer.WriteElementString("Height", package.RoundedHeight.ToString());
                        writer.WriteElementString("Girth", package.CalculatedGirth.ToString());

                        // add tracking
                        writer.WriteStartElement("SpecialServices");
                        writer.WriteElementString("SpecialService", USPS_TRACKING_SERVICE_ID);
                        writer.WriteEndElement();


                        writer.WriteElementString("Machinable", package.IsMachinablePackage.ToString());

                        if (shipDate != null) { writer.WriteElementString("ShipDate", ((DateTime)shipDate).ToString("yyyy-MM-dd")); }

                        writer.WriteEndElement();

                        i++;
                    }

                    writer.WriteEndElement();
                    writer.Flush();
                }

                var response = "";

                try
                {
                    var url = string.Concat(PRODUCTION_URL, "?API=RateV4&XML=", sb.ToString());
                    var webClient = new WebClient();

                    response = webClient.DownloadString(url);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                var rates = ParseDomesticResult(response);

                if (rates.Errors.Any()) { result.Errors.AddRange(rates.Errors); }
                if (rates.ServiceOptions.Any()) { result.ServiceOptions.AddRange(rates.ServiceOptions); }

            }


            return result;
        }

        private RatesResponse ParseDomesticResult(string response)
        {

            var result = new RatesResponse { ServiceOptions = new List<ShippingOption>(), Errors = new List<UspsError>() };

            var document = XElement.Parse(response, LoadOptions.None);

            //check for errors
            if (document.DescendantsAndSelf("Error").Any())
            {
                var errors = from item in document.DescendantsAndSelf("Error")
                             select
                                 new UspsError
                                 {
                                     Description = item.Element("Description").ToString(),
                                     Source = item.Element("Source").ToString(),
                                     HelpContext = item.Element("HelpContext").ToString(),
                                     HelpFile = item.Element("HelpFile").ToString(),
                                     Number = item.Element("Number").ToString()
                                 };

                result.Errors = errors.ToList();
            }

            var rates = from item in document.Descendants("Postage")
                        group item by (string)item.Element("MailService")
                into g
                        select new
                        {
                            Id = g.Select(x => x.Attribute("CLASSID")).FirstOrDefault().Value,
                            Name = g.Key,
                            Zones = g.Select(x => x.Element("Zone")).FirstOrDefault() != null ? g.Select(x => x.Element("Zone")).FirstOrDefault().Value : "",
                            PublishedRate = g.Sum(x => Decimal.Parse((string)x.Element("Rate"))),
                            // default to retail rate if no discounted rate
                            DiscountedRate = g.Sum(x => x.Element("CommercialPlusRate") != null ? Decimal.Parse((string)x.Element("CommercialPlusRate")) : Decimal.Parse((string)x.Element("Rate")))//,
                            //TrackingCost = g.Sum(x => x.Descendants("SpecialService").Where(s => s.Element("ServiceID").Value == USPS_TRACKING_SERVICE_ID).FirstOrDefault() != null ? Decimal.Parse((string)x.Descendants("SpecialService").Where(s => s.Element("ServiceID").Value == USPS_TRACKING_SERVICE_ID).FirstOrDefault().Element("Price")) : 0                            
                        };

            foreach (var r in rates)
            {
                // exclude options without cost price 
                // because shipping charge is calculated on a cost plus basis
                if (r.DiscountedRate > 0)
                {
                    var s = r.Name.ToUpper();

                    // exclude inappropriate services
                    // excluding Hold For Pickup as ShipStation does not appear to support it
                    if (!s.Contains("LIBRARY MAIL PARCEL") && !s.Contains("MEDIA MAIL PARCEL") && !s.Contains("HOLD FOR PICKUP"))
                    {

                        // remove trademark symbol
                        var name = Regex.Replace(r.Name, "&lt.*&gt;", "");

                        // remove USPS prefix
                        if (name.StartsWith("USPS ", StringComparison.CurrentCulture)) { name = name.Substring(5); }

                        var opt = new ShippingOption
                        {
                            ProviderRef = r.Id,
                            Name = name,
                            PublishedRate = r.PublishedRate,
                            DiscountedRate = r.DiscountedRate//,
                            //isDomestic = true
                        };

                        // add number of zones if supplied
                        if (!string.IsNullOrEmpty(r.Zones)) { opt.Zones = int.Parse(r.Zones); }

                        result.ServiceOptions.Add(opt);
                    }
                }
            }



            return result;
        }

        #endregion


        #region "International"

        private async Task<RatesResponse> GetInternationalServices(string service, Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate)
        {
            var response = await GetInternationalServices(shipFrom, shipTo, packages, shipDate);

            if (!response.Errors.Any())
            {
                // no errors

                // search results for the wanted service
                response.ServiceOptions = response.ServiceOptions.Where(o => o.ProviderRef == service).ToList();
            }

            return response;
        }

        private async Task<RatesResponse> GetInternationalServices(Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate)
        {
            var sb = new StringBuilder();

            var settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            settings.NewLineHandling = NewLineHandling.None;


            using (var writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartElement("IntlRateV2Request");
                writer.WriteAttributeString("USERID", _userId);


                writer.WriteElementString("Revision", "2");

                var i = 0;
                foreach (var package in packages)
                {

                    writer.WriteStartElement("Package");
                    writer.WriteAttributeString("ID", i.ToString());

                    writer.WriteElementString("Pounds", package.RoundedWeight.ToString());
                    writer.WriteElementString("Ounces", "0");
                    writer.WriteElementString("Machinable", package.IsMachinablePackage ? "true" : "false");
                    writer.WriteElementString("MailType", "PACKAGE");
                    writer.WriteElementString("ValueOfContents", package.InsuredValue > 0 ? package.InsuredValue.ToString() : "100"); //todo: figure out best way to come up with insured value
                    writer.WriteElementString("Country", shipTo.GetCountryName());

                    writer.WriteElementString("Container", package.IsRectangular ? "RECTANGULAR" : "NONRECTANGULAR");
                    //TODO: DIM Weights
                    writer.WriteElementString("Size", package.IsOversize ? "LARGE" : "REGULAR");
                    writer.WriteElementString("Width", package.RoundedWidth.ToString());
                    writer.WriteElementString("Length", package.RoundedLength.ToString());
                    writer.WriteElementString("Height", package.RoundedHeight.ToString());
                    writer.WriteElementString("Girth", package.CalculatedGirth.ToString());

                    writer.WriteElementString("OriginZip", shipFrom.ZipCode);
                    writer.WriteElementString("CommercialPlusFlag", "Y"); // get discounted cost rates

                    if (shipDate != null)
                    {
                        writer.WriteElementString("AcceptanceDateTime", ((DateTime)shipDate).AddHours(18).ToString("s"));
                        writer.WriteElementString("DestinationPostalCode", shipTo.ZipCode);
                    }

                    writer.WriteEndElement();

                    i++;
                }
                writer.WriteEndElement();
                writer.Flush();
            }

            var response = "";

            try
            {
                var url = string.Concat(PRODUCTION_URL, "?API=IntlRateV2&XML=", sb.ToString());
                var webClient = new WebClient();
                response = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return ParseInternationalResult(response);
        }

        private RatesResponse ParseInternationalResult(string response)
        {
            var result = new RatesResponse { ServiceOptions = new List<ShippingOption>(), Errors = new List<UspsError>() };

            var document = XElement.Parse(response, LoadOptions.None);

            //check for errors
            if (document.DescendantsAndSelf("Error").Any())
            {
                var errors = from item in document.DescendantsAndSelf("Error")
                             select
                                 new UspsError
                                 {
                                     Description = item.Element("Description").ToString(),
                                     Source = item.Element("Source").ToString(),
                                     HelpContext = item.Element("HelpContext").ToString(),
                                     HelpFile = item.Element("HelpFile").ToString(),
                                     Number = item.Element("Number").ToString()
                                 };

                result.Errors = errors.ToList();
            }

            var rates = document.Descendants("Service")
                .GroupBy(item => (string)item.Element("SvcDescription"))
                .Select(g => new
                {
                    Id = g.Select(x => x.FirstAttribute).FirstOrDefault().Value,
                    Name = g.Key,
                    DeliveryCommitment = g.Select(x => x.Element("SvcCommitments")).FirstOrDefault().Value,
                    PublishedRate = g.Sum(x => Decimal.Parse((string)x.Element("Postage"))),
                    // default to retail rate if no discounted rate
                    DiscountedRate = g.Sum(x => x.Element("CommercialPlusPostage") != null ? Decimal.Parse((string)x.Element("CommercialPlusPostage")) : Decimal.Parse((string)x.Element("Postage")))
                });


            foreach (var r in rates)
            {
                // remove trademark symbol
                var name = Regex.Replace(r.Name, "&lt.*&gt;", "");

                // remove USPS prefix
                if (name.StartsWith("USPS ", StringComparison.CurrentCulture)) { name = name.Substring(5); }

                var opt = new ShippingOption
                {
                    ProviderRef = r.Id,
                    Name = name,
                    DeliveryCommitment = r.DeliveryCommitment,
                    PublishedRate = r.PublishedRate,
                    DiscountedRate = r.DiscountedRate//,
                    //isDomestic = false
                };

                result.ServiceOptions.Add(opt);
            }


            return result;
        }

        #endregion
    }
}
