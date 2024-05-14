using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Tests.Gis;

public class LatitudeTests
{
    [Test]
    public void TestToString()
    {
        Latitude a = new ();
        a.ToString().Should().Be("0° 0.00000' N");
    }

    [TestCase(AngleFormat.Default, "0° 0.00000' N")]
    [TestCase(AngleFormat.Degrees, "0° N")]
    [TestCase(AngleFormat.DegreesDecimal, "0.0000000° N")]
    [TestCase(AngleFormat.Minutes, "0° 0' N")]
    [TestCase(AngleFormat.MinutesDecimal, "0° 0.00000' N")]
    [TestCase(AngleFormat.Seconds, "0° 0' 0\" N")]
    [TestCase(AngleFormat.SecondsDecimal, "0° 0' 0.000\" N")]
    public void TestToString(AngleFormat format, string expected)
    {
        Latitude a = new();
        a.ToString(format).Should().Be(expected);
    }

    [Test]
    public void TestEmptyConstructor()
    {
        Latitude a = new();
        a.Degrees.Should().Be(0.0);
        a.NorthSouth.Should().Be(NS.North);
    }

    [TestCase("", "", 0.0)]
    [TestCase("8000.00", "N", 80.0)]
    [TestCase("8030.00", "N", 80.5)]
    [TestCase("8030.60", "N", 80.51)]
    [TestCase("8000.00", "S", -80.0)]
    [TestCase("8030.00", "S", -80.5)]
    [TestCase("8030.60", "S", -80.51)]
    public void TestStringConstructor(string value, string ns, double expected)
    {
        Latitude a = new(value, ns);
        a.Value.Should().Be(expected);
    }

    [Test]
    public void TestDegreesConstructor()
    {
        Latitude a = new(23.5);
        a.Degrees.Should().Be(23.5);
        a.NorthSouth.Should().Be(NS.North);
    }

    [Test]
    public void TestDegreesMinutesConstructor()
    {
        Latitude a = new(89, 12.5);
        a.WholeDegrees.Should().Be(89);
        a.Minutes.Should().Be(12.5);
        a.NorthSouth.Should().Be(NS.North);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructor()
    {
        Latitude a = new(89, 30, 10.2);
        a.WholeDegrees.Should().Be(89);
        a.WholeMinutes.Should().Be(30);
        a.Seconds.Should().Be(10.2);
        a.NorthSouth.Should().Be(NS.North);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructorWithOverflow()
    {
        Latitude a = new(90, 60, 0);
        a.Degrees.Should().Be(90.0);
        a.NorthSouth.Should().Be(NS.North);
    }

    [Test]
    public void TestPositiveOverflow()
    {
        Latitude a = new(91);
        a.WholeDegrees.Should().Be(90);
        a.Degrees.Should().Be(90.0);
        a.NorthSouth.Should().Be(NS.North);
    }

    [Test]
    public void TestNegativeOverflow()
    {
        Latitude a = new(0);
        a.Set(-91);
        a.Value.Should().Be(-90.0);
        a.Degrees.Should().Be(90.0);
        a.NorthSouth.Should().Be(NS.South);
    }

    [Test]
    public void TestProperties()
    {
        Latitude a = new();
        a.Value = -90.5;

        a.Value.Should().Be(-90.5);
        a.WholeDegrees.Should().Be(90);
        a.Degrees.Should().Be(90.5);
        a.WholeMinutes.Should().Be(30);
    }

    [Test]
    public void TestEquality()
    {
        Latitude a = new(100.1);
        Latitude b = new(100.1);
        Latitude c = new(-100.1);

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
        Latitude a = new(45);
        a.Radians.Should().Be(Math.PI / 4);
    }

    [Test]
    public void TestNorthSouth()
    {
        Latitude a = new (-85.0);
        Latitude b = new (23.0);

        a.NorthSouth.Should().Be(NS.South);
        b.NorthSouth.Should().Be(NS.North);

        a.NorthSouth = NS.South;
        b.NorthSouth = NS.South;
        a.NorthSouth.Should().Be(NS.South);
        b.NorthSouth.Should().Be(NS.South);

        a.NorthSouth = NS.North;
        b.NorthSouth = NS.North;
        a.NorthSouth.Should().Be(NS.North);
        b.NorthSouth.Should().Be(NS.North);
    }

    [TestCase(45.0, NS.North, "45° 0.00000' N")]
    [TestCase(-45.0, NS.South, "45° 0.00000' S")]
    public void TestNorthSouth(double degrees, NS ns, string expected)
    {
        Latitude a = new (degrees);
        a.NorthSouth.Should().Be(ns);
        a.ToString().Should().Be(expected);
    }
}
