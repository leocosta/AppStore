using AppStore.Application.DataContracts.Orders;

namespace AppStore.Application.Services.Users
{
    public interface IOrderAppService
    {
        CreateOrderResponse Create(CreateOrderRequest request);
    }
}