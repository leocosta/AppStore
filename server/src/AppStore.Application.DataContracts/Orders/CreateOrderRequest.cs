using AppStore.Application.DataContracts.Users;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Orders
{
    [DataContract(Namespace = "")]
    public class CreateOrderRequest : BaseResponse
    {
        [DataMember]
        public Order Order { get; set; }
        public User User { get; set; }
    }
}
