namespace Alteridem.NMEA.Gis;

public enum AngleFormat
{
    /// <summary>
    /// DDD° MM' SS.SSS"
    /// </summary>
    Default,
    /// <summary>
    /// DDD°
    /// </summary>
    Degrees,
    /// <summary>
    /// DDD.DDDDDDD°
    /// </summary>
    DegreesDecimal,
    /// <summary>
    /// DDD° MM.MMMMM'
    /// </summary>
    Minutes,
    /// <summary>
    /// DDD° MM'
    /// </summary>
    MinutesDecimal,
    /// <summary>
    /// DDD° MM' SS"
    /// </summary>
    Seconds,
    /// <summary>
    /// DDD° MM' SS.SSS"
    /// </summary>
    SecondsDecimal
}
