using AppStore.Application.Services.Payment;
using AppStore.Infra.IoC;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace AppStore.Streaming.Consumer
{
    class Program
    {
        static Container container;

        static Program()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            DependencyConfig.InitializeContainer(container);
        }

        static void Main(string[] args)
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                Console.WriteLine("Start AppStore Payment Consumer. Ctrl-C to exit.");

                var paymentAppService = container.GetInstance<IPaymentAppService>();
                var consumer = new PaymentConsumer(paymentAppService);
                consumer.Listen(msg => Console.WriteLine(msg));
            }
        }
    }
}
