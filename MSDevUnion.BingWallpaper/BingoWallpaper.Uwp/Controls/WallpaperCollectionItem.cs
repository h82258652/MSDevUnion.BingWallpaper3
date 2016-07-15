using BingoWallpaper.Models.LeanCloud;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    public sealed class WallpaperCollectionItem : Control
    {
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(WallpaperCollectionItem), new PropertyMetadata(false));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(WallpaperCollection), typeof(WallpaperCollectionItem), new PropertyMetadata(null));

        public WallpaperCollectionItem()
        {
            DefaultStyleKey = typeof(WallpaperCollectionItem);
        }

        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        public WallpaperCollection ItemsSource
        {
            get
            {
                return (WallpaperCollection)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
    }
}