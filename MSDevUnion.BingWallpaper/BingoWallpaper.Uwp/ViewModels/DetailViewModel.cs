using BingoWallpaper.Configuration;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : ViewModelBase, INavigable
    {
        private readonly IAppToastService _appToastService;

        private readonly IBingoFileService _bingoFileService;

        private readonly IImageLoader _imageLoader;

        private readonly IBingoWallpaperSettings _settings;

        private readonly ISystemSettingService _systemSettingService;

        private readonly IWallpaperService _wallpaperService;

        private RelayCommand _openLockScreenSettingCommand;

        private RelayCommand _openWallpaperSettingCommand;

        private RelayCommand _saveCommand;

        private RelayCommand _setLockScreenCommand;

        private RelayCommand _setWallpaperCommand;

        private Wallpaper _wallpaper;

        public DetailViewModel(IWallpaperService wallpaperService, IBingoWallpaperSettings settings, ISystemSettingService systemSettingService, IBingoFileService bingoFileService, IImageLoader imageLoader, IAppToastService appToastService)
        {
            _wallpaperService = wallpaperService;
            _settings = settings;
            _systemSettingService = systemSettingService;
            _bingoFileService = bingoFileService;
            _imageLoader = imageLoader;
            _appToastService = appToastService;
        }

        public RelayCommand OpenLockScreenSettingCommand
        {
            get
            {
                _openLockScreenSettingCommand = _openLockScreenSettingCommand ?? new RelayCommand(async () =>
                {
                    await _systemSettingService.OpenLockScreenSettingAsync();
                });
                return _openLockScreenSettingCommand;
            }
        }

        public RelayCommand OpenWallpaperSettingCommand
        {
            get
            {
                _openWallpaperSettingCommand = _openWallpaperSettingCommand ?? new RelayCommand(async () =>
                {
                    await _systemSettingService.OpenWallpaperSettingAsync();
                });
                return _openWallpaperSettingCommand;
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(async () =>
                {
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var fileName = Path.GetFileName(url);
                        var isSaved = await _bingoFileService.SaveImageAsync(fileName, bytes);
                        if (isSaved)
                        {
                            // TODO to localized string.
                            _appToastService.ShowMessage("保存成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                });
                return _saveCommand;
            }
        }

        public RelayCommand SetLockScreenCommand
        {
            get
            {
                _setLockScreenCommand = _setLockScreenCommand ?? new RelayCommand(async () =>
                {
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var isSuccess = await _systemSettingService.SetLockScreenAsync(bytes);
                        if (isSuccess)
                        {
                            // TODO
                            _appToastService.ShowMessage("设置成功");
                        }
                        else
                        {
                            // TODO
                            _appToastService.ShowError("设置失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                });
                return _setLockScreenCommand;
            }
        }

        public RelayCommand SetWallpaperCommand
        {
            get
            {
                _setWallpaperCommand = _setWallpaperCommand ?? new RelayCommand(async () =>
                {
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var isSuccess = await _systemSettingService.SetWallpaperAsync(bytes);
                        if (isSuccess)
                        {
                            // TODO
                            _appToastService.ShowMessage("设置成功");
                        }
                        else
                        {
                            // TODO
                            _appToastService.ShowError("设置失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                });
                return _setWallpaperCommand;
            }
        }

        public Wallpaper Wallpaper
        {
            get
            {
                return _wallpaper;
            }
            private set
            {
                Set(ref _wallpaper, value);
                RaisePropertyChanged(nameof(WallpaperUrl));
            }
        }

        public string WallpaperUrl => _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);

        public void Activate(object parameter)
        {
            Wallpaper = (Wallpaper)parameter;
        }

        public void Deactivate(object parameter)
        {
        }
    }
}