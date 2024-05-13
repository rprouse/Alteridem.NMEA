using System;

namespace Alteridem.NMEA.Gis;

public enum NS { North, South };

public class Latitude : Angle
{
    public Latitude() : base() { }

    /// <summary>
    /// Create a latitude from a string in the format dddmm.mm
    /// </summary>
    /// <param name="ddmm"></param>
    public Latitude(string ddmm, string ns)
    {
        if (ddmm.Length >= 7 && ddmm[4] == '.' &&
            uint.TryParse(ddmm.Substring(0, 2), out uint dd) &&
            double.TryParse(ddmm.Substring(2), out double mm))
        {
            Set(dd, mm);
        }

        NorthSouth = ns == "N" ? NS.North : NS.South;
    }

    public Latitude(double Degrees) : base(Degrees) { }
    public Latitude(uint Degrees, double minutes) : base(Degrees, minutes) { }
    public Latitude(uint Degrees, uint minutes, double seconds) : base(Degrees, minutes, seconds) { }

    /// <summary>
    /// Clamps the angle to the poles
    /// </summary>
    protected override void Normalize()
    {
        if (Value > 90.0)
            Set(90.0);

        if (Value < -90.0)
            Set(-90.0);
    }

    public NS NorthSouth
    {
        get => Value >= 0 ? NS.North : NS.South;
        set
        {
            if (value == NS.North)
                Set(Math.Abs(Value));
            else
                Set(-Math.Abs(Value));
        }
    }

    public override string ToString(AngleFormat f) =>
        base.ToString(f) + (NorthSouth == NS.North ? " N" : " S");
}
