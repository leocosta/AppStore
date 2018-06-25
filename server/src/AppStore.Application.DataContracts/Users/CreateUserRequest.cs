using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class CreateUserRequest : BaseRequest
    {
        [DataMember]
        public User User { get; set; }
    }
}
