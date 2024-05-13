using System;
using System.Collections.Generic;
using Alteridem.NMEA.Extensions;

namespace Alteridem.NMEA;

/// <summary>
/// The NMEA Sentences that are supported by this library
/// </summary>
public static class NmeaSentences
{
    private static readonly Dictionary<string, Type> _sentences = new Dictionary<string, Type>
    {
        { "GGA", typeof(GgaSentence) },
        { "GLL", typeof(UnknownSentence) },
        { "GSA", typeof(UnknownSentence) },
        { "GSV", typeof(UnknownSentence) },
        { "RMC", typeof(UnknownSentence) },
        { "TXT", typeof(UnknownSentence) },
        { "VTG", typeof(UnknownSentence) },
    };

    public static BaseSentence? Parse(string sentence)
    {
        if (string.IsNullOrEmpty(sentence) ||
            sentence.Length < 7 ||
            !sentence.StartsWith('$'))
        {
            return new UnknownSentence(sentence);
        }

        var sentenceId = sentence.Substring(3, 3);
        if (!_sentences.ContainsKey(sentenceId))
            return new UnknownSentence(sentence);

        var type = _sentences[sentenceId];
        var instance = Activator.CreateInstance(type, sentence) as BaseSentence;

        return instance ?? new UnknownSentence(sentence);
    }
}

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
        Time = Fields[1].ParseAsUtcTime();
    }

    public override string Description => "Global Positioning System Fix Data";

    public DateTimeOffset Time { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    /// <summary>
    /// GPS Quality Indicator (non null)
    /// </summary>
    public GpsQuality Quality { get; set; }
    
    /// <summary>
    /// Number of satellites in use
    /// </summary>
    public int Satellites { get; set; }

    /// <summary>
    /// Horizontal Dilution of precision (meters)
    /// </summary>
    public double HorizontalDilution { get; set; }

    /// <summary>
    /// Antenna Altitude above/below mean-sea-level (geoid) (in meters)
    /// </summary>
    public double Altitude { get; set; }

    /// <summary>
    /// Geoidal separation in meters, the difference between the WGS-84 earth ellipsoid and mean-sea-level (geoid), "-" means mean-sea-level below ellipsoid
    /// </summary>
    public double GeoidHeight { get; set; }

    /// <summary>
    /// Age of differential GPS data, time in seconds since last SC104 type 1 or 9 update, null field when DGPS is not used
    /// </summary>
    public double LastUpdateAge { get; set; }

    /// <summary>
    /// Differential reference station ID, 0000-1023
    /// </summary>
    public string StationId { get; set; }
}