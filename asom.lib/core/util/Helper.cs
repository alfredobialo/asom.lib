using System;
using System.IO;
using System.Text.RegularExpressions;

namespace asom.lib.core.Util
{
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
}