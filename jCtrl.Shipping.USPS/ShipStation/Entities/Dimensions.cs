using System;

namespace jCtrl.Shipping.USPS.ShipStation
{
    internal enum DimensionUnits { inches, centimeters }

    internal class Dimensions
    {
        public Dimensions(decimal Length, decimal Width, decimal Height, DimensionUnits Units)
        {
            length = Length;
            width = Width;
            height = Height;
            units = Enum.GetName(typeof(DimensionUnits), Units);
        }

        public decimal length { get; set; }
        public decimal width { get; set; }
        public decimal height { get; set; }
        public string units { get; set; }
    }
}
