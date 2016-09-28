using jCtrl.Services.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace jCtrl.Shipping.Chronoship
{
    public static class ChronoshipRates
    {

        public static async Task<List<ChronopostShippingOption>> GetShippingOptions(string UserId, string UserPwd, Contact Sender, Contact Recipient, ChronpostShipmentType ShipmentType, decimal TotalWeightKGS)
        {

            var lst = new List<ChronopostShippingOption>();

            // set min weight to 1Kg
            if (TotalWeightKGS >= 0 & TotalWeightKGS < 1) { TotalWeightKGS = 1; }

            var payload = Properties.Resources.ChronoshipQuickCostRequest
                .Replace("#UserID", UserId)
                .Replace("#Password", UserPwd);

            string reqSoap = string.Empty;
            XmlDocument resXml = null;

            // get list of service options in alphabetical order
            List<string> names = Enum.GetNames(typeof(ChronopostShippingService)).ToList();
            names.Sort();

            ChronopostShippingService service;
            foreach (string serviceName in names)
            {
                service = (ChronopostShippingService)Enum.Parse(typeof(ChronopostShippingService), serviceName);

                switch (service)
                {
                    //Case ChronopostShippingService.Chrono_10, ChronopostShippingService.Chrono_13, ChronopostShippingService.Chrono_18 ', ChronopostShippingService.Chrono_relais
                    case ChronopostShippingService.Chrono_13:
                        // national service
                        if (Recipient.Country_Code == Sender.Country_Code)
                        {

                            try
                            {
                                reqSoap = payload;
                                reqSoap = reqSoap.Replace("#FromCode", Sender.PostalCode);
                                reqSoap = reqSoap.Replace("#ToCode", Recipient.PostalCode);
                                reqSoap = reqSoap.Replace("#Weight", TotalWeightKGS.ToString());
                                string ServiceCode = Convert.ToInt32(service).ToString();
                                reqSoap = reqSoap.Replace("#ProductCode", ServiceCode);

                                if (ShipmentType == ChronpostShipmentType.Document) {
                                    reqSoap = reqSoap.Replace("#Type", "D");
                                }
                                else {
                                    reqSoap = reqSoap.Replace("#Type", "M");
                                }


                                resXml = await CallWebService(payload);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("ERROR: " + ex.Message);
                                resXml = null;
                            }
                        }
                        else
                        {
                            resXml = null;
                        }

                        break;
                    case ChronopostShippingService.Chrono_Classic_International:
                    case ChronopostShippingService.Chrono_Express_International:
                    case ChronopostShippingService.Chrono_Premium_International:
                        // international service
                        if (Recipient.Country_Code != Sender.Country_Code)
                        {
                            try
                            {
                                reqSoap = payload;
                                reqSoap = reqSoap.Replace("#FromCode", Sender.PostalCode);
                                if (service == ChronopostShippingService.Chrono_Premium_International)
                                {
                                    // by postal code
                                    reqSoap = reqSoap.Replace("#ToCode", Recipient.PostalCode);
                                }
                                else
                                {
                                    // by country code
                                    reqSoap = reqSoap.Replace("#ToCode", Recipient.Country_Code);
                                }
                                reqSoap = reqSoap.Replace("#Weight", TotalWeightKGS.ToString());
                                string ServiceCode = Convert.ToInt32(service).ToString();
                                reqSoap = reqSoap.Replace("#ProductCode", ServiceCode);

                                if (ShipmentType == ChronpostShipmentType.Document) {
                                    reqSoap = reqSoap.Replace("#Type", "D");
                                }
                                else
                                {
                                    reqSoap = reqSoap.Replace("#Type", "M");
                                }

                                resXml = await CallWebService(payload);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("ERROR: " + ex.Message);
                                resXml = null;
                            }
                        }
                        else
                        {
                            resXml = null;
                        }

                        break;
                }

                if (resXml != null)
                {
                    // get info from result

                    var res = new QuickCostResponse(resXml);
                    if (res.Response == QuickCostReturnCode.Ok)
                    {
                        decimal basePrice = res.Amount;

                        var suppList = new List<string>();

                        // add base option with no supplements
                        lst.Add(new ChronopostShippingOption()
                        {
                            Service = service,
                            SupplementCodes = suppList,
                            Price = basePrice
                        });

                        if ((res.Supplements != null))
                        {
                            //Dim Collect_Return As Decimal = 0
                            //Dim Payment_On_Delivery As Decimal = 0
                            decimal Saturday_Delivery = 0;
                            decimal Residential_Address = 0;
                            decimal Premium_International = 0;

                            foreach (ChronopostSupplement supp in res.Supplements)
                            {
                                switch (supp.Code)
                                {
                                    case "P":
                                        // premium international
                                        Premium_International = supp.Amount;
                                        break;
                                    case "1":
                                        // removal on request
                                        break;
                                    // DON'T OFFER RETURNS
                                    //Collect_Return = supp.Amount                                        
                                    case "4":
                                        // return express payment
                                        break;
                                    // DON'T OFFER PAYMENT ON DELIVERY
                                    //Payment_On_Delivery = supp.Amount
                                    case "12":
                                        // classic saturday delivery
                                        Saturday_Delivery = supp.Amount;
                                        break;
                                    case "15":
                                        // express saturday delivery
                                        Saturday_Delivery = supp.Amount;
                                        break;
                                    case "B1":
                                        // express private home delivery
                                        Residential_Address = supp.Amount;
                                        break;
                                    case "B2":
                                        // classic private home delivery
                                        Residential_Address = supp.Amount;
                                        break;
                                }
                            }

                            // #######################################
                            // # 03/2013 Only allow residential addr #
                            // #######################################

                            //If (Saturday_Delivery > 0) Then
                            //    suppList = New List(Of String)
                            //    suppList.Add("SAT")

                            //    lst.Add(New ChronopostShippingOption(service.ToString, suppList, basePrice + Saturday_Delivery))
                            //End If

                            if (Residential_Address > 0)
                            {
                                suppList = new List<string>();
                                suppList.Add("RES");

                                lst.Add(new ChronopostShippingOption()
                                {
                                    Service = service,
                                    SupplementCodes = suppList,
                                    Price = basePrice + Residential_Address
                                });
                            }


                        }


                    }
                    else
                    {
                        // not ok

                        Debug.WriteLine("Request Failed: " + Enum.GetName(typeof(QuickCostReturnCode), res.Response).Replace("_", " "));
                        Debug.WriteLine(res.Message);
                    }


                }


            }


            return lst;
        }



        private static async Task<XmlDocument> CallWebService(string SOAP)
        {

            XmlDocument retXMLDoc = new XmlDocument();            

            try
            {
                byte[] payloadData = Encoding.ASCII.GetBytes(SOAP);

                var req = WebRequest.Create(Properties.Settings.Default.Rates_Url);
                req.Method = "POST";
                req.Timeout = 10000; // 10 secs
                req.ContentType = "application/soap+xml";
                req.ContentLength = payloadData.Length;

                Stream thisStream = req.GetRequestStream();
                thisStream.Write(payloadData, 0, payloadData.Length);
                thisStream.Close();

                WebResponse resp = req.GetResponse();
                thisStream = resp.GetResponseStream();

                StreamReader rdr = new StreamReader(thisStream, true);
                string xml = await rdr.ReadToEndAsync();
                
                retXMLDoc.LoadXml(xml);
              
            }
            catch (Exception ex)
            {
                // TODO: log the error
                
            }

            return retXMLDoc;

        }

    }
}
