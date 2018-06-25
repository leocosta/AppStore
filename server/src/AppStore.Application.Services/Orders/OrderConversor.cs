using AppStore.Application.DataContracts.Orders;
using OrdersDomain = AppStore.Domain.Orders;

namespace AppStore.Application.Services.Orders
{
    public static class OrderConversor
    {
        public static OrdersDomain.Order ToDomain(this Order order)
        {
            return new OrdersDomain.Order();
        }
    }
}
