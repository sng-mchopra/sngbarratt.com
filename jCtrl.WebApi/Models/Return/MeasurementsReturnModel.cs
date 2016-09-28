using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class MeasurementsReturnModel
    {

        public decimal HeightCms { get; set; }
        public decimal WidthCms { get; set; }
        public decimal DepthCms { get; set; }
        public decimal? WeightKgs { get; set; }

    }
}