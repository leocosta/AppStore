using AppStore.DataContracts.CreditCards;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract(Namespace = "")]
    public class GetUserCreditCardsResponse : BaseResponse
    {
        [DataMember]
        public IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
