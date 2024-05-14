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

    [TestCase("", 0)]
    [TestCase("a", 0)]
    [TestCase("0", 0)]
    [TestCase("1", 1)]
    [TestCase("10", 10)]
    public void CanParseInt(string value, int expected)
    {
        value.ParseInt().Should().Be(expected);
    }

    [TestCase("", 0.0)]
    [TestCase("a", 0.0)]
    [TestCase("0", 0.0)]
    [TestCase("1", 1.0)]
    [TestCase("10", 10.0)]
    [TestCase("10.1", 10.1)]
    [TestCase("10.10", 10.1)]
    public void CanParseDouble(string value, double expected)
    {
        value.ParseDouble().Should().Be(expected);
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

    [Test]
    public void CanParseDate()
    {
        var date = "090723";
        var expected = new DateOnly(2023, 07, 09);
        date.ParseAsDate().Should().Be(expected);
    }

    [TestCase("")]
    [TestCase("923")]
    [TestCase("123456789")]
    [TestCase("999999.99")]
    [TestCase("abcdef.gh")]
    public void InvalidDateReturnsMinValue(string date)
    {
        date.ParseAsDate().Should().Be(DateOnly.MinValue);
    }
}
