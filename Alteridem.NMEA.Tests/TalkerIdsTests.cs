namespace Alteridem.NMEA.Tests
{
    public class TalkerIdsTests
    {
        TalkerIds _talkerIds;
        
        [SetUp]
        public void Setup()
        {
            _talkerIds = new TalkerIds();
        }

        [Test]
        public void GetTalkerId_ValidId_ReturnsCorrectDescription()
        {
            _talkerIds["GP"].Should().Be("Global Positioning System receiver");
        }

        [Test]
        public void GetTalkerId_InvalidId_ReturnsUnknown()
        {
            _talkerIds["XY"].Should().Be("Unknown");
        }
    }
}
