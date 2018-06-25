using System;
using System.Runtime.Serialization;

namespace AppStore.DataContracts.CreditCards
{
    [DataContract(Namespace = "")]
    public class CreditCard
    {
        [DataMember]
        public int? CreditCardId { get; set; }
        [DataMember]
        public CreditCardBrand Brand { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public int ExpMonth { get; set; }
        [DataMember]
        public int ExpYear { get; set; }
        [DataMember]
        public string SecurityCode { get; set; }
        [DataMember]
        public string HolderName { get; set; }
        [DataMember]
        public string LastDigits { get; set; }

        internal bool IsValid()
        {
            var isValidCreditCardInfo = Enum.IsDefined(typeof(CreditCardBrand), Brand) &&
                !string.IsNullOrWhiteSpace(Number) &&
                (ExpMonth > 0 && ExpMonth < 13) &&
                (ExpYear >= DateTime.Now.Year) &&
                !string.IsNullOrWhiteSpace(SecurityCode) &&
                !string.IsNullOrWhiteSpace(HolderName);

            return CreditCardId.HasValue || isValidCreditCardInfo;
        }

    }
}
