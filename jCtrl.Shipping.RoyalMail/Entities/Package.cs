using System.Collections.Generic;

namespace jCtrl.Shipping.RoyalMail
{
    public class Package
    {
        public decimal HeightCms { get; set; }
        public decimal WidthCms { get; set; }
        public decimal DepthCms { get; set; }
        public decimal WeightKgs { get; set; }

        public List<ManifestItem> Manifest { get; set; }

    }
}
