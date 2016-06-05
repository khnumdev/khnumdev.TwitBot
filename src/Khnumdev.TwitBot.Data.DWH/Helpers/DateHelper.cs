namespace Khnumdev.TwitBot.Data.DWH.Helpers
{
    using System;

    public static class DateHelper
    {
        public static int GetKeyFromDate(DateTime date)
        {
            return (date.Year * 1000000) + (date.Month * 10000) + (date.Day * 100) + date.Hour;
        }
    }
}
