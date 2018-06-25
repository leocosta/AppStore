using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Orders
{
    [DataContract(Namespace = "")]
    public class CreateOrderResponse : BaseResponse
    {
        [DataMember]
        public Order Order { get; set; }
    }
}
