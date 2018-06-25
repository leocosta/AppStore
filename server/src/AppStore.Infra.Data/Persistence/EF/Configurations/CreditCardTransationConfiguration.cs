using System.Data.Entity.ModelConfiguration;
using AppStore.Domain.Orders;

namespace AppStore.Infra.Data.Persistence.EF.Configurations
{
    public class CreditCardTransationConfiguration : EntityTypeConfiguration<CreditCardTransation>
    {
        public CreditCardTransationConfiguration()
        {
            base.HasKey(u => u.CreditCardTransationId);
        }
    }
}