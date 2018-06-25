using System.Data.Entity;
using AppStore.Domain.Orders;

namespace AppStore.Infra.Data.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context)
            : base(context) { }
    
        public Order Get(int id)
        {
            return base.FirstOrDefault(i => i.OrderId == id);
        }
    }
}
