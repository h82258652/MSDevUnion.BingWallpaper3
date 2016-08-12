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
        private readonly IScreenService _screenService;

        private readonly IWallpaperService _wallpaperService;

        public BingoWallpaperSettings(IWallpaperService wallpaperService, IScreenService screenService)
        {
            _wallpaperService = wallpaperService;
            _screenService = screenService;
        }

        public bool IsAutoUpdateLockScreen
        {
            get
            {
                return Get(nameof(IsAutoUpdateLockScreen), ApplicationDataLocality.Local, () => false);
            }
            set
            {
                Set(nameof(IsAutoUpdateLockScreen), value, ApplicationDataLocality.Local);
                RaisePropertyChanged();
            }
        }

        public bool IsAutoUpdateWallpaper
        {
            get
            {
                return Get(nameof(IsAutoUpdateWallpaper), ApplicationDataLocality.Local, () => false);
            }
            set
            {
                Set(nameof(IsAutoUpdateWallpaper), value, ApplicationDataLocality.Local);
                RaisePropertyChanged();
            }
        }

        public string SelectedArea
        {
            get
            {
                return Get(nameof(SelectedArea), ApplicationDataLocality.Roaming, () =>
                {
                    var currentCulture = CultureInfo.CurrentCulture.Name;
                    if (_wallpaperService.GetSupportedAreas().Contains(currentCulture, StringComparer.OrdinalIgnoreCase))
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
                if (_wallpaperService.GetSupportedWallpaperSizes().Contains(screenSize))
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