using System;

namespace AppStore.Infra.Data.Integration.Gateway
{
    internal class CreditCardTransaction
    {
        public string CreditCardBrand { get; internal set; }
        public string CreditCardNumber { get; internal set; }
        public int? ExpMonth { get; internal set; }
        public int? ExpYear { get; internal set; }
        public string HolderName { get; internal set; }
        public Guid? InstantBuyKey { get; internal set; }
        public string SecurityCode { get; internal set; }
    }
}