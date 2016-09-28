using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping
{
    public static class Shipments
    {

        public static class RoyalMail
        {
            public static async Task<string[]> RequestShipment(jCtrl.Shipping.RoyalMail.ShipmentForm form)
            {
                // should these be passed in or stored in the config?
                var username = "";
                var password = "";
                var applicationId = "";
                var endPoint = "";
                var apiVersion = "";
                var senderVatNo = "";

                var api = new jCtrl.Shipping.RoyalMail.ShippingApi(username, password, applicationId, endPoint, apiVersion, senderVatNo);

                return await api.CreateShipmentAsync(form);
            }

            // returns base64 string representation of pdf label
            public static async Task<string> ConfirmShipment(string shipmentNumber)
            {
                // should these be passed in or stored in the config?
                var username = "";
                var password = "";
                var applicationId = "";
                var endPoint = "";
                var apiVersion = "";
                var senderVatNo = "";

                var api = new jCtrl.Shipping.RoyalMail.ShippingApi(username, password, applicationId, endPoint, apiVersion, senderVatNo);

                return await api.AcceptShipmentAsync(shipmentNumber);
            }

            public static async Task<bool> CancelShipment(string shipmentNumber)
            {
                // should these be passed in or stored in the config?
                var username = "";
                var password = "";
                var applicationId = "";
                var endPoint = "";
                var apiVersion = "";
                var senderVatNo = "";

                var api = new jCtrl.Shipping.RoyalMail.ShippingApi(username, password, applicationId, endPoint, apiVersion, senderVatNo);

                return await api.VoidShipmentAsync(shipmentNumber);
            }

        }

    }
}
