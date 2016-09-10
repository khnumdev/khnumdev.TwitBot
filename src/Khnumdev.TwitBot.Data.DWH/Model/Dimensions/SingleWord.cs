using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    public class SingleWord : IDimension
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
