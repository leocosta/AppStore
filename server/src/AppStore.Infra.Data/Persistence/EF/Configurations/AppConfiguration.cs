using System.Data.Entity.ModelConfiguration;
using AppStore.Domain.Apps;

namespace AppStore.Infra.Data.Persistence.EF.Configurations
{
    public class AppConfiguration : EntityTypeConfiguration<App>
    {
        public AppConfiguration()
        {
            base.HasKey(u => u.AppId);

            base.Property(p => p.Name)
                .IsRequired();

            base.Property(p => p.ThumbUrl)
                .IsRequired()
                .HasMaxLength(500);

            base.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}