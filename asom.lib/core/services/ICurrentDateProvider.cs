using System;

namespace asom.lib.core.services
{
    /// <summary>
    /// Provide us with the current date for Comparision
    /// </summary>
    public interface ICurrentDateProvider
    {
        DateTime Date { get; }
    }

    public class CurrentDateProvider : ICurrentDateProvider
    {
        public CurrentDateProvider(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }
    }
}
