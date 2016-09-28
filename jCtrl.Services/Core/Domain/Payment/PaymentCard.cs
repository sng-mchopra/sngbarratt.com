using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jCtrl.Services.Core.Domain.Payment
{
    public class PaymentCard : EntityBase
    {
        #region Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(25)]
        public string DisplayName { get; set; }

        [Required]
        public string EncyptedData { get; set; }

        [Index]
        [Required]
        public bool IsDefault { get; set; }

        //Following fields are not mapped 
        [NotMapped]
        [CreditCard]
        [MinLength(16)]
        [MaxLength(22)] // TODO: Check the max length
        public string CardNumber { get; set; }
        public string CardNumberEnds()
        {
            return CardNumber.Substring(CardNumber.Length - 4);
        }
        public string CardNumberMasked()
        {
            return CardNumber.Substring(0, 2) + "xx-xxxx-xxxx-" + CardNumberEnds();
        }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [NotMapped]
        public int? IssueNumber { get; set; }

        [NotMapped]
        [MinLength(3)]
        [MaxLength(4)]
        public string Cv2Number { get; set; }
        #endregion

        #region Navigation Properties 
        [Index]
        [Required]
        [ForeignKey("Customer")]
        public Guid Customer_Id { get; set; }
        public virtual Domain.Customer.Customer Customer { get; set; }
        #endregion

        #region Methods
        public void Encrypt(string key, string salt)
        {
            var wtr = new StringBuilder();

            wtr.Append(Customer_Id);
            wtr.Append(",");
            wtr.Append(CardNumber);
            wtr.Append(",");
            wtr.Append(ExpiryDate.Value.Month);
            wtr.Append(",");
            wtr.Append(ExpiryDate.Value.Year);
            wtr.Append(",");
            if (StartDate != null)
            {
                wtr.Append(StartDate.Value.Month);
                wtr.Append(",");
                wtr.Append(StartDate.Value.Year);
            }
            else
            {
                wtr.Append(",");
            }
            if (IssueNumber != null)
            {
                wtr.Append(IssueNumber.Value);
            }
            wtr.Append(",");
            if (Cv2Number != null)
            {
                wtr.Append(Cv2Number);
            }
            wtr.Append(",");

            EncyptedData = Utils.Encryptor.Encrypt(wtr.ToString(), key, salt);
        }

        public void Decrypt(string key, string salt)
        {
            if (EncyptedData != null)
            {
                var clearText = Utils.Encryptor.Decrypt(EncyptedData, key, salt);

                if (clearText != null)
                {
                    var fields = clearText.Split(',');

                    CardNumber = fields[1];

                    var Yr = int.Parse(fields[3]);
                    var Mth = int.Parse(fields[2]);

                    if (Yr.ToString().Length == 2)
                    {
                        Yr = int.Parse(string.Concat(DateTime.UtcNow.Year.ToString().Substring(0, 2), Yr.ToString()));
                    }

                    if (Yr.ToString().Length == 4)
                    {
                        ExpiryDate = new DateTime(Yr, Mth, 1).AddMonths(1).AddDays(-1);
                    }

                    StartDate = null;
                    if (fields[4] != null && fields[5] != null)
                    {
                        if (fields[4] != string.Empty && fields[5] != string.Empty)
                        {
                            Yr = int.Parse(fields[5]);
                            Mth = int.Parse(fields[4]);

                            if (Yr.ToString().Length == 2)
                            {
                                Yr = int.Parse(string.Concat(DateTime.UtcNow.Year.ToString().Substring(0, 2), Yr.ToString()));
                            }

                            if (Yr.ToString().Length == 4)
                            {
                                StartDate = new DateTime(Yr, Mth, 1);
                            }
                        }
                    }

                    IssueNumber = null;
                    if (fields[6] != null && fields[6] != string.Empty)
                    {
                        IssueNumber = int.Parse(fields[6]);
                    }

                    Cv2Number = null;
                    if (fields[7] != null && fields[7] != string.Empty)
                    {
                        Cv2Number = fields[7];
                    }
                }
            }
        }

        public void Reset()
        {
            CardNumber = null;
            ExpiryDate = null;
            StartDate = null;
            IssueNumber = null;
            Cv2Number = null;
        }
        #endregion
    }
}
