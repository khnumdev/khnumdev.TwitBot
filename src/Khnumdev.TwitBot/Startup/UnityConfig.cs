namespace Khnumdev.TwitBot.Startup
{
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Services;
    using System.Web.Http;

    static class UnityConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ITwitterRepository, TwitterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMessageMatcherProcessor, MessageMatcherProcessor>(new HierarchicalLifetimeManager());
            container.RegisterType<IChatRepository, ChatRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            // Other Web API configuration not shown.
        }
    }
}