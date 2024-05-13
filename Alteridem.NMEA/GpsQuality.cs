namespace Alteridem.NMEA;

public enum GpsQuality
{
    FixNotAvailable = 0,
    GPSFix = 1,
    DifferentialGPSFix = 2,
    PPSFix = 3,
    RealTimeKinematic = 4,
    FloatRTK = 5,
    DeadReckoning = 6,
    ManualInput = 7,
    Simulation = 8
}
