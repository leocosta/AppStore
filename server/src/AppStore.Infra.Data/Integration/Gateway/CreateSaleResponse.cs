using System;

namespace AppStore.Infra.Data.Integration.Gateway
{
    internal class CreateSaleResponse
    {
        public Guid InstantBuyKey { get; internal set; }
        public string TransactionReference { get; internal set; }
    }
}