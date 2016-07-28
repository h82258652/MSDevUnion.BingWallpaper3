using BingoWallpaper.Models;
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

        private readonly IScreenService _screenService;

        public BingoWallpaperSettings(ILeanCloudWallpaperService leanCloudWallpaperService, IScreenService screenService)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _screenService = screenService;
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

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return Get(nameof(SelectedSaveLocation), ApplicationDataLocality.Local, () => SaveLocation.PictureLibrary);
            }
            set
            {
                Set(nameof(SelectedSaveLocation), value, ApplicationDataLocality.Local);
                RaisePropertyChanged();
            }
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                var composite = Get(nameof(SelectedWallpaperSize), ApplicationDataLocality.Local, () => new ApplicationDataCompositeValue());
                object width;
                object height;
                if (composite.TryGetValue(nameof(WallpaperSize.Width), out width) && composite.TryGetValue(nameof(WallpaperSize.Height), out height))
                {
                    if (width is int && height is int)
                    {
                        return new WallpaperSize((int)width, (int)height);
                    }
                }

                var screenSize = new WallpaperSize((int)_screenService.ScreenWidthInRawPixels, (int)_screenService.ScreenHeightInRawPixels);
                if (_leanCloudWallpaperService.GetSupportedWallpaperSizes().Contains(screenSize))
                {
                    return screenSize;
                }

                return new WallpaperSize(800, 480);
            }
            set
            {
                var composite = new ApplicationDataCompositeValue()
                {
                    [nameof(WallpaperSize.Width)] = value.Width,
                    [nameof(WallpaperSize.Height)] = value.Height
                };
                Set(nameof(SelectedWallpaperSize), composite, ApplicationDataLocality.Local);
                RaisePropertyChanged();
            }
        }
    }
}