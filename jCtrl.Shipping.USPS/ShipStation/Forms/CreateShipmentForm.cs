
namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class CreateShipmentForm
    {
       
        //[Required]
        public string carrierCode { get; set; }
        //[Required]
        public string serviceCode { get; set; }
        //[Required]
        public string packageCode { get; set; }

        public string confirmation { get; set; }

        //[Required]
        public string shipDate { get; set; }
        
        //[Required]
        public Weight weight { get; set; }
        public Dimensions dimensions { get; set; }

        //[Required]
        public Address shipFrom { get; set; }

        //[Required]
        public Address shipTo { get; set; }

        public InsuranceOptions insuranceOptions { get; set; }
        public InternationalOptions internationalOptions { get; set; }
        public AdvancedOptions advancedOptions { get; set; }

        public bool testLabel { get; set; }
    }
}
