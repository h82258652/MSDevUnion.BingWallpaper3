using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class MonthNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var month = (int)value;
            if (CultureInfo.CurrentUICulture.Name == "zh-CN")
            {
                return $"{month}月";
            }
            else
            {
                var isNarrow = Equals(parameter, "Narrow");
                switch (month)
                {
                    case 1:
                        return isNarrow ? "Jan." : "January";

                    case 2:
                        return isNarrow ? "Feb." : "February";

                    case 3:
                        return isNarrow ? "Mar." : "March";

                    case 4:
                        return isNarrow ? "Apr." : "April";

                    case 5:
                        return isNarrow ? "May." : "May";

                    case 6:
                        return isNarrow ? "Jun." : "June";

                    case 7:
                        return isNarrow ? "Jul." : "July";

                    case 8:
                        return isNarrow ? "Aug." : "August";

                    case 9:
                        return isNarrow ? "Sep." : "September";

                    case 10:
                        return isNarrow ? "Oct." : "October";

                    case 11:
                        return isNarrow ? "Nov." : "November";

                    case 12:
                        return isNarrow ? "Dec." : "December";

                    default:
                        throw new ArgumentOutOfRangeException(nameof(month));
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}