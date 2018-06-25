using AppStore.DataContracts.CreditCards;
using System.Runtime.Serialization;
using System;

namespace AppStore.Application.DataContracts.Orders
{
    [DataContract(Namespace = "")]
    public class PaymentInfo
    {
        [DataMember]
        public CreditCard CreditCard { get; set; }
        [DataMember]
        public bool SaveCreditCard { get; set; }

        internal bool IsValid() => CreditCard?.IsValid() ?? false;
    }

}
