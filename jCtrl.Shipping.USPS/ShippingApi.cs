using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jCtrl.Shipping.USPS
{
    public static class ShippingApi
    {

        private static bool isTest = true;

        #region "Rates"
        
        public static async Task<RatesResponse> GetRates(string userId, string userPassword, Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate = null)
        {
            var api = new Rates(userId, userPassword);
            return await api.GetRates(shipFrom, shipTo, packages, shipDate);            
        }

        public static async Task<RatesResponse> GetRates(string userId, string userPassword, string service, Address shipFrom, Address shipTo, List<Package> packages, DateTime? shipDate = null)
        {
            var api = new Rates(userId, userPassword);
            return await api.GetRates(service, shipFrom, shipTo, packages, shipDate);           
        }

        #endregion


        #region "Shipments"

        public static ShipmentResponse CreateShipment(string key, string secret, string service, Address shipFrom, Address shipTo, List<Package> packages, DateTime shipDate)
        {
            var result = new ShipmentResponse();

            var api = new ShipStation.ShipStationClient(key, secret);

            var lst = new List<Shipment>();

            foreach (var pkg in packages)
            {
                // build request shipment form
                var form = new ShipStation.CreateShipmentForm
                {
                    carrierCode = "stamps_com",
                    serviceCode = jCtrl.Shipping.USPS.Utilities.GetShipStationServiceCode(service, shipTo.IsUnitedStatesAddress()),
                    packageCode = jCtrl.Shipping.USPS.Utilities.GetShipStationContainerCode(pkg.Packaging, pkg.IsLargePackage),

                    dimensions = new ShipStation.Dimensions(pkg.Length, pkg.Width, pkg.Height, ShipStation.DimensionUnits.inches),
                    weight = new ShipStation.Weight(pkg.Weight, ShipStation.WeightUnits.pounds),

                    shipDate = shipDate.ToString("yyyy-MM-dd"),
                    shipFrom = new ShipStation.Address
                    {
                        name = shipFrom.Name,
                        company = shipFrom.CompanyName,
                        street1 = string.IsNullOrEmpty(shipFrom.AppartmentBuilding) ? shipFrom.StreetAddress : shipFrom.AppartmentBuilding,
                        street2 = string.IsNullOrEmpty(shipFrom.AppartmentBuilding) ? null : shipFrom.StreetAddress,
                        city = shipFrom.City,
                        state = shipFrom.State,
                        postalCode = shipFrom.ZipCode,
                        country = shipFrom.CountryCode,
                        residential = shipFrom.IsResidential
                    },
                    shipTo = new ShipStation.Address
                    {
                        name = shipTo.Name,
                        company = shipTo.CompanyName,
                        street1 = string.IsNullOrEmpty(shipTo.AppartmentBuilding) ? shipTo.StreetAddress : shipTo.AppartmentBuilding,
                        street2 = string.IsNullOrEmpty(shipTo.AppartmentBuilding) ? null : shipTo.StreetAddress,
                        city = shipTo.City,
                        state = shipTo.State,
                        postalCode = shipTo.ZipCode,
                        country = shipTo.CountryCode,
                        residential = shipTo.IsResidential
                    },
                    testLabel = isTest
                };

                var shipment = api.CreateShipment(form);

                if (shipment != null)
                {
                    lst.Add(new Shipment()
                    {
                        Id = shipment.shipmentId,
                        TrackingNo = shipment.trackingNumber,
                        CostPrice = shipment.shipmentCost + shipment.insuranceCost,
                        Label = shipment.labelData
                    });
                }
            }

            if (lst.Any())
            {
                if (lst.Count == packages.Count) result.Shipments = lst;                                            
                else
                {
                    // we don't have a label for every package                    
                    result.Errors.Add(new UspsError { Source = "CreateShipment", Description = "Label count does not match package count" });

                    // void the labels that have been created
                    foreach (var lbl in lst)
                    {
                        if (!api.VoidShipment(lbl.Id))
                        {
                            // unable to void the label
                            result.Errors.Add(new UspsError { Source = "VoidShipment", Description = "Unable to void label for ShipmentId: " + lbl.Id.ToString() + " TrackingNo: " + lbl.TrackingNo });
                        }
                    }
                }
            } else
            {
                // no labels 
                result.Errors.Add(new UspsError { Source = "CreateShipment", Description = "Unable to create shipment for " + packages.Count() + " package(s)" });
            }

            return result;
        }

        public static bool VoidShipment(string key, string secret, int shipmentId)
        {        
            var api = new ShipStation.ShipStationClient(key, secret);
            return api.VoidShipment(shipmentId);
        }

        #endregion


    }
}
