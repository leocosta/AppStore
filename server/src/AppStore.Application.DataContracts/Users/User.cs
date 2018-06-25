using System;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class User
    {
        [DataMember]
        public int? UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
        [DataMember]
        public virtual Address Address { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
