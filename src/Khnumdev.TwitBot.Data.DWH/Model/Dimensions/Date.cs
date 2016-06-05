namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Date
    {
        public int Id { get; set; }

        public string Year { get; set; }

        public DateTime Quarter { get; set; }

        public string QuarterName { get; set; }

        public DateTime Month { get; set; }

        public string MonthName { get; set; }

        public byte Day { get; set; }

        public byte Hour { get; set; }

        public DateTime CompleteDate { get; set; }
    }
}
