using System;


namespace jCtrl.Shipping.USPS.ShipStation
{
    public enum WeightUnits { pounds, ounces, grams }

    public class Weight
    {

        public Weight(decimal Weight, WeightUnits Units)
        {
            value = Weight;            
            units = Enum.GetName(typeof(WeightUnits), Units);
        }

        public decimal value { get; set; }
        public string units { get; set; }
        
    }
}
