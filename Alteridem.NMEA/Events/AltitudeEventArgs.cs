using System;

namespace Alteridem.NMEA.Events;

public class AltitudeEventArgs(double altitude) : EventArgs
{
    public double Altitude { get; } = altitude;
}
