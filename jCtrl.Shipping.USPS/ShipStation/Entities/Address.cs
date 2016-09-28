﻿
namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class Address
    {

        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string street3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public bool residential { get; set; }
        public string addressVerified { get; set; }

    }
}
