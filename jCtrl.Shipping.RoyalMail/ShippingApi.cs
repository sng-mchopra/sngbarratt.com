using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using jCtrl.Shipping.RoyalMail.RoyalMailShippingApi;

namespace jCtrl.Shipping.RoyalMail
{
    public class ShippingApi
    {

        private string _username;
        private string _passsword;
        private string _endPoint;
        private string _applicationId;
        private string _apiVersion;
        private string _senderVatNo;

        public ShippingApi(string username, string password, string applicationId, string endPoint, string apiVersion, string senderVatNo)
        {

            _username = username;
            _passsword = password;
            _applicationId = applicationId;
            _endPoint = endPoint;
            _apiVersion = apiVersion;
            _senderVatNo = senderVatNo;
        }

        public async Task<string[]> CreateShipmentAsync(ShipmentForm form)
        {

            Trace.WriteLine(form.GetServiceString());

            shippingAPIPortTypeClient client = GetProxy();

            try
            {

                #region "Recipient Contact Details"

                var recipientContact = new contact();
                if (form.Recipient != null)
                {
                    recipientContact.name = form.Recipient.Name;

                    if (form.Recipient.EmailAddress != null)
                    {
                        recipientContact.electronicAddress = new digitalAddress()
                        {
                            electronicAddress = form.Recipient.EmailAddress
                        };
                    }

                    if (form.Recipient.PhoneNumber != null)
                    {
                        Regex phoneRegex = new Regex(@"[^\d]");

                        recipientContact.telephoneNumber = new telephoneNumber()
                        {
                            countryCode = form.Recipient.PhoneNumber.CountryCode,
                            telephoneNumber1 = phoneRegex.Replace(form.Recipient.PhoneNumber.AreaCode + form.Recipient.PhoneNumber.Number, "")
                        };
                    }
                }

                #endregion

                #region "Recipient Address"

                var recipientAddress = new address()
                {
                    addressLine1 = form.Recipient.AddressLine1,
                    addressLine2 = form.Recipient.AddressLine2,
                    addressLine3 = form.Recipient.TownCity,
                    addressLine4 = form.Recipient.CountyState,
                    postTown = form.Recipient.TownCity,
                    postcode = form.Recipient.PostalCode,
                    stateOrProvince = new stateOrProvinceType() { stateOrProvinceCode = new referenceDataType() },
                    country = new countryType() { countryCode = new referenceDataType() { code = form.Recipient.Country_Code } }
                };

                #endregion

                #region "Service Enhancements"

                var serviceEnhancements = new List<serviceEnhancementType>();
                foreach (var enhancement in form.ServiceEnhancements)
                {
                    serviceEnhancements.Add(new serviceEnhancementType()
                    {
                        serviceEnhancementCode = new referenceDataType()
                        {
                            code = enhancement.ToString()
                        }
                    });
                }

                #endregion



                #region "Shipment"

                var shipment = new requestedShipment()
                {
                    shipmentType = new referenceDataType() { code = form.ShipmentType },
                    //// Service Occurence (Identifies Agreement on Customers Account) Default to 1. Not Required If There Is There Is Only 1 On Account
                    serviceOccurrence = "1",
                    serviceType = new referenceDataType() { code = form.ServiceType },
                    // Service Offering (See Royal Mail Service Offering Type Codes. Too Many To List)
                    serviceOffering = new serviceOfferingType() { serviceOfferingCode = new referenceDataType() { code = form.ServiceOffering } },
                    serviceEnhancements = serviceEnhancements.ToArray(),
                    shippingDate = form.ShippingDate,
                    shippingDateSpecified = true,
                    // Signature Required (Only Available On Tracked Services)
                    signature = form.SignatureRequired,
                    signatureSpecified = true,
                    safePlace = form.SafePlace,
                    senderReference = form.SenderReference,
                    recipientContact = recipientContact,
                    recipientAddress = recipientAddress
                };

                #region "Service Format"

                if (!form.ServiceFormat.Equals(""))
                {
                    // Service Format Code
                    shipment.serviceFormat = new serviceFormatType() { serviceFormatCode = new referenceDataType() { code = form.ServiceFormat } };
                }

                #endregion

                var packageCount = form.Packages.Count();
                var totalWeight = 0.0m;
                foreach (var pkg in form.Packages) { totalWeight += pkg.WeightKgs; }

                var items = new List<item>()
                    {
                        new item() {
                            numberOfItems = packageCount.ToString(),
                            weight = new dimension() {
                                value = (float)(totalWeight * 1000),
                                unitOfMeasure = new unitOfMeasureType() {
                                    unitOfMeasureCode = new referenceDataType() { code = "g" }
                                }
                            }
                        }
                    };

                // add shipment items to request
                shipment.items = items.ToArray();

                #region "International Info"

                if (form.IsInternational)
                {

                    var InternationalInfo = new internationalInfo();
                    InternationalInfo.shipperExporterVatNo = _senderVatNo;
                    InternationalInfo.documentsOnly = false;
                    InternationalInfo.shipmentDescription = form.SenderReference;
                    InternationalInfo.invoiceDate = DateTime.Now;
                    InternationalInfo.invoiceDateSpecified = true;
                    InternationalInfo.termsOfDelivery = "EXW";
                    InternationalInfo.purchaseOrderRef = form.SenderReference;

                    var parcels = new List<parcel>();
                    foreach (var pkg in form.Packages)
                    {
                        parcel Parcel = new parcel();
                        Parcel.weight = new dimension();
                        Parcel.weight.value = (float)(pkg.WeightKgs * 1000);
                        Parcel.weight.unitOfMeasure = new unitOfMeasureType();
                        Parcel.weight.unitOfMeasure.unitOfMeasureCode = new referenceDataType();
                        Parcel.weight.unitOfMeasure.unitOfMeasureCode.code = "g";

                        Parcel.invoiceNumber = form.SenderReference;
                        Parcel.purposeOfShipment = new referenceDataType();
                        Parcel.purposeOfShipment.code = form.ShipmentPurpose;

                        if (pkg.Manifest != null)
                        {
                            var Contents = new List<contentDetail>();
                            foreach (var product in pkg.Manifest)
                            {
                                contentDetail ContentDetail = new contentDetail();
                                ContentDetail.articleReference = product.ProductCode;
                                ContentDetail.countryOfManufacture = new countryType();
                                ContentDetail.countryOfManufacture.countryCode = new referenceDataType();
                                ContentDetail.countryOfManufacture.countryCode.code = product.CountryOfOriginCode;

                                ContentDetail.currencyCode = new referenceDataType();
                                ContentDetail.currencyCode.code = product.CurrencyCode;
                                ContentDetail.description = product.Title;
                                ContentDetail.unitQuantity = product.Quantity.ToString();
                                ContentDetail.unitValue = Convert.ToDecimal(product.UnitPrice);
                                ContentDetail.unitWeight = new dimension();
                                ContentDetail.unitWeight.value = Convert.ToSingle(product.WeightKg * 1000);
                                ContentDetail.unitWeight.unitOfMeasure = new unitOfMeasureType();
                                ContentDetail.unitWeight.unitOfMeasure.unitOfMeasureCode = new referenceDataType();
                                ContentDetail.unitWeight.unitOfMeasure.unitOfMeasureCode.code = "g";

                                Contents.Add(ContentDetail);
                            }

                            Parcel.contentDetails = Contents.ToArray();
                        }

                        parcels.Add(Parcel);

                    }

                    InternationalInfo.parcels = parcels.ToArray();

                    shipment.internationalInfo = InternationalInfo;
                }

                #endregion

                #endregion

                var request = new createShipmentRequest()
                {
                    integrationHeader = GetIntegrationHeader(),
                    requestedShipment = shipment
                };

                var response = await client.createShipmentAsync(GetSecurityHeaderType(), request);

                // log errors and warnings
                if (!hasErrorsAndWarnings(response.createShipmentResponse.integrationFooter))
                {
                    // no errors

                    if (response.createShipmentResponse.completedShipmentInfo != null)
                    {
                        if (response.createShipmentResponse.completedShipmentInfo.status != null)
                        {
                            if (response.createShipmentResponse.completedShipmentInfo.status.status1 != null)
                            {
                                if (response.createShipmentResponse.completedShipmentInfo.status.status1.statusCode != null)
                                {
                                    if (response.createShipmentResponse.completedShipmentInfo.status.status1.statusCode.code != null)
                                    {
                                        if (response.createShipmentResponse.completedShipmentInfo.status.status1.statusCode.code == "Allocated")
                                        {
                                            // get shipment numbers
                                            var lst = new List<string>();

                                            if (response.createShipmentResponse.completedShipmentInfo.allCompletedShipments != null)
                                            {
                                                foreach (var completedShipment in response.createShipmentResponse.completedShipmentInfo.allCompletedShipments)
                                                {
                                                    foreach (var package in completedShipment.shipments)
                                                    {
                                                        foreach (var shipNo in package.shipmentNumber)
                                                        {
                                                            lst.Add(shipNo);
                                                        }
                                                    }
                                                }
                                            }

                                            return lst.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                return null;

            }
            catch (TimeoutException e)
            {
                client.Abort();
                Trace.WriteLine("Request Timed Out: " + e.Message);

                throw e;
            }
            catch (FaultException e)
            {
                client.Abort();

                var message = e.CreateMessageFault();
                var errorDetail = message.GetDetail<XmlElement>();
                var errorDetails = errorDetail.ChildNodes;

                var fullErrorDetails = "";

                for (int i = 0; i < errorDetails.Count; i++)
                {
                    fullErrorDetails += errorDetails.Item(i).Name + ": " + errorDetails.Item(i).InnerText + "\n";
                }

                Trace.WriteLine("An Error Occured With Royal Mail Service: " + message.Reason.ToString() + "\n\n" + fullErrorDetails);
            }
            catch (CommunicationException e)
            {
                client.Abort();
                Trace.WriteLine("A communication error has occured: " + e.Message + " - " + e.StackTrace);
            }
            catch (Exception e)
            {
                client.Abort();
                Trace.WriteLine("Error: " + e.Message);
            }

            return null;
        }

        public async Task<bool> VoidShipmentAsync(string shipmentNumber)
        {
            shippingAPIPortTypeClient client = GetProxy();

            try
            {
                var request = new cancelShipmentRequest()
                {
                    integrationHeader = GetIntegrationHeader(),
                    cancelShipments = new string[] { shipmentNumber }
                };

                var response = await client.cancelShipmentAsync(GetSecurityHeaderType(), request);

                // log errors and warnings
                if (!hasErrorsAndWarnings(response.cancelShipmentResponse.integrationFooter))
                {
                    // no errors

                    if (response.cancelShipmentResponse.completedCancelInfo != null)
                    {
                        if (response.cancelShipmentResponse.completedCancelInfo.status != null)
                        {
                            if (response.cancelShipmentResponse.completedCancelInfo.status.status1 != null)
                            {
                                if (response.cancelShipmentResponse.completedCancelInfo.status.status1.statusCode != null)
                                {
                                    if (response.cancelShipmentResponse.completedCancelInfo.status.status1.statusCode.code != null)
                                    {
                                        if (response.cancelShipmentResponse.completedCancelInfo.status.status1.statusCode.code == "Cancelled")
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                };

                return false;

            }
            catch (TimeoutException e)
            {
                client.Abort();
                Trace.WriteLine("Request Timed Out: " + e.Message);

                throw e;
            }
            catch (FaultException e)
            {
                client.Abort();

                var message = e.CreateMessageFault();
                var errorDetail = message.GetDetail<XmlElement>();
                var errorDetails = errorDetail.ChildNodes;

                var fullErrorDetails = "";

                for (int i = 0; i < errorDetails.Count; i++)
                {
                    fullErrorDetails += errorDetails.Item(i).Name + ": " + errorDetails.Item(i).InnerText + "\n";
                }

                Trace.WriteLine("An Error Occured With Royal Mail Service: " + message.Reason.ToString() + "\n\n" + fullErrorDetails);
            }
            catch (CommunicationException e)
            {
                client.Abort();
                Trace.WriteLine("A communication error has occured: " + e.Message + " - " + e.StackTrace);
            }
            catch (Exception e)
            {
                client.Abort();
                Trace.WriteLine("Error: " + e.Message);
            }

            return false;
        }

        public async Task<string> AcceptShipmentAsync(string shipmentNumber)
        {
            shippingAPIPortTypeClient client = GetProxy();

            try
            {
                var request = new printLabelRequest()
                {
                    integrationHeader = GetIntegrationHeader(),
                    shipmentNumber = shipmentNumber
                };

                var response = await client.printLabelAsync(GetSecurityHeaderType(), request);

                // log errors and warnings
                if (!hasErrorsAndWarnings(response.printLabelResponse.integrationFooter))
                {
                    // no errors

                    var bytes = response.printLabelResponse.label;
                    if (bytes != null)
                    {
                        return Convert.ToBase64String(bytes);
                    }

                };

                return null;

            }
            catch (TimeoutException e)
            {
                client.Abort();
                Trace.WriteLine("Request Timed Out: " + e.Message);

                throw e;
            }
            catch (FaultException e)
            {
                client.Abort();

                var message = e.CreateMessageFault();
                var errorDetail = message.GetDetail<XmlElement>();
                var errorDetails = errorDetail.ChildNodes;

                var fullErrorDetails = "";

                for (int i = 0; i < errorDetails.Count; i++)
                {
                    fullErrorDetails += errorDetails.Item(i).Name + ": " + errorDetails.Item(i).InnerText + "\n";
                }

                Trace.WriteLine("An Error Occured With Royal Mail Service: " + message.Reason.ToString() + "\n\n" + fullErrorDetails);
            }
            catch (CommunicationException e)
            {
                client.Abort();
                Trace.WriteLine("A communication error has occured: " + e.Message + " - " + e.StackTrace);
            }
            catch (Exception e)
            {
                client.Abort();
                Trace.WriteLine("Error: " + e.Message);
            }

            return null;
        }

        public async Task<string[]> CreateEndOfDayManifestAsync()
        {           
            shippingAPIPortTypeClient client = GetProxy();

            try
            {
                var request = new createManifestRequest()
                {
                    integrationHeader = GetIntegrationHeader()                    
                };

                var response = await client.createManifestAsync(GetSecurityHeaderType(), request);

                // log errors and warnings
                if (!hasErrorsAndWarnings(response.createManifestResponse.integrationFooter))
                {
                    // no errors

                    var lst = new List<string>();

                    if (response.createManifestResponse.completedManifests != null)
                    {                        
                        foreach (var item in response.createManifestResponse.completedManifests)
                        {
                            lst.Add(item.manifestBatchNumber);
                        }
                    }

                    return lst.ToArray();
                };                

            }
            catch (TimeoutException e)
            {
                client.Abort();
                Trace.WriteLine("Request Timed Out: " + e.Message);

                throw e;
            }
            catch (FaultException e)
            {
                client.Abort();

                var message = e.CreateMessageFault();
                var errorDetail = message.GetDetail<XmlElement>();
                var errorDetails = errorDetail.ChildNodes;

                var fullErrorDetails = "";

                for (int i = 0; i < errorDetails.Count; i++)
                {
                    fullErrorDetails += errorDetails.Item(i).Name + ": " + errorDetails.Item(i).InnerText + "\n";
                }

                Trace.WriteLine("An Error Occured With Royal Mail Service: " + message.Reason.ToString() + "\n\n" + fullErrorDetails);
            }
            catch (CommunicationException e)
            {
                client.Abort();
                Trace.WriteLine("A communication error has occured: " + e.Message + " - " + e.StackTrace);
            }
            catch (Exception e)
            {
                client.Abort();
                Trace.WriteLine("Error: " + e.Message);
            }

            return null;
        }

        public async Task<string> PrintEndOfDayManifestAsync(string manifestNumber)
        {
            shippingAPIPortTypeClient client = GetProxy();

            try
            {
                var request = new printManifestRequest()
                {
                    integrationHeader = GetIntegrationHeader(),
                    ItemElementName = ItemChoiceType.manifestBatchNumber,
                    Item = manifestNumber
                };

                var response = await client.printManifestAsync(GetSecurityHeaderType(), request);

                // log errors and warnings
                if (!hasErrorsAndWarnings(response.printManifestResponse.integrationFooter))
                {
                    // no errors

                    var bytes = response.printManifestResponse.manifest;
                    if (bytes != null)
                    {
                        return Convert.ToBase64String(bytes);
                    }

                };

                return null;

            }
            catch (TimeoutException e)
            {
                client.Abort();
                Trace.WriteLine("Request Timed Out: " + e.Message);

                throw e;
            }
            catch (FaultException e)
            {
                client.Abort();

                var message = e.CreateMessageFault();
                var errorDetail = message.GetDetail<XmlElement>();
                var errorDetails = errorDetail.ChildNodes;

                var fullErrorDetails = "";

                for (int i = 0; i < errorDetails.Count; i++)
                {
                    fullErrorDetails += errorDetails.Item(i).Name + ": " + errorDetails.Item(i).InnerText + "\n";
                }

                Trace.WriteLine("An Error Occured With Royal Mail Service: " + message.Reason.ToString() + "\n\n" + fullErrorDetails);
            }
            catch (CommunicationException e)
            {
                client.Abort();
                Trace.WriteLine("A communication error has occured: " + e.Message + " - " + e.StackTrace);
            }
            catch (Exception e)
            {
                client.Abort();
                Trace.WriteLine("Error: " + e.Message);
            }

            return null;
        }



        #region "SOAP Service & Methods"

        private shippingAPIPortTypeClient GetProxy()
        {

            BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            myBinding.MaxReceivedMessageSize = 2147483647;

            var shippingClient = new shippingAPIPortTypeClient(myBinding, new EndpointAddress(new Uri(_endPoint), EndpointIdentity.CreateDnsIdentity("api.royalmail.com"), new AddressHeaderCollection()));

            foreach (OperationDescription od in shippingClient.Endpoint.Contract.Operations)
            {
                od.Behaviors.Add(new RoyalMailIEndpointBehavior());
            }
            return shippingClient;
        }

        private SecurityHeaderType GetSecurityHeaderType()
        {
            SecurityHeaderType securityHeader = new SecurityHeaderType();

            DateTime created = DateTime.Now;

            string creationDate;
            creationDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string nonce = nonce = (new Random().Next(0, int.MaxValue)).ToString();

            byte[] hashedPassword;
            hashedPassword = GetSHA1(_passsword);

            string concatednatedDigestInput = string.Concat(nonce, creationDate, Encoding.Default.GetString(hashedPassword));
            byte[] digest;
            digest = GetSHA1(concatednatedDigestInput);

            string passwordDigest;
            passwordDigest = Convert.ToBase64String(digest);

            string encodedNonce;
            encodedNonce = Convert.ToBase64String(Encoding.Default.GetBytes(nonce));

            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Security");
                writer.WriteStartElement("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
                writer.WriteElementString("Username", _username);
                writer.WriteElementString("Password", passwordDigest);
                writer.WriteElementString("Nonce", encodedNonce);
                writer.WriteElementString("Created", creationDate);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }

            doc.DocumentElement.RemoveAllAttributes();

            System.Xml.XmlElement[] headers = doc.DocumentElement.ChildNodes.Cast<XmlElement>().ToArray<XmlElement>();

            securityHeader.Any = headers;

            return securityHeader;

        }

        private integrationHeader GetIntegrationHeader()
        {
            integrationHeader header = new integrationHeader();

            DateTime created = DateTime.Now;
            String createdAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            header.dateTime = created;
            header.version = decimal.Parse(_apiVersion);
            header.dateTimeSpecified = true;
            header.versionSpecified = true;

            identificationStructure idStructure = new identificationStructure();
            idStructure.applicationId = _applicationId;

            string nonce = (new Random().Next(0, int.MaxValue)).ToString();

            idStructure.transactionId = CalculateMD5Hash(nonce + createdAt);

            header.identification = idStructure;

            return header;
        }

        private static byte[] GetSHA1(string input)
        {
            return SHA1Managed.Create().ComputeHash(Encoding.Default.GetBytes(input));
        }

        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        // Check Response Footer For Errors & Warnings From Service         
        // Ignore Warnings For Now         
        private bool hasErrorsAndWarnings(integrationFooter integrationFooter)
        {
            if (integrationFooter != null)
            {

                if (integrationFooter.errors != null && integrationFooter.errors.Length > 0)
                {
                    errorDetail[] errors = integrationFooter.errors;
                    for (int i = 0; i < errors.Length; i++)
                    {
                        errorDetail error = errors[i];
                        Trace.WriteLine("Royal Mail Request Error: " + error.errorDescription + ". " + error.errorResolution);
                    }
                    if (errors.Length > 0)
                    {
                        return true;
                    }
                }

                if (integrationFooter.warnings != null && integrationFooter.warnings.Length > 0)
                {
                    warningDetail[] warnings = integrationFooter.warnings;
                    for (int i = 0; i < warnings.Length; i++)
                    {
                        warningDetail warning = warnings[i];
                        Trace.WriteLine("Royal Mail Request Warning: " + warning.warningDescription + ". " + warning.warningResolution);
                    }
                }
            }

            return false;

        }
        

        #endregion

    }
}
