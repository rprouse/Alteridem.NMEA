using System.IO.Ports;
using Alteridem.NMEA;

SerialPort com = new ("COM10");

com.BaudRate = 9600;
com.DataBits = 8;
com.Parity = Parity.None;
com.StopBits = StopBits.One;
com.Handshake = Handshake.None;

// Set the read/write timeouts
com.ReadTimeout = 5000;    // Timeout for read operations in milliseconds
com.WriteTimeout = 1000;    // Timeout for write operations in milliseconds
com.Open();

try
{
    do
    {
        string data = com.ReadLine();
        var nmea = NmeaSentences.Parse(data);
        if (nmea?.GetType() == typeof(UnknownSentence))
            continue;

        Console.WriteLine(nmea);
    } while (true);
}
catch (TimeoutException)
{
    Console.WriteLine("Read timeout occurred");
}

com.Close();