using System;

namespace AppStore.Streaming.Handlers
{
    public class ProcessPaymentHandler
    {
        public int OrderId { get; set; }
        public Guid? InstantBuyKey { get; set; }
        public CreditCardBrand? Brand { get; set; }
        public string Number { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public bool? SaveCreditCard { get; set; }
    }
}
