using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Authentication
{
    [DataContract(Namespace = "")]
    public class Credential
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}