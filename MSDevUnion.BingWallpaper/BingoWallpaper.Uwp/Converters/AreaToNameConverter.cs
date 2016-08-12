using System;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class AreaToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string area = (string)value;
            string name = null;
            switch (area)
            {
                case "de-DE":
                    break;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}