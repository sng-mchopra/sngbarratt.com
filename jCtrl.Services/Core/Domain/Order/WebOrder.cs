using jCtrl.Services.Core.Utils.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jCtrl.Services.Core.Domain.Order
{
    public class WebOrder : AuditEntityBase
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Index("Idx_WebOrder", Order = 1, IsUnique = true)]
        public int OrderNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Index("Idx_WebOrder", Order = 0, IsUnique = true)]
        public DateTime OrderDate { get; set; }

        [MaxLength(10)]
        public string InternalCustNo { get; set; }

        [MaxLength(25)]
        public string CustomerTaxNo { get; set; }

        [MaxLength(15)]
        public string CustomerOrderRef { get; set; }

        [Required]
        public bool CustomerConfirmationRequired { get; set; }
        public DateTime? CustomerConfirmationTimestamp { get; set; }

        [MinLength(5)]
        [MaxLength(5)]
        [Index("Idx_WebOrder_InternalQuote", Order = 1, IsUnique = true)]
        public string InternalQuoteNo { get; set; }
        [Index("Idx_WebOrder_InternalQuote", Order = 0, IsUnique = true)]
        [DataType(DataType.Date)]
        public DateTime? InternalQuoteDate { get; set; }

        [MinLength(5)]
        [MaxLength(5)]
        [Index("Idx_WebOrder_InternalOrder", Order = 1, IsUnique = true)]
        public string InternalOrderNo { get; set; }
        [Index("Idx_WebOrder_InternalOrder", Order = 0, IsUnique = true)]
        [DataType(DataType.Date)]
        public DateTime? InternalOrderDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string BillingName { get; set; }

        [Required]
        [MaxLength(100)]
        public string BillingAddressLine1 { get; set; }

        [MaxLength(100)]
        public string BillingAddressLine2 { get; set; }

        [MaxLength(30)]
        public string BillingTownCity { get; set; }

        [MaxLength(30)]
        public string BillingCountyState { get; set; }

        [MinLength(5)]
        [MaxLength(10)]
        public string BillingPostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string BillingCountryName { get; set; }

        [Required]
        [MaxLength(2)]
        public string BillingCountryCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string DeliveryName { get; set; }

        [Required]
        [MaxLength(100)]
        public string DeliveryAddressLine1 { get; set; }

        [MaxLength(100)]
        public string DeliveryAddressLine2 { get; set; }

        [MaxLength(30)]
        public string DeliveryTownCity { get; set; }

        [MaxLength(30)]
        public string DeliveryCountyState { get; set; }

        [MinLength(5)]
        [MaxLength(10)]
        public string DeliveryPostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeliveryCountryName { get; set; }

        [Required]
        [MaxLength(2)]
        public string DeliveryCountryCode { get; set; }

        [Required]
        [MaxLength(25)]
        public string DeliveryContactNumber { get; set; }

        public int ShippingMethod_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShippingMethodName { get; set; }

        [Required]
        [DecimalPrecision(8, 3)]
        public decimal EstimatedShippingWeightKgs { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal EstimatedShippingCost { get; set; }

        [Required]
        [DecimalPrecision(6, 2)]
        public decimal ShippingCharge { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal ShippingTaxRate { get; set; }


        [Required]
        [DecimalPrecision(10, 2)]
        public decimal GoodsAtRate1 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal GoodsTaxRate1 { get; set; }


        [Required]
        [DecimalPrecision(10, 2)]
        public decimal GoodsAtRate2 { get; set; }

        [Required]
        [DecimalPrecision(5, 2)]
        public decimal GoodsTaxRate2 { get; set; }

        [Required]
        [DecimalPrecision(10, 2)]
        public decimal DiscountTotal { get; set; }

        [Required]
        [DecimalPrecision(10, 2)]
        public decimal TaxTotal { get; set; }

        [Required]
        [DecimalPrecision(14, 2)]
        public decimal GrandTotal { get; set; }

        [Required]
        public ICollection<WebOrderEvent> OrderEvents { get; set; }

        [Required]
        public ICollection<WebOrderItem> Items { get; set; }
        #endregion

        #region Navigation Properties
        [Index]
        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }

        [Index]
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }

        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }

        [ForeignKey("CustomerVehicle")]
        public int? CustomerVehicle_Id { get; set; }
        public Domain.Customer.CustomerVehicle CustomerVehicle { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethod_Code { get; set; }
        public virtual Domain.Payment.PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("PaymentCard")]
        public int? PaymentCard_Id { get; set; }
        public virtual Domain.Payment.PaymentCard PaymentCard { get; set; }

        [ForeignKey("Voucher")]
        public int? Voucher_Id { get; set; }
        public virtual Domain.Voucher.Voucher Voucher { get; set; }

        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("Status")]
        public string WebOrderStatus_Id { get; set; }
        public virtual WebOrderStatus Status { get; set; }
        #endregion
    }
}