namespace Khnumdev.TwitBot
{
    using Startup;
    using Sync.Startup;
    using System.Data.Entity;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DbConfiguration.SetConfiguration(new DatabaseConfiguration());
        }
    }
}
