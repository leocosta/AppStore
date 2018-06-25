using System;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Payment
{

    [DataContract(Namespace = "")]
    public class PaymentResult
    {
        public string TransactionReference { get; set; }
        public Guid? InstantBuyKey { get; set; }
    }
}
