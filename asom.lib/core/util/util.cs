namespace asom.lib.core.util
{
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
            string res = text.Substring(text.Length - len);
            return res;
        }
    }
}
