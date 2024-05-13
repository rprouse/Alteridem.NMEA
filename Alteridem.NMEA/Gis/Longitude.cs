using System;

namespace Alteridem.NMEA.Gis;

public enum EW { East, West };

public class Longitude : Angle
{
    public Longitude() : base() { }
    public Longitude(double Degrees) : base(Degrees) { }
    public Longitude(uint Degrees, double minutes) : base(Degrees, minutes) { }
    public Longitude(uint Degrees, uint minutes, double seconds) : base(Degrees, minutes, seconds) { }

    /// <summary>
    /// Clamps the angle to the poles
    /// </summary>
    protected override void Normalize()
    {
        if (Value > 180)
            Set(Value - 360.0);

        if (Value < -180.0)
            Set(Value + 360.0);
    }

    public EW EastWest
    {
        get => Value <= 0 ? EW.West : EW.East;
        set
        {
            if (value == EW.East)
                Set(Math.Abs(Value));
            else
                Set(-Math.Abs(Value));
        }
    }

    public override string ToString(AngleFormat f) =>
        base.ToString(f) + (EastWest == EW.East ? " E" : " W");
}