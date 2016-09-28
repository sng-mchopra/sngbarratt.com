using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Validation
{
    public static class PaymentCard
    {

        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
        }

        public static ValidationResult Validate(string CardNumber, int ExpiryMonth, int ExpiryYear, int? StartMonth, int? StartYear, int? IssueNumber)
        {

            // get last valid date
            var expDate = new DateTime(ExpiryYear, ExpiryMonth, 1).AddMonths(1).AddDays(-1);
            if (DateTime.UtcNow > expDate)
            {
                return new ValidationResult() { IsValid = false, Message = "Card has expired." };
            }

            DateTime? StartDate = null;
            if (StartMonth != null && StartYear != null)
            {
                StartDate = new DateTime((int)StartYear, (int)StartMonth, 1);
            }

            return ValidateCardDetails(CardNumber, IssueNumber, StartDate);

        }

        private static ValidationResult ValidateCardDetails(string CardNumber, int? IssueNumber, DateTime? StartDate)
        {

            if (!IsValidCardNumber(CardNumber))
            {
                return new ValidationResult() { IsValid = false, Message = "Card number not valid." };
            }

            // card number passes check digit validation, use WinTi INNTable to verify that
            // we have all the data required for the card number and we accept it

            // get the data from the embedded delimited text file
            string CardVerificationData = Properties.Resources.INNTable;

            // import the check data from the delimited file into our datatable
            string[] rows = CardVerificationData.Split('\n');
            //new line
            foreach (string r in rows)
            {
                var line = r.Trim();
                //if (line.EndsWith("\r")) line = line.Substring(0, line.Length - 2);
                
                if (line.Length > 0)
                {
                    string[] fields = line.Split(';');
                    if (fields.Length == 10)
                    {
                        int tmpIntLowRange = 0;
                        int.TryParse(fields[0], out tmpIntLowRange);
                        int LowRange = tmpIntLowRange;

                        int tmpIntHighRange = 0;
                        int.TryParse(fields[1], out tmpIntHighRange);
                        int HighRange = tmpIntHighRange;

                        int tmpIntLow = 0;
                        int.TryParse(CardNumber.Substring(0, LowRange.ToString().Length), out tmpIntLow);

                        int tmpIntHigh = 0;
                        int.TryParse(CardNumber.Substring(0, HighRange.ToString().Length), out tmpIntHigh);

                        if (tmpIntLow >= LowRange & tmpIntHigh <= HighRange)
                        {
                            // found 1st scheme for card number

                            string SchemeName = fields[8];
                            
                            bool Accepted = Convert.ToBoolean(Convert.ToInt16(fields[9]));

                            if (!Accepted)
                            {
                                return new ValidationResult() { IsValid = false, Message = "Card scheme not accepted: " + SchemeName };
                            }


                            string[] PanLengths = fields[2].Split(',');

                            int tmpIntMin = 0;
                            int.TryParse(PanLengths.First(), out tmpIntMin);
                            int MinPanLength = tmpIntMin;

                            int tmpIntMax = 0;
                            int.TryParse(PanLengths.Last(), out tmpIntMax);
                            int MaxPanLength = tmpIntMax;

                            if (CardNumber.Length < MinPanLength || CardNumber.Length > MaxPanLength)
                            {
                                return new ValidationResult() { IsValid = false, Message = "Invalid card number length: " + MinPanLength + "-" + MaxPanLength };
                            }

                            int tmpIntIssue = 0;
                            int.TryParse(fields[3], out tmpIntIssue);
                            int IssueLength = tmpIntIssue;
                            bool IssueRequired = IssueLength > 0 ? true : false;

                            if (IssueRequired)
                            {
                                if (IssueNumber == null)
                                {
                                    return new ValidationResult() { IsValid = false, Message = "Issue number required." };
                                }

                                if (IssueNumber.ToString().Length > IssueLength)
                                {
                                    return new ValidationResult() { IsValid = false, Message = "Issue number too long, max length " + IssueLength + "." };
                                }
                            }

                            int tmpIntStDate = 0;
                            int.TryParse(fields[4], out tmpIntStDate);
                            bool StartDateRequired = tmpIntStDate == 1 ? true : false;

                            if (StartDateRequired)
                            {
                                if (StartDate == null)
                                {
                                    return new ValidationResult() { IsValid = false, Message = "Start date required." };
                                }

                                if (StartDate > DateTime.UtcNow.Date)
                                {
                                    return new ValidationResult() { IsValid = false, Message = "Start date is in the future." };
                                }
                            }



                            //return new ValidationResult() { IsValid = true, Message = "Ok: " + SchemeName };
                            return new ValidationResult() { IsValid = true, Message = "Card details ok." };

                        }




                    }
                }
            }

            return new ValidationResult() { IsValid = true, Message = "Card scheme not found." };

        }

        private static bool IsValidCardNumber(string CardNumber)
        {

            // validate the card number's check digit using luhn's formula
            int iCount = 0;
            // substring is zero based
            int iCheckSum = 0;
            int iTemp = 0;
            int iLength = CardNumber.Length - 1;

            if (iLength % 2 == 0)
            {
                // even qty of digits, so double odd ones
                while (iCount <= iLength)
                {

                    if (iCount % 2 == 0)
                    {
                        // is even digit    
                        int tmpInt = 0;
                        int.TryParse(CardNumber.Substring(iCount, 1), out tmpInt);
                        iCheckSum += tmpInt;

                    }
                    else
                    {
                        // is odd number
                        int tmpInt = 0;
                        int.TryParse(CardNumber.Substring(iCount, 1), out tmpInt);
                        iTemp = tmpInt * 2;
                        if (iTemp > 9)
                        {
                            // substring is zero based
                            iCheckSum += Convert.ToInt32(iTemp.ToString().Substring(0, 1));
                            iCheckSum += Convert.ToInt32(iTemp.ToString().Substring(1, 1));
                        }
                        else
                        {
                            iCheckSum += iTemp;
                        }
                    }
                    iCount += 1;
                }

            }
            else
            {
                // odd qty of digits, so double even ones

                while (iCount <= iLength)
                {

                    if (iCount % 2 == 0)
                    {
                        // is even digit
                        int tmpInt = 0;
                        int.TryParse(CardNumber.Substring(iCount, 1), out tmpInt);
                        iTemp = tmpInt * 2;
                        if (iTemp > 9)
                        {
                            // substring is zero based
                            tmpInt = 0;
                            int.TryParse(iTemp.ToString().Substring(0, 1), out tmpInt);
                            iCheckSum += tmpInt;
                            tmpInt = 0;
                            int.TryParse(iTemp.ToString().Substring(1, 1), out tmpInt);
                            iCheckSum += tmpInt;
                        }
                        else
                        {
                            iCheckSum += iTemp;
                        }

                    }
                    else
                    {
                        // is odd number
                        int tmpInt = 0;
                        int.TryParse(CardNumber.Substring(iCount, 1), out tmpInt);
                        iCheckSum += tmpInt;
                    }
                    iCount += 1;
                }
            }

            if (iCheckSum % 10 == 0)
            {
                return true;
            }

            return false;
        }

    }
}
