using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class WallpaperUrlConverter : IValueConverter
    {
        private readonly IWallpaperService _wallpaperService;

        public WallpaperUrlConverter()
        {
            _wallpaperService = ServiceLocator.Current.GetInstance<IWallpaperService>();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = value as IImage ?? (value as Wallpaper)?.Image;
            var args = (parameter as string)?.Split('x', ',');
            if (image != null && args != null && args.Length == 2)
            {
                return _wallpaperService.GetUrl(image, new WallpaperSize(int.Parse(args[0]), int.Parse(args[1])));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}