namespace Khnumdev.TwitBot.SyncJob.Startup
{
    using Data.DWH.Repositories;
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Processors;

    class UnityResolver
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IStagingRepository, StagingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDWHRepository, DWHRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITwitterRepository, TwitterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITopicProcessor, TopicProcessor>(new HierarchicalLifetimeManager());
            container.RegisterType<IConfigurationRepository, ConfigurationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IChatIngestionProcessor, ChatIngestionProcessor>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
