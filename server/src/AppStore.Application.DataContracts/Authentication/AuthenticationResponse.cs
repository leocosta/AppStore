using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Authentication
{
    [DataContract(Namespace = "")]
    public class AuthenticationRequest : BaseRequest
    {
        [DataMember]
        public Credential Credential { get; set; }
    }
}
