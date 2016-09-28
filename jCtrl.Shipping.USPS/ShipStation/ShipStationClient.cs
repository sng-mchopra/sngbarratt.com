using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace jCtrl.Shipping.USPS.ShipStation
{
    internal class ShipStationClient
    {
        private const string PRODUCTION_ENDPOINT = "https://ssapi.shipstation.com";
        private readonly string AuthHash;
        
        public ShipStationClient (string key, string secret)
        {
            AuthHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(key + ":" + secret));            
        }

        public List<Rate> GetRates(GetRatesForm form)
        {
            var lst = new List<Rate>();

            using (var client = new WebClient())
            {
                var url = PRODUCTION_ENDPOINT + "/shipments/getrates";

                client.Headers.Add("Authorization", "Basic " + AuthHash);
                client.Headers.Add("content-type", "application/json");

                var json = JsonConvert.SerializeObject(
                    form, 
                    Newtonsoft.Json.Formatting.None, 
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );


                var resp = Encoding.UTF8.GetString(client.UploadData(url, "POST", Encoding.Default.GetBytes(json)));

                

                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response =  client.PostAsync(url, content).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    dynamic options = JsonConvert.DeserializeObject(
                //        response.Content.ReadAsStringAsync()
                //        .Result);

                //    foreach (var opt in options)
                //    {
                //        var rate = new Rate
                //        {
                //            Code = opt.serviceCode,                            
                //            Name = opt.serviceName,
                //            Container = "VARIABLE",
                //            ShippingCost = opt.shipmentCost,
                //            OtherCost = opt.otherCost
                //        };

                //        // get container type from service name
                //        if (rate.Name.Contains(" - "))
                //        {
                //            var s = rate.Name.Split('-');

                //            if (s.Length >= 2)
                //            {
                //                rate.Name = s[0].Trim();
                //                rate.Container = s[1].Trim();
                //            }
                //        }

                //        // remove USPS prefix
                //        if(rate.Name.StartsWith("USPS "))
                //        {
                //            rate.Name = rate.Name.Substring(5);
                //        }

                //        // add to list
                //        lst.Add(rate);
                //    }
                //}

            }

            return lst;
        }

        public Shipment CreateShipment(CreateShipmentForm form)
        {            

            using (var client = new WebClient())
            {
                var url = PRODUCTION_ENDPOINT + "/shipments/createlabel";

                client.Headers.Add("Authorization", "Basic " + AuthHash);
                client.Headers.Add("content-type", "application/json");

                var json = JsonConvert.SerializeObject(
                    form,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include }
                );

                var data = Encoding.UTF8.GetBytes(json);

                var bytes = client.UploadData(url, "POST", data);

                var resp = Encoding.UTF8.GetString(bytes);

                System.Diagnostics.Trace.WriteLine(resp);

                Shipment shipment;

                try
                {
                    shipment = JsonConvert.DeserializeObject<Shipment>(resp);
                    return shipment;
                }
                catch (Exception e)
                {
                    // do nothing
                    System.Diagnostics.Trace.WriteLine("ERROR: " + e.Message);
                }
                

                
                


                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = client.PostAsync(url, content).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //dynamic shipment = JsonConvert.DeserializeObject(resp);
                    //response.Content.ReadAsStringAsync()
                    //.Result);

                //return new Shipment
                //{
                //    shipmentId = (int)shipment.shipmentId,
                //    //orderId = (int)shipment.orderId,
                //    //userId = (int)shipment.userId,
                //    //customerEmail = shipment.customerEmail,
                //    //orderNumber = shipment.orderNumber,
                //    createDate = shipment.createDate,
                //    shipDate = shipment.shipDate,
                //    shipmentCost = shipment.shipmentCost,
                //    insuranceCost = shipment.insuranceCost,
                //    trackingNumber = shipment.trackingNumber,
                //    //isReturnLabel = (bool)shipment.isReturnLabel,
                //    //batchNumber = shipment.batchNumber,
                //    carrierCode = shipment.carrierCode,
                //    serviceCode = shipment.serviceCode,
                //    packageCode = shipment.packageCode,
                //    confirmation = shipment.confirmation,
                //    //warehouseId = (int)shipment.warehouseId,
                //    voided = (bool)shipment.voided,
                //    voidDate = shipment.voidDate,
                //    //marketplaceNotified = (bool)shipment.marketplaceNotified,
                //    notifyErrorMessage = shipment.notifyErrorMessage,
                //    shipTo = (Address)shipment.shipTo,
                //    weight = (Weight)shipment.weight,
                //    dimensions = (Dimensions)shipment.dimensions,
                //    insuranceOptions = shipment.insuranceOptions,
                //    advancedOptions = shipment.advancedOptions,
                //    //shipmentItems = shipment.shipmentItems,
                //    labelData = shipment.labelData//,
                //                                  //formData = shipment.formData

                //};


                //}

            }

            return null;
        }

        public bool VoidShipment(int shipmentId)
        {

            using (var client = new WebClient())
            {
                var url = PRODUCTION_ENDPOINT + "/shipments/voidlabel";

                client.Headers.Add("Authorization", "Basic " + AuthHash);
                client.Headers.Add("content-type", "application/json");

                var json = "{ \"shipmentId\": " + shipmentId + " }";

                var resp = Encoding.UTF8.GetString(client.UploadData(url, "POST", Encoding.Default.GetBytes(json)));

                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = client.PostAsync(url, content).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    dynamic result = JsonConvert.DeserializeObject(
                //        response.Content.ReadAsStringAsync()
                //        .Result);

                //    if(result.message != null)
                //    {                        
                //        System.Diagnostics.Debug.WriteLine((string)result.message);
                //    }

                //    return result.approved;

                //}

            }

            return false;
        }

    }
}
