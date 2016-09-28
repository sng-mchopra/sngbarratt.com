using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Product
{
    public class ProductQuantityBreakDiscountLevel : AuditEntityBase
    {
        #region Properties
        [Key, Column(Order = 1)]
        public int Quantity { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal MinMargin { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Retail { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level2 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level3 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level4 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level5 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal Level6 { get; set; }

        public decimal UnitPrice(string tradingLevel, decimal retailPrice, decimal costPrice)
        {

            decimal price = retailPrice;

            decimal factor = 0.00m;

            switch (tradingLevel.Substring(0, 1))
            {
                case "R":
                    factor = this.Retail;
                    break;
                case "2":
                    factor = this.Level2;
                    break;
                case "3":
                    factor = this.Level3;
                    break;
                case "4":
                    factor = this.Level4;
                    break;
                case "5":
                    factor = this.Level5;
                    break;
                case "6":
                    factor = this.Level6;
                    break;
            }

            if (factor == 0) { factor = 100; }

            if (factor == 100)
            {
                price = retailPrice;
            }
            else
            {
                // calc discounted price
                price = Math.Round(retailPrice * (factor / 100), 2, MidpointRounding.AwayFromZero);
            }

            // check against min price
            decimal minPrice = Math.Round(costPrice * (1 + (this.MinMargin / 100)), 2, MidpointRounding.AwayFromZero);
            if (price >= minPrice)
            {
                return price;
            }
            else
            {
                return 0;
            }
        }

        [Index]
        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Navigation Properties
        [Key, Column(Order = 0)]
        [ForeignKey("ProductDetails")]
        public int Product_Id { get; set; }
        public virtual Product ProductDetails { get; set; }
        #endregion
    }
}
