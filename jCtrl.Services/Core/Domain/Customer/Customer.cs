using jCtrl.Services.Core.Domain.Order;
using jCtrl.Services.Core.Domain.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jCtrl.Services.Core.Domain.Customer
{
    public class Customer : Address
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Index]
        [MaxLength(10)]
        public string AccountNumber { get; set; }

        // for internal use
        [Index]
        [MaxLength(35)]
        public string AccountName { get; set; }

        [MaxLength(5)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Index]
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Index]
        [MaxLength(35)]
        public string CompanyName { get; set; }

        [MaxLength(25)]
        public string CompanyTaxNo { get; set; }

        public DateTime? CompanyTaxNo_LastChecked { get; set; }

        [MaxLength(200)]
        public string InternalComment { get; set; }

        [Index]
        [Required]
        public bool IsMarketingSubscriber { get; set; }

        [Required]
        public bool IsPaperlessBilling { get; set; }

        [Required]
        public bool IsCreditAccount { get; set; }

        [Required]
        public bool IsOnStop { get; set; }

        [Index]
        [Required]
        public bool IsActive { get; set; }

        [NotMapped]
        public CustomerVehicle DefaultVehicle
        {
            get { return Vehicles.FirstOrDefault(v => v.IsDefault == true); }
            set
            {
                foreach (CustomerVehicle v in (Vehicles.Where(v => v.IsDefault == true)))
                {
                    v.IsDefault = false;
                };

                value.IsDefault = true;
            }
        }

        [NotMapped]
        public CustomerShippingAddress DefaultShippingAddress
        {
            get
            {
                if (this.ShippingAddresses != null)
                {
                    return ShippingAddresses.SingleOrDefault(a => a.IsDefault == true);
                }

                return null;
            }
            set
            {
                foreach (CustomerShippingAddress a in (ShippingAddresses.Where(a => a.IsDefault == true)))
                {
                    a.IsDefault = false;
                }

                value.IsDefault = true;
            }
        }

        [NotMapped]
        public CustomerPhoneNumber DefaultPhoneNumber
        {
            get { return PhoneNumbers.SingleOrDefault(p => p.IsDefault == true); }
            set
            {
                foreach (CustomerPhoneNumber p in (PhoneNumbers.Where(p => p.IsDefault == true)))
                {
                    p.IsDefault = false;
                }

                value.IsDefault = true;
            }
        }

        [NotMapped]
        public CustomerEmailAddress DefaultEmailAddress
        {
            get { return EmailAddresses.SingleOrDefault(e => e.IsDefault == true); }
            set
            {
                foreach (CustomerEmailAddress e in (EmailAddresses.Where(e => e.IsDefault == true)))
                {
                    e.IsDefault = false;
                }

                value.IsDefault = true;
            }
        }

        [NotMapped]
        public PaymentCard DefaultPaymentCard
        {
            get { return PaymentCards.FirstOrDefault(p => p.IsDefault == true); }
            set
            {
                foreach (PaymentCard p in (PaymentCards.Where(p => p.IsDefault == true)))
                {
                    p.IsDefault = false;
                };

                value.IsDefault = true;
            }
        }

        public virtual ICollection<WebOrder> WebOrders { get; set; }
        public ICollection<CustomerVehicle> Vehicles { get; set; }
        public ICollection<CustomerEmailAddress> EmailAddresses { get; set; }
        public ICollection<CustomerShippingAddress> ShippingAddresses { get; set; }
        public ICollection<CustomerPhoneNumber> PhoneNumbers { get; set; }
        public ICollection<PaymentCard> PaymentCards { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [ForeignKey("AccountType")]
        public string CustomerAccountType_Code { get; set; }
        public virtual CustomerAccountType AccountType { get; set; }

        [Index]
        [Required]
        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Domain.Branch.Branch Branch { get; set; }

        [Required]
        [ForeignKey("Language")]
        public int Language_Id { get; set; }
        public virtual Language Language { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("PaymentMethod")]
        public string PaymentMethod_Code { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("ShippingMethod")]
        public int? ShippingMethod_Id { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [ForeignKey("TradingTerms")]
        public string TradingTerms_Code { get; set; }
        public virtual CustomerTradingLevel TradingTerms { get; set; }
        #endregion

        #region Methods 
        public ICollection<CustomerEmailAddress> EmailAddresses_Billing()
        {
            return EmailAddresses.Where(e => e.IsBilling == true).ToList();
        }
        #endregion
    }
}
