namespace Khnumdev.TwitBot.Data.DWH.Seed
{
    using Model.Dimensions;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    class DateSeed : ISeed<Date>
    {
        public IEnumerable<Date> Generate()
        {
            var dateList = new List<Date>();

            var currentDate = new DateTime(2016, 6, 1);
            var endDate = new DateTime(2020, 12, 31);

            Date date = null;
            while (currentDate <= endDate)
            {
                date = new Date()
                {
                    CompleteDate = currentDate,
                    Day = (byte)currentDate.Day,
                    Id = (currentDate.Year * 1000000) + (currentDate.Month * 10000) + (currentDate.Day * 100) + currentDate.Hour,
                    Month = new DateTime(currentDate.Year, currentDate.Month, 1),
                    MonthName = currentDate.ToString("MMMM", CultureInfo.InvariantCulture),
                    Hour = (byte)currentDate.Hour,
                    Year = currentDate.Year.ToString()
                };

                CalculateQuarters(date, currentDate);

                dateList.Add(date);

                currentDate = currentDate.AddHours(1);
            }

            return dateList;
        }


        void CalculateQuarters(Date date, DateTime currentDate)
        {
            if (currentDate.Month >= 1 && currentDate.Month <= 3)
            {
                date.Quarter = new DateTime(currentDate.Year, 1, 1);
                date.QuarterName = "Q1";
            }
            else if (currentDate.Month >= 4 && currentDate.Month <= 6)
            {
                date.Quarter = new DateTime(currentDate.Year, 4, 1);
                date.QuarterName = "Q2";
            }
            else if (currentDate.Month >= 7 && currentDate.Month <= 9)
            {
                date.Quarter = new DateTime(currentDate.Year, 7, 1);
                date.QuarterName = "Q3";
            }
            else
            {
                date.Quarter = new DateTime(currentDate.Year, 10, 1);
                date.QuarterName = "Q4";
            }
        }
    }
}
