using AppStore.Application.DataContracts.Apps;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class GetAppResponse : BaseResponse
    {
        [DataMember]
        public App App { get; set; }
    }
}
