namespace Khnumdev.TwitBot.Data
{
    using System.Data.Entity;

    public class TwitBotContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var currentAssembly = this.GetType().Assembly;

            modelBuilder.Configurations.AddFromAssembly(currentAssembly);
            modelBuilder.Conventions.AddFromAssembly(currentAssembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
