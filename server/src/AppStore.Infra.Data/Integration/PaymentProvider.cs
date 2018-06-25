using AppStore.Domain.Orders;
using AppStore.Infra.CrossCutting.Logging;
using AppStore.Infra.CrossCutting.Serialization;
using AppStore.Infra.Data.Integration.Gateway;
using System;

namespace AppStore.Infra.Data.Integration
{
    public class PaymentProvider : IPaymentService
    {
        private readonly PaymentGatewayMock _serviceClient = new PaymentGatewayMock();

        public PaymentResult CreateTransaction(PaymentInfo paymentInfo)
        {
            var createSaleResponse = default(CreateSaleResponse);
            try
            {
                var transactionRequest = createCreditCardTransation(paymentInfo);
                Logger.Info("ServiceClient Request: {0}", transactionRequest.Serialize());

                createSaleResponse = _serviceClient.Process(transactionRequest);
                Logger.Info("ServiceClient Response: {0}", createSaleResponse.Serialize());
            }
            catch (Exception ex)
            {
                Logger.Error("ServiceClient Error: {0}", ex);
            }

            if (createSaleResponse == null)
                throw new InvalidOperationException("Unable to process request.");

            return new PaymentResult(createSaleResponse.TransactionReference,
                createSaleResponse.InstantBuyKey, paymentInfo);
        }

        private CreditCardTransaction createCreditCardTransation(PaymentInfo paymentInfo)
        {
            return new CreditCardTransaction
            {
                InstantBuyKey = paymentInfo.InstantBuyKey,
                CreditCardNumber = paymentInfo.CreditCardNumber,
                CreditCardBrand = paymentInfo.CreditCardBrand.ToString(),
                ExpMonth = paymentInfo.ExpMonth,
                ExpYear = paymentInfo.ExpYear,
                SecurityCode = paymentInfo.SecurityCode,
                HolderName = paymentInfo.HolderName
            };
        }
    }

}
