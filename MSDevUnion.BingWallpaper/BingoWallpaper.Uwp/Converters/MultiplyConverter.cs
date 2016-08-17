using System;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((IConvertible)value).ToDouble(null) * ((IConvertible)parameter).ToDouble(null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}