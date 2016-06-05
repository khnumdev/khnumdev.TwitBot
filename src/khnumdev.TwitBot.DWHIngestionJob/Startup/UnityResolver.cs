namespace Khnumdev.TwitBot.DWHIngestionJob.Startup
{
    using Data.DWH.Repositories;
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Services;

    class UnityResolver
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IChatRepository, ChatRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDWHRepository, DWHRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDWHIngestionService, DWHIngestionService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
