using System.Collections.Generic;

namespace Alteridem.NMEA;

/// <summary>
/// A two-character prefix to an NMEA sentence that identifies the type of the transmitting unit. 
/// By far the most common talker ID is "GP", identifying a generic GPS. This list contains the most 
/// common talker IDs but is not exhaustive.
/// </summary>
/// <remarks>
/// For a complete list of talker IDs, see https://gpsd.gitlab.io/gpsd/NMEA.html#_talker_ids
/// </remarks>
public class TalkerIds
{
    private readonly Dictionary<string, string> _ids = new Dictionary<string, string>
    {
        { "AI", "Alarm Indicator, (AIS?)" },
        { "AP", "Auto Pilot (pypilot?)" },
        { "BD", "BeiDou (China)" },
        { "CD", "Digital Selective Calling (DSC)" },
        { "EC", "Electronic Chart Display & Information System (ECDIS)" },
        { "GA", "Galileo Positioning System" },
        { "GB", "BeiDou (China)" },
        { "GI", "NavIC, IRNSS (India)" },
        { "GL", "GLONASS, according to IEIC 61162-1" },
        { "GN", "Combination of multiple satellite systems (NMEA 1083)" },
        { "GP", "Global Positioning System receiver" },
        { "GQ", "QZSS regional GPS augmentation system (Japan)" },
        { "HC", "Heading/Compass" },
        { "HE", "Gyro, north seeking" },
        { "II", "Integrated Instrumentation" },
        { "IN", "Integrated Navigation" },
        { "LC", "Loran-C receiver (obsolete)" },
        { "Pxxx", "Proprietary (Vendor specific)" },
        { "PQ", "QZSS (Quectel Quirk)" },
        { "QZ", "QZSS regional GPS augmentation system (Japan)" },
        { "SD", "Depth Sounder" },
        { "ST", "Skytraq" },
        { "TI", "Turn Indicator" },
        { "YX", "Transducer" },
        { "WI", "Weather Instrument" }
    };

    public string this[string id]
    {
        get
        {
            if (_ids.ContainsKey(id))
            {
                return _ids[id];
            }
            return "Unknown";
        }
    }
}