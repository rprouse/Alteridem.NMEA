namespace Alteridem.NMEA.Gis;

/// <summary>
/// A base class to represent an angle in degrees.  Internally it can be
/// positive or negative, but Degrees will always return a positive
/// value.  Use AbsoluteDegreesDecimal to get the actual value.
/// </summary>
public class Angle
{
    public Angle(double degrees = 0)
    {
        Set(degrees);
    }

    public Angle(uint degrees, double minutes)
    {
        Set(degrees, minutes);
    }

    public Angle(uint degrees, uint minutes, double seconds)
    {
        Set(degrees, minutes, seconds);
    }

    public uint WholeDegrees => (uint)Degrees;

    /// <summary>
    /// Converts the angle in degrees to radians
    /// </summary>
    public double Radians
    {
        get { return Value * System.Math.PI / 180.0; }
        set { Set(value * 180.0 / System.Math.PI); }
    }

    public double Degrees
    {
        get
        {
            double d = (Value >= 0 ? Value : -Value);

            // Round to 7 decimal places
            long temp = (long)((d + 0.00000005) * 10000000.0);
            return temp / 10000000.0;
        }
    }

    public uint WholeMinutes => (uint)Minutes;

    public double Minutes
    {
        get
        {
            double minutes = Degrees;
            minutes = (minutes - (long)minutes) * 60;

            // Round to 5 decimal places
            long temp = (long)((minutes + 0.000005) * 100000.0);
            return (double)temp / 100000.0;
        }
    }

    public double Seconds
    {
        get
        {
            double seconds = Minutes;
            seconds = (seconds - (long)seconds) * 60;

            // Round to 3 decimal places
            long temp = (long)((seconds + 0.0005) * 1000.0);
            return temp / 1000.0;
        }
    }

    public uint WholeSeconds => (uint)(Seconds + 0.5);

    public double Value { get; set; }

    public void Set(double deg)
    {
        Value = deg;
        Normalize();
    }

    public void Set(uint deg, double min)
    {
        Value = deg + min / 60.0;
        Normalize();
    }

    public void Set(uint deg, double min, double sec)
    {
        Value = deg + min / 60.0 + sec / 3600.0;
        Normalize();
    }

    /// <summary>
    /// Override this in Lat/Long, this clamps to a circle
    /// </summary>
    protected virtual void Normalize()
    {
        double maximum = 360.0;
        while (Value > maximum)
            Value -= maximum;

        while (Value < 0.0)
            Value += maximum;

    }

    public static Angle operator +(Angle a, Angle b) => new Angle(a.Value + b.Value);

    public static Angle operator -(Angle a, Angle b) => new Angle(a.Value - b.Value);

    public static bool operator ==(Angle a, Angle b) => a.Equals(b);

    public static bool operator !=(Angle a, Angle b) => !a.Equals(b);

    public override string ToString() => ToString(AngleFormat.Default);

    public virtual string ToString(AngleFormat f)
    {
        switch (f)
        {
            case AngleFormat.Degrees:
                return $"{WholeDegrees}°";
            case AngleFormat.DegreesDecimal:
                return $"{Degrees:F7}°";
            case AngleFormat.Minutes:
                return $"{WholeDegrees}° {WholeMinutes}'";
            case AngleFormat.MinutesDecimal:
                return $"{WholeDegrees}° {Minutes:F5}'";
            case AngleFormat.Seconds:
                return $"{WholeDegrees}° {WholeMinutes}' {WholeSeconds}\"";
            case AngleFormat.SecondsDecimal:
            case AngleFormat.Default:
            default:
                return $"{WholeDegrees}° {WholeMinutes}' {Seconds:F3}\"";
        }
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is Angle a)
            return Value == a.Value;

        return false;
    }
}
