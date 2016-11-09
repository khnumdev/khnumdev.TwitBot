namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Khnumdev.TwitBot.Data.DWH.Model.Configuration;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ConfigurationRepository : IConfigurationRepository
    {
        public async Task<List<State>> GetStatesAsync()
        {
            using (var context = new DWHContext())
            {
                return await context.Set<State>()
                    .ToListAsync();
            }
        }

        public async Task<State> GetStateByIdAsync(byte id)
        {
            using (var context = new DWHContext())
            {
                return await context.Set<State>()
                    .Where(i => i.Id == id)
                    .SingleAsync();
            }
        }

        public async Task UpdateStateAsync(State state)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context.Set<State>()
                    .Where(i => i.Id == state.Id)
                    .SingleAsync();

                context.Entry(existingEntity).CurrentValues.SetValues(state);

                await context.SaveChangesAsync();
            }
        }
    }
}
