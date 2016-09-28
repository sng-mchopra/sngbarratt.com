using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping.Chronoship
{
    public class ChronopostShippingOption
    {

        //private string _ServiceCode;
        //private ChronopostShippingService _ShippingService;
        //private List<string> _SupplementCodes;
        //private decimal _PublishedRate;
        //Private _DiscountedRate As Decimal


            public ChronopostShippingService Service { get; set; }
            public List<string> SupplementCodes { get; set; }
            public decimal Price { get; set; }


        //public ChronopostShippingOption(ChronopostShippingService Service, List<string> SupplementCodes, decimal Price)
        //{
        //    _ShippingService = Service;
        //    _ServiceCode = Service.ToString();
        //    //_ServiceCode = Code;            
        //    //_ShippingService =  (ChronopostShippingService)int.Parse(Code);
        //    _SupplementCodes = SupplementCodes;
        //    _PublishedRate = Price;
        //}

        public string ServiceCode
        {
            get { return Service.ToString(); }
        }

        public string ServiceName
        {
            get
            {
                string service = string.Empty;

                switch (Service)
                {
                    //Case ChronopostShippingService.Chrono_10
                    //    service = "CHRONO 10"
                    case ChronopostShippingService.Chrono_13:
                        service = "CHRONO 13";
                        break;
                    //Case ChronopostShippingService.Chrono_18
                    //    service = "CHRONO 18"
                    case ChronopostShippingService.Chrono_Classic_International:
                        service = "CLASSIC INTL";
                        break;
                    case ChronopostShippingService.Chrono_Express_International:
                        service = "EXPRESS INTL";
                        break;
                    case ChronopostShippingService.Chrono_Premium_International:
                        service = "PREMIUM INTL";
                        break;
                }

                if ((SupplementCodes != null))
                {
                    foreach (string code in SupplementCodes)
                    {
                        service += " " + code;
                    }
                }

                return service;
            }
        }

        public string Description
        {
            get
            {
                string service = string.Empty;

                switch (Service)
                {
                    //Case ChronopostShippingService.Chrono_10
                    //    service = "Chrono 10"
                    case ChronopostShippingService.Chrono_13:
                        service = "Chrono 13";
                        break;
                    //Case ChronopostShippingService.Chrono_18
                    //    service = "Chrono 18"
                    case ChronopostShippingService.Chrono_Classic_International:
                        if (SupplementCodes.Any())
                        {
                            service = "Classic Intl";
                        }
                        else
                        {
                            service = "Classic International";
                        }
                        break;
                    case ChronopostShippingService.Chrono_Express_International:
                        if (SupplementCodes.Any())
                        {
                            service = "Express Intl";
                        }
                        else
                        {
                            service = "Express International";
                        }
                        break;
                    case ChronopostShippingService.Chrono_Premium_International:
                        if (SupplementCodes.Any())
                        {
                            service = "Premium Intl";
                        }
                        else
                        {
                            service = "Premium International";
                        }
                        break;
                }

                if (SupplementCodes.Contains("SAT"))
                {
                    service += ", Saturday";
                }

                if (SupplementCodes.Contains("RES"))
                {
                    service += ", Private Address";
                }

                if (SupplementCodes.Contains("PAY"))
                {
                    service += ", Payment on Delivery";
                }

                if (SupplementCodes.Contains("COL"))
                {
                    service += ", Collect Return";
                }


                return service;
            }
        }

        //public decimal Price
        //{
        //    get { return _PublishedRate; }
        //}

        //Public ReadOnly Property DiscountedRate As Decimal
        //    Get
        //        Return _DiscountedRate
        //    End Get
        //End Property
        //Public ReadOnly Property CurrencyCode
        //    Get
        //        Return _CurrencyCode
        //    End Get
        //End Property

    }

}
