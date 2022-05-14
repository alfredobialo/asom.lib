using System;

namespace asom.lib.core.util
{
    public class DateRange
    {
        public override string ToString()
        {
            return string.Format("{0:dd-MMM yyyy} and {1:dd-MMM yyyy}", StartDate, EndDate);
        }

        public DateRange()
        {
        }

        public DateRange(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null) st = startDate.Value.Date;
            if (endDate != null) end = endDate.Value.Date.AddHours(23);
            if (st > end)
            {
                st = end;
            }
        }

        DateTime? st = DateTime.Today, end = DateTime.Today.AddHours(23);

        public DateTime? StartDate
        {
            get { return st; }
            set { st = value; }
        }

        public DateTime? EndDate
        {
            get { return end; }

            set { end = value; }
        }

        public static DateRange CurrentMonth()
        {
            DateRange res = DateRange.GetForMonth((MonthsOfTheYear)DateTime.Today.Month);
            return res;
        }

        public static DateRange PreviousMonth()
        {
            DateRange res = DateRange.GetForMonth((MonthsOfTheYear)CurrentMonth().StartDate.Value.AddDays(-10).Month);
            return res;
        }

        //public static DateRange PreviousMonth(DateRange dr)
        //{
        //    DateRange res = DateRange.GetForMonth((MonthsOfTheYear)CurrentMonth().StartDate.Value.AddDays(-10).Month);
        //    return res;
        //}
        public static DateRange NextMonth()
        {
            DateRange res = DateRange.GetForMonth((MonthsOfTheYear)CurrentMonth().StartDate.Value.AddDays(10).Month);
            return res;
        }

//        // Next(4) Next four month from the given Daterange
//        public static DateRange Next(int month)
//        {
//            DateRange res = DateRange.GetForMonth((MonthsOfTheYear)CurrentMonth().StartDate.Value.AddDays(10).Month);
//            return res;
//        }
        //public static DateRange CurrentMonth()
        //{
        //    DateRange res = DateRange.GetForMonth((MonthsOfTheYear)DateTime.Today.Month);
        //    return res;
        //}
        public static DateRange GetForWholeYear(int year)
        {
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jan, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Dec, year));
            return res;
        }

        public static DateRange QuarterOf(QuarterOfYear quarter, int year)
        {
            // Get the DateRange for the first Quarter of the given year
            switch (quarter)
            {
                case QuarterOfYear.First:
                    return FirstQuarter(year);
                case QuarterOfYear.Second:
                    return SecondQuarter(year);
                case QuarterOfYear.Third:
                    return ThirdQuarter(year);
                case QuarterOfYear.Fouth:
                    return FouthQuarter(year);
            }

            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jan, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Mar, year));
            ;
            return res;
        }

        public static DateRange Today()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = new DateRange(DateTime.Today, DateTime.Today.AddHours(23));
            return res;
        }

        public static DateRange Yesterday()
        {
            // Get the DateRange for the first Quarter of the given year
            var d = DateTime.Today.AddDays(-1);
            DateRange res = new DateRange();
            res.StartDate = d;
            res.EndDate = d.AddHours(23);
            return res;
        }

        public static DateRange FirstHalfOf(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jan, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Jun, year));
            ;
            return res;
        }

        public static DateRange SecondHalfOf(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jul, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Dec, year));
            ;
            return res;
        }

        public static DateRange FirstHalfOf()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FirstHalfOf(DateTime.Today.Year);
            return res;
        }

        public static DateRange SecondHalfOf()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = SecondHalfOf(DateTime.Today.Year);
            return res;
        }

        public static DateRange FirstQuarter(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jan, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Mar, year));
            ;
            return res;
        }

        public static DateRange SecondQuarter(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Apr, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Jun, year));
            ;
            return res;
        }

        public static DateRange ThirdQuarter(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Jul, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Sept, year));
            ;
            return res;
        }

        public static DateRange FouthQuarter(int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = DateRange.GetForMonth(MonthsOfTheYear.Oct, year).Add(DateRange.GetForMonth(MonthsOfTheYear.Dec, year));
            ;
            return res;
        }

        public static DateRange FirstQuarter()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FirstQuarter(DateTime.Today.Year);
            return res;
        }

        public static DateRange SecondQuarter()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = SecondQuarter(DateTime.Today.Year);
            return res;
        }

        public static DateRange ThirdQuarter()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = ThirdQuarter(DateTime.Today.Year);
            return res;
        }

        public static DateRange FouthQuarter()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FouthQuarter(DateTime.Today.Year);
            return res;
        }

        public static DateRange ThisQuarter()
        {
            // Get the DateRange for the first Quarter of the given year
            int month = DateTime.Today.Month;
            DateRange res = FirstQuarter();
            if (month >= 4 && month <= 6)
            {
                res = SecondQuarter();
            }
            else if (month >= 7 && month <= 9)
            {
                res = ThirdQuarter();
            }
            else if (month >= 10)
            {
                res = FouthQuarter();
            }

            return res;
        }

        public static DateRange ThisWeek()
        {
            // get the current current date
            var cmonth = CurrentMonth();
            var currentWeek = new DateRange();
            var currentDate = DateTime.Today;
            // we want to get Day of the Week of Monday to Saturday

            if (currentDate.DayOfWeek == DayOfWeek.Monday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                currentWeek.StartDate = currentDate;
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Tuesday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                // take date one day backward but ensure we don jumb out of the current month
                currentWeek.StartDate = currentDate.AddDays(-1);
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Wednesday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                // take date one day backward but ensure we don jumb out of the current month
                currentWeek.StartDate = currentDate.AddDays(-2);
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Thursday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                // take date one day backward but ensure we don jumb out of the current month
                currentWeek.StartDate = currentDate.AddDays(-3);
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Friday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                // take date one day backward but ensure we don jumb out of the current month
                currentWeek.StartDate = currentDate.AddDays(-4);
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Saturday && currentDate >= cmonth.StartDate && currentDate <= cmonth.EndDate)
            {
                // take date one day backward but ensure we don jumb out of the current month
                currentWeek.StartDate = currentDate.AddDays(-5);
                currentWeek.EndDate = currentWeek.StartDate.Value.AddDays(5);
            }

            return currentWeek;
        }

        public static DateRange FirstWeekOf(MonthsOfTheYear month, int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = new DateRange(DateRange.GetForMonth(month, year).StartDate,
                DateRange.GetForMonth(month, year).StartDate.Value.AddDays(7));
            return res;
        }

        public static DateRange SecondWeekOf(MonthsOfTheYear month, int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = new DateRange(FirstWeekOf(month, year).EndDate.Value.AddDays(1),
                FirstWeekOf(month, year).EndDate.Value.AddDays(7));
            return res;
        }

        public static DateRange ThirdWeekOf(MonthsOfTheYear month, int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = new DateRange(SecondWeekOf(month, year).EndDate.Value.AddDays(1),
                SecondWeekOf(month, year).EndDate.Value.AddDays(7));
            return res;
        }

        public static DateRange FouthWeekOf(MonthsOfTheYear month, int year)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = new DateRange(ThirdWeekOf(month, year).EndDate.Value.AddDays(1),
                ThirdWeekOf(month, year).EndDate.Value.AddDays(7));
            return res;
        }

        public static DateRange FirstWeekOf(MonthsOfTheYear month)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FirstWeekOf(month, DateTime.Today.Year);
            return res;
        }

        public static DateRange SecondWeekOf(MonthsOfTheYear month)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = SecondWeekOf(month, DateTime.Today.Year);
            return res;
        }

        public static DateRange ThirdWeekOf(MonthsOfTheYear month)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = ThirdWeekOf(month, DateTime.Today.Year);
            return res;
        }

        public static DateRange FouthWeekOf(MonthsOfTheYear month)
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FouthWeekOf(month, DateTime.Today.Year);
            return res;
        }

        public static DateRange FirstWeekOfThisMonth()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FirstWeekOf((MonthsOfTheYear)DateTime.Today.Month, DateTime.Today.Year);
            return res;
        }

        public static DateRange SecondWeekOfThisMonth()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = SecondWeekOf((MonthsOfTheYear)DateTime.Today.Month, DateTime.Today.Year);
            return res;
        }

        public static DateRange ThirdWeekOfThisMonth()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = ThirdWeekOf((MonthsOfTheYear)DateTime.Today.Month, DateTime.Today.Year);
            return res;
        }

        public static DateRange FouthWeekOfThisMonth()
        {
            // Get the DateRange for the first Quarter of the given year
            DateRange res = FouthWeekOf((MonthsOfTheYear)DateTime.Today.Month, DateTime.Today.Year);
            return res;
        }

        public DateRange Add(DateRange futuredateToAdd)
        {
            DateRange res = new DateRange();
            if (futuredateToAdd.StartDate <= StartDate)
            {
                // backward Date
                res.StartDate = futuredateToAdd.StartDate;
                res.EndDate = EndDate;
                return res;
            }

            res.EndDate = futuredateToAdd.EndDate;
            res.StartDate = StartDate;
            return res;
        }

        ///// <summary>
        ///// Returns a Data Range object representing the week specified for the particular
        ///// month in this intance
        ///// </summary>
        ///// <param name="week">Week to return </param>
        ///// <returns>a New date range object</returns>
        //public DateRange GetWeek(WeeksOfTheMonth week)
        //{
        //    // return the week for the current month and year of this intance
        //    DateRange res = new DateRange();
        //    int month = StartDate.Month;
        //    DateTime st = DateTime.Today, end = DateTime.Today;
        //    switch (week)
        //    {
        //        case WeeksOfTheMonth.First:
        //            end  = StartDate.AddDays(7);
        //            break;
        //        case WeeksOfTheMonth.Second:
        //            break;
        //        case WeeksOfTheMonth.Third:
        //            break;
        //        case WeeksOfTheMonth.Fourth:
        //            break;
        //        default:
        //            break;
        //    }
        //    return new DateRange();
        //}
        public static DateRange GetForMonth(MonthsOfTheYear month)
        {
            return GetForMonth(month, DateTime.Today.Year);
        }

        public static DateRange GetForMonth(MonthsOfTheYear month, int year)
        {
            if (year > DateTime.Today.Year)
            {
                year = DateTime.Today.Year;
            }

            int monthIndex = (int)month;
            int addDays = 28;
            switch (month)
            {
                case MonthsOfTheYear.Jan:
                case MonthsOfTheYear.Mar:
                case MonthsOfTheYear.May:
                case MonthsOfTheYear.Jul:
                case MonthsOfTheYear.Aug:
                case MonthsOfTheYear.Oct:
                case MonthsOfTheYear.Dec:
                    addDays = 31;
                    break;
                case MonthsOfTheYear.Apr:
                case MonthsOfTheYear.Jun:
                case MonthsOfTheYear.Sept:
                case MonthsOfTheYear.Nov:
                    addDays = 30;
                    break;
                case MonthsOfTheYear.Feb:
                    addDays = 28;
                    /*
                        Rules for calculating leap years
    A year that is divisible by 4 is a leap year. (Y % 4) == 0
    Exception to rule 1: a year that is divisible by 100 is not a leap year. (Y % 100) != 0
    Exception to rule 2: a year that is divisible by 400 is a leap year. (Y % 400) == 0
                     */
                    bool isLeapYear = ((year % 4 == 0) && (year % 100 > 0)) || (year % 400 == 0);
                    if (isLeapYear) addDays = 29;

                    break;
            }

            DateTime startDate = new DateTime(year, monthIndex, 1);
            DateTime endDate = startDate.AddDays(addDays - 1);

            return new DateRange(startDate, endDate);
        }
    }
}
