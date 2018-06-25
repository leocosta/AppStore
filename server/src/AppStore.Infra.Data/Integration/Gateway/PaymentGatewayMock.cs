using System;

namespace AppStore.Infra.Data.Integration.Gateway
{
    internal class PaymentGatewayMock
    {
        internal CreateSaleResponse Process(CreditCardTransaction transaction)
        {
            return new CreateSaleResponse
            {
                InstantBuyKey = Guid.NewGuid(),
                TransactionReference = Guid.NewGuid().ToString()
            };
        }
    }
}