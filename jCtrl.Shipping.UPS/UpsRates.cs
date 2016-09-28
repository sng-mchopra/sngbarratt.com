using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Security;
using System.Threading.Tasks;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Shipping;

namespace jCtrl.Shipping.UPS
{
    public static class UpsRates
    {

        // get by total weight
        public static async Task<List<UpsShippingOption>> GetShippingOptions(string Key, string AccountNo, Contact Sender, Contact Recipient, bool IsResidentialAddress, decimal TotalWeightKGS, string RequestReference, bool UseLiveSystem = true)
        {

            // set minimum weight to 1Kg
            if (TotalWeightKGS < 1) { TotalWeightKGS = 1; }
           
            // create package details - weight only
            var packages = Properties.Resources.UpsPackage_WithoutDims;
            packages = packages.Replace("#weightkgs", Math.Round(TotalWeightKGS, 1).ToString());

            if (TotalWeightKGS <= 30)
            {
                // doesn't require additional handling
                packages = packages.Replace("<AdditionalHandling/>", string.Empty);
            }

            return await GetShippingOptions(Key, AccountNo, Sender, Recipient, IsResidentialAddress, packages, RequestReference, UseLiveSystem);
        }

        // get by package list
        public static async Task<List<UpsShippingOption>> GetShippingOptions(string Key, string AccountNo, Contact Sender, Contact Recipient, bool IsResidentialAddress, List<ShippingQuotePackage> Packages, string RequestReference, bool UseLiveSystem = true)
        {
           
            // package details
            string packages = string.Empty;

            foreach (var pkg in Packages)
            {
                
                var thisPackage = Properties.Resources.UpsPackage;
                thisPackage = thisPackage.Replace("#lengthCms", Math.Round(pkg.WidthCms, 2).ToString());
                thisPackage = thisPackage.Replace("#widthCms", Math.Round(pkg.HeightCms, 2).ToString());
                thisPackage = thisPackage.Replace("#heightCms", Math.Round(pkg.DepthCms, 2).ToString());

                // set minimum weight to 1Kg
                if (pkg.WeightKgs < 1) { pkg.WeightKgs = 1; }

                thisPackage = thisPackage.Replace("#weightkgs", Math.Round(pkg.WeightKgs, 1).ToString());

                if (pkg.WeightKgs <= 30)
                {
                    // doesn't require additional handling
                    thisPackage = thisPackage.Replace("<AdditionalHandling/>", string.Empty);
                }

                if ((pkg.WidthCms + (2 * (pkg.HeightCms + pkg.DepthCms))) <= 330)
                {
                    // not large package
                    thisPackage = thisPackage.Replace("<LargePackageIndicator/>", string.Empty);
                }

                // add to collection
                packages += thisPackage;
            }

            return await GetShippingOptions(Key, AccountNo, Sender, Recipient, IsResidentialAddress, packages, RequestReference, UseLiveSystem);
        }

        // send the request
        private static async Task<List<UpsShippingOption>> GetShippingOptions(string Key, string AccountNo, Contact Sender, Contact Recipient, bool IsResidentialAddress, string Packages_Xml, string RequestReference, bool UseLiveSystem)
        {

            var lst = new List<UpsShippingOption>();
            var s = "";

            // get request template
            string payload = Properties.Resources.UpsGetShippingRatesRequest_Packages;
    
            // update placeholder values            
            payload = payload.Replace("#userid", Properties.Settings.Default.UserId);
            payload = payload.Replace("#password", Properties.Settings.Default.UserPwd);
            payload = payload.Replace("#accesskey", Key);
            payload = payload.Replace("#accountnumber", AccountNo);
            payload = payload.Replace("#requestreference", SecurityElement.Escape(RequestReference));
            payload = payload.Replace("#requestoption", "Shop");
            payload = payload.Replace("#servicecode", string.Empty);


            // sender name
            s = "";
            if (Sender.Name != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Sender.Name.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#sendername", s);

            // sender address line 1
            s = "";
            if (Sender.AddressLine1 != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Sender.AddressLine1.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#senderaddressline1", s);

            // sender address line 1
            s = "";
            if (Sender.AddressLine2 != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Sender.AddressLine2.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#senderaddressline2", s);

            // sender town  /city
            s = "";
            if (Sender.TownCity != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Sender.TownCity.Trim()));
                if (s.Length > 30) { s = s.Substring(0, 30); } // trim if too long
            }
            payload = payload.Replace("#sendercity", s);

            // sender postal code
            s = "";
            if (Sender.PostalCode != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Sender.PostalCode.Trim().Replace("-", "").Replace(" ", ""))); // remove dashes and spaces
                if (s.Length > 9) { s = s.Substring(0, 9); } // trim if too long
            }
            payload = payload.Replace("#senderpostalcode", s);

