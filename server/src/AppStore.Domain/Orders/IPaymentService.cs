namespace AppStore.Domain.Orders
{
    public interface IPaymentService
    {
        PaymentResult CreateTransaction(PaymentInfo paymentInfo);
    }
}
