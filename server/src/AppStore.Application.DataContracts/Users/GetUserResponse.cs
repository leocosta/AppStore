using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class GetUserResponse : BaseResponse
    {
        [DataMember]
        public User User { get; set; }
    }
}
