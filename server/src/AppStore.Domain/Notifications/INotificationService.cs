using AppStore.Domain.Orders;

namespace AppStore.Domain.Notifications
{
    public interface INotificationService
    {
        void SendPaymentReceived(Order order);
        void SendPaymentReview(Order order);
    }
}