using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class Address
    {
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Complement { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
    }
}