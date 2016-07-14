using NUnit.Framework;
using System;
using System.Linq;
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
        public async Task TestGetArchivesAsync()
        {
            var archives = await _service.GetArchivesAsync(2015, 1, "zh-CN");
            Assert.AreEqual(archives.ErrorCode, 0);
            Assert.True(archives.Any());
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _service.GetArchivesAsync(2014, 12, "zh-CN");
            });
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, null);
            });
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, string.Empty);
            });
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

        [Test]
        public async Task TestGetImagesAsync()
        {
            var images = await _service.GetImagesAsync(new[]
            {
                "559d0e88e4b03bd51879a0de",
                "559d0e88e4b0a35bc506fbf8"
            });
            Assert.AreEqual(images.ErrorCode, 0);
            Assert.AreEqual(images.Results.Count, 2);
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetImagesAsync(null);
            });
        }

        [Test]
        public async Task TestGetWallpapersAsync()
        {
            var wallpapers = await _service.GetWallpapersAsync(2015, 1, "zh-CN");
            Assert.True(wallpapers.Any());
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _service.GetWallpapersAsync(2014, 12, "zh-CN");
            });
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetWallpapersAsync(2015, 1, null);
            });
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.GetWallpapersAsync(2015, 1, string.Empty);
            });
        }
    }
}