using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public class Address
    {
        public Address()
        {
            CountryCode = "US";
            IsResidential = false;
        }

  
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string AppartmentBuilding { get; set; }
        public string StreetAddress { get; set; }
        //public string Line3 { get; set; }
        public string City { get; set; }              
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodePlus { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public bool IsResidential { get; set; }



        public string GetCountryName()
        {
            if (!string.IsNullOrEmpty(CountryName))
            {
                return CountryName;
            }

            if (string.IsNullOrEmpty(CountryCode))
            {
                return string.Empty;
            }
            try
            {
                var regionInfo = new RegionInfo(CountryCode);
                return regionInfo.EnglishName;
            }
            catch (ArgumentException e)
            {
                //causes the whole application to crash.
            }

            return string.Empty;
        }

        public bool IsCanadaAddress()
        {
            return !string.IsNullOrEmpty(CountryCode) && string.Equals(CountryCode, "CA", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Returns true if the CountryCode matches US or one of the US territories.
        /// </summary>
        /// <returns></returns>
        public bool IsUnitedStatesAddress()
        {
            var usAndTerritories = new List<string> { "AS", "GU", "MP", "PR", "UM", "VI", "US" };

            return usAndTerritories.Contains(CountryCode);
        }
    }
}
