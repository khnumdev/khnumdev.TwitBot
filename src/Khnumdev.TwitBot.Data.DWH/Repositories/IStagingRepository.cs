namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Staging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStagingRepository
    {
        Task AddAsync(RAWMessage entity);

        Task<List<RAWMessage>> GetAsync();

        Task TruncateTableAsync();
    }
}
