using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using WinRTXamlToolkit.Tools;

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

        public IReadOnlyList<string> Areas => _leanCloudWallpaperService.GetSupportedAreas();

        public bool IsAutoUpdateLockScreen
        {
            get
            {
                return _settings.IsAutoUpdateLockScreen;
            }
            set
            {
                _settings.IsAutoUpdateLockScreen = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAutoUpdateWallpaper
        {
            get
            {
                return _settings.IsAutoUpdateWallpaper;
            }
            set
            {
                _settings.IsAutoUpdateWallpaper = value;
                RaisePropertyChanged();
            }
        }

        public IReadOnlyList<SaveLocation> SaveLocations
        {
            get;
        } = EnumExtensions.GetValues<SaveLocation>();

        public string SelectedArea
        {
            get
            {
                return _settings.SelectedArea;
            }
            set
            {
                _settings.SelectedArea = value;
                RaisePropertyChanged();
            }
        }

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