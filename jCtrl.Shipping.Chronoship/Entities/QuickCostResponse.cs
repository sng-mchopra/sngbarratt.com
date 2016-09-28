using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace jCtrl.Shipping.Chronoship
{
    internal class QuickCostResponse
    {

        private decimal _dAmount;
        private decimal _dAmountTTC;
        private decimal _dAmountTVA;
        private QuickCostReturnCode _eReturnCode;
        private string _sMessage;

        private List<ChronopostSupplement> _lSupplements;

        public QuickCostResponse(XmlDocument xmlDoc)
        {
            _dAmount = 0;
            _dAmountTTC = 0;
            _dAmountTVA = 0;
            _eReturnCode = QuickCostReturnCode.System_Error;
            _sMessage = "Unknown Error";
            _lSupplements = new List<ChronopostSupplement>();

            // get result node
            XmlNode ndResult = xmlDoc;
            if ((ndResult != null))
            {
                if (ndResult.HasChildNodes)
                {
                    ndResult = ndResult.FirstChild;
                }
            }
            if ((ndResult != null))
            {
                if (ndResult.HasChildNodes)
                {
                    ndResult = ndResult.FirstChild;
                }
            }
            if ((ndResult != null))
            {
                if (ndResult.HasChildNodes)
                {
                    ndResult = ndResult.FirstChild;
                }
            }
            if ((ndResult != null))
            {
                if (ndResult.HasChildNodes)
                {
                    ndResult = ndResult.FirstChild;
                }
            }

            if ((ndResult != null))
            {
                XmlNode nd = null;

                nd = ndResult.SelectSingleNode("amount");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmount);
                }

                nd = ndResult.SelectSingleNode("amountTTC");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmountTTC);
                }

                nd = ndResult.SelectSingleNode("amountTVA");
                if ((nd != null))
                {
                    decimal.TryParse(nd.InnerText, out _dAmountTVA);
                }

                nd = ndResult.SelectSingleNode("errorCode");
                if ((nd != null))
                {
                    //_eReturnCode = Enum.Parse(typeof(ChronopostReturnCode), nd.InnerText);
                    _eReturnCode = (QuickCostReturnCode)int.Parse(nd.InnerText);
                }

                nd = ndResult.SelectSingleNode("errorMessage");
                if ((nd != null))
                {
                    _sMessage = nd.InnerText;
                }

                XmlNodeList lst = ndResult.SelectNodes("service");
                if ((lst != null))
                {
                    ChronopostSupplement supp = null;
                    foreach (XmlNode node in lst)
                    {
                        supp = new ChronopostSupplement(node);
                        if (supp.Amount > 0)
                        {
                            _lSupplements.Add(supp);
                        }
                    }
                }

            }

        }

        public QuickCostReturnCode Response
        {
            get { return _eReturnCode; }
        }
        public string Message
        {
            get { return _sMessage; }
        }
        public decimal Amount
        {
            get { return _dAmount; }
        }
        public List<ChronopostSupplement> Supplements
        {
            get { return _lSupplements; }
        }

    }


}
