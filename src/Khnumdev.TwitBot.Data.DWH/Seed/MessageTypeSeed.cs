namespace Khnumdev.TwitBot.Data.DWH.Seed
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.Collections.Generic;


    class MessageTypeSeed : ISeed<MessageType>
    {
        public IEnumerable<MessageType> Generate()
        {
            return new List<MessageType>
                {
                new MessageType { Id =1 , Name="Request" },
                new MessageType { Id = 2, Name = "Response" }
            };
        }
    }
}
