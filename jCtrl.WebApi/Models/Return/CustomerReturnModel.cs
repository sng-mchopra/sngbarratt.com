using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.Models.Return
{
    public class CustomerReturnModel
    {
        public string Url { get; set; }
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public string CompanyName { get; set; }
        public string CompanyTaxNo { get; set; }


        public AddressReturnModel Address { get; set; }

        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public ShippingAddressReturnModel ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }

        public string PaymentMethod { get; set; }
        public PaymentCardReturnModel PaymentCard { get; set; }

        public CustomerVehicleReturnModel Vehicle { get; set; }

        public string SiteCode { get; set; }
        public string LanguageCode { get; set; }
        public bool Marketing { get; set; }

        public PagedResultsReturnModel<WebOrderListReturnModel> ActiveOrders { get; set; }
        public PagedResultsReturnModel<WebOrderListReturnModel> HistoricOrders { get; set; }   

        //public int BranchId { get; set; }
        //public int LanguageId { get; set; }
        //public int PaymentMethodId { get; set; }
        //public int PrimaryEmailAddressId { get; set; }
        //public int PrimaryPhoneNumberId { get; set; }
        //public int PrimaryDeliveryAddressId { get; set; }
        //public int PrimaryPaymentCardId { get; set; }
        //public int PrimaryVehicleId { get; set; }

        //public List<CustomerEmailAddressReturnModel> EmailAddresses { get; set; }
        //public List<PhoneNumberReturnModel> PhoneNumbers { get; set; }
        //public List<DeliveryAddressReturnModel> DeliveryAddresses { get; set; }        
        //public List<PaymentCardReturnModel> PaymentCards { get; set; }
        //public List<CustomerVehicleReturnModel> Vehicles { get; set; }



    }

}