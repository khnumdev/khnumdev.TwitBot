namespace Khnumdev.TwitBot.TwitterIngestion.Startup
{
    using Data.Repositories;
    using Microsoft.Practices.Unity;
    using Services;
    using Core.TextAnalyzer.Services;
    class UnityResolver
    {
        public static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ITwitterRepository, TwitterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITwitterServiceProvider, TwitterServiceProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<ITextAnalyzerService, TextAnalyzerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITwitterAnalyzer, TwitterAnalyzer>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
