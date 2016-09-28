using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jCtrl.Shipping.USPS
{


    public class Package
    {
        /// <summary>
        ///     Creates a new package object.
        /// </summary>
        /// <param name="length">The length of the package, in inches.</param>
        /// <param name="width">The width of the package, in inches.</param>
        /// <param name="height">The height of the package, in inches.</param>
        /// <param name="weight">The weight of the package, in pounds.</param>
        /// <param name="insuredValue">The insured-value of the package, in dollars.</param>
        /// <param name="container">A specific packaging from a shipping provider. E.g. "LG FLAT RATE BOX" for USPS</param>
        public Package(int length_Inches, int width_Inches, int height_Inches, int weight_Lbs, decimal insuredValue, PackageType container) : this(length_Inches, width_Inches, height_Inches, (decimal)weight_Lbs, insuredValue, container)
        {
        }

        /// <summary>
        ///     Creates a new package object.
        /// </summary>
        /// <param name="length">The length of the package, in inches.</param>
        /// <param name="width">The width of the package, in inches.</param>
        /// <param name="height">The height of the package, in inches.</param>
        /// <param name="weight">The weight of the package, in pounds.</param>
        /// <param name="insuredValue">The insured-value of the package, in dollars.</param>
        /// <param name="container">A specific packaging from a shipping provider. E.g. "LG FLAT RATE BOX" for USPS</param>
        public Package(decimal length_Inches, decimal width_Inches, decimal height_Inches, decimal weight_Lbs, decimal insuredValue, PackageType container)
        {
            Length = length_Inches;
            Width = width_Inches;
            Height = height_Inches;
            Weight = weight_Lbs;
            InsuredValue = insuredValue;
            Packaging = container;
        }

        public decimal CalculatedGirth { get { return Math.Ceiling((Width * 2) + (Height * 2)); } }
        public decimal Height { get; set; }
        public decimal InsuredValue { get; set; }
        public bool IsOversize { get; set; }
        public bool IsRectangular { get; set; }
        public decimal Length { get; set; }
        public decimal RoundedHeight
        {
            get { return Math.Ceiling(Height); }
        }
        public decimal RoundedLength
        {
            get { return Math.Ceiling(Length); }
        }
        public decimal RoundedWeight
        {
            get { return Math.Ceiling(Weight); }
        }
        public decimal RoundedWidth
        {
            get { return Math.Ceiling(Width); }
        }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        
        public PackageType Packaging { get; set; }

        //internal string Container
        //{
        //    get
        //    {
        //        switch (Packaging)
        //        {

        //            case PackageType.Unknown:
        //                return "variable";
        //            case PackageType.Own_Packaging:
        //                return "rectangular or nonrectangular";
        //            case PackageType.Flat_Rate_Envelope:
        //                return "flat rate envelope";
        //            case PackageType.Flat_Rate_Envelope_Padded:
        //                return "padded flat rate envelope";
        //            case PackageType.Flat_Rate_Envelope_Legal:
        //                return "legal flat rate envelope";
        //            //case PackageType.Flat_Rate_Envelope_Small:
        //            //    return "sm flat rate envelope";
        //            //case PackageType.Flat_Rate_Box:
        //            //    return "flat rate box";
        //            case PackageType.Flat_Rate_Box_Small:
        //                return "sm flat rate box";
        //            case PackageType.Flat_Rate_Box_Medium:
        //                return "md flat rate box";
        //            case PackageType.Flat_Rate_Box_Large:
        //                return "lg flat rate box";
        //            case PackageType.Regional_Rate_Box_A:
        //                return "regionalrateboxa";
        //            case PackageType.Regional_Rate_Box_B:
        //                return "regionalrateboxb";
        //            //case PackageType.Regional_Rate_Box_C:
        //            //    return "regionalrateboxc";
        //            default:
        //                return "variable";
        //        }
        //    }
        
        //}

        public PoundsAndOunces PoundsAndOunces
        {
            get
            {
                var poundsAndOunces = new PoundsAndOunces();
                if (Weight <= 0)
                {
                    return poundsAndOunces;
                }

                poundsAndOunces.Pounds = (int)Math.Truncate(Weight);
                var decimalPart = (Weight - poundsAndOunces.Pounds) * 16;

                poundsAndOunces.Ounces = (int)Math.Ceiling(decimalPart);

                return poundsAndOunces;
            }
        }


        public bool IsLargePackage { get { return (this.IsOversize || this.Width > 12 || this.Length > 12 || this.Height > 12); } }
        public bool IsMachinablePackage
        {
            get
            {
                // Machinable parcels cannot be larger than 27 x 17 x 17 and cannot weight more than 25 lbs.
                if (this.Weight > 25)
                {
                    return false;
                }

                return (this.Width <= 27 && this.Height <= 17 && this.Length <= 17) || (this.Width <= 17 && this.Height <= 27 && this.Length <= 17) || (this.Width <= 17 && this.Height <= 17 && this.Length <= 27);
            }
        }
    }
}
