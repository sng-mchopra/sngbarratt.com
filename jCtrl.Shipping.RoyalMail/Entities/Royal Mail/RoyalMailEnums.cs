using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping.RoyalMail
{
    public enum RoyalMailShipmentType
    {
        Delivery
        //Return
    }

    public enum RoyalMailShipmentPurpose
    {
        //Returned_Goods = 21,
        Gift = 31,
        Commercial_Sample = 32,
        Documents = 91,
        Mixed_Content = 991,
        Other = 999
    }

    public enum RoyalMailServiceType
    {
        FirstClass,
        SecondClass,
        SpecialDelivery,
        //HMForces,
        International
        //TrackedReturns,
        //Tracked
    }

    public enum RoyalMailServiceOffering
    {
        ROYAL_MAIL_24_48, //CRL                
        //ROYAL_MAIL_24_48_PARCEL_FLAT_RATE, //PPF 
        //ROYAL_MAIL_24_SORT8_PARCEL_FLAT_RATE, //PK1
        //ROYAL_MAIL_24_SORT8_LRG_LTR_PARCEL_DAILY_RATE, //PK3
        //ROYAL_MAIL_48_LRG_LTR_FLAT_RATE, //PK0
        //ROYAL_MAIL_48_SORT8_PARCEL_FLAT_RATE, //PK2
        //ROYAL_MAIL_48_SORT8_LRG_LTR_PARCEL_DAILY_RATE, //PK4
        //ROYAL_MAIL_24_LRG_LTR_FLAT_RATE,//PK9        
        //ROYAL_MAIL_24_PARCEL_FLAT_RATE, //RM5
        //ROYAL_MAIL_24_LRG_LTR_DAILY_RATE, //RM1        
        //ROYAL_MAIL_24_PARCEL_DAILY_RATE, //RM2
        //ROYAL_MAIL_24_SORT8_LRG_LTR_FLAT_RATE, //FS1
        //ROYAL_MAIL_24_SORT8_LRG_LTR_DAILY_RATE, //RM7
        //ROYAL_MAIL_24_SORT8_PARCEL_DAILY_RATE, //RM8
        //ROYAL_MAIL_48_PARCEL_FLAT_RATE, //RM6
        //ROYAL_MAIL_48_LRG_LTR__DAILY_RATE, //RM3
        //ROYAL_MAIL_48_PARCEL_DAILY_RATE, //RM4
        //ROYAL_MAIL_48_SORT8_LRG_LTR_FLAT_RATE, //FS2
        //ROYAL_MAIL_48_SORT8_LRG_LTR_DAILY_RATE, //RM9
        //ROYAL_MAIL_48_SORT8_PARCEL_DAILY_RATE, //RM0
        //ROYAL_MAIL_TRACKED_24, //TPN        
        //ROYAL_MAIL_TRACKED_24_HV, //TRM
        //ROYAL_MAIL_TRACKED_24_LBT, //TRN
        //ROYAL_MAIL_TRACKED_48, //TPS
        //ROYAL_MAIL_TRACKED_48_HV, //TPL
        //ROYAL_MAIL_TRACKED_48_LBT, //TRS
        //ROYAL_MAIL_TRACKED_RETURNS_24, //TSN
        //ROYAL_MAIL_TRACKED_RETURNS_48, //TSS
        //INTL_BUS_PARCELS_PRINT_DIRECT_PRIORITY,//MB1
        //INTL_BUS_PARCELS_PRINT_DIRECT_STANDARD, //MB2
        //INTL_BUS_PARCELS_PRINT_DIRECT_ECONOMY, //MB3
        //INTL_BUS_PARCELS_SIGNED, //MP5
        //INTL_BUS_PARCELS_SIGNED_EXTRA_COMP, //MP6
        //INTL_BUS_PARCELS_SIGNED_EXTRA_COMP_CTRY, //MP0
        //INTL_BUS_PARCELS_SIGNED_COUNTRY_PRICED, //MP9
        //INTL_BUS_PARCELS_TRACKED, //MP1
        //INTL_BUS_PARCELS_TRACKED_EXTRA_COMP, //MP4
        //INTL_BUS_PARCELS_TRACKED_EXTRA_COMP_CTRY, //MP8
        //INTL_BUS_PARCELS_TRACKED_COUNTRY_PRICED, //MP7
        //INTL_BUS_PARCELS_TRACKED_SIGNED, //MTA
        //INTL_BUS_PARCELS_TRACKED_SIGNED_EXTRA_COMP, //MTB        
        //INTL_BUS_PARCELS_TRACKED_SIGNED_CTRY_EXTRA_COMP, //MTF
        //INTL_BUS_PARCELS_TRACKED_SIGNED_COUNTRY_PRICED, //MTE
        //INTL_BUS_PARCELS_ZERO_SORT_PRIORITY, //WE1
        //INTL_BUS_PARCELS_ZERO_SORT_ECONOMY, //WE3
        //INTL_BUS_PARCELS_ZERO_SORT_HI_VOL_PRIORITY, //DE1
        //INTL_BUS_PARCELS_ZERO_SORT_HI_VOL_ECONOMY, //DE3
        //INTL_BUS_PARCELS_ZERO_SORT_LO_VOL_PRIORITY, //DE4
        //INTL_BUS_PARCELS_ZERO_SORT_LO_VOL_ECONOMY, //DE6
        //INTL_BUS_PARCELS_ZONE_SORT_PRIORITY, //IE1
        //INTL_BUS_PARCELS_ZONE_SORT_ECONOMY, //IE3
        //INTL_BUS_PARCELS_ZONE_SORT_PLUS_PRIORITY, //MTQ
        //INTL_BUS_PARCELS_ZONE_SORT_PLUS_ECONOMY, //MTS
        //INTL_BUS_PARCELS_MAX_SORT_ECONOMY, //PS0
        //INTL_BUS_PARCELS_MAX_SORT_STANDARD, //PSC
        //INTL_BUS_PARCELS_MAX_SORT_PRIORITY, //PS9
        //INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_HI_VOL_PRIORITY, //DG1
        //INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_HI_VOL_ECONOMY, //DG3
        //INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_LO_VOL_PRIORITY, //DG4
        //INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_LO_VOL_ECONOMY, //DG6            
        //INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_PRIORITY, //IG1
        //INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_ECONOMY, //IG3
        //INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_PRIORITY_MCH, //IG4
        //INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_ECONOMY_MCH, //IG6
        //INTL_BUS_MAIL_SIGNED, //MTM
        //INTL_BUS_MAIL_SIGNED_EXTRA_COMP, //MTN
        //INTL_BUS_MAIL_SIGNED_COUNTRY_PRICED, //MTO
        //INTL_BUS_MAIL_SIGNED_COUNTRY_EXTRA_COMP, //MTP
        //INTL_BUS_MAIL_TRACKED, //MTI
        //INTL_BUS_MAIL_TRACKED_EXTRA_COMP, //MTJ
        //INTL_BUS_MAIL_TRACKED_COUNTRY_PRICED, //MTK
        //INTL_BUS_MAIL_TRACKED_COUNTRY_EXTRA_COMP, //MTL
        //INTL_BUS_MAIL_TRACKED_SIGNED, //MTC
        //INTL_BUS_MAIL_TRACKED_SIGNED_EXTRA_COMP, //MTD
        //INTL_BUS_MAIL_TRACKED_SIGNED_COUNTRY, //MTG
        //INTL_BUS_MAIL_TRACKED_SIGNED_COUNTRY_EXTRA_COMP, //MTH
        INTL_BUS_MAIL_MIXED_ZONE_SORT_PRIORITY, //OZ1
        //INTL_BUS_MAIL_MIXED_ZONE_SORT_ECONOMY, //OZ3
        //INTL_BUS_MAIL_MIXED_ZONE_SORT_PRIORITY_MCH, //OZ4
        //INTL_BUS_MAIL_MIXED_ZONE_SORT_ECONOMY_MCH, //OZ6
        //INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_PRIORITY, //WG1
        //INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_ECONOMY, //WG3
        //INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_PRIORITY_MCH, //WG4
        //INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_ECONOMY_MCH, //WG6
        //INTL_BUS_MAIL_MIXED_ZERO_SORT_PRIORITY, //WW1
        //INTL_BUS_MAIL_MIXED_ZERO_SORT_ECONOMY, //WW3
        //INTL_BUS_MAIL_MIXED_ZERO_SORT_PRIORITY_MCH, //WW4
        //INTL_BUS_MAIL_MIXED_ZERO_SORT_ECONOMY_MCH, //WW6
        //INTL_BUS_MAIL_MIXED_ZERO_SORT_PREMIUM, //ZC1
        INTL_STANDARD_ON_ACCOUNT, //OLA
        INTL_ECONOMY_ON_ACCOUNT, //OLS
        //INTL_SIGNED_ON_ACCOUNT, //OSA
        //INTL_SIGNED_ON_ACCOUNT_EXTRA_COMP, //OSB
        //INTL_TRACKED_ON_ACCOUNT, //OTA
        //INTL_TRACKED_ON_ACCOUNT_EXTRA_COMP, //OTB
        //INTL_TRACKED_SIGNED_ON_ACCOUNT,//OTC
        //INTL_TRACKED_SIGNED_ON_ACCOUNT_EXTRA_COMP, //OTD
        //INTL_BUS_MAIL_LRG_LTR_MAX_SORT_ECONOMY, //PS8
        //INTL_BUS_MAIL_LRG_LTR_MAX_SORT_STANDARD, //PSB
        //INTL_BUS_MAIL_LRG_LTR_MAX_SORT_PRIORITY, //PS7        
        SPECIAL_DELIVERY_BY_1PM, //SD1
        //SPECIAL_DELIVERY_BY_1PM_1000GBP, //SD2
        //SPECIAL_DELIVERY_BY_1PM_2500GBP, //SD3
        SPECIAL_DELIVERY_BY_9AM, //SD4
        //SPECIAL_DELIVERY_BY_9AM_1000GBP, //SD5
        //SPECIAL_DELIVERY_BY_9AM_2500GBP, //SD6
        STANDARD_1ST_AND_2ND_CLASS_ACCOUNT_MAIL, //STL        
    }

    public enum RoyalMailServiceEnhancement
    {
        //Insurance_750GBP = 11,
        //Insurance_1000GBP = 1,
        //Insurance_2500GBP = 2,
        //Insurance_5000GBP = 3,
        //Insurance_7500GBP = 4,
        //Insurance_10000GBP = 5,
        //Recorded = 6,
        //TrackedSignature = 12,
        //Notification_SMS = 13,    // Requires a mobile number
        Notification_Email = 14,
        //Notification_SMS_Email = 16,    // Requires a mobile number
        //LocalCollect = 22,
        SaturdayGuaranteed = 24
    }

    public enum RoyalMailInlandServiceFormat
    {
        Inland_Letter,
        Inland_Letter_Large,
        Inland_Parcel,
        Inland_Format_NA
    }

    public enum RoyalMailInternationalServiceFormat
    {
        International_Letter,
        International_Letter_Large,
        International_Parcel,
        International_Format_NA
    }

    internal static class EnumConversion
    {

        internal static string RoyalMailServiceTypeToCode(RoyalMailServiceType Type)
        {
            switch (Type)
            {
                case RoyalMailServiceType.FirstClass:
                    return "1";
                case RoyalMailServiceType.SecondClass:
                    return "2";
                case RoyalMailServiceType.SpecialDelivery:
                    return "D";
                //case RoyalMailServiceType.HMForces:
                //return "H";
                case RoyalMailServiceType.International:
                    return "I";
                //case RoyalMailServiceType.TrackedReturns:
                //    return "R";
                //case RoyalMailServiceType.Tracked:
                //    return "T";
                default:
                    return "";
            }
        }

        internal static string RoyalMailServiceOfferingToCode(RoyalMailServiceOffering Type)
        {
            switch (Type)
            {
                case RoyalMailServiceOffering.ROYAL_MAIL_24_48:
                    return "CRL";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_HI_VOL_PRIORITY:
                //    return "DE1";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_HI_VOL_ECONOMY:
                //    return "DE3";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_LO_VOL_PRIORITY:
                //    return "DE4";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_LO_VOL_ECONOMY:
                //    return "DE6";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_HI_VOL_PRIORITY:
                //    return "DG1";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_HI_VOL_ECONOMY:
                //    return "DG3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_LO_VOL_PRIORITY:
                //    return "DG4";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_CTRY_SORT_LO_VOL_ECONOMY:
                //    return "DG6";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_SORT8_LRG_LTR_FLAT_RATE:
                //    return "FS1";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_SORT8_LRG_LTR_FLAT_RATE:
                //    return "FS2";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZONE_SORT_PRIORITY:
                //    return "IE1";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZONE_SORT_ECONOMY:
                //    return "IE3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_PRIORITY:
                //    return "IG1";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_ECONOMY:
                //    return "IG3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_PRIORITY_MCH:
                //    return "IG4";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZONE_SORT_ECONOMY_MCH:
                //    return "IG6";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_PRINT_DIRECT_PRIORITY:
                //    return "MB1";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_PRINT_DIRECT_STANDARD:
                //    return "MB2";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_PRINT_DIRECT_ECONOMY:
                //    return "MB3";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_SIGNED_EXTRA_COMP_CTRY:
                //    return "MP0";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED:
                //    return "MP1";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_EXTRA_COMP:
                //    return "MP4";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_SIGNED:
                //    return "MP5";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_SIGNED_EXTRA_COMP:
                //    return "MP6";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_COUNTRY_PRICED:
                //    return "MP7";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_EXTRA_COMP_CTRY:
                //    return "MP8";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_SIGNED_COUNTRY_PRICED:
                //    return "MP9";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_SIGNED:
                //    return "MTA";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_SIGNED_EXTRA_COMP:
                //    return "MTB";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_SIGNED:
                //    return "MTC";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_SIGNED_EXTRA_COMP:
                //    return "MTD";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_SIGNED_COUNTRY_PRICED:
                //    return "MTE";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_TRACKED_SIGNED_CTRY_EXTRA_COMP:
                //    return "MTF";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_SIGNED_COUNTRY:
                //    return "MTG";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_SIGNED_COUNTRY_EXTRA_COMP:
                //    return "MTH";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED:
                //    return "MTI";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_EXTRA_COMP:
                //    return "MTJ";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_COUNTRY_PRICED:
                //    return "MTK";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_TRACKED_COUNTRY_EXTRA_COMP:
                //    return "MTL";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_SIGNED:
                //    return "MTM";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_SIGNED_EXTRA_COMP:
                //    return "MTN";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_SIGNED_COUNTRY_PRICED:
                //    return "MTO";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_SIGNED_COUNTRY_EXTRA_COMP:
                //    return "MTP";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZONE_SORT_PLUS_PRIORITY:
                //    return "MTQ";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZONE_SORT_PLUS_ECONOMY:
                //    return "MTS";
                case RoyalMailServiceOffering.INTL_STANDARD_ON_ACCOUNT:
                    return "OLA";
                case RoyalMailServiceOffering.INTL_ECONOMY_ON_ACCOUNT:
                    return "OLS";
                //case RoyalMailServiceOffering.INTL_SIGNED_ON_ACCOUNT:
                //    return "OSA";
                //case RoyalMailServiceOffering.INTL_SIGNED_ON_ACCOUNT_EXTRA_COMP:
                //    return "OSB";
                //case RoyalMailServiceOffering.INTL_TRACKED_ON_ACCOUNT:
                //    return "OTA";
                //case RoyalMailServiceOffering.INTL_TRACKED_ON_ACCOUNT_EXTRA_COMP:
                //    return "OTB";
                //case RoyalMailServiceOffering.INTL_TRACKED_SIGNED_ON_ACCOUNT:
                //    return "OTC";
                //case RoyalMailServiceOffering.INTL_TRACKED_SIGNED_ON_ACCOUNT_EXTRA_COMP:
                //    return "OTD";
                case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZONE_SORT_PRIORITY:
                    return "OZ1";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZONE_SORT_ECONOMY:
                //    return "OZ3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZONE_SORT_PRIORITY_MCH:
                //    return "OZ4";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZONE_SORT_ECONOMY_MCH:
                //    return "OZ6";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_LRG_LTR_FLAT_RATE:
                //    return "PK0";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_SORT8_PARCEL_FLAT_RATE:
                //    return "PK1";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_SORT8_PARCEL_FLAT_RATE:
                //    return "PK2";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_SORT8_LRG_LTR_PARCEL_DAILY_RATE:
                //    return "PK3";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_SORT8_LRG_LTR_PARCEL_DAILY_RATE:
                //    return "PK4";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_LRG_LTR_FLAT_RATE:
                //    return "PK9";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_48_PARCEL_FLAT_RATE:
                //    return "PPF";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_MAX_SORT_ECONOMY:
                //    return "PS0";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_MAX_SORT_STANDARD:
                //    return "PSC";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_MAX_SORT_PRIORITY:
                //    return "PS9";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_MAX_SORT_ECONOMY:
                //    return "PS8";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_MAX_SORT_STANDARD:
                //    return "PSB";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_MAX_SORT_PRIORITY:
                //    return "PS7";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_SORT8_PARCEL_DAILY_RATE:
                //    return "RM0";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_LRG_LTR_DAILY_RATE:
                //    return "RM1";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_PARCEL_DAILY_RATE:
                //    return "RM2";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_LRG_LTR__DAILY_RATE:
                //    return "RM3";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_PARCEL_DAILY_RATE:
                //    return "RM4";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_PARCEL_FLAT_RATE:
                //    return "RM5";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_PARCEL_FLAT_RATE:
                //    return "RM6";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_SORT8_LRG_LTR_DAILY_RATE:
                //    return "RM7";
                //case RoyalMailServiceOffering.ROYAL_MAIL_24_SORT8_PARCEL_DAILY_RATE:
                //    return "RM8";
                //case RoyalMailServiceOffering.ROYAL_MAIL_48_SORT8_LRG_LTR_DAILY_RATE:
                //    return "RM9";
                case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_1PM:
                    return "SD1";
                //case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_1PM_1000GBP:
                //    return "SD2";
                //case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_1PM_2500GBP:
                //    return "SD3";
                case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_9AM:
                    return "SD4";
                //case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_9AM_1000GBP:
                //    return "SD5";
                //case RoyalMailServiceOffering.SPECIAL_DELIVERY_BY_9AM_2500GBP:
                //    return "SD6";
                case RoyalMailServiceOffering.STANDARD_1ST_AND_2ND_CLASS_ACCOUNT_MAIL:
                    return "STL  ";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_48_HV:
                //    return "TPL";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_24:
                //    return "TPN";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_48:
                //    return "TPS";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_24_HV:
                //    return "TRM";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_24_LBT:
                //    return "TRN";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_48_LBT:
                //    return "TRS";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_RETURNS_24:
                //    return "TSN";
                //case RoyalMailServiceOffering.ROYAL_MAIL_TRACKED_RETURNS_48:
                //    return "TSS";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_PRIORITY:
                //    return "WE1";
                //case RoyalMailServiceOffering.INTL_BUS_PARCELS_ZERO_SORT_ECONOMY:
                //    return "WE3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_PRIORITY:
                //    return "WG1";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_ECONOMY:
                //    return "WG3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_PRIORITY_MCH:
                //    return "WG4";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_LRG_LTR_ZERO_SORT_ECONOMY_MCH:
                //    return "WG6";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZERO_SORT_PRIORITY:
                //    return "WW1";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZERO_SORT_ECONOMY:
                //    return "WW3";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZERO_SORT_PRIORITY_MCH:
                //    return "WW4";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZERO_SORT_ECONOMY_MCH:
                //    return "WW6";
                //case RoyalMailServiceOffering.INTL_BUS_MAIL_MIXED_ZERO_SORT_PREMIUM:
                //    return "ZC1";
                default:
                    return "";
            }
        }

        internal static string RoyalMailInlandServiceFormatToCode(RoyalMailInlandServiceFormat Type)
        {
            switch (Type)
            {
                case RoyalMailInlandServiceFormat.Inland_Letter:
                    return "L";
                case RoyalMailInlandServiceFormat.Inland_Letter_Large:
                    return "F";
                case RoyalMailInlandServiceFormat.Inland_Parcel:
                    return "P";
                case RoyalMailInlandServiceFormat.Inland_Format_NA:
                    return "N";
                default:
                    return "";
            }
        }

        internal static string RoyalMailInternationalServiceFormatToCode(RoyalMailInternationalServiceFormat Type)
        {
            switch (Type)
            {
                case RoyalMailInternationalServiceFormat.International_Letter:
                    return "P";
                case RoyalMailInternationalServiceFormat.International_Letter_Large:
                    return "G";
                case RoyalMailInternationalServiceFormat.International_Parcel:
                    return "E";
                case RoyalMailInternationalServiceFormat.International_Format_NA:
                    return "N";
                default:
                    return "";
            }
        }

    }
}
