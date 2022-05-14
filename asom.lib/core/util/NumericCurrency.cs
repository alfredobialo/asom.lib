namespace asom.lib.core.util
{
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
}