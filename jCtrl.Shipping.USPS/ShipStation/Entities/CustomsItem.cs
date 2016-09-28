
namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class CustomsItem
    {
        public string customsItemId { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public double value { get; set; }
        public string harmonizedTariffCode { get; set; }
        public string countryOfOrigin { get; set; }

    }
}
