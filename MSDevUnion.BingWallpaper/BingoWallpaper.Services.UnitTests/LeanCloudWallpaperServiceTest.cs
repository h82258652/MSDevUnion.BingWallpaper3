using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
        public async Task TestGetImageAsync()
        {
            var image = await _service.GetImageAsync("559d0e88e4b03bd51879a0de");
            Assert.AreEqual(image.ErrorCode, 0);
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetImageAsync(null);
            });
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.GetImageAsync(string.Empty);
            });
        }
    }
}