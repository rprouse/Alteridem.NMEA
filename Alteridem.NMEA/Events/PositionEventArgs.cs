using System;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Events;

public class PositionEventArgs(Latitude latitude, Longitude longitude) : EventArgs
{
    public Latitude Latitude { get; } = latitude;
    public Longitude Longitude { get; } = longitude;
}
