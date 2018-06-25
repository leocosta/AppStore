using AppStore.Domain.Common;

namespace AppStore.Domain.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Get(int id);
    }
}
