using Alteridem.NMEA.Extensions;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Sentences;

/// <summary>
/// https://gpsd.gitlab.io/gpsd/NMEA.html#_vtg_track_made_good_and_ground_speed
/// </summary>
public class VtgSentence : BaseSentence
{
    public VtgSentence(string sentence) : base(sentence)
    {
        if (Fields.Length < 8)
            throw new InvalidNmeaSentenceException(sentence);

        TrueCourse = new Angle(Fields[1].ParseDouble());
        MagneticCourse = new Angle(Fields[3].ParseDouble());
        SpeedKnots = Fields[5].ParseDouble();
        SpeedKph = Fields[7].ParseDouble();
    }

    public override string Description => "Track Made Good and Ground Speed";

    public Angle TrueCourse { get; }
    public Angle MagneticCourse { get; }
    public double SpeedKnots { get; }
    public double SpeedKph { get; }

    public override string ToString() => $"VTG: {TrueCourse} {MagneticCourse} {SpeedKnots} knots {SpeedKph} km/h";
}
