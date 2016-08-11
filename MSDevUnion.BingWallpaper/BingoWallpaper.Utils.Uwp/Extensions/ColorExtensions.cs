using Windows.UI;
using Windows.UI.Xaml;
using WinRTXamlToolkit.Imaging;

namespace BingoWallpaper.Extensions
{
    public static class ColorExtensions
    {
        public static Color AccentColor => (Color)Application.Current.Resources["SystemAccentColor"];

        public static Color Darker(this Color color)
        {
            var hslColor = color.ToHsl();
            hslColor.L = hslColor.L * 0.5;
            return WinRTXamlToolkit.Imaging.ColorExtensions.FromHsl(hslColor.H, hslColor.S, hslColor.L, color.A);
        }

        public static Color Lighter(this Color color)
        {
            var hslColor = color.ToHsl();
            hslColor.L = hslColor.L + (1 - hslColor.L) * 0.5;
            return WinRTXamlToolkit.Imaging.ColorExtensions.FromHsl(hslColor.H, hslColor.S, hslColor.L, color.A);
        }
    }
}