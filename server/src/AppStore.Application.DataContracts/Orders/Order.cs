using AppStore.Application.DataContracts.Apps;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Orders
{
    [DataContract(Namespace = "")]
    public class Order
    {
        [DataMember]
        public App App { get; set; }
        [DataMember]
        public PaymentInfo PaymentInfo { get; set; }
    }
}
