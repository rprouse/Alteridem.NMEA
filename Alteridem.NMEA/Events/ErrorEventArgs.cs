using System;

namespace Alteridem.NMEA.Events;

public class ErrorEventArgs(string message) : EventArgs
{
    public string Message { get; } = message;
}
