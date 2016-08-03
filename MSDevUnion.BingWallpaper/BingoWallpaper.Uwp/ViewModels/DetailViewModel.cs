using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using Windows.Storage;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : ViewModelBase, INavigable
    {
        private readonly IAppToastService _appToastService;

        private readonly IFileService _fileService;

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

        public DetailViewModel(IWallpaperService wallpaperService, IBingoWallpaperSettings settings, ISystemSettingService systemSettingService, IFileService fileService, IImageLoader imageLoader, IAppToastService appToastService)
        {
            _wallpaperService = wallpaperService;
            _settings = settings;
            _systemSettingService = systemSettingService;
            _fileService = fileService;
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
                    // TODO check exception and to service.

                    var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                    var bytes = await _imageLoader.GetBytesAsync(url);
                    var saveLocation = _settings.SelectedSaveLocation;
                    StorageFile file = null;
                    switch (saveLocation)
                    {
                        case SaveLocation.PictureLibrary:
                            file = await KnownFolders.PicturesLibrary.CreateFileAsync(Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);
                            break;

                        case SaveLocation.ChooseEveryTime:
                            file = await _fileService.PickerSaveFilePathAsync(Path.GetFileName(url));
                            break;

                        case SaveLocation.SavedPictures:
                            file = await KnownFolders.SavedPictures.CreateFileAsync(Path.GetFileName(url), CreationCollisionOption.ReplaceExisting);
                            break;
                    }
                    if (file != null)
                    {
                        await FileIO.WriteBytesAsync(file, bytes);
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
                    // TODO check exception and show result.

                    var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                    var bytes = await _imageLoader.GetBytesAsync(url);
                    var isSuccess = await _systemSettingService.SetLockScreenAsync(bytes);
                    _appToastService.ShowMessage(isSuccess.ToString());
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
                    // TODO check exception and show result.

                    var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                    var bytes = await _imageLoader.GetBytesAsync(url);
                    var isSuccess = await _systemSettingService.SetWallpaperAsync(bytes);
                    _appToastService.ShowMessage(isSuccess.ToString());
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