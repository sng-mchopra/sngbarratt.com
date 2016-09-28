using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jCtrl.Shipping.USPS.ShipStation;

namespace jCtrl.Shipping.USPS
{
    internal static class Labels
    {

        public static jCtrl.Shipping.USPS.Shipment CreateShipment(string key, string secret, CreateShipmentForm form)
        {

            var api = new ShipStationClient(key, secret);

            var shipment = api.CreateShipment(form);

            if(shipment != null)
            {
                return new jCtrl.Shipping.USPS.Shipment
                {
                    Id = shipment.shipmentId,
                    TrackingNo = shipment.trackingNumber,
                    CostPrice = shipment.shipmentCost + shipment.insuranceCost,
                    Label = shipment.labelData
                };
            }
                     
            return null;
        }

        public static bool VoidShipment(string key, string secret, int shipmentId)
        {

            var api = new ShipStationClient(key, secret);

            return api.VoidShipment(shipmentId);
            
        }

    }
}
