namespace Alteridem.NMEA.Tests;

public class NmeaSentencesTests
{
    [TestCase("")]
    [TestCase("$LCFSI,123456,789012,1,7*21")]
    [TestCase("GPTXT,01,01,02,u-blox ag - www.u-blox.com*50")]
    public void UnknownSentencesReturnUnknownSentence(string sentence)
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
    }
}
