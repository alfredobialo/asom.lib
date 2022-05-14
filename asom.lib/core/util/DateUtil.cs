using System;

namespace asom.lib.core.util
{
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
}