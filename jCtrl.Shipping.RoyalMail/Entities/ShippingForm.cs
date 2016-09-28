using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Shipping.RoyalMail
{
    public class ShipmentForm
    {

        private RoyalMailShipmentType _shipmentType;
        private RoyalMailShipmentPurpose _shipmentPurpose;
        private RoyalMailServiceType _serviceType;
        private RoyalMailServiceOffering _serviceOffering;
        private List<RoyalMailServiceEnhancement> _serviceEnhancements;
        private RoyalMailInlandServiceFormat? _serviceFormat;
        private RoyalMailInternationalServiceFormat? _intlServiceFormat;
        private bool _isInternational;
        private DateTime _shippingDate;
        private bool _signatureRequired;
        private string _safePlace;
        private string _senderReference;
        private Recipient _recipient;
        private List<Package> _packages;

        #region "Constructors"

        public ShipmentForm(RoyalMailShipmentType ShipmentType, RoyalMailShipmentPurpose ShipmentPurpose, RoyalMailServiceType ServiceType, RoyalMailServiceOffering ServiceOffering, List<RoyalMailServiceEnhancement> ServiceEnhancements, DateTime ShippingDate, bool SignatureRequired, string SafePlace, string SendersReference, Recipient Recipient, List<Package> Packages)
        {
            _shipmentType = ShipmentType;
            _shipmentPurpose = ShipmentPurpose;
            _serviceType = ServiceType;
            _serviceOffering = ServiceOffering;
            _serviceEnhancements = ServiceEnhancements;
            _serviceFormat = null;
            _intlServiceFormat = null;
            _isInternational = false;
            _shippingDate = ShippingDate;
            _signatureRequired = SignatureRequired;
            _safePlace = SafePlace;
            _senderReference = SendersReference;
            _recipient = Recipient;
            _packages = Packages;

        }

        public ShipmentForm(RoyalMailShipmentType ShipmentType, RoyalMailShipmentPurpose ShipmentPurpose, RoyalMailServiceType ServiceType, RoyalMailServiceOffering ServiceOffering, List<RoyalMailServiceEnhancement> ServiceEnhancements, RoyalMailInlandServiceFormat ServiceFormat, DateTime ShippingDate, bool SignatureRequired, string SafePlace, string SendersReference, Recipient Recipient, List<Package> Packages)
        {
            _shipmentType = ShipmentType;
            _shipmentPurpose = ShipmentPurpose;
            _serviceType = ServiceType;
            _serviceOffering = ServiceOffering;
            _serviceEnhancements = ServiceEnhancements;
            _serviceFormat = ServiceFormat;
            _intlServiceFormat = null;
            _isInternational = false;
            _shippingDate = ShippingDate;
            _signatureRequired = SignatureRequired;
            _safePlace = SafePlace;
            _senderReference = SendersReference;
            _recipient = Recipient;
            _packages = Packages;

        }

        public ShipmentForm(RoyalMailShipmentType ShipmentType, RoyalMailShipmentPurpose ShipmentPurpose, RoyalMailServiceType ServiceType, RoyalMailServiceOffering ServiceOffering, List<RoyalMailServiceEnhancement> ServiceEnhancements, RoyalMailInternationalServiceFormat InternationalServiceFormat, DateTime ShippingDate, bool SignatureRequired, string SafePlace, string SendersReference, Recipient Recipient, List<Package> Packages)
        {
            _shipmentType = ShipmentType;
            _shipmentPurpose = ShipmentPurpose;
            _serviceType = ServiceType;
            _serviceOffering = ServiceOffering;
            _serviceEnhancements = ServiceEnhancements;
            _serviceFormat = null;
            _intlServiceFormat = InternationalServiceFormat;
            _isInternational = true;
            _shippingDate = ShippingDate;
            _signatureRequired = SignatureRequired;
            _safePlace = SafePlace;
            _senderReference = SendersReference;
            _recipient = Recipient;
            _packages = Packages;
        }

        #endregion



        public string ShipmentType
        {
            get { return Enum.GetName(typeof(RoyalMailShipmentType), _shipmentType); }
        }

        public string ShipmentPurpose
        {
            get { return _shipmentPurpose.ToString(); }
        }

        public string ServiceType
        {
            get { return EnumConversion.RoyalMailServiceTypeToCode(_serviceType); }
        }

        public string ServiceOffering
        {
            get { return EnumConversion.RoyalMailServiceOfferingToCode(_serviceOffering); }
        }

        public List<int> ServiceEnhancements
        {
            get
            {

                var lst = new List<int>();

                foreach (var e in _serviceEnhancements)
                {
                    lst.Add((int)e);
                }

                return lst;
            }
        }

        public string ServiceFormat
        {
            get
            {
                if (_isInternational)
                {
                    if (_intlServiceFormat != null)
                    {
                        return EnumConversion.RoyalMailInternationalServiceFormatToCode((RoyalMailInternationalServiceFormat)_intlServiceFormat);
                    }
                }
                else
                {
                    if (_serviceFormat != null)
                    {
                        return EnumConversion.RoyalMailInlandServiceFormatToCode((RoyalMailInlandServiceFormat)_serviceFormat);
                    }
                }

                return "";
            }
        }

        public bool IsInternational
        {
            get { return _isInternational; }
        }

        public DateTime ShippingDate
        {
            get { return _shippingDate; }
        }

        public bool SignatureRequired
        {
            get { return _signatureRequired; }
        }

        public string SafePlace
        {
            get { return _safePlace; }
        }

        public string SenderReference
        {
            get { return _senderReference; }
        }

        public Recipient Recipient
        {
            get { return _recipient; }
        }

        public List<Package> Packages
        {
            get { return _packages; }
        }

        public string GetServiceString()
        {
            var s = "Type: " + Enum.GetName(typeof(RoyalMailServiceType), _serviceType)
                + " Offering: " + Enum.GetName(typeof(RoyalMailServiceOffering), _serviceOffering);

            if (_isInternational)
            {
                s += " Format: " + Enum.GetName(typeof(RoyalMailInternationalServiceFormat), _intlServiceFormat);
            }
            else if (_serviceFormat != null)
            {
                s += " Format: " + Enum.GetName(typeof(RoyalMailInlandServiceFormat), _serviceFormat);
            }
            else
            {
                s += " Format: N/A";
            }

            s += " Enhancements:";
            foreach (var enhancement in _serviceEnhancements)
            {
                s += " " + Enum.GetName(typeof(RoyalMailServiceEnhancement), enhancement);
            }


            return s;
        }
    }
}
