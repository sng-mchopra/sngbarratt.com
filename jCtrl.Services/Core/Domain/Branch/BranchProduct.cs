using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jCtrl.Services.Core.Domain.Branch
{
    public class BranchProduct : EntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Index]
        [Required]
        [DecimalPrecision(8, 2)]
        public decimal RetailPrice { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal TradePrice { get; set; }

        [Index]
        [Required]
        [DecimalPrecision(8, 2)]
        public decimal ClearancePrice { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal AvgCostPrice { get; set; }

        [Required]
        [DecimalPrecision(8, 2)]
        public decimal Surcharge { get; set; }

        [Required]
        public int MinStockLevel { get; set; }
        [Required]
        public int MaxStockLevel { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties 
        [Index("Idx_BranchProduct", 1, IsUnique = true)]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }

        [Index("Idx_BranchProduct", 2, IsUnique = true)]
        [ForeignKey("ProductDetails")]
        public int Product_Id { get; set; }
        public virtual Domain.Product.Product ProductDetails { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("BranchStatus")]
        public string BranchStatus_Code { get; set; }
        public virtual ProductStatus BranchStatus { get; set; }

        [ForeignKey("BranchProduct_Id")]
        public virtual ICollection<BranchProductOffer> SpecialOffers { get; set; }
        [ForeignKey("BranchProduct_Id")]
        public virtual ICollection<WebOrderItem> WebOrderLines { get; set; }
        #endregion

        #region Methods 
        public decimal UnitPrice(string tradingLevel)
        {
            if (BranchStatus_Code == "S" || BranchStatus_Code == "X") return 0.00m;

            decimal price = this.RetailPrice;

            decimal factor = 0.00m;

            if (this.ProductDetails != null)
            {
                if (this.ProductDetails.DiscountLevel != null)
                {
                    switch (tradingLevel.Substring(0, 1))
                    {
                        case "R":
                            factor = this.ProductDetails.DiscountLevel.Retail;
                            break;
                        case "2":
                            factor = this.ProductDetails.DiscountLevel.Level2;
                            break;
                        case "3":
                            factor = this.ProductDetails.DiscountLevel.Level3;
                            break;
                        case "4":
                            factor = this.ProductDetails.DiscountLevel.Level4;
                            break;
                        case "5":
                            factor = this.ProductDetails.DiscountLevel.Level5;
                            break;
                        case "6":
                            factor = this.ProductDetails.DiscountLevel.Level6;
                            break;
                    }
                }
            }

            if (factor == 0) { factor = 100; }

            if (factor == 100)
            {
                price = this.RetailPrice;
            }
            else
            {
                // calc discounted price
                price = Math.Round(this.RetailPrice * (factor / 100), 2, MidpointRounding.AwayFromZero);
            }

            if (tradingLevel.EndsWith("Y"))
            {
                // apply Trade Net price
                if (this.TradePrice < price)
                {
                    price = this.TradePrice;
                }
            }

            if (this.SpecialOffers != null)
            {
                if (this.SpecialOffers.Any())
                {
                    if (this.SpecialOffers.Where(o => o.IsActive == true && o.ExpiryDate > DateTime.Now.Date).Any())
                    {

                        var offer = this.SpecialOffers.Where(o => o.IsActive == true && o.ExpiryDate > DateTime.Now.Date).OrderBy(o => o.ExpiryDate).FirstOrDefault();

                        if (offer != null)
                        {
                            // apply offer price
                            if (offer.OfferPrice < price) { price = offer.OfferPrice; }
                        }

                    }
                }
            }

            // TODO: maintenance task to clear clearance price when all qty has been sold
            // if a clearance price is setup and we still have stock
            if (this.ClearancePrice > 0 && this.Quantity > 0)
            {
                // apply clearance price
                if (this.ClearancePrice < price)
                {
                    price = this.ClearancePrice;
                }
            }

            return price;
        }

        public string StockStatus()
        {
            if (this.ProductDetails != null)
            {
                if (this.ProductDetails.PartStatus_Code == "F")
                {
                    // frozen
                    return "NAO"; // Not available online
                }
            }


            if (this.BranchStatus_Code == "Z")
            {
                return "NLA"; // Turned down

            }
            else if (this.BranchStatus_Code == "S")
            {
                return "SUP"; // Superceded
            }
            else if (this.Quantity > 0 && this.MinStockLevel > 0 && this.Quantity >= this.MinStockLevel)
            {
                // has min level and current stock qty is more than min level
                return "STK"; // In Stock
            }
            else if (this.Quantity > 0 && this.MinStockLevel > 0 && this.Quantity < this.MinStockLevel)
            {
                // has min level and some qty in stock but less than min level
                return "LOW"; // Low Stock
            }
            else if (this.Quantity == 0 && this.MinStockLevel > 0)
            {
                // has min level and no quantity

                if (this.ProductDetails != null)
                {
                    if (this.ProductDetails.ProductType_Code == "GJ")    // genuine jaguar, assume supplied by JAG
                    {
                        return "UDW"; // Usually dispatched within x days
                    }
                }

                return "OOS"; // Back Soon - Out of Stock
            }
            else if (this.Quantity > 0 && this.MinStockLevel == 0)
            {
                // not a normally stocked item with qty surplus
                return "STK"; // In Stock
            }
            else if (this.Quantity == 0 && this.MinStockLevel == 0)
            {
                // not stocked and no qty

                if (this.ProductDetails != null)
                {
                    if (this.ProductDetails.ProductType_Code == "GJ")    // genuine jaguar, assume supplied by JAG
                    {
                        return "UDW"; // Usually dispatched within x days
                    }
                }

                return "SOO"; // Special order only                              
            }
            else
            {
                return "NAO"; // default to not available
            }
        }
        #endregion
    }
}
