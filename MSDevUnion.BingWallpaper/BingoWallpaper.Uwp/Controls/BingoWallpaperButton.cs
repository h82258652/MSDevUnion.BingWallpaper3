using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    public sealed class BingoWallpaperButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(BingoWallpaperButton), new PropertyMetadata(default(CornerRadius)));

        public BingoWallpaperButton()
        {
            this.DefaultStyleKey = typeof(BingoWallpaperButton);
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
    }
}