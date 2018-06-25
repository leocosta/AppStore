using AppStore.DataContracts.CreditCards;
using System;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Payment
{
    [DataContract(Namespace = "")]
    public class ProcessPaymentRequest : BaseResponse
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public Guid? InstantBuyKey { get; set; }
        [DataMember]
        public CreditCardBrand Brand { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public int? ExpMonth { get; set; }
        [DataMember]
        public int? ExpYear { get; set; }
        [DataMember]
        public string SecurityCode { get; set; }
        [DataMember]
        public string HolderName { get; set; }
        public bool? SaveCreditCard { get; set; }
    }
}
