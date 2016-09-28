using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace jCtrl.Shipping.Chronoship
{
    internal class ChronopostSupplement
    {
        private string _sServiceCode;
        private string _sDescription;
        private decimal _dAmount;
        private decimal _dAmountTTC;
        private decimal _dAmountTVA;

        public ChronopostSupplement(XmlNode xmlNode)
        {
            _dAmount = 0;
            _dAmountTTC = 0;
            _dAmountTVA = 0;
            _sServiceCode = string.Empty;
            _sDescription = "Unknown Supplement";


            if (xmlNode.HasChildNodes)
            {
                XmlNode nd = null;

                nd = xmlNode.SelectSingleNode("amount");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmount);
                }

                nd = xmlNode.SelectSingleNode("amountTTC");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmountTTC);
                }

                nd = xmlNode.SelectSingleNode("amountTVA");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmountTVA);
                }

                nd = xmlNode.SelectSingleNode("codeService");
                if ((nd != null))
                {
                    _sServiceCode = nd.InnerText;

                    switch (_sServiceCode.ToUpper())
                    {
                        case "P":
                            _sDescription = "Premium";
                            break;
                        case "1":
                            _sDescription = "Collect Return";
                            break;
                        case "4":
                            _sDescription = "Payment on Delivery";
                            break;
                        case "9":
                            _sDescription = "Corsica";
                            break;
                        case "11":
                            _sDescription = "Removing Office";
                            break;
                        case "12":
                        case "15":
                            _sDescription = "Saturday";
                            break;
                        case "B1":
                        case "B2":
                            _sDescription = "Private Address";
                            break;
                    }
                }


            }
        }

        public string Code
        {
            get { return _sServiceCode; }
        }
        public string Description
        {
            get { return _sDescription; }
        }
        public decimal Amount
        {
            get { return _dAmount; }
        }

    }

}
