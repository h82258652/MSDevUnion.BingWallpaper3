using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class AreaToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var area = (string)value;
            var name = ResourceLoader.GetForCurrentView("Area").GetString(area);
            return $"{name}({area})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}