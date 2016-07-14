using BingoWallpaper.Services;
using System;
using System.Globalization;
using System.Linq;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public class BingoWallpaperSettings : AppSettingsBase, IBingoWallpaperSettings
    {
        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        public BingoWallpaperSettings(ILeanCloudWallpaperService leanCloudWallpaperService)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
        }

        public string SelectedArea
        {
            get
            {
                return Get(nameof(SelectedArea), ApplicationDataLocality.Roaming, () =>
                {
                    var currentCulture = CultureInfo.CurrentCulture.Name;
                    if (_leanCloudWallpaperService.GetSupportedAreas().Contains(currentCulture, StringComparer.OrdinalIgnoreCase))
                    {
                        return currentCulture;
                    }
                    return "en-US";
                });
            }
            set
            {
                Set(nameof(SelectedArea), value, ApplicationDataLocality.Roaming);
            }
        }
    }
}