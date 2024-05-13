using System;
using System.Globalization;

namespace Alteridem.NMEA.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Parse a two character hex string to a byte value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static byte ParseByte(this string value, byte defaultValue = 0)
    {
        if (byte.TryParse(value, NumberStyles.HexNumber, null, out byte result))
            return result;
        return defaultValue;
    }

    /// <summary>
    /// Parse a string to an integer value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int ParseInt(this string value, int defaultValue = 0)
    {
        if (int.TryParse(value, out int result))
            return result;
        return defaultValue;
    }

    /// <summary>
    /// Parse a string to a double value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double ParseDouble(this string value, double defaultValue = 0)
    {
        if (double.TryParse(value, out double result))
            return result;
        return defaultValue;
    }

    /// <summary>
    /// Parse a string in the format hhmmss.ss to a DateTimeOffset value with today's date.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTimeOffset ParseAsUtcTime(this string value)
    {
        int hour = 0, min = 0, sec = 0, fs = 0;
        if (value is null ||
            value.Length != 9 ||
            value[6] != '.' ||
            !int.TryParse(value.Substring(0, 2), out hour) ||
            !int.TryParse(value.Substring(2, 2), out min) ||
            !int.TryParse(value.Substring(4, 2), out sec) ||
            !int.TryParse(value.Substring(7, 2), out fs) ||
             hour > 23 || min > 59 || sec > 59 || fs > 99)
        {
            return DateTimeOffset.MinValue;
        }

        var now = DateTimeOffset.Now;
        return new DateTimeOffset(now.Year, now.Month, now.Day, hour, min, sec, fs * 10, TimeSpan.Zero);
    }
}
