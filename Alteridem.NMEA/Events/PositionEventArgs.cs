using System;
using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Events;

public class PositionEventArgs : EventArgs
{
    public PositionEventArgs(Latitude latitude, Longitude longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public Latitude Latitude { get; }
    public Longitude Longitude { get; }
}
