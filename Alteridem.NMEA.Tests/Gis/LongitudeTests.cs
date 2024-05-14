using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Tests.Gis;

public class LongitudeTests
{
    [Test]
    public void TestToString()
    {
        Longitude a = new ();
        a.ToString().Should().Be("0° 0.00000' W");
    }

    [TestCase(AngleFormat.Default, "0° 0.00000' W")]
    [TestCase(AngleFormat.Degrees, "0° W")]
    [TestCase(AngleFormat.DegreesDecimal, "0.0000000° W")]
    [TestCase(AngleFormat.Minutes, "0° 0' W")]
    [TestCase(AngleFormat.MinutesDecimal, "0° 0.00000' W")]
    [TestCase(AngleFormat.Seconds, "0° 0' 0\" W")]
    [TestCase(AngleFormat.SecondsDecimal, "0° 0' 0.000\" W")]
    public void TestToString(AngleFormat format, string expected)
    {
        Longitude a = new();
        a.ToString(format).Should().Be(expected);
    }

    [Test]
    public void TestEmptyConstructor()
    {
        Longitude a = new();
        a.Degrees.Should().Be(0.0);
        a.EastWest.Should().Be(EW.West);
    }

    [TestCase("", "", 0.0)]
    [TestCase("12000.00", "E", 120.0)]
    [TestCase("12030.00", "E", 120.5)]
    [TestCase("12030.60", "E", 120.51)]
    [TestCase("12000.00", "W", -120.0)]
    [TestCase("12030.00", "W", -120.5)]
    [TestCase("12030.60", "W", -120.51)]
    public void TestStringConstructor(string value, string ns, double expected)
    {
        Longitude a = new(value, ns);
        a.Value.Should().Be(expected);
    }

    [Test]
    public void TestDegreesConstructor()
    {
        Longitude a = new(23.5);
        a.Degrees.Should().Be(23.5);
        a.EastWest.Should().Be(EW.East);
    }

    [Test]
    public void TestDegreesMinutesConstructor()
    {
        Longitude a = new(89, 12.5);
        a.WholeDegrees.Should().Be(89);
        a.Minutes.Should().Be(12.5);
        a.EastWest.Should().Be(EW.East);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructor()
    {
        Longitude a = new(89, 30, 10.2);
        a.WholeDegrees.Should().Be(89);
        a.WholeMinutes.Should().Be(30);
        a.Seconds.Should().Be(10.2);
        a.EastWest.Should().Be(EW.East);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructorWithOverflow()
    {
        Longitude a = new(90, 60, 0);
        a.Degrees.Should().Be(91.0);
        a.EastWest.Should().Be(EW.East);
    }

    [Test]
    public void TestPositiveOverflow()
    {
        Longitude a = new(181);
        a.WholeDegrees.Should().Be(179);
        a.Value.Should().Be(-179.0);
        a.EastWest.Should().Be(EW.West);
    }

    [Test]
    public void TestNegativeOverflow()
    {
        Longitude a = new(-181);
        a.Value.Should().Be(179.0);
        a.Degrees.Should().Be(179.0);
        a.EastWest.Should().Be(EW.East);
    }

    [Test]
    public void TestProperties()
    {
        Longitude a = new();
        a.Value = -90.5;

        a.Value.Should().Be(-90.5);
        a.WholeDegrees.Should().Be(90);
        a.Degrees.Should().Be(90.5);
        a.WholeMinutes.Should().Be(30);
    }

    [Test]
    public void TestEquality()
    {
        Longitude a = new(100.1);
        Longitude b = new(100.1);
        Longitude c = new(-100.1);

        a.Should().Be(b);
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
        b.Equals(c).Should().BeFalse();
        (b == c).Should().BeFalse();
        (b != c).Should().BeTrue();
    }

    [Test]
    public void TestRadians()
    {
        Longitude a = new(90);
        a.Radians.Should().Be(Math.PI / 2);
    }

    [Test]
    public void TestEastWest()
    {
        Longitude a = new(-85.0);
        Longitude b = new(23.0);

        a.EastWest.Should().Be(EW.West);
        b.EastWest.Should().Be(EW.East);

        a.EastWest = EW.East;
        b.EastWest = EW.East;
        a.EastWest.Should().Be(EW.East);
        b.EastWest.Should().Be(EW.East);

        a.EastWest = EW.West;
        b.EastWest = EW.West;
        a.EastWest.Should().Be(EW.West);
        b.EastWest.Should().Be(EW.West);
    }

    [TestCase(145.0, EW.East, "145° 0.00000' E")]
    [TestCase(-145.0, EW.West, "145° 0.00000' W")]
    public void TestEastWest(double degrees, EW EW, string expected)
    {
        Longitude a = new(degrees);
        a.EastWest.Should().Be(EW);
        a.ToString().Should().Be(expected);
    }
}
