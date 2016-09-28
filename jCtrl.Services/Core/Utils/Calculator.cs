using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Utils
{
    public static class Calculator
    {
        public static decimal ConvertLength_Cms_to_Ins(decimal lengthCms)
        {
            return Math.Round(lengthCms / 2.254m, 3, MidpointRounding.AwayFromZero);
        }

        public static decimal ConvertWeight_Kgs_to_Lbs(decimal weightKgs)
        {
            return Math.Round(weightKgs * 2.20462m, 3, MidpointRounding.AwayFromZero);
        }
        public static decimal ConvertWeight_Lbs_to_Kgs(decimal weightLbs)
        {
            return Math.Round(weightLbs / 2.20462m, 3, MidpointRounding.AwayFromZero);
        }

        public static decimal CalculateVolume(decimal width, decimal height, decimal depth)
        {
            return Math.Round((width * height * depth), 2, MidpointRounding.AwayFromZero);
        }
        public static decimal CalculateVolumetricWeight_Kgs(decimal widthCm, decimal heightCm, decimal depthCm)
        {
            // using UPS factor of 5000, some use 6000
            return Math.Round(CalculateVolume(widthCm, heightCm, depthCm) / 5000, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal CalculateVolumetricWeight_Lbs(decimal widthCm, decimal heightCm, decimal depthCm)
        {
            return ConvertWeight_Kgs_to_Lbs(CalculateVolumetricWeight_Kgs(widthCm, heightCm, depthCm));
        }
    }
}
