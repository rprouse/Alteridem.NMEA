using System;

namespace Alteridem.NMEA.Events;

public class ErrorEventArgs : EventArgs
{
    public ErrorEventArgs(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
