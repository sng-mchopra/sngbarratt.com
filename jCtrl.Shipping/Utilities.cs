using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jCtrl.Shipping
{
    public static class Utilities
    {

        public static List<ShippingQuotePackage> PackCartItems(List<CartItem> items, List<PackingContainer> boxes, bool includeUps = false)
        {
            var packages = new List<ShippingQuotePackage>();
            var manifest = new List<PackageManifestItem>();

            // TODO: Allow for Packing buffers

            // calc totals
            int totalQuantity = 0;
            int countWithDims = 0;
            int countWithWeight = 0;
            decimal itemWeight = 0m;
            decimal itemVolume = 0m;

            foreach (CartItem itm in items)
            {

                var mi = new PackageManifestItem()
                {
                    PartNumber = itm.PartNumber,
                    Title = itm.PartTitle,
                    Quantity = itm.QuantityRequired,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    UpdatedTimestampUtc = DateTime.UtcNow
                };

                totalQuantity += itm.QuantityRequired;

                if (itm.BranchProduct != null)
                {
                    var productInfo = itm.BranchProduct.ProductDetails;
                    if (productInfo != null)
                    {

                        // check for dims
                        if (productInfo.PackedWidthCms > 0 && productInfo.PackedHeightCms > 0 && productInfo.PackedDepthCms > 0)
                        {
                            countWithDims += itm.QuantityRequired;
                            itemVolume += Math.Ceiling(productInfo.PackedWidthCms * productInfo.PackedHeightCms * productInfo.PackedDepthCms);

                            mi.PackedWidthCms = productInfo.PackedWidthCms;
                            mi.PackedHeightCms = productInfo.PackedHeightCms;
                            mi.PackedDepthCms = productInfo.PackedDepthCms;

                        }
                        else if (productInfo.ItemWidthCms > 0 && productInfo.ItemHeightCms > 0 && productInfo.ItemDepthCms > 0)
                        {
                            countWithDims += itm.QuantityRequired;
                            itemVolume += Math.Ceiling(productInfo.ItemWidthCms * productInfo.ItemHeightCms * productInfo.ItemDepthCms) * itm.QuantityRequired;

                            mi.PackedWidthCms = productInfo.ItemWidthCms;
                            mi.PackedHeightCms = productInfo.ItemHeightCms;
                            mi.PackedDepthCms = productInfo.ItemDepthCms;
                        }

                        // check for weight
                        if (productInfo.PackedWeightKgs > 0)
                        {
                            countWithWeight += itm.QuantityRequired;
                            itemWeight += productInfo.PackedWeightKgs * itm.QuantityRequired;

                            mi.PackedWeightKgs = productInfo.PackedWeightKgs;
                        }
                        else if (productInfo.ItemWeightKgs > 0)
                        {
                            countWithWeight += itm.QuantityRequired;
                            itemWeight += productInfo.ItemWeightKgs * itm.QuantityRequired;

                            mi.PackedWeightKgs = productInfo.ItemWeightKgs;
                        }

                        mi.Product_Id = productInfo.Id;
                        mi.UpdatedTimestampUtc = DateTime.UtcNow;

                        // add to manifest
                        manifest.Add(mi);

                    }
                }
            }

            // calc confidence
            decimal volumeConfidence = Math.Round((decimal)(countWithDims / totalQuantity), 2, MidpointRounding.AwayFromZero);
            decimal weightConfidence = Math.Round((decimal)(countWithWeight / totalQuantity), 2, MidpointRounding.AwayFromZero);


            // try and fit all items in a single box
            if (boxes.Any())
            {
                // find first option where all items fit

                IEnumerable<PackingContainer> filteredBoxes;
                
                if (includeUps == true)
                {
                    filteredBoxes = boxes
                        .Where(b =>
                            (b.InternalVolumeCm3 == 0 || b.InternalVolumeCm3 >= itemVolume)
                            && (b.MaxWeightKgs == 0 || b.MaxWeightKgs >= itemWeight)
                        );
                } else
                {
                    // exclude UPS only boxes
                    filteredBoxes = boxes
                        .Where(b =>
                            (b.InternalVolumeCm3 == 0 || b.InternalVolumeCm3 >= itemVolume)
                            && (b.MaxWeightKgs == 0 || b.MaxWeightKgs >= itemWeight)
                            && b.IsUpsOnly == false
                        );
                }


                // check each item will fit
                foreach (PackingContainer box in filteredBoxes)                    
                {

                    int packedQuantity = 0;
                    foreach (PackageManifestItem itm in manifest)
                    {
                        // TODO: pack each item or make sure that the dims of each item do not exceed dims of box
                        packedQuantity += itm.Quantity;
                    }

                    // add to list if all ok
                    if (packedQuantity == totalQuantity)
                    {
                        var pkg = new ShippingQuotePackage()
                        {
                            PackageNo = 1,
                            PackingContainer = box,
                            Manifest = manifest,
                            WidthCms = box.ExternalWidthCms,
                            HeightCms = box.ExternalHeightCms,
                            DepthCms = box.ExternalDepthCms,
                            WeightKgs = box.UnitWeightKgs + itemWeight,
                            VolumetricWeightKgs = box.WeightKgs_Volumetric(),
                            Confidence_Volume = volumeConfidence,
                            Confidence_Weight = weightConfidence,
                            RowVersion = 1,
                            CreatedTimestampUtc = DateTime.UtcNow,
                            UpdatedTimestampUtc = DateTime.UtcNow
                        };

                        packages.Add(pkg);
                        break;
                    }
                }
            }

            if (!packages.Any())
            {
                // items need multiple boxes

                // TODO: pack largest box and then try putting remaining items in a single box ???
            }

            return packages;
        }

        public static decimal CalcCartWeight_Kgs(List<CartItem> items)
        {
            decimal totalWeightKgs = 0m;

            // calc total weight from each cart item                                                   
            foreach (var item in items)
            {
                if (item.BranchProduct != null)
                {
                    if (item.BranchProduct.ProductDetails != null)
                    {

                        if (item.BranchProduct.ProductDetails.PackedWeightKgs > 0)
                        {
                            totalWeightKgs += item.BranchProduct.ProductDetails.PackedWeightKgs;
                        }
                        else
                        {
                            totalWeightKgs += item.BranchProduct.ProductDetails.ItemWeightKgs;
                        }
                    }
                }
            }

            return totalWeightKgs;
        }

    }
}
