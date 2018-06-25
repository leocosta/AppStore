using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AppStore.Domain.Apps;
using AppStore.Domain.Orders;
using AppStore.Domain.Users;
using AppStore.Infra.Data.Persistence.EF.Configurations;

namespace AppStore.Infra.Data.Persistence.EF.Contexts
{
    public class AppStoreContext : DbContext
    {
        public AppStoreContext()
            : base("AppStoreContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppStoreContext>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<App> Apps { get; set; }
        public IDbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            setPropertyConfigurations(modelBuilder);
            setConvetions(modelBuilder);
            setEntityTypeConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        private void setConvetions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private void setPropertyConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
        }

        private void setEntityTypeConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AppConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
        }
    }
}