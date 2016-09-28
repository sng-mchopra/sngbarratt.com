using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.Services.Core.Configuration
{
    public static class Settings
    {

        // enables quicker EF queries by not having to join to Branches by SiteCode to get the Id for the branch
        // friendlier to access the api using the SiteCode which is used to set the styling on the website
        public enum SiteCodes { UK = 1, US = 2, NL = 3, DE = 4, FR = 5 };

        public static int[] PageSizes = { 5, 10, 25, 50, 100 };

        public enum ProductListSortOptions { Popularity = 1, PartNumber = 2, ProductTitle = 3, PriceLowHigh = 4, PriceHighLow = 5 };

        public enum CategoryTypes { Accessories = 1, ServiceParts = 2, Upgrades = 3 };


    }
}