using AppStore.Application.DataContracts.Users;
using System.Runtime.Serialization;
using System;

namespace AppStore.Application.DataContracts.Authentication
{
    [DataContract(Namespace = "")]
    public class AuthenticationResponse : BaseResponse
    {
        public string AuthToken { get; set; }
        public DateTime? Expires { get; set; }
        public string TokenName { get; set; }
        [DataMember]
        public User User { get; set; }
    }
}
