﻿
namespace jCtrl.Shipping.RoyalMail
{
    public class PhoneNumber
    {

        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }

        //public string FullNumber()
        //{
        //    return (CountryCode + " " + AreaCode + " " + Number).Replace("  ", " ");
        //}
    }
}
