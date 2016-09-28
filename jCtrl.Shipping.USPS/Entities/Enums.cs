using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    
    public enum PackageType
    {
        Unknown, // variable
        Own_Packaging, // rectangular or nonrectangular
        Flat_Rate_Envelope, // flat rate envelope
        Flat_Rate_Envelope_Padded, // padded flat rate envelope
        Flat_Rate_Envelope_Legal, // legal flat rate envelope
        //Flat_Rate_Envelope_Small, // sm flat rate envelope
        //Flat_Rate_Box, // flat rate box
        Flat_Rate_Box_Small, // sm flat rate box
        Flat_Rate_Box_Medium, // md flat rate box
        Flat_Rate_Box_Large, // lg flat rate box
        Regional_Rate_Box_A, // regionalrateboxa
        Regional_Rate_Box_B, // regionalrateboxb
        //Regional_Rate_Box_C // regionalrateboxc

    }


    //public enum PackageSize { None, Regular, Large, Oversize };
    //public enum LabelImageType { TIF, PDF, None };
    //public enum ServiceType {
    //    Priority,
    //    First_Class,
    //    //Parcel_Post,
    //    //Bound_Printed_Matter,
    //    //Media_Mail,
    //    //Library_Mail

    //};
    //public enum LabelType { FullLabel = 1, DeliveryConfirmationBarcode = 2 };
}
