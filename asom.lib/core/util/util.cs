using System;
using System.Linq;

namespace asom.lib.core.util
{
    public static class Util
    {
        public static string NewId()
        {
            string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            id = id.Substring(0, 4) + id.Substring(7, 6);
            return id;
        }

        public static string NewId(int maxLength)
        {
            string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            id = id.Substring(0, 4) + id.Substring(7, 6);

            if (id.Length < maxLength)
            {
                string id2 = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                var remaining = maxLength - id.Length;
                id += id2.Substring(3, remaining);
            }

            return id;
        }

        public static string NewNumericId()
        {
            string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            // remove alphabets
            id = string.Concat(id.Where(x => char.IsDigit(x)));
            int len = id.Length;
            var date = $"{DateTime.UtcNow.Millisecond}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Month}";
            id = id.Substring(0, len >= 10 ? 10 : len) + date;
            return id;
        }

        public static string NewNumericId(int maxLength)
        {
            string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            // remove alphabets
            id = string.Concat(id.Where(x => char.IsDigit(x)));
            int len = id.Length;
            var date = $"{DateTime.UtcNow.Millisecond}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Month}";
            
            id = id.Substring(0, len >= maxLength ? maxLength : len);
            if (id.Length < maxLength) id += date;
            
            return id;
        }

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
