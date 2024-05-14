using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Alteridem.NMEA.Events;
using Alteridem.NMEA.Gis;
using Alteridem.NMEA.Sentences;

namespace Alteridem.NMEA;

public class NmeaListener(string port, int baud = 9600)
{
    private readonly string _port = port;
    private readonly int _baud = baud;
    private bool _cancel = false;

    // Delegates for events
    public delegate void ErrorHandler(object sender, ErrorEventArgs args);
    public delegate void PositionChangedHandler(object sender, PositionEventArgs args);
    public delegate void AltitudeChangedHandler(object sender, AltitudeEventArgs args);

    // Events
    public event ErrorHandler? Error;
    public event PositionChangedHandler? PositionChanged;
    public event AltitudeChangedHandler? AltitudeChanged;

    public void Start() => 
        Task.Run(BackgroundListener);

    public void Stop()
    {
        _cancel = true;
    }

    private void BackgroundListener()
    {
        SerialPort com = new(_port);

        com.BaudRate = _baud;
        com.DataBits = 8;
        com.Parity = Parity.None;
        com.StopBits = StopBits.One;
        com.Handshake = Handshake.None;

        // Set the read/write timeouts
        com.ReadTimeout = 5000;    // Timeout for read operations in milliseconds
        com.WriteTimeout = 1000;    // Timeout for write operations in milliseconds

        try
        {
            com.Open();
            while (!_cancel && com.IsOpen)
            {
                ProcessSentence(com.ReadLine());
            }
        }
        catch (TimeoutException)
        {
            OnError("Read timeout occurred");
        }
        catch (Exception ex)
        {
            OnError(ex.Message);
        }
        finally
        {
            com.Close();
        }
    }

    private void ProcessSentence(string data)
    {
        var nmea = NmeaSentences.Parse(data);
        if (nmea?.GetType() == typeof(UnknownSentence))
            return;

        switch(nmea)
        {
            case GgaSentence gga:
                if (gga.Quality == GpsQuality.FixNotAvailable || gga.Latitude.Value == 0 || gga.Longitude.Value == 0)
                    break;

                OnPositionChanged(gga.Latitude, gga.Longitude);

                if (gga.Altitude != 0)
                    OnAltitudeChanged(gga.Altitude);

                break;
            case GllSentence gll:
                if (gll.Status == Status.Invalid || gll.Latitude.Value == 0 || gll.Longitude.Value == 0)
                    break;

                OnPositionChanged(gll.Latitude, gll.Longitude);
                break;
            case RmcSentence rmc:
                if (rmc.Latitude.Value == 0 || rmc.Longitude.Value == 0)
                    break;

                OnPositionChanged(rmc.Latitude, rmc.Longitude);
                break;
            case VtgSentence _:                
                break;
        }
    }

    private void OnError(string message)
    {
        Error?.Invoke(this, new ErrorEventArgs(message));
    }

    private void OnPositionChanged(Latitude latitude, Longitude longitude)
    {
        PositionChanged?.Invoke(this, new PositionEventArgs(latitude, longitude));
    }

    private void OnAltitudeChanged(double altitude)
    {
        AltitudeChanged?.Invoke(this, new AltitudeEventArgs(altitude));
    }
}
