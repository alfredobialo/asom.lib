using System;

namespace asom.lib.core.util
{
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
}
