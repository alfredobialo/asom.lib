using System;
using System.Collections.Generic;

namespace asom.lib.core.Util
{
    public class DateRangeHelper
    {
        internal DateRangeHelper()
        {
        }

        public string Title { get; set; }
        public DateRange DateInterval { get; set; }

        public string Description
        {
            get { return $"{Title} : Date Btw {DateInterval.StartDate:ddd, dd-MMM-yyyy} And {DateInterval.EndDate:ddd, dd-MMM-yyyy}"; }
        }

        public static IEnumerable<DateRangeHelper> GetDateHelpers()
        {
            List<DateRangeHelper> res = new List<DateRangeHelper>();
            res.Add(new DateRangeHelper()
            {
                Title = "Today",
                DateInterval = DateRange.Today()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Yesterday",
                DateInterval = DateRange.Yesterday()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "This Week",
                DateInterval = DateRange.ThisWeek()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "First Week this Month",
                DateInterval = DateRange.FirstWeekOfThisMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Second Week this Month",
                DateInterval = DateRange.SecondWeekOfThisMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Third Week this Month",
                DateInterval = DateRange.ThirdWeekOfThisMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Fourth Week this Month",
                DateInterval = DateRange.FouthWeekOfThisMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Current Month",
                DateInterval = DateRange.CurrentMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Last Month",
                DateInterval = DateRange.PreviousMonth()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Current Quarter",
                DateInterval = DateRange.ThisQuarter()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Q1",
                DateInterval = DateRange.FirstQuarter()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Q2",
                DateInterval = DateRange.FirstQuarter()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Q3",
                DateInterval = DateRange.ThirdQuarter()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "Q4",
                DateInterval = DateRange.FouthQuarter()
            });
            res.Add(new DateRangeHelper()
            {
                Title = "This Year",
                DateInterval = DateRange.GetForWholeYear(DateTime.Today.Year)
            });

            return res;
        }
    }
}