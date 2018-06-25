using AppStore.Application.Services.Apps;
using AppStore.Application.Services.Authentication;
using AppStore.Application.Services.Payment;
using AppStore.Application.Services.Users;
using AppStore.Domain.Apps;
using AppStore.Domain.Common;
using AppStore.Domain.Notifications;
using AppStore.Domain.Orders;
using AppStore.Domain.Users;
using AppStore.Infra.CrossCutting.Notification;
using AppStore.Infra.CrossCutting.Notifications;
using AppStore.Infra.Data.Integration;
using AppStore.Infra.Data.Persistence.EF.Contexts;
using AppStore.Infra.Data.Persistence.Repositories;
using AppStore.Streaming.Producer;
using SimpleInjector;
using System.Data.Entity;

namespace AppStore.Infra.IoC
{
    public class DependencyConfig
    {
        public static void InitializeContainer(Container container)
        {
            // Infra
            container.Register<IMessageService, MailService>(Lifestyle.Scoped);

            // Repositories
            container.Register<DbContext, AppStoreContext>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IAuthenticationService, AuthenticationService>(Lifestyle.Scoped);
            container.Register<IAppRepository, AppRepository>(Lifestyle.Scoped);
            container.Register<IOrderRepository, OrderRepository>(Lifestyle.Scoped);

            // Domain Services
            container.Register<IPaymentService, PaymentProvider>(Lifestyle.Scoped);
            container.Register<INotificationService, NotificationService>(Lifestyle.Scoped);

            // Application Services
            container.Register<IAuthenticationAppService, AuthenticationAppService>(Lifestyle.Scoped);
            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            container.Register<IAppAppService, AppAppService>(Lifestyle.Scoped);
            container.Register<IOrderAppService, OrderAppService>(Lifestyle.Scoped);
            container.Register<IPaymentAppService, PaymentAppService>(Lifestyle.Scoped);

            // Streaming
            container.Register<IPaymentProducer, PaymentProducer>(Lifestyle.Singleton);
        }
    }
}
