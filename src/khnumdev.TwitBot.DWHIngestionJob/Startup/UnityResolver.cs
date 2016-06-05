namespace Khnumdev.TwitBot.DWHIngestionJob.Startup
{
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Services;
    
    class UnityResolver
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IChatRepository, ChatRepository>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
