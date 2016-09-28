using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace jCtrl.Shipping.UPS
{               

    public class UpsShippingOption
    {

        private string _Code;
        private UpsShippingService _ShippingService;
        private decimal _PublishedRate;
        private decimal _DiscountedRate;
        private string _CurrencyCode;
        private int _DeliveryByDays;
        private string _DeliveryByTime;        
        private bool _IsDomestic;

        public UpsShippingOption(UpsShippingService Service, int DaysToDelivery, string DeliveryByTime, decimal PublishedRate, decimal DiscountedRate, string CurrencyCode,  bool IsDomestic)
        {
            _ShippingService = Service;
            _Code = ((int)Service).ToString();            
            _PublishedRate = PublishedRate;
            _DiscountedRate = DiscountedRate;
            _CurrencyCode = CurrencyCode;
            _DeliveryByDays = DaysToDelivery;
            _DeliveryByTime = DeliveryByTime;
            
            _IsDomestic = IsDomestic;

        }

        public string ServiceCode
        {
            get { return _Code; }
        }

        public string ServiceName
        {
            get { return Enum.GetName(typeof(UpsShippingService), _ShippingService).Replace("_", " "); }
        }
        

        // TODO: Handle this higher up
        //public string Description
        //{
        //    get
        //    {

        //        if (_DeliveryByDays > 0)
        //        {
        //            string delTime = string.Empty;
        //            if (!string.IsNullOrWhiteSpace(_DeliveryByTime))
        //            {
        //                delTime = ", " + _DeliveryByTime.Trim();
        //            }

        //            if (_DeliveryByDays == 1)
        //            {
        //                // next day
        //                if (!(_BranchCode == "BAU"))
        //                {
        //                    return ServiceName + " Next Day" + delTime;
        //                }
        //                else
        //                {
        //                    return ServiceName + delTime;
        //                }

        //            }
        //            else
        //            {
        //                if (!(_BranchCode == "BAU"))
        //                {
        //                    return ServiceName + " " + _DeliveryByDays.ToString() + " Days" + delTime;
        //                }
        //                else
        //                {
        //                    return ServiceName + delTime;
        //                }
        //            }

        //        }
        //        else if (_BranchCode == "SNG" & _IsDomestic & _ShippingService == UpsShippingService.UPS_Standard)
        //        {
        //            return ServiceName + " Next Day";
        //        }
        //        else
        //        {
        //            return ServiceName;                    
        //        }
        //    }
        //}


        public decimal PublishedRate
        {
            get { return _PublishedRate; }
        }
        public decimal DiscountedRate
        {
            get { return _DiscountedRate; }
        }
        public object CurrencyCode
        {
            get { return _CurrencyCode; }
        }
    }

}
