
namespace jCtrl.Shipping.RoyalMail
{
    public class ManifestItem
    {
        public string ProductCode { get; set; }
        public string Title { get; set; }

        public string TariffCode { get; set; }
        public string TariffDescription { get; set; }

        public string CountryOfOriginCode { get; set; }

        public int Quantity { get; set; }
        public decimal WeightKg { get; set; }

        public decimal UnitPrice { get; set; }
        public string CurrencyCode { get; set; }
    }
}
