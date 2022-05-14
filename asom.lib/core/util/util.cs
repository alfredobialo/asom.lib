using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace itrex.businessObjects.model.core.util
{
    public class RandomSelector<T, U> where T : List<U>, new()
    {
        private int limit = 10; // default selector limit
        private T sourceLst;

        public RandomSelector(T source)
        {
            sourceLst = source;
        }

        public RandomSelector()
        {
        }

        public int Limit
        {
            get { return limit; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                limit = value;
            }
        }

        public T Randomize(int limit)
        {
            Limit = limit;
            return Randomize();
        }

        public T Randomize(int limit, T sourceList)
        {
            this.sourceLst = sourceList;
            Limit = limit;
            return Randomize();
        }

        public T SourceList
        {
            get { return sourceLst; }
            set { sourceLst = value; }
        }

        public T Randomize()
        {
            T res = sourceLst;
            T result = new T();
            int z = 0;
            int count = res.Count;
            if (sourceLst.Count < 1)
            {
                return sourceLst;
            }

            if (Limit > sourceLst.Count)
            {
                Limit = sourceLst.Count;
            }

            for (int i = 0; i < Limit; i++)
            {
                System.Threading.Thread.Sleep(50);
                Random rnd = new Random();
                for (int j = 0; j < count; j++)
                {
                    rnd = new Random();
                    z = new Random().Next(0, new Random().Next(1, count));
                }

                int index = rnd.Next(z, count);
                result.Add(res[index]);
                res.RemoveAt(index);
                count = res.Count;
            }

            return result;
        }
    }

    public static class NumericFormat
    {
        /// <summary>
        /// Use this function to format large numbers into a shorten representation
        /// eg: 20,000 becomes 20K, --> 1,200,000 becomes 1.2M
        /// </summary>
        /// <param name="value">the numeric value to format</param>
        /// <param name="startAt">at what position do u want the conversion to start. default is 10,000
        /// value below 10,000 will be displayed as it is</param>
        /// <returns>Textual representation of the formatted number</returns>
        public static string Format(decimal value, int startAt = 10000)
        {
            decimal divisor = 0.0m;
            string res = "";
            if (value < startAt)
            {
                res = value.ToString("#,###");
            }
            else
            {
                if (value >= 1000 && value < 1000000)
                {
                    divisor = Math.Round(value / 1000);
                    res = divisor.ToString() + "K";
                }
                else if (value >= 1000000 && value < 1000000000)

                {
                    divisor = Math.Round(value / 1000000);
                    res = divisor.ToString() + "M";
                }
                else if (value >= 1000000000 && value < 1000000000000)
                {
                    divisor = Math.Round(value / 1000000000);
                    res = divisor.ToString() + "B";
                }
                else if (value >= 1000000000000 && value < 1000000000000000)
                {
                    divisor = Math.Round(value / 1000000000000);
                    res = divisor.ToString() + "T";
                }
                else if (value >= 1000000000000000 && value < 1000000000000000000)
                {
                    divisor = Math.Round(value / 1000000000000000);
                    res = divisor.ToString() + "Q";
                }
                else
                {
                    res = value.ToString();
                }
            }

            return res;
        }
    }

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

    public enum MonthsOfTheYear
    {
        Jan = 1,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sept,
        Oct,
        Nov,
        Dec
    };

    public enum WeeksOfTheMonth
    {
        First = 1,
        Second,
        Third,
        Fourth
    };

    public enum QuarterOfYear
    {
        First = 1,
        Second,
        Third,
        Fouth
    };

    public enum DateFormat
    {
        Month,
        Day,
        Year,
        Minute,
        Second,
        Hour,
    };

    /// <summary>
    /// Use the DateUtil Class to display friendly date interval.
    ///
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// Display a friendly Date interval between two dates.
        /// Note: if lowerdate is greater than upperDate, 1sec is returned.
        /// Eg. lowerDate  = DateTime.Today();
        ///     upperDate  = lowerDate.AddTicks(8000);
        ///     string interval  = DateDiff(lowerdate,upperDate);  // = 8secs
        /// </summary>
        /// <param name="lowerDate">lowerDate value</param>
        /// <param name="upperDate">UpperDate value</param>
        /// <returns>Date Difference in string </returns>
        public static string DateDiff(DateTime lowerDate, DateTime upperDate)
        {
            return DateDiff(lowerDate, upperDate, false);
        }

        /// <summary>
        /// Do not use these function now because its still under dev. use the overloaded version
        ///
        /// </summary>
        /// <param name="lowerDate"></param>
        /// <param name="upperDate"></param>
        /// <param name="useExplicitFormat"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime lowerDate, DateTime upperDate, bool useExplicitFormat)
        {
            useExplicitFormat = true;
            string res = "";
            DateFormat datetype = DateFormat.Second;
            double v = DateChecker(lowerDate, upperDate, out datetype);

            string s_dateFormat1 = "secs", s_dateFormat2 = "";
            if (useExplicitFormat)
            {
                switch (datetype)
                {
                    case DateFormat.Month:
                        s_dateFormat1 = "month";
                        TimeSpan tsmon = upperDate.Subtract(lowerDate);
                        var tmor = GetQuotient(tsmon.Ticks, (TimeSpan.TicksPerDay * 30));
                        s_dateFormat1 = tmor > 1 ? s_dateFormat1 + "s" : s_dateFormat1;
                        var mon = tmor > 0;
                        if (mon)
                        {
                            TimeSpan tss = new TimeSpan(tsmon.Ticks % (TimeSpan.TicksPerDay * 30));
                            var days2 = Math.Floor(tss.TotalDays);
                            if (days2 >= 7)
                            {
                                var wks = GetQuotient((long)days2, (long)7);
                                var dayRem = days2 % 7;
                                if (dayRem > 0)
                                {
                                    // we have days left, so convert
                                    s_dateFormat2 = dayRem > 1 ? " and " + dayRem.ToString() + "days" : " and " + dayRem.ToString() + "day";
                                }

                                if (wks > 1)
                                {
                                    res = wks.ToString() + " wks" + s_dateFormat2;
                                }
                                else
                                {
                                    res = wks.ToString() + " wk"; // +" and " + s_dateFormat2;
                                }

                                // return res;
                            }

                            s_dateFormat2 = res;
                        }

                        break;
                    case DateFormat.Day:
                        //
                        s_dateFormat1 = "day";
                        TimeSpan tsdays = upperDate.Subtract(lowerDate);
                        var tdr = GetQuotient(tsdays.Ticks, TimeSpan.TicksPerDay);
                        s_dateFormat1 = tdr > 1 ? s_dateFormat1 + "s" : s_dateFormat1;
                        var days = tdr > 0;
                        if (days)
                        {
                            TimeSpan tss = new TimeSpan(tsdays.Ticks % TimeSpan.TicksPerDay);
                            var hrs = Math.Floor(tss.TotalHours);
                            if (hrs > 0)
                            {
                                s_dateFormat2 = hrs > 1 ? hrs.ToString() + "hrs" : hrs.ToString() + "hr";
                            }
                        }

                        break;
                    case DateFormat.Year:
                        s_dateFormat1 = "yr";
                        TimeSpan tsyear = upperDate.Subtract(lowerDate);
                        var tyr = GetQuotient(tsyear.Ticks, (TimeSpan.TicksPerDay * 30 * 12));
                        s_dateFormat1 = tyr > 1 ? s_dateFormat1 + "s" : s_dateFormat1;
                        var year = tyr > 0;
                        if (year)
                        {
                            TimeSpan tss = new TimeSpan(tsyear.Ticks % (TimeSpan.TicksPerDay * 30 * 12));
                            var month = Math.Floor((tss.TotalDays / 30));
                            if (month > 0)
                            {
                                s_dateFormat2 = month > 1 ? month.ToString() + "months" : month.ToString() + "month";
                            }
                        }

                        break;
                    case DateFormat.Minute:

                        // let work with minutes first,
                        // the idea here is that when we have a remainder from the
                        // get minute function, we convert to secs
                        s_dateFormat1 = "min";
                        TimeSpan ts = upperDate.Subtract(lowerDate);
                        var tmr = GetQuotient(ts.Ticks, TimeSpan.TicksPerMinute);
                        s_dateFormat1 = tmr > 1 ? s_dateFormat1 + "s" : s_dateFormat1;
                        var minn = tmr > 0;
                        if (minn)
                        {
                            TimeSpan tss = new TimeSpan(ts.Ticks % TimeSpan.TicksPerMinute);
                            var secs = Math.Floor(tss.TotalSeconds);
                            if (secs > 0)
                            {
                                s_dateFormat2 = secs > 1 ? secs.ToString() + "secs" : secs.ToString() + "sec";
                            }
                        }

                        break;
                    case DateFormat.Second:
                        if (v < 1)
                        {
                            v = 1;
                        }

                        return v > 1 ? v.ToString() + s_dateFormat1 : v.ToString() + "sec";
                        break;
                    case DateFormat.Hour:
                        s_dateFormat1 = "hr";
                        TimeSpan tshr = upperDate.Subtract(lowerDate);
                        var thr = GetQuotient(tshr.Ticks, TimeSpan.TicksPerHour);
                        s_dateFormat1 = thr > 1 ? s_dateFormat1 + "s" : s_dateFormat1;
                        var hr = thr > 0;
                        if (hr)
                        {
                            TimeSpan tss = new TimeSpan(tshr.Ticks % TimeSpan.TicksPerHour);
                            var min = Math.Floor(tss.TotalMinutes);
                            if (min > 0)
                            {
                                s_dateFormat2 = min > 1 ? min.ToString() + "mins" : min.ToString() + "min";
                            }
                        }

                        break;
                    default:
                        break;
                }
            }

            if (v > 1)
            {
                if (datetype == DateFormat.Day)
                {
                    if (v >= 7)
                    {
                        var wks = GetQuotient((long)v, (long)7);
                        var dayRem = v % 7;
                        if (dayRem > 0)
                        {
                            // we have days left, so convert
                            s_dateFormat2 = dayRem > 1 ? dayRem.ToString() + "days" : dayRem.ToString() + "day";
                        }

                        if (wks > 1)
                        {
                            res = wks.ToString() + " wks" + " " + s_dateFormat2;
                        }
                        else
                        {
                            res = wks.ToString() + " wk" + " " + s_dateFormat2;
                        }

                        return res;
                    }
                }

                res = v.ToString() + "" + s_dateFormat1;
            }
            else
            {
                res = v.ToString() + "" + s_dateFormat1;
            }

            return res + " " + s_dateFormat2;
        }

        static double DateChecker(DateTime lowerDate, DateTime upperDate, out DateFormat returnedDateType)
        {
            int res = 0;
            TimeSpan ts = upperDate.Subtract(lowerDate);
            var toMin = GetTicksForMinutes(ts.Ticks);
            var minn = GetQuotient(ts.Ticks, TimeSpan.TicksPerMinute);
            TimeSpan tss = new TimeSpan(ts.Ticks % TimeSpan.TicksPerMinute);
            var min_to_sec = tss.TotalSeconds;
            var toSec = GetTicksForSeconds(ts.Ticks);
            var resw = DateDiff(DateFormat.Second, tss);
            returnedDateType = DateFormat.Second;
            var yr = Math.Floor(DateDiff(DateFormat.Year, lowerDate, upperDate));
            var month = Math.Floor(DateDiff(DateFormat.Month, lowerDate, upperDate));
            var day = Math.Floor(DateDiff(DateFormat.Day, lowerDate, upperDate));
            var hour = Math.Floor(DateDiff(DateFormat.Hour, lowerDate, upperDate));
            var min = Math.Floor(DateDiff(DateFormat.Minute, lowerDate, upperDate));

            var sec = Math.Floor(DateDiff(DateFormat.Second, lowerDate, upperDate));
            double choose = sec;
            if (min >= 1 && min < choose)
            {
                // do logic here, like calculating the fraction of seconds remaining
                choose = min;
                returnedDateType = DateFormat.Minute;
            }

            if (hour >= 1 && hour < choose)
            {
                // do logic here, like calculating the fraction of seconds remaining
                choose = hour;
                returnedDateType = DateFormat.Hour;
            }

            if (day >= 1 && day < choose)
            {
                // do logic here, like calculating the fraction of seconds remaining
                choose = day;
                returnedDateType = DateFormat.Day;
            }

            if (month >= 1 && month < choose)
            {
                // do logic here, like calculating the fraction of seconds remaining
                choose = month;
                returnedDateType = DateFormat.Month;
            }

            if (yr >= 1 && yr < choose)
            {
                // do logic here, like calculating the fraction of seconds remaining
                choose = yr;
                returnedDateType = DateFormat.Year;
            }

            return choose;
        }

        public static double DateDiff(DateFormat format, TimeSpan timeDiff)
        {
            //TimeSpan ts = upperDate.Subtract(lowerDate);
            double res = timeDiff.TotalDays;
            switch (format)
            {
                case DateFormat.Month:
                    res = getMonth(timeDiff);
                    break;
                case DateFormat.Year:
                    res = getYear(timeDiff);
                    break;


                case DateFormat.Day:
                    res = Math.Floor(timeDiff.TotalDays);
                    break;
                case DateFormat.Minute:
                    res = Math.Floor(timeDiff.TotalMinutes);
                    break;
                case DateFormat.Second:
                    res = Math.Floor(timeDiff.TotalSeconds);
                    break;
                case DateFormat.Hour:
                    res = Math.Floor(timeDiff.TotalHours);
                    break;
            }

            return res;
        }

        public static double DateDiff(DateFormat format, DateTime lowerDate, DateTime upperDate)
        {
            TimeSpan ts = upperDate.Subtract(lowerDate);
            //double rem = 0;
            double res = ts.TotalDays;
            switch (format)
            {
                case DateFormat.Month:
                    res = getMonth(lowerDate, upperDate);
                    break;
                case DateFormat.Year:
                    res = getYear(lowerDate, upperDate);
                    break;
                case DateFormat.Day:
                    res = (ts.TotalDays);
                    break;


                case DateFormat.Minute:
                    res = (ts.TotalMinutes);
                    break;
                case DateFormat.Second:
                    res = (ts.TotalSeconds);
                    break;
                case DateFormat.Hour:
                    res = (ts.TotalHours);
                    break;
            }

            return res;
        }

        static double getMonth(DateTime lowerDate, DateTime upperDate)
        {
            return IntDiv((upperDate - lowerDate).TotalDays, 31);
        }

        static double getYear(DateTime lowerDate, DateTime upperDate)
        {
            return IntDiv(getMonth(lowerDate, upperDate), 12);
        }

        static double getMonth(TimeSpan ts)
        {
            return GetQuotient((ts.TotalDays), 31);
        }

        static double getYear(TimeSpan ts)
        {
            return GetQuotient(getMonth(ts), 12);
        }

        static double GetTicksForHour(double hour)
        {
            return hour * TimeSpan.TicksPerHour;
        }

        static double GetTicksForMinutes(double mins)
        {
            return mins * TimeSpan.TicksPerMinute;
        }

        static double GetTicksForDay(double day)
        {
            return day * TimeSpan.TicksPerDay;
        }

        static double GetTicksForSeconds(double sec)
        {
            return sec * TimeSpan.TicksPerSecond;
        }

        /*
This example of the fields of the TimeSpan class
generates the following output.

Field                              Value
-----                              -----
Maximum TimeSpan       10675199.02:48:05.4775807
Minimum TimeSpan      -10675199.02:48:05.4775808
Zero TimeSpan                   00:00:00

Ticks per day            864,000,000,000
Ticks per hour            36,000,000,000
Ticks per minute             600,000,000
Ticks per second              10,000,000
Ticks per millisecond             10,000
*/
        public static double IntDiv(double a, double b)
        {
            // take the quotient from a division
            return Math.Floor(a / b);
        }

        public static long GetQuotient(long v1, long v2)
        {
            long res = 0;
            res = ((v1 - (v1 % v2)) / v2);
            return res;
        }

        public static double GetQuotient(double v1, double v2)
        {
            double res = 0;
            res = ((v1 - (v1 % v2)) / v2);
            return res;
        }
    }

    public static class Helper
    {
        /// <summary>
        /// Build a SEO friendly Id from The Source String as Id Optional Character for replacement, default is '-'
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public static string GetPrettyUrlId(string sourceId)
        {
            return GetPrettyUrlId(sourceId, "-");
        }

        public static string GetPrettyUrlId(string sourceId, int restrictTo)
        {
            string res = GetPrettyUrlId(sourceId, "-");
            if (restrictTo > 0)
            {
                if (res.Length > restrictTo)
                {
                    res = res.Substring(0, restrictTo - 1);
                    if (RightString(res, 1) == "-")
                    {
                        res = LeftString(res, res.Length - 1);
                    }
                }
            }

            return res;
        }

        public static string GetPrettyUrlId(string sourceId, string replaceCharacter)
        {
            string res = sourceId.ToLower();
            const string regExPattern = @"[A-Za-z0-9]+";

            Regex reg = new Regex(regExPattern, RegexOptions.IgnoreCase);
            var newMatch = reg.Match(res);

            string builder = "";
            while (newMatch.Success)
            {
                builder += newMatch.Value + replaceCharacter;
                newMatch = newMatch.NextMatch();
            }

            if (!string.IsNullOrWhiteSpace(builder) && builder.Length > 1)
            {
                builder = builder.Substring(0, builder.Length - 1);
                res = builder;
            }

            return res;
        }

        public static byte[] GetPictureBytes(string imageFileName)
        {
            byte[] res = null;
            FileStream fs = new FileStream(imageFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            res = new byte[fs.Length];
            string fileExtension = Path.GetExtension(imageFileName);
            if ((fileExtension.ToLower() != ".jpg") && (fileExtension.ToLower() != ".gif") && (fileExtension.ToLower() != ".bmp") &&
                (fileExtension.ToLower() != ".png") && (fileExtension.ToLower() != ".jpeg"))
            {
                throw new IOException("Invalid Image Detected. Image File type Must either JPEG, GIF, BMP Or PNG Files");
            }

            fs.Read(res, 0, res.Length);
            fs.Flush();
            fs.Close();
            return res;
        }

        public static bool IsValidImageFile(string imageFileName)
        {
            bool res = true;

            string fileExtension = Path.GetExtension(imageFileName);
            if ((fileExtension.ToLower() != ".jpg") && (fileExtension.ToLower() != ".gif") && (fileExtension.ToLower() != ".bmp") &&
                (fileExtension.ToLower() != ".png") && (fileExtension.ToLower() != ".jpeg"))
            {
                res = false;
            }

            return res;
        }

        public enum DateMode
        {
            Day,
            Month,
            Year,
            Hour,
            Minute,
            Second,
            MilliSecond
        };

        /// <summary>
        /// Gets the Quotient from the Division of Two Numbers
        /// </summary>
        /// <param name="v1">Numerator</param>
        /// <param name="v2">Denominator</param>
        /// <returns>Integer without Remainder</returns>
        public static long IntDiv(long v1, long v2)
        {
            long res = 0;
            res = ((v1 - (v1 % v2)) / v2);
            return res;
        }

        /// <summary>
        /// Returns an Inverse of a String. eg. ABC = CBA
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReverseString(string value)
        {
            string res = "";
            for (int i = 0; i < value.Length; i++)
            {
                res = value[i].ToString() + res;
            }

            return res;
        }

        /// <summary>
        /// Returns a Substring of a string from it's right most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from right.</param>
        /// <returns>substring</returns>
        public static string RightString(string text, int len)
        {
            if (len > text.Length) return text;
            string res = text.Substring((text.Length - 1) - (len - 1), len);
            return res;
        }

        /// <summary>
        /// Returns a Substring of a string from it's left most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from left.</param>
        /// <returns>substring</returns>
        public static string LeftString(string text, int len)
        {
            if (len > text.Length) return text;
            return text.Substring(0, len);
        }

        /// <summary>
        /// Get a file as byte array
        /// </summary>
        /// <param name="fileName">The File Name and Path to retrived</param>
        /// <param name="fileExtension">outputs the file extension</param>
        /// <returns>File as Byte Array</returns>
        public static byte[] GetFileAsBytes(string fileName, out string fileExtension)
        {
            byte[] res = null;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            res = new byte[fs.Length];
            fileExtension = Path.GetExtension(fileName);
            fs.Read(res, 0, res.Length);
            fs.Flush();
            fs.Close();
            return res;
        }

        /// <summary>
        /// Get a file as byte array
        /// </summary>
        /// <param name="fileName">The File Name and Path to retrived</param>
        /// <returns>File as Byte Array</returns>
        public static byte[] GetFileAsBytes(string fileName)
        {
            byte[] res = null;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            res = new byte[fs.Length];

            fs.Read(res, 0, res.Length);
            fs.Flush();
            fs.Close();
            return res;
        }

        public static bool IsDate(string date)
        {
            bool res = false;
            try
            {
                DateTime.Parse(date);
                res = true;
            }
            catch (Exception err)
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Determines whether the specified
        /// value can be converted to a valid number.
        /// </summary>
        public static bool IsNumeric(object value)
        {
            double dbl;
            return double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo, out dbl);
        }
    }

    /// <summary>
    /// Binary Number calculation Helper Class
    /// </summary>
    public static class BinaryConversion
    {
        public static string ConvertBase10ToBaseN(long base10Value, int baseN)
        {
            bool exit = false;
            string hexMap = "0123456789ABCDEF";
            string res = "";
            int baseValue = baseN;
            if (baseValue < 2 || baseValue > 16)
            {
                throw new Exception("Wrong base Value entered.");
            }

            try
            {
                long v1 = base10Value;
                long v2 = 0;

                do
                {
                    v2 = v1 % baseValue;
                    v1 = intDiv(v1, baseValue);
                    res += hexMap[(int)v2].ToString();
                    if (v1 < baseValue)
                    {
                        res += hexMap[(int)v1].ToString();
                        exit = true;
                    }
                } while (exit == false);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return reverse(res);
        }

        public static long intDiv(long v1, long v2)
        {
            long res = 0;
            res = ((v1 - (v1 % v2)) / v2);
            return res;
        }

        public static string reverse(string value)
        {
            string res = "";
            for (int i = 0; i < value.Length; i++)
            {
                res = value[i].ToString() + res;
            }

            return res;
        }

        public static string HTMLColorCode(int r, int g, int b)
        {
            // red validator
            if (r < 0) r = 0;
            if (r > 255) r = 255;
            //////////////////////

            // green validator
            if (g < 0) g = 0;
            if (g > 255) g = 255;

            ////////////////////

            // blue validator
            if (b < 0) b = 0;
            if (b > 255) b = 255;

            string colorCode = "";
            string r1, g1, b1;
            r1 = ConvertBase10ToBaseN(r, 16);

            if (r1.Length < 2)
            {
                r1 = "0" + r1;
            }

            g1 = ConvertBase10ToBaseN(g, 16);

            if (g1.Length < 2)
            {
                g1 = "0" + r1;
            }

            b1 = ConvertBase10ToBaseN(b, 16);

            if (b1.Length < 2)
            {
                b1 = "0" + r1;
            }

            colorCode = "#" + r1 + g1 + b1;
            return colorCode;
        }

        /// <summary>
        /// Converts a Number in base N to Base 10.
        /// Example Convert AEF214F in Base 16 to Base 10
        /// </summary>
        /// <param name="value">Number to Convert</param>
        /// <param name="baseN">base of number</param>
        /// <returns>Result of Convertion as a String</returns>
        public static string ConvertToBase10(string value, int baseN)
        {
            Dictionary<string, int> mapper = new Dictionary<string, int>();
            mapper.Add("0", 0);
            mapper.Add("1", 1);
            mapper.Add("2", 2);
            mapper.Add("3", 3);
            mapper.Add("4", 4);
            mapper.Add("5", 5);
            mapper.Add("6", 6);
            mapper.Add("7", 7);
            mapper.Add("8", 8);
            mapper.Add("9", 9);
            mapper.Add("A", 10);
            mapper.Add("B", 11);
            mapper.Add("C", 12);
            mapper.Add("D", 13);
            mapper.Add("E", 14);
            mapper.Add("F", 15);

            string data = value.ToUpper();
            long res = 0;
            int j = 0;
            for (int i = 0; i < data.Length; i++)
            {
                j++;
                res += mapper[data[i].ToString()] * (long)Math.Pow(baseN, data.Length - j);
            }

            return res.ToString();
        }

        /// <summary>
        /// Convert a number in Base N to another base
        /// </summary>
        /// <param name="value">Number to Convert</param>
        /// <param name="inBaseN">Current Base of Number</param>
        /// <param name="toBaseNi">New Base to Convert To</param>
        /// <returns></returns>
        public static string ConvertFromBaseN_To_N1(string value, int inBaseN, int toBaseNi)
        {
            Dictionary<string, int> mapper = new Dictionary<string, int>();
            mapper.Add("0", 0);
            mapper.Add("1", 1);
            mapper.Add("2", 2);
            mapper.Add("3", 3);
            mapper.Add("4", 4);
            mapper.Add("5", 5);
            mapper.Add("6", 6);
            mapper.Add("7", 7);
            mapper.Add("8", 8);
            mapper.Add("9", 9);
            mapper.Add("A", 10);
            mapper.Add("B", 11);
            mapper.Add("C", 12);
            mapper.Add("D", 13);
            mapper.Add("E", 14);
            mapper.Add("F", 15);
            string res = "";
            /*
             * First step:
             * Convert the Number in base N to base 10
             * Convert the  result in Base 10 to base Ni
             * */
            string re2 = "", re = ConvertToBase10(value, inBaseN);
            for (int i = 0; i < re.Length; i++)
            {
                re2 += mapper[re[i].ToString()].ToString();
            }

            res = ConvertBase10ToBaseN(long.Parse(re2), toBaseNi);
            return res;
        }
    }


    /// <summary>
    /// A utility Class use to Perform Byte calculation.
    /// This class Cannot be Inherited
    /// </summary>
    public sealed class Bytes
    {
        /// <summary>
        /// Converts Bytes in KiloByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static double GetKB(double bytes)
        {
            if (bytes <= 0) return 0;
            return Math.Round((bytes / 1024), 2);
        }

        /// <summary>
        /// Converts Bytes in MegaByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static double GetMB(double bytes)
        {
            return Math.Round((GetKB(bytes) / 1024), 2);
        }

        /// <summary>
        /// Converts Bytes in GigaByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static double GetGB(double bytes)
        {
            return Math.Round((GetMB(bytes) / 1024), 2);
        }

        public static double FromXtoBytes(int x, SizeType sizeOfx)
        {
            double res = x;
            switch (sizeOfx)
            {
                case SizeType.Bytes:
                    res = x;
                    break;
                case SizeType.KiloBytes:
                    res = 1024 * x;
                    break;
                case SizeType.MegaBytes:
                    res = 1024 * 1024 * x;
                    break;
                case SizeType.GigaBytes:
                    res = 1024 * 1024 * 1024 * x;
                    break;

                default:
                    break;
            }

            return res;
        }

        /// <summary>
        /// Returns Byte Size in a Specified Format
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <param name="type">Size type Constants</param>
        /// <returns>size</returns>
        public static double GetSizeIn(double bytes, SizeType type)
        {
            double res = 0.0;
            switch (type)
            {
                case SizeType.Bytes:
                    res = bytes;
                    break;
                case SizeType.GigaBytes:
                    res = GetGB(bytes);
                    break;
                case SizeType.KiloBytes:
                    res = GetKB(bytes);
                    break;
                case SizeType.MegaBytes:
                    res = GetMB(bytes);
                    break;
                case SizeType.LargerThanGigaByte:
                    res = GetGB(bytes) / 1024;
                    break;
            }

            return res;
        }

        /// <summary>
        /// Get The Size Type of a byte
        /// </summary>
        /// <param name="bytes">byte data</param>
        /// <returns>Size Type Constans</returns>
        public static SizeType GetSizeType(double bytes)
        {
            SizeType res = SizeType.LargerThanGigaByte;
            if (IsInGBRange(bytes))
            {
                res = SizeType.GigaBytes;
            }
            else if (IsInMBRange(bytes))
            {
                res = SizeType.MegaBytes;
            }
            else if (IsInKBRange(bytes))
            {
                res = SizeType.KiloBytes;
            }
            else if (bytes < 1024)
            {
                res = SizeType.Bytes;
            }

            return res;
        }

        /// <summary>
        /// Textual representation of Bytes Converted to KiloByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static string GetKBString(double bytes)
        {
            return Bytes.GetKB(bytes).ToString() + "KB";
        }

        /// <summary>
        /// Textual representation of Bytes Converted to MegaByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static string GetMBString(double bytes)
        {
            return Bytes.GetMB(bytes).ToString() + "MB";
        }

        /// <summary>
        /// Textual representation of Bytes Converted to GigaByte
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <returns>result of Conversion</returns>
        public static string GetGBString(double bytes)
        {
            return Bytes.GetGB(bytes).ToString() + "GB";
        }

        private Bytes()
        {
        }

        /// <summary>
        /// Checks if a byte value is in Giga byte Range.
        /// 1024MB  = 1GB, but 640MB, should be Interpreted as 640MB, instead of 0.62GB
        /// </summary>
        /// <param name="bytes">bytes to check</param>
        /// <returns>true if bytes is in Giga byte Range</returns>
        public static bool IsInGBRange(double bytes)
        {
            return (GetGB(bytes) >= 0.99);
        }

        /// <summary>
        /// Checks if a byte value is in Mega byte Range.
        /// 1024KB  = 1MB, but 640KB, should be Interpreted as 640 Kilo Byte, instead of 0.62MB
        /// </summary>
        /// <param name="bytes">bytes to check</param>
        /// <returns>true if bytes is in Mega byte Range</returns>
        public static bool IsInMBRange(double bytes)
        {
            return (GetMB(bytes) >= 0.99);
        }

        /// <summary>
        /// Checks if a byte value is in Giga byte Range.
        /// 1024 bytes  = 1KB, but 640 bytes, should be Interpreted as 640 bytes, instead of 0.62KB
        /// </summary>
        /// <param name="bytes">bytes to check</param>
        /// <returns>true if bytes is in Kilo byte Range</returns>
        public static bool IsInKBRange(double bytes)
        {
            return (GetKB(bytes) >= 0.99);
        }

        /// <summary>
        /// Performs automatic Calculation of a Byte value
        /// </summary>
        /// <param name="bytes">bytes to perform calculation on</param>
        /// <returns>size in double</returns>
        public static double GetLogicalSize(double bytes)
        {
            double res = 0;
            if (IsInGBRange(bytes))
            {
                res = GetGB(bytes);
            }
            else if (IsInMBRange(bytes))
            {
                res = GetMB(bytes);
            }
            else if (IsInKBRange(bytes))
            {
                res = GetKB(bytes);
            }
            else
            {
                res = (bytes);
            }

            return res;
        }

        /// <summary>
        /// Performs automatic Calculation of a Byte value and returns a textual representation of the result.
        /// </summary>
        /// <param name="bytes">bytes to perform calculation on</param>
        /// <returns>size in string</returns>
        public static string GetSize(double bytes)
        {
            string res = "";
            if (IsInGBRange(bytes))
            {
                res = GetGBString(bytes);
            }
            else if (IsInMBRange(bytes))
            {
                res = GetMBString(bytes);
            }
            else if (IsInKBRange(bytes))
            {
                res = GetKBString(bytes);
            }
            else
            {
                res = (bytes.ToString() + "bytes");
            }

            return res;
        }

        public double GetByteSizeFor(double size, SizeType sizeIsIn)
        {
            double res = 0.0;
            switch (sizeIsIn)
            {
                case SizeType.KiloBytes:

                    res = size * 1024;
                    break;

                case SizeType.MegaBytes:

                    res = size * 1024 * 1024;
                    break;
                case SizeType.GigaBytes:

                    res = size * 1024 * 1024 * 1024;
                    break;
                default:
                    res = size;
                    break;
            }

            return res;
        }
    }

    /// <summary>
    /// Size Type Constants
    /// </summary>
    public enum SizeType : int
    {
        /// <summary>
        /// Size is In Bytes
        /// </summary>
        Bytes,

        /// <summary>
        /// Size Is In KiloBytes
        /// </summary>
        KiloBytes,

        /// <summary>
        /// Size is in MegaBytes
        /// </summary>
        MegaBytes,

        /// <summary>
        /// Size is in GigaBytes
        /// </summary>
        GigaBytes,

        /// <summary>
        /// Size is greater than GigaBytes
        /// </summary>
        LargerThanGigaByte
    };

    public enum CurrencyFormat
    {
        NGN = 1,
        USD,
        YEN,
        KES,
        EUR,
        GBP,
    }

    public static class Util
    {
        public static string DuplicateString(string value, int num)
        {
            string res = "";
            for (int i = 1; i <= num; i++)
            {
                res += value.Trim().Substring(0, 1);
            }

            return res;
        }

        /// <summary>
        /// Duplicate a Character
        /// </summary>
        /// <param name="value">character</param>
        /// <param name="num">number of times</param>
        /// <returns>duplicated string</returns>
        public static string DuplicateString(char value, int num)
        {
            string res = "";
            for (int i = 1; i <= num; i++)
            {
                res += value;
            }

            return res;
        }

        /// <summary>
        /// Returns a Substring of a string from it's left most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from left.</param>
        /// <returns>substring</returns>
        public static string LeftString(string text, int len)
        {
            if (len > text.Length) return text;
            return text.Substring(0, len);
        }

        /// <summary>
        /// Returns a Substring of a string from it's right most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from right.</param>
        /// <returns>substring</returns>
        public static string RightString(string text, int len)
        {
            if (len > text.Length) return text;
            string res = text.Substring((text.Length - 1) - (len - 1), len);
            return res;
        }
    }

    public class NumericCurrency
    {
        static NumericToText n = new NumericToText();
        CurrencyFormat cur = CurrencyFormat.NGN;

        public NumericCurrency(CurrencyFormat currency)
        {
            cur = currency;
        }


        public int IntegerDivisor(int numerator, int denominator)
        {
            int res = 0;
            // return zero if numerator is less than denominator
            if (numerator < denominator)
            {
                res = 0;
            }
            else
            {
                res = (numerator - (numerator % denominator)) / denominator;
            }

            return res;
        }

        public static string ConvertCurrencyValueToWord(double amount, string currencyName, string fractionPart)
        {
            int rem = 0;
            string result;
            long wholeValue; //  stores the whole value part of the currency
            int decimalPart; //stores the decimal part of the currency
            string amountInString = amount.ToString();
            int decimalPos;
            // look for decimal
            if (amountInString.IndexOf(".") != -1)
            {
                // we found decimal
                decimalPos = amountInString.IndexOf(".");

                wholeValue = long.Parse(amountInString.Substring(0, decimalPos));

                decimalPart = int.Parse(amountInString.Substring(decimalPos + 1));

                wholeValue += DecimalCurrencyToWholeCurrencyConverter(decimalPart, out rem);
                if (rem > 0)
                {
                    if (wholeValue > 0)
                    {
                        result = ConvertCurrencyToWords(wholeValue, currencyName) + ", " + n.NumericText(rem) + " " + fractionPart;
                    }
                    else
                    {
                        result = n.NumericText(rem) + " " + fractionPart;
                    }
                }
                else
                {
                    result = ConvertCurrencyToWords(wholeValue, currencyName);
                }
            }
            else
            {
                wholeValue = (long)amount;
                result = ConvertCurrencyToWords(wholeValue, currencyName);
            }

            return result;
        }

        public string ConvertDecimalCurrency(double amount, string koboSymbol)
        {
            int rem = 0;
            string result;
            long wholeValue; //  stores the whole value part of the currency
            int decimalPart; //stores the decimal part of the currency
            string amountInString = amount.ToString();
            int decimalPos;
            // look for decimal
            if (amountInString.IndexOf(".") != -1)
            {
                // we found decimal
                decimalPos = amountInString.IndexOf(".");

                wholeValue = long.Parse(amountInString.Substring(0, decimalPos));

                decimalPart = int.Parse(amountInString.Substring(decimalPos + 1));

                wholeValue += DecimalCurrencyToWholeCurrencyConverter(decimalPart, out rem);
                if (rem > 0)
                {
                    if (wholeValue > 0)
                    {
                        result = this.ConvertNumericToTextCurrency(wholeValue) + ", " + n.NumericText(rem) + " " + koboSymbol;
                    }
                    else
                    {
                        result = n.NumericText(rem) + " " + koboSymbol;
                    }
                }
                else
                {
                    result = this.ConvertNumericToTextCurrency(wholeValue);
                }
            }
            else
            {
                wholeValue = (long)amount;
                result = this.ConvertNumericToTextCurrency(wholeValue);
            }

            return result;
        }

        private static int DecimalCurrencyToWholeCurrencyConverter(int value, out int remainder)
        {
            int res = 0;
            if (value > 99)
            {
                if ((value % 100) != 0)
                {
                    res = new NumericCurrency().IntegerDivisor(value, 100);
                    remainder = value % 100;
                    //return res;
                }
                else
                {
                    res = value / 100;
                    remainder = 0;
                }
            }
            else
            {
                res = 0;
                remainder = value;
            }

            return res;
        }

        public NumericCurrency()
        {
        }

        /// <summary>
        /// Converts Numeric Number to Their Textual equivalent and appends a Default Currency Name (Naira)
        /// </summary>
        /// <param name="value">number</param>
        /// <returns>Textual Representation of Number</returns>
        internal static string ConvertCurrencyToWords(long value, string currencyName)
        {
            return new NumericCurrency().ConvertNumericToTextCurrency(value, currencyName);
        }

        public string ConvertNumericToTextCurrency(long value)
        {
            return ConvertNumericToTextCurrency(value, Currency.ToString());
        }

        /// <summary>
        /// Converts Numeric Number to Their Textual equivalent and appends Currency Name
        /// </summary>
        /// <param name="value">number</param>
        /// <param name="currencyName">Currency Name to use. eg USD, YEN etc.</param>
        /// <returns>Textual Representation of Number</returns>
        public string ConvertNumericToTextCurrency(long value, string currencyName)
        {
            string minus = "";
            if (value < 0)
            {
                minus = "Minus";
                value *= -1;
            }

            string res = n.NumericText(value);
            res = minus + " " + res + " " + currencyName;
            return res;
        }

        public CurrencyFormat Currency
        {
            get { return cur; }
            set { cur = value; }
        }
    }

    /// <summary>
    /// Represents a class that can convert numeric numbers to Textual Equivalent
    /// </summary>
    public class NumericToText
    {
        const string HUNDRED = "Hundred";
        const string THOUSAND = "Thousand";
        const string MILLION = "Million";
        const string BILLION = "Billion";

        Dictionary<long, string> map = new Dictionary<long, string>();

        public NumericToText()
        {
            #region Declaration

            map.Add(0, "");
            map.Add(1, "One");
            map.Add(2, "Two");
            map.Add(3, "Three");
            map.Add(4, "Four");
            map.Add(5, "Five");
            map.Add(6, "Six");
            /////////////////
            map[7] = "Seven";
            map[8] = "Eight";
            map[9] = "Nine";
            map[10] = "Ten";
            map[11] = "Eleven";
            map[12] = "Twelve";
            map[13] = "Thirteen";
            /////////////////////
            map.Add(14, "Fourteen");
            map.Add(15, "Fifteen");
            map.Add(16, "Sixteen");
            map.Add(17, "Seventeen");
            map.Add(18, "Eighteen");
            map.Add(19, "Nineteen");
            map.Add(20, "Twenty");
            /////////////////////
            map.Add(30, "Thirty");
            map.Add(40, "Fourty");
            map.Add(50, "Fifty");
            map.Add(60, "Sixty");
            map.Add(70, "Seventy");
            map.Add(80, "Eighty");
            map.Add(90, "Ninety");
            /////////////////////

            #endregion
        }

        /// <summary>
        /// Converts a numeric number to a Text
        /// </summary>
        /// <param name="value">a positive interger to convert</param>
        /// <returns>Converted Text</returns>
        public string NumericText(long value)
        {
            #region CORE API From CSD INC

            string res = "";
            try
            {
                if (IsNegative(value))
                {
                    return "Negative Not Supported";
                }

                if ((IsTens(value)) && (Digits(value) <= 2))
                {
                    res = map[value];
                }
                else
                {
                    #region

                    if ((Digits(value) == 2) && (value.ToString().Substring(1, 1) == "0"))
                    {
                        res = map[value];
                    }
                    else if ((Digits(value) == 2) && (value.ToString().Substring(1, 1) != "0"))
                    {
                        res = GetTwoDigitsFromTwenty(value) + " " + map[long.Parse(value.ToString().Substring(1, 1))];
                    }
                    else if ((Digits(value) == 3) && (value.ToString().Substring(1) == "00"))
                    {
                        res = map[long.Parse(value.ToString().Substring(0, 1))] + " " + HUNDRED;
                    }
                    else if ((Digits(value) == 3) && (value.ToString().Substring(1) != "00"))
                    {
                        res = map[long.Parse(value.ToString().Substring(0, 1))] + " " + HUNDRED + " And " +
                            NumericText(long.Parse(value.ToString().Substring(1)));
                    }
                    else if ((Digits(value) == 4) && (value.ToString().Substring(1) == "000"))
                    {
                        res = map[long.Parse(value.ToString().Substring(0, 1))] + " " + THOUSAND;
                    }
                    else if ((Digits(value) == 4) && (value.ToString().Substring(1) != "000"))
                    {
                        if (long.Parse(value.ToString().Substring(1)) < 100)
                        {
                            res = map[long.Parse(value.ToString().Substring(0, 1))] + " " + THOUSAND + " And, " +
                                NumericText(long.Parse(value.ToString().Substring(1)));
                        }
                        else
                        {
                            res = map[long.Parse(value.ToString().Substring(0, 1))] + " " + THOUSAND + " " +
                                NumericText(long.Parse(value.ToString().Substring(1)));
                        }
                    }

                    // if ends here

                    #endregion

                    #region complex if...else

                    else if ((Digits(value) > 4) && (Digits(value) < 7))
                    {
                        if (IsInThousandth(value))
                        {
                            if (Digits(value) == 5)
                            {
                                if (value.ToString().Substring(2) == "000")
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 2))) + " " + THOUSAND;
                                }
                                else if (long.Parse(value.ToString().Substring(2)) < 100)
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 2))) + " " + THOUSAND + " And, " +
                                        NumericText(long.Parse(value.ToString().Substring(2)));
                                }
                                else
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 2))) + " " + THOUSAND + ", " +
                                        NumericText(long.Parse(RightStr(value.ToString(), 3)));
                                }
                            }
                            else
                            {
                                if (value.ToString().Substring(3) == "000")
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 3))) + " " + THOUSAND;
                                }
                                else if (long.Parse(value.ToString().Substring(3)) < 100)
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 3))) + " " + THOUSAND + " And, " +
                                        NumericText(long.Parse(value.ToString().Substring(3)));
                                }
                                else
                                {
                                    res = NumericText(long.Parse(value.ToString().Substring(0, 3))) + " " + THOUSAND + ", " +
                                        NumericText(long.Parse(RightStr(value.ToString(), 3)));
                                }
                            }
                        }
                    }

                    #endregion

                    ///////////////////////////

                    #region complex if...else Section 3

                    else if ((Digits(value) > 6) && (Digits(value) < 10))
                    {
                        if (!IsInThousandth(value))
                        {
                            if (Digits(value) == 7)
                            {
                                res = NumericText(long.Parse(LeftStr(value.ToString(), 1))) + " " + MILLION + ", " +
                                    NumericText(long.Parse(RightStr(value.ToString(), 6)));
                            }
                            else if (Digits(value) == 8)
                            {
                                if (RightStr(value.ToString(), 6) == "000000")
                                {
                                    res = NumericText(long.Parse(LeftStr(value.ToString(), 2))) + " " + MILLION;
                                }
                                else
                                {
                                    res = NumericText(long.Parse(LeftStr(value.ToString(), 2))) + " " + MILLION + ", " +
                                        NumericText(long.Parse(RightStr(value.ToString(), 6)));
                                }
                            }
                            else if (Digits(value) == 9)
                            {
                                if (RightStr(value.ToString(), 6) == "000000")
                                {
                                    res = NumericText(long.Parse(LeftStr(value.ToString(), 3))) + " " + MILLION;
                                }
                                else
                                {
                                    res = NumericText(long.Parse(LeftStr(value.ToString(), 3))) + " " + MILLION + ", " +
                                        NumericText(long.Parse(RightStr(value.ToString(), 6)));
                                }
                            }
                        }
                    }

                    #endregion

                    #region complex if...else Section 4

                    else if ((Digits(value) > 9) && (Digits(value) < 13))
                    {
                        if (Digits(value) == 10)
                        {
                            res = NumericText(long.Parse(LeftStr(value.ToString(), 1))) + " " + BILLION + ", " +
                                NumericText(long.Parse(RightStr(value.ToString(), 9)));
                        }
                        else if (Digits(value) == 11)
                        {
                            res = NumericText(long.Parse(LeftStr(value.ToString(), 2))) + " " + BILLION + ", " +
                                NumericText(long.Parse(RightStr(value.ToString(), 9)));
                        }
                        else if (Digits(value) == 12)
                        {
                            res = NumericText(long.Parse(LeftStr(value.ToString(), 3))) + " " + BILLION + ", " +
                                NumericText(long.Parse(RightStr(value.ToString(), 9)));
                        }
                    }

                    #endregion

                    ///////////////////////////
                }
            }
            catch
            {
                return "Unknown Format";
            }

            return res;

            #endregion
        }

        #region Helpers

        bool IsTens(long value)
        {
            if (value < 0)
            {
                return ((value * -1) < 20);
            }

            return (value < 20);
        }

        long Digits(long value)
        {
            return value.ToString().Length;
        }

        bool IsNegative(long value)
        {
            return value < 0;
        }

        bool IsInThousandth(long value)
        {
            bool res = false;
            if ((value.ToString().Length >= 4) && (value.ToString().Length <= 6))
            {
                res = true;
            }

            return res;
        }

        string GetTwoDigitsFromTwenty(long value)
        {
            string res = "";
            if ((Digits(value) == 2) && (value.ToString().Substring(1, 1) != "0"))
            {
                value = value - long.Parse(value.ToString().Substring(1, 1));
                if ((Digits(value) == 2) && (value.ToString().Substring(1, 1) == "0"))
                {
                    res = map[value];
                }
            }

            return res;
        }

        /// <summary>
        /// Returns a Substring of a string from it's left most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from left.</param>
        /// <returns>substring</returns>
        public string LeftStr(string text, int len)
        {
            if (len > text.Length) return text;
            return text.Substring(0, len);
        }

        /// <summary>
        /// Returns a Substring of a string from it's right most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from right.</param>
        /// <returns>substring</returns>
        public string RightStr(string text, int len)
        {
            if (len > text.Length) return text;
            string res = text.Substring((text.Length - 1) - (len - 1), len);
            return res;
        }


        ////// static functions

        /// <summary>
        /// Returns a Substring of a string from it's left most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from left.</param>
        /// <returns>substring</returns>
        public static string LeftString(string text, int len)
        {
            if (len > text.Length) return text;
            return text.Substring(0, len);
        }

        /// <summary>
        /// Returns a Substring of a string from it's right most position
        /// </summary>
        /// <param name="text">string to extract a substring</param>
        /// <param name="len">the length of string to to retrive from right.</param>
        /// <returns>substring</returns>
        public static string RightString(string text, int len)
        {
            if (len > text.Length) return text;
            string res = text.Substring((text.Length - 1) - (len - 1), len);
            return res;
        }

        #endregion
    }
}
