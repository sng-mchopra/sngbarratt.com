using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{
    public static class Utilities
    {

        public static string GetShipStationServiceCode(string ProviderRef, bool isDomestic)
        {
          
                if (isDomestic == true)
                {
                    #region "Domestic"
                    switch (ProviderRef)
                    {
                        case "0":   //First - Class Mail Large Envelope
                                    //First - Class Mail Letter
                                    //First - Class Mail Parcel
                                    //First - Class Mail Postcards
                            return "usps_first_class_mail";
                        case "1":   //Priority Mail ____
                            return "usps_priority_mail";
                        case "2":   //Priority Mail Express ____ Hold For Pickup
                            return "usps_priority_mail_express";
                        case "3":   //Priority Mail Express ____
                            return "usps_priority_mail_express";
                        case "4":   //Standard Post
                            return "";
                        case "6":   //Media Mail
                            return "";
                        case "7":   //Library Mail
                            return "";
                        case "13":  //Priority Mail Express ____ Flat Rate Envelope
                            return "usps_priority_mail_express";
                        case "15":  //First - Class Mail Large Postcards
                            return "usps_first_class_mail";
                        case "16":  //Priority Mail ____ Flat Rate Envelope
                            return "usps_priority_mail";
                        case "17":  //Priority Mail ____ Medium Flat Rate Box
                            return "usps_priority_mail";
                        case "22":  //Priority Mail ____ Large Flat Rate Box
                            return "usps_priority_mail";
                        case "23":  //Priority Mail Express ____ Sunday / Holiday Delivery
                            return "usps_priority_mail_express";
                        case "25":  //Priority Mail Express ____ Sunday / Holiday Delivery Flat Rate Envelope
                            return "usps_priority_mail_express";
                        case "27":  //Priority Mail Express ____ Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail_express";
                        case "28":  //Priority Mail ____ Small Flat Rate Box
                            return "usps_priority_mail";
                        case "29":  //Priority Mail ____ Padded Flat Rate Envelopereturn "";
                            return "usps_priority_mail";
                        case "30":  //Priority Mail Express ____ Legal Flat Rate Envelope
                            return "usps_priority_mail_express";
                        case "31":  //Priority Mail Express ____ Legal Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail_express";
                        case "32":  //Priority Mail Express ____ Sunday / Holiday Delivery Legal Flat Rate Envelope
                            return "usps_priority_mail_express";
                        case "33":  //Priority Mail ____ Hold For Pickup
                            return "usps_priority_mail";
                        case "34":  //Priority Mail ____ Large Flat Rate Box Hold For Pickup
                            return "usps_priority_mail";
                        case "35":  //Priority Mail ____ Medium Flat Rate Box Hold For Pickup
                            return "usps_priority_mail";
                        case "36":  //Priority Mail ____ Small Flat Rate Box Hold For Pickup
                            return "usps_priority_mail";
                        case "37":  //Priority Mail ____ Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "38":  //Priority Mail ____ Gift Card Flat Rate Envelope
                            return "usps_priority_mail";
                        case "39":  //Priority Mail ____ Gift Card Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "40":  //Priority Mail ____ Window Flat Rate Envelope
                            return "usps_priority_mail";
                        case "41":  //Priority Mail ____ Window Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "42":  //Priority Mail ____ Small Flat Rate Envelope
                            return "usps_priority_mail";
                        case "43":  //Priority Mail ____ Small Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "44":  //Priority Mail ____ Legal Flat Rate Envelope
                            return "usps_priority_mail";
                        case "45":  //Priority Mail ____ Legal Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "46":  //Priority Mail ____ Padded Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail";
                        case "47":  //Priority Mail ____ Regional Rate Box A
                            return "usps_priority_mail";
                        case "48":  //Priority Mail ____ Regional Rate Box A Hold For Pickup
                            return "usps_priority_mail";
                        case "49":  //Priority Mail ____ Regional Rate Box B
                            return "usps_priority_mail";
                        case "50":  //Priority Mail ____ Regional Rate Box B Hold For Pickup
                            return "usps_priority_mail";
                        case "53":  //First - Class / Package Service Hold For Pickup
                            return "usps_first_class_mail";
                        case "55":  //Priority Mail Express ____ Flat Rate Boxes
                            return "usps_priority_mail_express";
                        case "56":  //Priority Mail Express ____ Flat Rate Boxes Hold For Pickup
                            return "usps_priority_mail_express";
                        case "57":  //Priority Mail Express ____ Sunday / Holiday Delivery Flat Rate Boxes
                            return "usps_priority_mail_express";
                        case "58":  //Priority Mail ____ Regional Rate Box C
                            return "usps_priority_mail";
                        case "59":  //Priority Mail ____ Regional Rate Box C Hold For Pickup
                            return "usps_priority_mail";
                        case "61":  //First - Class / Package Service
                            return "usps_first_class_mail";
                        case "62":  //Priority Mail Express ____ Padded Flat Rate Envelope
                            return "usps_priority_mail_express";
                        case "63":  //Priority Mail Express ____ Padded Flat Rate Envelope Hold For Pickup
                            return "usps_priority_mail_express";
                        case "64":  //Priority Mail Express ____ Sunday / Holiday Delivery Padded Flat Rate Envelope
                            return "usps_priority_mail_express";
                        default:
                            return "";
                    }
                    #endregion
                }
                else
                {
                    #region "International"

                    switch (ProviderRef)
                    {
                        case "1":   //Priority Mail Express International
                            return "usps_priority_mail_express_international";
                        case "2":   //Priority Mail International
                            return "usps_priority_mail_international";
                        case "4":   //Global Express Guaranteed(GXG)
                            return "";
                        case "5":   //Global Express Guaranteed Document
                            return "";
                        case "6":   //Global Express Guaranteed Non-Document Rectangular
                            return "";
                        case "7":   //Global Express Guaranteed Non-Document Non - Rectangular
                            return "";
                        case "8":   //Priority Mail International Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "9":   //Priority Mail International Medium Flat Rate Box
                            return "usps_priority_mail_international";
                        case "10":  //Priority Mail Express International Flat Rate Envelope
                            return "usps_priority_mail_express_international";
                        case "11":  //Priority Mail International Large Flat Rate Box
                            return "usps_priority_mail_international";
                        case "12":  //USPS GXG Envelopes
                            return "";
                        case "13":  //First - Class Mail International Letter
                            return "usps_first_class_mail_international";
                        case "14":  //First - Class Mail International Large Envelope
                            return "usps_first_class_mail_international";
                        case "15":  //First - Class Package International Service
                            return "usps_first_class_mail_international";
                        case "16":  //Priority Mail International Small Flat Rate Box
                            return "usps_priority_mail_international";
                        case "17":  //Priority Mail Express International Legal Flat Rate Envelope
                            return "usps_priority_mail_express_international";
                        case "18":  //Priority Mail International Gift Card Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "19":  //Priority Mail International Window Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "20":  //Priority Mail International Small Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "21":  //First - Class Mail International Postcard
                            return "usps_first_class_mail_international";
                        case "22":  //Priority Mail International Legal Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "23":  //Priority Mail International Padded Flat Rate Envelope
                            return "usps_priority_mail_international";
                        case "24":  //Priority Mail International DVD Flat Rate priced box
                            return "usps_priority_mail_international";
                        case "25":  //Priority Mail International Large Video Flat Rate priced box
                            return "usps_priority_mail_international";
                        case "26":  //Priority Mail Express International Flat Rate Boxes
                            return "usps_priority_mail_express_international";
                        case "27":  //Priority Mail Express International Padded Flat Rate Envelope
                            return "usps_priority_mail_express_international";
                        default:
                            return "";
                    }

                    #endregion
                }
            
        }

        public static string GetShipStationContainerCode(PackageType Packaging, bool isLargePackage)
        {
        
                switch (Packaging)
                {

                    case PackageType.Unknown:
                        if (isLargePackage) return "large_package";
                        return "package";
                    case PackageType.Own_Packaging:
                        if (isLargePackage) return "large_package";
                        return "package";
                    case PackageType.Flat_Rate_Envelope:
                        return "flat_rate_envelope";
                    case PackageType.Flat_Rate_Envelope_Padded:
                        return "flat_rate_padded_envelope";
                    case PackageType.Flat_Rate_Envelope_Legal:
                        return "flat_rate_legal_envelope";
                    //case PackageType.Flat_Rate_Envelope_Small:
                    //    return "sm flate rate envelope";
                    //case PackageType.Flat_Rate_Box:
                    //return "flat rate box";
                    case PackageType.Flat_Rate_Box_Small:
                        return "small_flat_rate_box";
                    case PackageType.Flat_Rate_Box_Medium:
                        return "medium_flat_rate_box";
                    case PackageType.Flat_Rate_Box_Large:
                        return "large_flat_rate_box";
                    case PackageType.Regional_Rate_Box_A:
                        return "regional_rate_box_a";
                    case PackageType.Regional_Rate_Box_B:
                        return "regional_rate_box_b";
                    //case PackageType.Regional_Rate_Box_C:
                    //return "regionalrateboxc";
                    default:
                        return "package";
                
            }

        }
    }
}
