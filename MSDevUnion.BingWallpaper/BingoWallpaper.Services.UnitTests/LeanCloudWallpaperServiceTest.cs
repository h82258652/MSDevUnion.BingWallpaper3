using NUnit.Framework;

namespace BingoWallpaper.Services.UnitTests
{
    [TestFixture]
    public class LeanCloudWallpaperServiceTest
    {
        private ILeanCloudWallpaperService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new LeanCloudWallpaperService();
        }

        [Test]
        public void TestGetImageAsync()
        {
            Assert.Fail();
        }
    }
}