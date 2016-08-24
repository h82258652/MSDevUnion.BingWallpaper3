using BingoWallpaper.Configuration;
using BingoWallpaper.Extensions;
using BingoWallpaper.Models;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Extensions;
using BingoWallpaper.Uwp.Messages;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinRTXamlToolkit.Tools;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase, INavigable
    {
        private readonly IAppToastService _appToastService;

        private readonly IImageLoader _imageLoader;

        private readonly ILeanCloudWallpaperServiceWithCache _leanCloudWallpaperServiceWithCache;

        private readonly IBingoWallpaperSettings _settings;

        private RelayCommand _clearCacheCommand;

        private string _previousSelectedArea;

        public SettingViewModel(ILeanCloudWallpaperServiceWithCache leanCloudWallpaperServiceWithCache, IBingoWallpaperSettings settings, IAppToastService appToastService, IImageLoader imageLoader)
        {
            _leanCloudWallpaperServiceWithCache = leanCloudWallpaperServiceWithCache;
            _settings = settings;
            _appToastService = appToastService;
            _imageLoader = imageLoader;
        }

        public IReadOnlyList<string> Areas => _leanCloudWallpaperServiceWithCache.GetSupportedAreas();

        public string CacheDataSizeString => _leanCloudWallpaperServiceWithCache.CalculateSizeString();

        public string CacheImageSizeString => _imageLoader.CalculateCacheSizeString();

        public RelayCommand ClearCacheCommand
        {
            get
            {
                _clearCacheCommand = _clearCacheCommand ?? new RelayCommand(async () =>
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            _leanCloudWallpaperServiceWithCache.DeleteAllCache();
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        try
                        {
                            _imageLoader.DeleteAllCache();
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    });
                    RaisePropertyChanged(nameof(CacheDataSizeString));
                    RaisePropertyChanged(nameof(CacheImageSizeString));
                    _appToastService.ShowMessage(LocalizedStrings.ClearCacheFinish);
                });
                return _clearCacheCommand;
            }
        }

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

        public IReadOnlyList<WallpaperSize> WallpaperSizes => _leanCloudWallpaperServiceWithCache.GetSupportedWallpaperSizes();

        public void Activate(object parameter)
        {
            _previousSelectedArea = SelectedArea;
        }

        public void Deactivate(object parameter)
        {
            if (_previousSelectedArea != SelectedArea)
            {
                MessengerInstance.Send(new SelectedAreaChangedMessage());
            }
        }
    }
}