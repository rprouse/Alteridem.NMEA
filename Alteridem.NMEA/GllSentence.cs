using System;
using Alteridem.NMEA.Extensions;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA;

/// <summary>
/// https://gpsd.gitlab.io/gpsd/NMEA.html#_gll_geographic_position_latitudelongitude
/// </summary>
public class GllSentence : BaseSentence
{
    public GllSentence(string sentence) : base(sentence) 
    {
        if (Fields.Length != 7 && Fields.Length != 8)
            throw new InvalidNmeaSentenceException(sentence);

        Latitude = new Latitude(Fields[1], Fields[2]);
        Longitude = new Longitude(Fields[3], Fields[4]);
        Time = Fields[5].ParseAsUtcTime();
        Status = Fields[6].ToLowerInvariant() == "a" ? Status.DataValid : Status.DataInvalid;
    }

    public override string Description => "Geographic Position - Latitude/Longitude";

    public Latitude Latitude { get; }
    public Longitude Longitude { get; }
    public DateTimeOffset Time { get; }
    public Status Status { get; }
}
