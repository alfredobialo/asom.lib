using System.Collections.Generic;

namespace asom.lib.core.util
{
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
        /// <param name="value">a positive integer to convert</param>
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
            bool res = (value.ToString().Length >= 4) && (value.ToString().Length <= 6);

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
