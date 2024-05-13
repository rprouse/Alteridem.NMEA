namespace Alteridem.NMEA.Tests;

public class NmeaSentencesTests
{
    [TestCase("")]
    [TestCase("$LCFSI,123456,789012,1,7*21")]
    [TestCase("GPTXT,01,01,02,u-blox ag - www.u-blox.com*50")]
    [TestCase("$GPGGA,202224.00*23")]
    [TestCase("$GPGLL,202224.00*23")]
    [TestCase("$GPGSA,202224.00*23")]
    [TestCase("$GPGSV,202224.00*23")]
    [TestCase("$GPRMC,202224.00*23")]
    [TestCase("$GPVTG,202224.00*23")]
    public void InvalidOrUnknownSentencesReturnUnknownSentence(string sentence)
    {
        var nmea = NmeaSentences.Parse(sentence);
        nmea.Should().BeOfType<UnknownSentence>();
    }

    [Test]
    public void CanParseGgaSentence()
    {
        var sentence = "$GPGGA,202224.00,4314.72223,N,07956.69578,W,2,08,1.32,131.2,M,-35.9,M,,0000*6E";
        var nmea = NmeaSentences.Parse(sentence);
        nmea.Should().BeOfType<GgaSentence>();

        var now = DateTimeOffset.Now;
        var gga = nmea as GgaSentence;
        gga.Time.Should().Be(new DateTimeOffset(now.Year, now.Month, now.Day, 20, 22, 24, TimeSpan.Zero));
        gga.Latitude.Value.Should().BeApproximately(43.2453705, 0.0000001);
        gga.Longitude.Value.Should().BeApproximately(-79.9449296667, 0.0000001);
        gga.Quality.Should().Be(GpsQuality.DifferentialGPSFix);
        gga.Satellites.Should().Be(8);
        gga.HorizontalDilution.Should().Be(1.32);
        gga.Altitude.Should().Be(131.2);
        gga.GeoidalSeparation.Should().Be(-35.9);
    }

    [Test]
    public void CanParseGllSentence()
    {
        var sentence = "$GPGLL,4314.72223,N,07956.69578,W,202224.00,A,D*76";
        var nmea = NmeaSentences.Parse(sentence);
        nmea.Should().BeOfType<GllSentence>();

        var now = DateTimeOffset.Now;
        var gll = nmea as GllSentence;
        gll.Latitude.Value.Should().BeApproximately(43.2453705, 0.0000001);
        gll.Longitude.Value.Should().BeApproximately(-79.9449296667, 0.0000001);
        gll.Time.Should().Be(new DateTimeOffset(now.Year, now.Month, now.Day, 20, 22, 24, TimeSpan.Zero));
        gll.Status.Should().Be(Status.DataValid);
    }
}
