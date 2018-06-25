using System;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Apps
{
    [DataContract(Namespace = "")]
    public class App
    {
        [DataMember]
        public int? AppId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Developer { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string ThumbUrl { get; set; }

        internal bool IsValid() => AppId.HasValue;
    }
}
