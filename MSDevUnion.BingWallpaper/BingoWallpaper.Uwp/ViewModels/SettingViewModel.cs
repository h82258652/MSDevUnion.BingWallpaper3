using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        private readonly IBingoWallpaperSettings _settings;

        public SettingViewModel(ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings settings)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _settings = settings;
        }

        public IReadOnlyList<SaveLocation> SaveLocations
        {
            get;
        } = Enum.GetValues(typeof(SaveLocation)).Cast<SaveLocation>().ToList();

        public SaveLocation SelectedSaveLocation
        {
            get
            {
                return _settings.SelectedSaveLocation;
            }
            set
            {
                _settings.SelectedSaveLocation = value;
                RaisePropertyChanged();
            }
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                return _settings.SelectedWallpaperSize;
            }
            set
            {
                _settings.SelectedWallpaperSize = value;
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<WallpaperSize> WallpaperSizes => _leanCloudWallpaperService.GetSupportedWallpaperSizes();
    }
}