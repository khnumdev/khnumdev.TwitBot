namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConversationTrack: IDimension
    {
        public int Id { get; set; }

        public string ConversationId { get; set; }
    }
}
