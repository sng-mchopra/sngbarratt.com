
namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class Shipment
    {
        public int shipmentId { get; set; }
        //public int? orderId { get; set; }
        //public int? userId { get; set; }
        //public string customerEmail { get; set; }
        //public string orderNumber { get; set; }
        public string createDate { get; set; }
        public string shipDate { get; set; }
        public decimal shipmentCost { get; set; }
        public decimal insuranceCost { get; set; }
        public string trackingNumber { get; set; }
        //public bool isReturnLabel { get; set; }
        //public string batchNumber { get; set; }
        public string carrierCode { get; set; }
        public string serviceCode { get; set; }
        public string packageCode { get; set; }
        public string confirmation { get; set; }
        //public int? warehouseId { get; set; }
        public bool voided { get; set; }
        public string voidDate { get; set; }
        //public bool marketplaceNotified { get; set; }
        public string notifyErrorMessage { get; set; }
        public Address shipTo { get; set; }
        public Weight weight { get; set; }
        public Dimensions dimensions { get; set; }
        public InsuranceOptions insuranceOptions { get; set; }
        public AdvancedOptions advancedOptions { get; set; }
        //public object shipmentItems { get; set; }
        public string labelData { get; set; }
        //public object formData { get; set; }



    }
}
