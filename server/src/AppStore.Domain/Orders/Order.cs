using System;
using AppStore.Infra.CrossCutting.Helpers;
using AppStore.Domain.Users;
using AppStore.Domain.Apps;

namespace AppStore.Domain.Orders
{
    public class Order
    {
        public Order() { }

        public Order(User customer, App app)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (app == null)
                throw new ArgumentNullException("app");

            Customer = customer;
            App = app;
            Price = app.Price;
        }

        public int OrderId { get; private set; }
        public virtual User Customer { get; set; }
        public virtual App App { get; set; }
        public Status Status { get; set; } = Status.Created;
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }
        public virtual CreditCardTransation CreditCardTransation { get; set; }

        public void PaymentReceived(PaymentResult paymentResult)
        {
            Status = Status.PaymentReceived;
            registerCreditCardTransation(paymentResult);
            registerUserCreditCard(paymentResult);
        }

        public void PaymentReview() => Status = Status.PaymentReview;

        public void Processing() => Status = Status.Processing;

        private void registerCreditCardTransation(PaymentResult paymentResult) =>
            CreditCardTransation = new CreditCardTransation(paymentResult.TransactionReference);

        private void registerUserCreditCard(PaymentResult paymentResult)
        {
            if (!paymentResult.PaymentInfo.ShouldSaveCreditCard())
                return;

            Customer.AddCreditCard(paymentResult.InstantBuyKey.Value,
                paymentResult.PaymentInfo.CreditCardBrand, paymentResult.PaymentInfo.CreditCardNumber.GetLast(4),
                paymentResult.PaymentInfo.ExpMonth.Value, paymentResult.PaymentInfo.ExpYear.Value);
        }
    }
}
