using System;

namespace Alteridem.NMEA;

public class InvalidNmeaSentenceException : Exception
{
    public InvalidNmeaSentenceException(string sentence) 
        : base($"Invalid NMEA sentence: {sentence}")
    {
    }
}
