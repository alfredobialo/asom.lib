using System;

namespace asom.lib.core.util
{
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
}