using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class BranchReturnModel
    {

        public string Url { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string Name { get; set; }
        public AddressReturnModel Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string CurrenyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string FlagUrl { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public IntroductionReturnModel Introduction { get; set; }
        public List<TaxRateReturnModel> TaxRates { get; set; }
        public List<OpeningTimeReturnModel> OpeningTimes { get; set; }


    }
}