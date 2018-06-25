namespace AppStore.API.Core.App_Start
{
    using Handlers;
    using Infra.IoC;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using System.Web.Http;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            DependencyConfig.InitializeContainer(container);

            RegisterMessageHandlers(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterMessageHandlers(Container container)
        {
            container.Register<TokenValidatorHandler>(Lifestyle.Scoped);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<TokenValidatorHandler>(container));
        }
    }
}