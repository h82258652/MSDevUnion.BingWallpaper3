using BingoWallpaper.Configuration;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Messages;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.Social.SinaWeibo;
using System;
using System.IO;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : ViewModelBase, INavigable
    {
        private readonly IAppToastService _appToastService;

        private readonly IBingoFileService _bingoFileService;

        private readonly IBingoShareService _bingoShareService;

        private readonly IImageLoader _imageLoader;

        private readonly IBingoWallpaperSettings _settings;

        private readonly ISystemSettingService _systemSettingService;

        private readonly IWallpaperService _wallpaperService;

        private RelayCommand _chooseShareCommand;

        private bool _isBusy;

        private RelayCommand _openLockScreenSettingCommand;

        private RelayCommand _openWallpaperSettingCommand;

        private RelayCommand _saveCommand;

        private RelayCommand _setLockScreenCommand;

        private RelayCommand _setWallpaperCommand;

        private Wallpaper _wallpaper;

        public DetailViewModel(IWallpaperService wallpaperService, IBingoWallpaperSettings settings, ISystemSettingService systemSettingService, IBingoFileService bingoFileService, IImageLoader imageLoader, IAppToastService appToastService, IBingoShareService bingoShareService)
        {
            _wallpaperService = wallpaperService;
            _settings = settings;
            _systemSettingService = systemSettingService;
            _bingoFileService = bingoFileService;
            _imageLoader = imageLoader;
            _appToastService = appToastService;
            _bingoShareService = bingoShareService;
        }

        public RelayCommand ChooseShareCommand
        {
            get
            {
                _chooseShareCommand = _chooseShareCommand ?? new RelayCommand(() =>
                {
                    MessengerInstance.Send(new OpenSharePopupMessage());
                });
                return _chooseShareCommand;
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                Set(ref _isBusy, value);
            }
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
                    IsBusy = true;
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var fileName = Path.GetFileName(url);
                        var isSaved = await _bingoFileService.SaveImageAsync(fileName, bytes);
                        if (isSaved)
                        {
                            _appToastService.ShowMessage(LocalizedStrings.SaveSuccess);
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
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
                    IsBusy = true;
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var isSuccess = await _systemSettingService.SetLockScreenAsync(bytes);
                        if (isSuccess)
                        {
                            _appToastService.ShowMessage(LocalizedStrings.SetSuccess);
                        }
                        else
                        {
                            _appToastService.ShowError(LocalizedStrings.SetFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
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
                    IsBusy = true;
                    try
                    {
                        var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                        var bytes = await _imageLoader.GetBytesAsync(url);
                        var isSuccess = await _systemSettingService.SetWallpaperAsync(bytes);
                        if (isSuccess)
                        {
                            _appToastService.ShowMessage(LocalizedStrings.SetSuccess);
                        }
                        else
                        {
                            _appToastService.ShowError(LocalizedStrings.SetFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
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
            internal set
            {
                Set(ref _wallpaper, value);
                RaisePropertyChanged(nameof(WallpaperUrl));
            }
        }

        public string WallpaperUrl => _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);

        public void Activate(object parameter)
        {
            MessengerInstance.Register<SinaWeiboSelectedMessage>(this, message =>
            {
                ShareToSinaWeibo();
            });
            MessengerInstance.Register<WechatSelectedMessage>(this, message =>
            {
                ShareToWechat();
            });
            MessengerInstance.Register<SystemShareSelectedMessage>(this, message =>
            {
                ShareToSystem();
            });
        }

        public void Deactivate(object parameter)
        {
            MessengerInstance.Unregister(this);
        }

        private async void ShareToSinaWeibo()
        {
            IsBusy = true;
            try
            {
                var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                var bytes = await _imageLoader.GetBytesAsync(url);
                bool isSuccess = await _bingoShareService.ShareToSinaWeiboAsync(bytes, Wallpaper.Archive.Info + url);
                if (isSuccess)
                {
                    _appToastService.ShowMessage(LocalizedStrings.ShareSuccess);
                }
            }
            catch (UserCancelAuthorizeException)
            {
                _appToastService.ShowInformation(LocalizedStrings.CancelAuthorize);
            }
            catch (Exception ex)
            {
                _appToastService.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ShareToSystem()
        {
            IsBusy = true;
            try
            {
                var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                await _bingoShareService.ShareToSystemAsync(url, Wallpaper.Archive.Info);
            }
            catch (Exception ex)
            {
                _appToastService.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ShareToWechat()
        {
            IsBusy = true;
            try
            {
                var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.SelectedWallpaperSize);
                var bytes = await _imageLoader.GetBytesAsync(url);
                await _bingoShareService.ShareToWechatAsync(bytes, Wallpaper.Archive.Info);
            }
            catch (Exception ex)
            {
                _appToastService.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}