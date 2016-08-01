using BingoWallpaper.Models;
using BingoWallpaper.Models.Bing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public class BingWallpaperService : IBingWallpaperService
    {
        public async Task<BingResult> GetAsync(int daysAgo, int count, string area)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            string requestUrl = $"{Constants.BingUrlBase}/hpimagearchive.aspx?format=js&idx={daysAgo}&n={count}&mkt={area}";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<BingResult>(json);
            }
        }

        public IReadOnlyList<string> GetSupportedAreas()
        {
            return new[]
            {
                "de-DE",
                "en-AU",
                "en-CA",
                "en-GB",
                "en-IN",
                "en-US",
                "fr-CA",
                "fr-FR",
                "ja-JP",
                "pt-BR",
                "zh-CN",
            };
        }

        public IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes()
        {
            return new[]
            {
                new WallpaperSize(150,150),
                new WallpaperSize(200,200),
                new WallpaperSize(240,240),
                new WallpaperSize(240,400),
                new WallpaperSize(310,150),
                new WallpaperSize(320,180),
                new WallpaperSize(400,240),
                new WallpaperSize(480,800),
                new WallpaperSize(640,360),
                new WallpaperSize(720,1280),
                new WallpaperSize(768,1024),
                new WallpaperSize(768,1280),
                new WallpaperSize(768,1366),
                new WallpaperSize(800,480),
                new WallpaperSize(800,600),
                new WallpaperSize(1024,768),
                new WallpaperSize(1080,1920),
                new WallpaperSize(1280,720),
                new WallpaperSize(1280,768),
                new WallpaperSize(1366,768),
                new WallpaperSize(1920,1080),
                new WallpaperSize(1920,1200),
            };
        }

        public string GetUrl(IImage image, WallpaperSize size)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            if (GetSupportedWallpaperSizes().Contains(size) == false)
            {
                throw new NotSupportedException();
            }

            return $"{Constants.BingUrlBase}{image.UrlBase}_{size.Width}x{size.Height}.jpg";
        }
    }
}