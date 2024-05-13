using Alteridem.NMEA.Extensions;

namespace Alteridem.NMEA.Tests.Extensions;

public class StringExtenionTests
{
    // Valid values
    [TestCase("00", 0x00)]
    [TestCase("01", 0x01)]
    [TestCase("10", 0x10)]
    [TestCase("1A", 0x1A)]
    [TestCase("F0", 0xF0)]
    [TestCase("FF", 0xFF)]
    [TestCase("1a", 0x1A)]
    [TestCase("f0", 0xF0)]
    [TestCase("ff", 0xFF)]
    // Invalid values
    [TestCase("G1", 0x00)]
    [TestCase("1G", 0x00)]
    [TestCase("FFFF", 0x00)]
    [TestCase("GG", 0x00)]
    [TestCase("GF", 0x00)]
    public void CanParseByte(string value, byte expected)
    {
        value.ParseByte().Should().Be(expected);
    }

    [Test]
    public void CanParseTime()
    {
        var time = "202224.12";
        var expected = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 22, 24, 120, TimeSpan.Zero);
        time.ParseAsUtcTime().Should().Be(expected);
    }

    [TestCase("")]
    [TestCase("923")]
    [TestCase("123456789")]
    [TestCase("999999.99")]
    [TestCase("abcdef.gh")]
    public void InvalidTimeReturnsMinValue(string time)
    {
        time.ParseAsUtcTime().Should().Be(DateTimeOffset.MinValue);
    }
}
