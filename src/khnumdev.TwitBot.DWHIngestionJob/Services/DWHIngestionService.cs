namespace Khnumdev.TwitBot.DWHIngestionJob.Services
{
    using Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DWHIngestionService
    {
        readonly IChatRepository _chatRepository;

        public DWHIngestionService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        //public Task ProcessMessageAsync(string message)
        //{
        //    var chat = _chatRepository.DeserializeFrom(message);

        //    chat.
        //}

        //Task<int>
    }
}
