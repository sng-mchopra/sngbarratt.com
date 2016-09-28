
namespace jCtrl.Shipping.USPS.ShipStation
{
    public class Rate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Container { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal OtherCost { get; set; }
    }
}
