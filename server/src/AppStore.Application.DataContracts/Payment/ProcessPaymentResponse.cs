using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Payment
{
    [DataContract(Namespace = "")]
    public class ProcessPaymentResponse : BaseResponse
    {
        [DataMember]
        public PaymentResult Result { get; set; }
    }
}
