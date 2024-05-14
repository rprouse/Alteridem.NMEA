using System;
using Alteridem.NMEA.Extensions;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Sentences;

/// <summary>
/// Time, Position and fix related data for a GPS receiver.
/// </summary>
/// <remarks>
/// https://gpsd.gitlab.io/gpsd/NMEA.html#_gga_global_positioning_system_fix_data
/// </remarks>
public class GgaSentence : BaseSentence
{
    public GgaSentence(string sentence) : base(sentence)
    {
        if (Fields.Length != 15)
            throw new InvalidNmeaSentenceException(sentence);

        Time = Fields[1].ParseAsUtcTime();
        Latitude = new Latitude(Fields[2], Fields[3]);
        Longitude = new Longitude(Fields[4], Fields[5]);
        Quality = (GpsQuality)Fields[6].ParseInt();
        Satellites = Fields[7].ParseInt();
        HorizontalDilution = Fields[8].ParseDouble();
        Altitude = Fields[9].ParseDouble();
        GeoidalSeparation = Fields[11].ParseDouble();
    }

    public override string Description => "Global Positioning System Fix Data";

    public DateTimeOffset Time { get; }

    public Latitude Latitude { get; }

    public Longitude Longitude { get; }

    /// <summary>
    /// GPS Quality Indicator (non null)
    /// </summary>
    public GpsQuality Quality { get; }

    /// <summary>
    /// Number of satellites in use
    /// </summary>
    public int Satellites { get; }

    /// <summary>
    /// Horizontal Dilution of precision (meters)
    /// </summary>
    public double HorizontalDilution { get; }

    /// <summary>
    /// Antenna Altitude above/below mean-sea-level (geoid) (in meters)
    /// </summary>
    public double Altitude { get; }

    /// <summary>
    /// Geoidal separation in meters, the difference between the WGS-84 earth ellipsoid and mean-sea-level (geoid), "-" means mean-sea-level below ellipsoid
    /// </summary>
    public double GeoidalSeparation { get; }

    public override string ToString() => $"GGA: {Time:HH:mm:ss} {Latitude} {Longitude} {Quality} {Satellites} {HorizontalDilution} {Altitude} {GeoidalSeparation}";
}