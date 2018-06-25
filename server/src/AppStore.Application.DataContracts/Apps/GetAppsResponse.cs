using AppStore.Application.DataContracts.Apps;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class GetAppsResponse : BaseResponse
    {
        [DataMember]
        public IEnumerable<App> Apps { get; set; }
    }
}
