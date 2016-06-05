using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ConversationId { get; set; }

        public string ChannelId { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }

        public DateTime LoadedOn { get; set; }
    }
}
