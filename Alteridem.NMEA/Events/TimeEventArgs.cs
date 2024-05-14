using System;

namespace Alteridem.NMEA.Events;

public class TimeEventArgs(DateTimeOffset time) : EventArgs
{
    public DateTimeOffset Time { get; } = time;
}
