using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class CreateUserResponse : BaseResponse
    {
        [DataMember]
        public User User { get; set; }
    }
}
