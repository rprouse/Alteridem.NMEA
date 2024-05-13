using Alteridem.NMEA.Gis;

namespace Alteridem.NMEA.Tests.Gis;

public class AngleTests
{
    [Test]
    public void TestToString()
    {
        Angle a = new Angle();
        a.ToString().Should().Be("0° 0' 0.000\"");
    }

    [TestCase(AngleFormat.Default, "0° 0' 0.000\"")]
    [TestCase(AngleFormat.Degrees, "0°")]
    [TestCase(AngleFormat.DegreesDecimal, "0.0000000°")]
    [TestCase(AngleFormat.Minutes, "0° 0'")]
    [TestCase(AngleFormat.MinutesDecimal, "0° 0.00000'")]
    [TestCase(AngleFormat.Seconds, "0° 0' 0\"")]
    [TestCase(AngleFormat.SecondsDecimal, "0° 0' 0.000\"")]
    public void TestToString(AngleFormat format, string expected)
    {
        Angle a = new ();
        a.ToString(format).Should().Be(expected);
    }

    [Test]
    public void TestEmptyConstructor()
    {
        Angle a = new ();
        a.Degrees.Should().Be(0.0);
    }

    [Test]
    public void TestDegreesConstructor()
    {
        Angle a = new (123.5);
        a.Degrees.Should().Be(123.5);
    }

    [Test]
    public void TestDegreesMinutesConstructor()
    {
        Angle a = new (90, 12.5);
        a.WholeDegrees.Should().Be(90);
        a.Minutes.Should().Be(12.5);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructor()
    {
        Angle a = new (180, 30, 10.2);
        a.WholeDegrees.Should().Be(180);
        a.WholeMinutes.Should().Be(30);
        a.Seconds.Should().Be(10.2);
    }

    [Test]
    public void TestDegreesMinutesSecondsConstructorWithOverflow()
    {
        Angle a = new (360, 60, 0);
        a.Degrees.Should().Be(1.0);
    }

    [Test]
    public void TestPositiveOverflow()
    {
        Angle a = new (370);
        a.WholeDegrees.Should().Be(10);
    }

    [Test]
    public void TestNegativeOverflow()
    {
        Angle a = new (0);
        a.Set(-370);
        a.Value.Should().Be(350.0);
        a.Degrees.Should().Be(350.0);
    }

    [Test]
    public void TestProperties()
    {
        Angle a = new ();
        a.Value = -90.5;

        a.Value.Should().Be(-90.5);
        a.WholeDegrees.Should().Be(90);
        a.Degrees.Should().Be(90.5);
        a.WholeMinutes.Should().Be(30);
    }

    [Test]
    public void TestEquality()
    {
        Angle a = new (100.1);
        Angle b = new (100.1);
        Angle c = new (-100.1);

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
        Angle a = new (180);
        a.Radians.Should().Be(Math.PI);
    }
}
