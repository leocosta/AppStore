using System;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts
{
    [DataContract(Namespace = "")]
    public abstract class BaseResponse
    {
        /// <summary>
        /// Exception content
        /// </summary>
        [DataMember]
        public Exception Exception { get; set; }
    }
}
