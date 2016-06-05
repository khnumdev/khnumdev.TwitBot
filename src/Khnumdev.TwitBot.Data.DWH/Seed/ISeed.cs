namespace Khnumdev.TwitBot.Data.DWH.Seed
{
    using System.Collections.Generic;

    interface ISeed<T>
    {
        IEnumerable<T> Generate();
    }
}
