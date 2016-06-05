namespace Khnumdev.TwitBot.Data.DWH.Seed
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.Collections.Generic;


    class MessageSourceSeed : ISeed<MessageSource>
    {
        public IEnumerable<MessageSource> Generate()
        {
            return new List<MessageSource>
                {
                new MessageSource { Id =1 , Source="Request" },
                new MessageSource { Id = 2, Source = "Response" }
            };
        }
    }
}
