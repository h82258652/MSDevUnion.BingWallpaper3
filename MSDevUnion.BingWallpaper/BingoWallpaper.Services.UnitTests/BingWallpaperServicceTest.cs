using NUnit.Framework;
using System;

namespace BingoWallpaper.Services.UnitTests
{
    [TestFixture]
    public class BingWallpaperServicceTest
    {
        private IBingWallpaperService _bingWallpaperService;

        [SetUp]
        public void SetUp()
        {
            _bingWallpaperService = new BingWallpaperService();
        }

        [Test]
        public void TestGetAsync()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _bingWallpaperService.GetAsync(0, 0, "zh-CN");
            });
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _bingWallpaperService.GetAsync(0, 1, null);
            });
        }
    }
}