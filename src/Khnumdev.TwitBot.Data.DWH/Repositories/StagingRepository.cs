namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Staging;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class StagingRepository : IStagingRepository
    {
        public async Task AddAsync(RAWMessage entity)
        {
            using (var context = new DWHContext())
            {
                context.Set<RAWMessage>()
                .Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<RAWMessage>> GetAsync()
        {
            using (var context = new DWHContext())
            {
                return await context.Set<RAWMessage>()
                .OrderBy(i => i.Id)
                .Take(1000)
                .ToListAsync();
            }
        }

        public async Task TruncateTableAsync()
        {
            using (var context = new DWHContext())
            {
                await context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [Staging].[RAWMessage]");
            }
        }
    }
}
