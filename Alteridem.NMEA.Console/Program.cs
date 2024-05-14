using Alteridem.NMEA;

string[] lines = new[]
{
    "$GPTXT,01,01,02,u-blox ag - www.u-blox.com*50",
    "$GPTXT,01,01,02,HW  UBX-G70xx   00070000 FF7FFFFFo*69",
    "$GPTXT,01,01,02,ROM CORE 1.00 (59842) Jun 27 2012 17:43:52 * 59",
    "$GPTXT,01,01,02, PROTVER 14.00*1E",
    "$GPTXT,01,01,02, ANTSUPERV = AC SD PDoS SR*20",
    "$GPTXT,01,01,02, ANTSTATUS = OK * 3B",
    "$GPTXT,01,01,02, LLC FFFFFFFF-FFFFFFFF-FFFFFFFF-FFFFFFFF-FFFFFFFD*2C",
    "$GPRMC,, V,,,,,,,,,, N*53",
    "$GPVTG,,,,,,,,, N*30",
    "$GPGGA,,,,,,0,00,99.99,,,,,,*48",
    "$GPGSA, A,1,,,,,,,,,,,,,99.99,99.99,99.99*30",
    "$GPGLL,,,,,, V, N*64",
    "$GPGGA,202224.00,4314.72223, N,07956.69578, W,2,08,1.32,131.2, M,-35.9, M,,0000*6E",
    "$GPGSA, A,3,05,29,13,15,20,23,46,18,,,,,2.06,1.32,1.59*0B",
    "$GPGSV,4,1,15,05,37,070,44,10,03,266,21,11,01,127,,13,54,064,39*73",
    "$GPGSV,4,2,15,15,80,149,38,16,00,306,,18,57,310,30,20,13,084,34*73",
    "$GPGSV,4,3,15,23,35,277,43,24,13,158,08,27,03,331,23,29,35,206,35*71",
    "$GPGSV,4,4,15,30,07,036,,46,20,239,29,48,23,236,*45",
    "$GPGLL,4314.72223, N,07956.69578, W,202224.00, A, D*76",
    "$GPRMC,202225.00, A,4314.72224, N,07956.69578, W,0.060,,090723,,, D*60",
    "$GPVTG,, T,, M,0.060, N,0.112, K, D*22",
    "$GNRMC,, V,,,,,,,,,, N*4D",
    "$GNVTG,,,,,,,,, N*2E",
    "$GNGGA,,,,,,0,00,99.99,,,,,,*56",
    "$GNGSA, A,1,,,,,,,,,,,,,99.99,99.99,99.99*2E",
    "$GNGSA, A,1,,,,,,,,,,,,,99.99,99.99,99.99*2E",
    "$GPGSV,1,1,00*79",
    "$GLGSV,1,1,00*65",
    "$GNGLL,,,,,, V, N*7A",
};

foreach (var nmea in lines.Select(l => NmeaSentences.Parse(l)).Where(n => n.GetType() != typeof(UnknownSentence)))
{
    Console.WriteLine(nmea);
}