namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Configuration;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConfigurationRepository
    {
        Task<List<State>> GetStatesAsync();

        Task<State> GetStateByIdAsync(byte id);

        Task UpdateStateAsync(State state);
    }
}
