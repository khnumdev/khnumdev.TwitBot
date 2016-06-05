namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Channel: IDimension
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
