using System;
using System.Linq;
using Alteridem.NMEA.Extensions;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA;

/// <summary>
/// https://gpsd.gitlab.io/gpsd/NMEA.html#_rmc_recommended_minimum_navigation_information
/// </summary>
public class RmcSentence : BaseSentence
{
    public RmcSentence(string sentence) : base(sentence)
    {
        if (Fields.Length < 13)
            throw new InvalidNmeaSentenceException(sentence);

        var time = Fields[1].ParseAsUtcTime();
        var date = Fields[9].ParseAsDate();
        DateTime = new DateTimeOffset(date, new TimeOnly(time.Hour, time.Minute, time.Second, time.Millisecond), time.Offset);
        Status = Fields[2].ToLowerInvariant() == "a" ? Status.Valid : Status.Warning;
        Latitude = new Latitude(Fields[3], Fields[4]);
        Longitude = new Longitude(Fields[5], Fields[6]);
        SpeedKnots = Fields[7].ParseDouble();
        Course = new Angle(Fields[8].ParseDouble());
        MagneticVariation = new Angle(Fields[10].ParseDouble());
        EastWest = Fields[11].FirstOrDefault();
    }

    public override string Description => "Satellites in view";

    public DateTimeOffset DateTime { get; }
    public Status Status { get; }
    public Latitude Latitude { get; }
    public Longitude Longitude { get; }
    public double SpeedKnots { get; }
    public Angle Course { get; }
    public Angle MagneticVariation { get; }
    public char EastWest { get; }

    public override string ToString() => $"RMC: {DateTime} {Status} {Latitude} {Longitude} {SpeedKnots} knots {Course} {MagneticVariation}{EastWest}";
}