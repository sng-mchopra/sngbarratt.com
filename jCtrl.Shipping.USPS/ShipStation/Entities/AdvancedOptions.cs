
namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class AdvancedOptions
    {

        public int warehouseId { get; set; }
        public bool nonMachinable { get; set; }
        public bool saturdayDelivery { get; set; }
        public bool containsAlcohol { get; set; }
        public int storeId { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string customField3 { get; set; }
        public string source { get; set; }
        public bool mergedOrSplit { get; set; }
        public int[] mergedIds { get; set; }
        public int parentId { get; set; }
        public string billToParty { get; set; }
        public string billToPostalCode { get; set; }
        public string billToCountryCode { get; set; }
    }
}

