using System.Collections.Generic;

namespace BingoWallpaper.Services
{
    public class LeanCloudWallpaperService : ILeanCloudWallpaperService
    {
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
    }
}