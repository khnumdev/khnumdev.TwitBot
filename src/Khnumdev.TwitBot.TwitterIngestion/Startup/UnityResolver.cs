namespace Khnumdev.TwitBot.TwitterIngestion.Startup
{
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Services;

    class UnityResolver
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ITwitterRepository, TwitterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITwitterServiceProvider, TwitterServiceProvider>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
