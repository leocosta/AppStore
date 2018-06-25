using System.Data.Entity.ModelConfiguration;
using AppStore.Domain.Orders;

namespace AppStore.Infra.Data.Persistence.EF.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            base.HasKey(u => u.OrderId);

            base.HasRequired(p => p.Customer)
                .WithMany()
                .Map(m => m.MapKey("UserId"));

            base.HasRequired(p => p.App)
                .WithMany()
                .Map(m => m.MapKey("AppId"));

            base.HasOptional(p => p.CreditCardTransation)
                .WithRequired(r => r.Order)
                .Map(m => m.MapKey("OrderId"));
        }
    }
}