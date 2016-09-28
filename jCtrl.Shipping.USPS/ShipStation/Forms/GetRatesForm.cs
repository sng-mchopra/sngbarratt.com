

namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class GetRatesForm
    {       
        //[Required]
        public string carrierCode { get; set; }

        public string serviceCode { get; set; }
        public string packageCode { get; set; }

        //[Required]
        public string fromPostalCode { get; set; }

        public string toCity { get; set; }
        public string toState { get; set; }
        //[Required]
        public string toPostalCode { get; set; }
        //[Required]
        public string toCountry { get; set; }
        
        //[Required]
        public Weight weight { get; set; }
        public Dimensions dimensions { get; set; }

        public string confirmation { get; set; }
        public bool residential { get; set; }
    }
}
