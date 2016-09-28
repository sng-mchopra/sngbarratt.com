namespace jCtrl.Shipping.RoyalMail
{
    public class Recipient
    {

        public string Name { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string TownCity { get; set; }
        public string CountyState { get; set; }
        public string PostalCode { get; set; }
        public string CountryName { get; set; }
        public string Country_Code { get; set; }

        public PhoneNumber PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VatNumber { get; set; }

    }
}