            // sender country code
            payload = payload.Replace("#sendercountrycode", Sender.Country_Code);

            // sender contact number
            s = "";
            if (Sender.PhoneNumber != null)
            {
                s = SecurityElement.Escape(Sender.PhoneNumber.Trim());
                if (s.Length > 15) { s = s.Substring(0, 15); } // trim if too long
            }            
            payload = payload.Replace("#senderphone", s);



            // delivery name
            s = "";
            if (Recipient.Name != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Recipient.Name.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#deliveryname", s);

            // delivery address line 1
            s = "";
            if (Recipient.AddressLine1 != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Recipient.AddressLine1.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#deliveryaddressline1", s);

            // delivery address line 1
            s = "";
            if (Recipient.AddressLine2 != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Recipient.AddressLine2.Trim()));
                if (s.Length > 35) { s = s.Substring(0, 35); } // trim if too long
            }
            payload = payload.Replace("#deliveryaddressline2", s);

            // delivery town  /city
            s = "";
            if (Recipient.TownCity != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Recipient.TownCity.Trim()));
                if (s.Length > 30) { s = s.Substring(0, 30); } // trim if too long
            }
            payload = payload.Replace("#deliverycity", s);

            // delivery postal code
            s = "";
            if (Recipient.PostalCode != null)
            {
                s = SecurityElement.Escape(Services.Core.Utils.StringUtils.RemoveDiacritics(Recipient.PostalCode.Trim().Replace("-", "").Replace(" ", ""))); // remove dashes and spaces
                if (s.Length > 9) { s = s.Substring(0, 9); } // trim if too long
            }
            payload = payload.Replace("#deliverypostalcode", s);

            // delivery county / state
            s = "";
            if (Recipient.CountyState != null)
            {
                // TODO: translate county names to codes before hitting this service?
                //          or translate it here
                // UPS only seem to like state codes for US addresses
                if (Recipient.Country_Code == "US" & Recipient.CountyState.Trim().Length == 2)
                {
                    payload = payload.Replace("#deliverycountystate", "<StateProvinceCode>" + Recipient.CountyState.Trim() + "</StateProvinceCode>");
                }
            }
            payload = payload.Replace("#deliverycountystate", s);
            

            // delivery country code
            payload = payload.Replace("#deliverycountrycode", Recipient.Country_Code);

            // delivery residential indicator
            s = "";
            if (IsResidentialAddress)
            {
                s= "<ResidentialAddressIndicator />";
            }
            payload.Replace("#residentialaddressindicator", s);

            // delivery contact number
            s = "";
            if (Recipient.PhoneNumber != null) { 
                s = SecurityElement.Escape(Recipient.PhoneNumber.Trim());
                if (s.Length > 15) { s = s.Substring(0, 15); } // trim if too long
            }
            payload = payload.Replace("#deliveryphone", s);


            // package details
            payload = payload.Replace("#packages", Packages_Xml);


            byte[] payloadData = Encoding.ASCII.GetBytes(payload);

            bool IsDomestic = false;
            if (Recipient.Country_Code == Sender.Country_Code) { IsDomestic = true; }

            var url = Properties.Settings.Default.Rates_Url_Live;
            if (UseLiveSystem == false) { url = Properties.Settings.Default.Rates_Url_Test; }

            try
            {
                var req = WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = 10000; // 10 secs
                req.ContentType = "application/xml";
                req.ContentLength = payloadData.Length;

                Stream thisStream = req.GetRequestStream();
                thisStream.Write(payloadData, 0, payloadData.Length);
                thisStream.Close();

                WebResponse resp = req.GetResponse();
                thisStream = resp.GetResponseStream();

                StreamReader rdr = new StreamReader(thisStream, true);
                string xml = await rdr.ReadToEndAsync();

                lst = ParseUpsRateResponseXml(xml, IsDomestic);
            }
            catch (Exception ex)
            {
                // TODO: log the error
                throw ex;
            }


            return lst;
        }

        //TODO: Does USA have to be in LBS as below or could we just do it via Kgs ????

        // parse response
        private static List<UpsShippingOption> ParseUpsRateResponseXml(string xml, bool IsDomestic)
        {

            var lst = new List<UpsShippingOption>();

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode ndResponse = doc.SelectSingleNode("/RatingServiceSelectionResponse/Response");

            if ((ndResponse != null))
            {
                var statusCode = ndResponse.SelectSingleNode("ResponseStatusCode").InnerText;

                if (statusCode == "1")
                {
                    // success

                    string Code = string.Empty;
                    string CurrencyCode = string.Empty;
                    decimal PublishedRate = 0;
                    decimal DiscountedRate = 0;
                    int DeliveryByDays = 0;
                    string DeliveryByTime = string.Empty;

                    XmlNode nd = null;

                    XmlNodeList lstShippingOptions = doc.SelectNodes("/RatingServiceSelectionResponse/RatedShipment");

                    foreach (XmlNode ndShippingOption in lstShippingOptions)
                    {
                        nd = ndShippingOption.SelectSingleNode("Service/Code");
                        if ((nd != null))
                        {
                            Code = nd.InnerText;
                        }
                        else
                        {
                            Code = string.Empty;
                        }

                        nd = ndShippingOption.SelectSingleNode("TotalCharges/CurrencyCode");
                        if ((nd != null))
                        {
                            CurrencyCode = nd.InnerText;
                        }
                        else
                        {
                            CurrencyCode = string.Empty;
                        }

                        nd = ndShippingOption.SelectSingleNode("TotalCharges/MonetaryValue");
                        if ((nd != null))
                        {
                            decimal.TryParse(nd.InnerText, out PublishedRate);
                        }
                        else
                        {
                            PublishedRate = 0;
                        }

                        nd = ndShippingOption.SelectSingleNode("NegotiatedRates/NetSummaryCharges/GrandTotal/MonetaryValue");
                        if ((nd != null))
                        {
                            decimal.TryParse(nd.InnerText, out DiscountedRate);
                        }
                        else
                        {
                            DiscountedRate = 0;
                        }

                        nd = ndShippingOption.SelectSingleNode("GuaranteedDaysToDelivery");
                        if ((nd != null))
                        {
                            int.TryParse(nd.InnerText, out DeliveryByDays);
                        }
                        else
                        {
                            DeliveryByDays = 0;
                        }

                        nd = ndShippingOption.SelectSingleNode("ScheduledDeliveryTime");
                        if ((nd != null))
                        {
                            DeliveryByTime = nd.InnerText;
                        }
                        else
                        {
                            DeliveryByTime = string.Empty;
                        }

                        Debug.WriteLine(string.Format("Shipping Option - Code: {0} Days: {1} Before Time: {2} Published Rate: {3}{5} Discounted Rate: {4}{5}", Code, DeliveryByDays, DeliveryByTime, PublishedRate, DiscountedRate, CurrencyCode));

                        if (!string.IsNullOrEmpty(Code))
                        {

                            var Service = (UpsShippingService)Enum.Parse(typeof(UpsShippingService), Code);

                            if (!string.IsNullOrEmpty(CurrencyCode))
                            {
                                if (PublishedRate > 0 & DiscountedRate > 0)
                                {
                                    // add to list with negotiated rate                                    
                                    lst.Add(new UpsShippingOption(Service, DeliveryByDays, DeliveryByTime, PublishedRate, DiscountedRate, CurrencyCode, IsDomestic));
                                }
                                else if (PublishedRate > 0)
                                {
                                    // add to list without any cost price discount
                                    lst.Add(new UpsShippingOption(Service, DeliveryByDays, DeliveryByTime, PublishedRate, PublishedRate, CurrencyCode, IsDomestic));
                                }
                            }
                        }
                    }


                }
                else
                {
                    // failed


                    // TODO: log the error

                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        Debug.WriteLine("UPS Request Failed");

                        string ErrorSeverity = string.Empty;
                        string ErrorCode = string.Empty;
                        string ErrorDescription = string.Empty;
                        string ElementName = string.Empty;
                        string ElementReference = string.Empty;
                        string AttributeName = string.Empty;

                        XmlNodeList lstErrors = ndResponse.SelectNodes("Error");

                        foreach (XmlNode ndError in lstErrors)
                        {
                            ErrorSeverity = ndError.SelectSingleNode("ErrorSeverity").InnerText;
                            ErrorCode = ndError.SelectSingleNode("ErrorCode").InnerText;
                            ErrorDescription = ndError.SelectSingleNode("ErrorDescription").InnerText;

                            Debug.WriteLine(string.Format("Error Information - Code: {0} Level: {1} Description: {2}", ErrorCode, ErrorSeverity, ErrorDescription));

                            XmlNodeList lstErrorLocations = ndError.SelectNodes("ErrorLocation");

                            foreach (XmlNode ndErrorLocation in lstErrorLocations)
                            {
                                ElementName = ndErrorLocation.SelectSingleNode("ErrorLocationElementName").InnerText;
                                ElementReference = ndErrorLocation.SelectSingleNode("ErrorLocationElementReference").InnerText;
                                AttributeName = ndErrorLocation.SelectSingleNode("ErrorLocationAttributeName").InnerText;

                                Debug.WriteLine(string.Format("Error Details - Element Name: {0} Element Reference: {1} Attribute Name: {2}", ElementName, ElementReference, AttributeName));

                            }

                            Debug.WriteLine("--------------------");

                        }

                    }

                }
            }

            return lst;
        }
    }





    #region "Old Rates Calculator - Based on what is in use on website"

    //public class RatesCalculator
    //    {

    //        private bool _IsProduction;
    //        private string _UserID;
    //        private string _Password;
    //        private string _AccessKey;
    //        private string _UpsAccountNumber;
    //        private string _SenderName;
    //        private string _SenderPhoneNumber;
    //        private string _SenderAddressLine1;
    //        private string _SenderAddressLine2;
    //        private string _SenderTownCity;
    //        private string _SenderPostalCode;
    //        private string _SenderCountryCode;

    //        private string _BranchCode;


    //        #region "Constructors"


    //        public RatesCalculator(string BranchCode, string BranchName, string BranchAddressLine1, string BranchAddressLine2, string BranchTownCity, string BranchPostalCode, string BranchCountryCode, string BranchPhoneNumber)
    //        {
    //            _IsProduction = true;

    //            _UserID = My.Settings.userID;
    //            _Password = My.Settings.userPwd;

    //            _BranchCode = BranchCode.Trim.ToUpper;

    //            switch (_BranchCode)
    //            {
    //                case "SNG":
    //                    _AccessKey = My.Settings.accessKey_SNG;
    //                    _UpsAccountNumber = My.Settings.accountNumber_SNG;
    //                    break;
    //                case "SND":
    //                    // HACK: SNH and SND are the same branch and share the same stock, prices etc
    //                    _AccessKey = My.Settings.accessKey_SNH;
    //                    _UpsAccountNumber = My.Settings.accountNumber_SNH;
    //                    break;
    //                case "SNH":
    //                    _AccessKey = My.Settings.accessKey_SNH;
    //                    _UpsAccountNumber = My.Settings.accountNumber_SNH;
    //                    break;
    //                case "BAU":
    //                    _AccessKey = My.Settings.accessKey_BAU;
    //                    _UpsAccountNumber = My.Settings.accountNumber_BAU;
    //                    break;
    //            }

    //            _SenderName = BranchName;
    //            _SenderAddressLine1 = BranchAddressLine1;
    //            _SenderAddressLine2 = BranchAddressLine2;
    //            _SenderTownCity = BranchTownCity;
    //            _SenderPostalCode = BranchPostalCode;
    //            _SenderCountryCode = BranchCountryCode;
    //            _SenderPhoneNumber = BranchPhoneNumber;

    //        }

    //        #endregion

    //        #region "Methods"

    //        public void SetTestMode()
    //        {
    //            _IsProduction = false;
    //        }





    //        public List<UpsShippingOption> GetShippingOptions(decimal PackageWidthCms, decimal PackageHeightCms, decimal PackageDepthCms, decimal TotalWeightIncPackagingKGS, string DeliveryName, string DestinationAddressLine1, string DestinationAddressLine2, string DestinationTownCity, string DestinationCountyState, string DestinationPostalCode,
    //        string DestinationCountryCode, string DestinationPhoneNumber, bool IsResidentialAddress, string RequestReference)
    //        {
    //            DateTime startTime = Now;
    //            Debug.WriteLine("UpsShippingDLL.GetShippingOptions " + RequestReference + " " + TotalWeightIncPackagingKGS.ToString + "Kgs " + DeliveryName + " " + DestinationAddressLine1 + " " + DestinationPostalCode + " " + DestinationCountryCode + " started at " + startTime.ToLongTimeString);

    //            List<UpsShippingOption> lst = new List<UpsShippingOption>();

    //            string payload = My.Resources.UpsGetShippingRatesRequest_Packages;

    //            if (TotalWeightIncPackagingKGS < 1)
    //            {
    //                TotalWeightIncPackagingKGS = 1;
    //            }

    //            // update values
    //            payload = payload.Replace("#accesskey", _AccessKey);
    //            payload = payload.Replace("#userid", _UserID);
    //            payload = payload.Replace("#password", _Password);
    //            payload = payload.Replace("#requestreference", SecurityElement.Escape(RequestReference));
    //            payload = payload.Replace("#sendername", SecurityElement.Escape(_SenderName));
    //            payload = payload.Replace("#senderphone", SecurityElement.Escape(_SenderPhoneNumber));
    //            payload = payload.Replace("#accountnumber", _UpsAccountNumber);
    //            payload = payload.Replace("#senderaddressline1", SecurityElement.Escape(_SenderAddressLine1));
    //            payload = payload.Replace("#senderaddressline2", SecurityElement.Escape(_SenderAddressLine2));
    //            payload = payload.Replace("#senderaddresscity", SecurityElement.Escape(_SenderTownCity));
    //            payload = payload.Replace("#senderpostalcode", SecurityElement.Escape(_SenderPostalCode));
    //            payload = payload.Replace("#sendercountrycode", SecurityElement.Escape(_SenderCountryCode));

    //            payload = payload.Replace("#requestoption", "Shop");
    //            payload = payload.Replace("#servicecode", string.Empty);

    //            string s = SecurityElement.Escape(DeliveryName).Trim;
    //            if (s.Length > 35)
    //            {
    //                s = s.Substring(0, 35);
    //            }
    //            payload = payload.Replace("#deliveryname", s);

    //            s = SecurityElement.Escape(DestinationPhoneNumber.Trim);
    //            if (s.Length > 15)
    //            {
    //                s = s.Substring(0, 15);
    //            }
    //            payload = payload.Replace("#deliveryphone", s);

    //            s = SecurityElement.Escape(DestinationAddressLine1.Trim);
    //            if (s.Length > 35)
    //            {
    //                s = s.Substring(0, 35);
    //            }
    //            payload = payload.Replace("#deliveryaddressline1", s);

    //            s = SecurityElement.Escape(DestinationAddressLine2.Trim);
    //            if (s.Length > 35)
    //            {
    //                s = s.Substring(0, 35);
    //            }
    //            payload = payload.Replace("#deliveryaddressline2", s);

    //            s = SecurityElement.Escape(DestinationTownCity.Trim);
    //            if (s.Length > 30)
    //            {
    //                s = s.Substring(0, 30);
    //            }
    //            payload = payload.Replace("#deliverycity", s);

    //            s = SecurityElement.Escape(DestinationPostalCode.Replace("-", "").Replace(" ", "").Trim);
    //            if (s.Length > 9)
    //            {
    //                s = s.Substring(0, 9);
    //            }
    //            payload = payload.Replace("#deliverypostalcode", s);

    //            payload = payload.Replace("#deliverycountrycode", DestinationCountryCode);

    //            if (IsResidentialAddress)
    //            {
    //                payload = payload.Replace("#residentialaddressindicator", "<ResidentialAddressIndicator />");
    //            }
    //            else
    //            {
    //                payload = payload.Replace("#residentialaddressindicator", string.Empty);
    //            }

    //            if (DestinationCountryCode == "US" & DestinationCountyState.Trim.Length == 2)
    //            {
    //                payload = payload.Replace("#deliverycountystate", "<StateProvinceCode>" + DestinationCountyState.Trim + "</StateProvinceCode>");
    //            }
    //            else
    //            {
    //                payload = payload.Replace("#deliverycountystate", string.Empty);
    //            }

    //            // package details
    //            string packages = string.Empty;

    //            string thisPackage = My.Resources.UpsPackage;
    //            thisPackage = thisPackage.Replace("#lengthCms", Math.Round(PackageWidthCms, 2));
    //            thisPackage = thisPackage.Replace("#widthCms", Math.Round(PackageHeightCms, 2));
    //            thisPackage = thisPackage.Replace("#heightCms", Math.Round(PackageDepthCms, 2));
    //            thisPackage = thisPackage.Replace("#weightkgs", Math.Round(TotalWeightIncPackagingKGS, 1));

    //            if (TotalWeightIncPackagingKGS <= 30)
    //            {
    //                // doesn't require additional handling
    //                thisPackage = thisPackage.Replace("<AdditionalHandling/>", string.Empty);
    //            }

    //            if ((PackageWidthCms + (2 * (PackageHeightCms + PackageDepthCms))) <= 330)
    //            {
    //                // not large package
    //                thisPackage = thisPackage.Replace("<LargePackageIndicator/>", string.Empty);
    //            }

    //            packages += thisPackage;

    //            payload = payload.Replace("#packages", packages);

    //            byte[] payloadData = Encoding.ASCII.GetBytes(payload);

    //            bool IsDomestic = false;
    //            if (DestinationCountryCode == _SenderCountryCode)
    //            {
    //                IsDomestic = true;
    //            }

    //            WebRequest req = null;
    //            if (_IsProduction)
    //            {
    //                req = WebRequest.Create(My.Settings.endPoint_Production);
    //            }
    //            else
    //            {
    //                req = WebRequest.Create(My.Settings.endPoint_Testing);
    //            }

    //            if ((req != null))
    //            {

    //                try
    //                {
    //                    req.Method = "POST";
    //                    req.Timeout = 10000;
    //                    // 10 secs
    //                    req.ContentType = "application/xml";
    //                    req.ContentLength = payloadData.Length;

    //                    Stream thisStream = req.GetRequestStream;
    //                    thisStream.Write(payloadData, 0, payloadData.Length);
    //                    thisStream.Close();

    //                    WebResponse resp = req.GetResponse;
    //                    thisStream = resp.GetResponseStream;

    //                    StreamReader rdr = new StreamReader(thisStream, true);
    //                    string xml = rdr.ReadToEnd;

    //                    lst = ParseUpsRateResponseXml(xml, IsDomestic);
    //                }
    //                catch (Exception ex)
    //                {
    //                    lst = null;
    //                }
    //            }


    //            DateTime endTime = Now;
    //            double ElapsedTime = TimeSpan.FromTicks(endTime.Ticks - startTime.Ticks).TotalSeconds;
    //            Debug.WriteLine("UpsShipping.GetShippingOptions " + RequestReference + " ended at " + endTime.ToLongTimeString);
    //            Debug.WriteLine("Operation found " + lst.Count.ToString + " shipping methods(s) and took " + string.Format("{0:0.000} seconds", ElapsedTime));

    //            return lst;
    //        }










    //        public List<UpsShippingOption> GetShippingOptions_USA(decimal TotalWeightLBS, string DeliveryName, string DestinationAddressLine1, string DestinationAddressLine2, string DestinationTownCity, string DestinationCountyState, string DestinationPostalCode, string DestinationCountryCode, string DestinationPhoneNumber, string RequestReference)
    //        {

    //            DateTime startTime = Now;
    //            Debug.WriteLine("RatesCalculator.GetShippingOptions " + RequestReference + " " + TotalWeightLBS.ToString + "Lbs " + DeliveryName + " " + DestinationAddressLine1 + " " + DestinationPostalCode + " " + DestinationCountryCode + " started at " + startTime.ToLongTimeString);


    //            List<UpsShippingOption> lst = new List<UpsShippingOption>();

    //            string payload = My.Resources.UpsGetShippingRatesRequest_USA;

    //            if (TotalWeightLBS >= 0 & TotalWeightLBS < 1)
    //            {
    //                TotalWeightLBS = 1;
    //            }

    //            string s = string.Empty;

    //            // update values
    //            payload = payload.Replace("#accesskey", _AccessKey);
    //            payload = payload.Replace("#userid", _UserID);
    //            payload = payload.Replace("#password", _Password);
    //            payload = payload.Replace("#requestreference", RequestReference);
    //            payload = payload.Replace("#sendername", _SenderName);
    //            payload = payload.Replace("#senderphone", _SenderPhoneNumber);
    //            payload = payload.Replace("#accountnumber", _UpsAccountNumber);
    //            payload = payload.Replace("#senderaddressline1", _SenderAddressLine1);
    //            payload = payload.Replace("#senderaddressline2", _SenderAddressLine2);
    //            payload = payload.Replace("#senderaddresscity", _SenderTownCity);
    //            payload = payload.Replace("#senderpostalcode", _SenderPostalCode);
    //            payload = payload.Replace("#sendercountrycode", _SenderCountryCode);
    //            payload = payload.Replace("#deliveryname", DeliveryName);
    //            payload = payload.Replace("#deliveryphone", DestinationPhoneNumber);

    //            s = SecurityElement.Escape(DestinationAddressLine1.Trim);
    //            if (s.Length > 35)
    //            {
    //                s = s.Substring(0, 35);
    //            }
    //            payload = payload.Replace("#deliveryaddressline1", s);

    //            s = SecurityElement.Escape(DestinationAddressLine2.Trim);
    //            if (s.Length > 35)
    //            {
    //                s = s.Substring(0, 35);
    //            }
    //            payload = payload.Replace("#deliveryaddressline2", s);

    //            s = SecurityElement.Escape(DestinationTownCity.Trim);
    //            if (s.Length > 30)
    //            {
    //                s = s.Substring(0, 30);
    //            }
    //            payload = payload.Replace("#deliverycity", s);

    //            // TODO: convert long names to 2 char state code

    //            payload = payload.Replace("#deliverycountystate", DestinationCountyState.ToUpper);
    //            // convert lowercase state codes

    //            s = SecurityElement.Escape(DestinationPostalCode.Trim);
    //            if (s.Length > 9)
    //            {
    //                s = s.Substring(0, 9);
    //            }
    //            payload = payload.Replace("#deliverypostalcode", s);

    //            payload = payload.Replace("#deliverycountrycode", DestinationCountryCode);
    //            payload = payload.Replace("#weightPounds", TotalWeightLBS);

    //            bool IsDomestic = false;
    //            if (DestinationCountryCode == _SenderCountryCode)
    //            {
    //                IsDomestic = true;
    //            }

    //            byte[] payloadData = Encoding.ASCII.GetBytes(payload);

    //            WebRequest req = null;
    //            if (_IsProduction)
    //            {
    //                req = WebRequest.Create(My.Settings.endPoint_Production);
    //            }
    //            else
    //            {
    //                req = WebRequest.Create(My.Settings.endPoint_Testing);
    //            }

    //            if ((req != null))
    //            {
    //                req.Method = "POST";
    //                req.Timeout = 10000;
    //                // 10 secs
    //                req.ContentType = "application/xml";
    //                req.ContentLength = payloadData.Length;

    //                Stream thisStream = req.GetRequestStream;
    //                thisStream.Write(payloadData, 0, payloadData.Length);
    //                thisStream.Close();

    //                WebResponse resp = req.GetResponse;
    //                thisStream = resp.GetResponseStream;

    //                string xml = string.Empty;
    //                using (StreamReader rdr = new StreamReader(thisStream, true))
    //                {
    //                    xml = rdr.ReadToEnd;
    //                }

    //                if (xml.Contains("NegotiatedRates"))
    //                {
    //                    // cost price returned
    //                    lst = ParseUpsResponseXml(xml, _BranchCode, IsDomestic);
    //                }
    //                else if (!string.IsNullOrWhiteSpace(DestinationTownCity))
    //                {
    //                    // drop city and try again
    //                    lst = GetShippingOptions_USA(TotalWeightLBS, DeliveryName, DestinationAddressLine1, DestinationAddressLine2, string.Empty, DestinationCountyState, DestinationPostalCode, DestinationCountryCode, DestinationPhoneNumber, RequestReference);
    //                }
    //                else if (!string.IsNullOrWhiteSpace(DestinationAddressLine2))
    //                {
    //                    // drop address line 2 and try again
    //                    lst = GetShippingOptions_USA(TotalWeightLBS, DeliveryName, DestinationAddressLine1, string.Empty, string.Empty, DestinationCountyState, DestinationPostalCode, DestinationCountryCode, DestinationPhoneNumber, RequestReference);
    //                }
    //                else
    //                {
    //                    // still no costs prices, use published rates
    //                    lst = ParseUpsResponseXml(xml, _BranchCode, IsDomestic);
    //                }
    //            }


    //            DateTime endTime = Now;
    //            double ElapsedTime = TimeSpan.FromTicks(endTime.Ticks - startTime.Ticks).TotalSeconds;
    //            Debug.WriteLine("RatesCalculator.GetShippingOptions " + RequestReference + " ended at " + endTime.ToLongTimeString);
    //            Debug.WriteLine("Operation found " + lst.Count.ToString + " shipping methods(s) and took " + string.Format("{0:0.000} seconds", ElapsedTime));

    //            return lst;
    //        }

    //        private static List<UpsShippingOption> ParseUpsResponseXml(string xml, string BranchCode, bool IsDomestic)
    //        {

    //            List<UpsShippingOption> lst = new List<UpsShippingOption>();

    //            XmlDocument doc = new XmlDocument();
    //            doc.LoadXml(xml);

    //            XmlNode ndResponse = doc.SelectSingleNode("/RatingServiceSelectionResponse/Response");

    //            if ((ndResponse != null))
    //            {
    //                string statusCode = ndResponse.SelectSingleNode("ResponseStatusCode").InnerText;

    //                if (statusCode == "1")
    //                {
    //                    // success

    //                    string Code = string.Empty;
    //                    string CurrencyCode = string.Empty;
    //                    decimal PublishedRate = 0;
    //                    decimal DiscountedRate = 0;
    //                    int DeliveryByDays = 0;
    //                    string DeliveryByTime = string.Empty;

    //                    XmlNode nd = null;

    //                    XmlNodeList lstShippingOptions = doc.SelectNodes("/RatingServiceSelectionResponse/RatedShipment");

    //                    foreach (XmlNode ndShippingOption in lstShippingOptions)
    //                    {
    //                        nd = ndShippingOption.SelectSingleNode("Service/Code");
    //                        if ((nd != null))
    //                        {
    //                            Code = nd.InnerText;
    //                        }
    //                        else
    //                        {
    //                            Code = string.Empty;
    //                        }

    //                        nd = ndShippingOption.SelectSingleNode("TotalCharges/CurrencyCode");
    //                        if ((nd != null))
    //                        {
    //                            CurrencyCode = nd.InnerText;
    //                        }
    //                        else
    //                        {
    //                            CurrencyCode = string.Empty;
    //                        }

    //                        nd = ndShippingOption.SelectSingleNode("TotalCharges/MonetaryValue");
    //                        if ((nd != null))
    //                        {
    //                            decimal.TryParse(nd.InnerText, PublishedRate);
    //                        }
    //                        else
    //                        {
    //                            PublishedRate = 0;
    //                        }

    //                        nd = ndShippingOption.SelectSingleNode("NegotiatedRates/NetSummaryCharges/GrandTotal/MonetaryValue");
    //                        if ((nd != null))
    //                        {
    //                            decimal.TryParse(nd.InnerText, DiscountedRate);
    //                        }
    //                        else
    //                        {
    //                            DiscountedRate = 0;
    //                        }

    //                        nd = ndShippingOption.SelectSingleNode("GuaranteedDaysToDelivery");
    //                        if ((nd != null))
    //                        {
    //                            int.TryParse(nd.InnerText, DeliveryByDays);
    //                        }
    //                        else
    //                        {
    //                            DeliveryByDays = 0;
    //                        }

    //                        nd = ndShippingOption.SelectSingleNode("ScheduledDeliveryTime");
    //                        if ((nd != null))
    //                        {
    //                            DeliveryByTime = nd.InnerText;
    //                        }
    //                        else
    //                        {
    //                            DeliveryByTime = string.Empty;
    //                        }

    //                        Debug.WriteLine(Now.ToString + Constants.vbTab + "Shipping Option - Code: {0} Days: {1} Before Time: {2} Published Rate: {3}{5} Discounted Rate: {4}{5}", Code, DeliveryByDays.ToString, DeliveryByTime, PublishedRate, DiscountedRate, CurrencyCode);

    //                        if (!string.IsNullOrEmpty(Code))
    //                        {
    //                            if (!string.IsNullOrEmpty(CurrencyCode))
    //                            {
    //                                if (PublishedRate > 0 & DiscountedRate > 0)
    //                                {
    //                                    // add to list with negotiated rate                                    
    //                                    lst.Add(new UpsShippingOption(Code, DeliveryByDays, DeliveryByTime, PublishedRate, DiscountedRate, CurrencyCode, BranchCode, IsDomestic));
    //                                }
    //                                else if (PublishedRate > 0)
    //                                {
    //                                    // add to list without any cost price discount
    //                                    lst.Add(new UpsShippingOption(Code, DeliveryByDays, DeliveryByTime, PublishedRate, PublishedRate, CurrencyCode, BranchCode, IsDomestic));
    //                                }
    //                            }
    //                        }
    //                    }


    //                }
    //                else
    //                {
    //                    // failed


    //                    if (System.Diagnostics.Debugger.IsAttached)
    //                    {
    //                        Debug.WriteLine("UPS Request Failed");

    //                        string ErrorSeverity = string.Empty;
    //                        string ErrorCode = string.Empty;
    //                        string ErrorDescription = string.Empty;
    //                        string ElementName = string.Empty;
    //                        string ElementReference = string.Empty;
    //                        string AttributeName = string.Empty;

    //                        XmlNodeList lstErrors = ndResponse.SelectNodes("Error");

    //                        foreach (XmlNode ndError in lstErrors)
    //                        {
    //                            ErrorSeverity = ndError.SelectSingleNode("ErrorSeverity").InnerText;
    //                            ErrorCode = ndError.SelectSingleNode("ErrorCode").InnerText;
    //                            ErrorDescription = ndError.SelectSingleNode("ErrorDescription").InnerText;

    //                            Debug.WriteLine("Error Information - Code: {0} Level: {1} Description: {2}", ErrorCode, ErrorSeverity, ErrorDescription);

    //                            XmlNodeList lstErrorLocations = ndError.SelectNodes("ErrorLocation");

    //                            foreach (XmlNode ndErrorLocation in lstErrorLocations)
    //                            {
    //                                ElementName = ndErrorLocation.SelectSingleNode("ErrorLocationElementName").InnerText;
    //                                ElementReference = ndErrorLocation.SelectSingleNode("ErrorLocationElementReference").InnerText;
    //                                AttributeName = ndErrorLocation.SelectSingleNode("ErrorLocationAttributeName").InnerText;

    //                                Debug.WriteLine("Error Details - Element Name: {0} Element Reference: {1} Attribute Name: {2}", ElementName, ElementReference, AttributeName);

    //                            }

    //                            Debug.WriteLine("--------------------");

    //                        }

    //                    }

    //                }
    //            }

    //            return lst;
    //        }




    //        #endregion

    //    }

    #endregion


}
