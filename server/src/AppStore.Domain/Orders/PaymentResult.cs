using System;

namespace AppStore.Domain.Orders
{
    public class PaymentResult
    {
        public PaymentResult(string transactionReference, Guid instantBuyKey, PaymentInfo paymentInfo)
        {
            TransactionReference = transactionReference;
            InstantBuyKey = instantBuyKey;
            PaymentInfo = paymentInfo;
        }

        public string TransactionReference { get; set; }
        public Guid? InstantBuyKey { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        public bool Success() => !string.IsNullOrEmpty(TransactionReference);
    }
}