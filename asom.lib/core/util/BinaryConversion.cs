using System;
using System.Collections.Generic;

namespace asom.lib.core.Util
{
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
}