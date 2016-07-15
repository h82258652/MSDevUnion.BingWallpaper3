using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = BackgroundImageTemplateName, Type = typeof(Windows.UI.Xaml.Controls.Image))]
    public sealed class WallpaperCollectionItem : ItemsControl
    {
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(WallpaperCollectionItem), new PropertyMetadata(false));

        private const string BackgroundImageTemplateName = "PART_BackgroundImage";

        private Windows.UI.Xaml.Controls.Image _backgroundImage;

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

        public new WallpaperCollection ItemsSource
        {
            get
            {
                return (WallpaperCollection)base.ItemsSource;
            }
            set
            {
                base.ItemsSource = value;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _backgroundImage = (Windows.UI.Xaml.Controls.Image)GetTemplateChild(BackgroundImageTemplateName);
            UpdateBackground();
        }

        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);

            UpdateBackground();
        }

        private void UpdateBackground()
        {
            if (_backgroundImage != null)
            {
                var image = ItemsSource.FirstOrDefault()?.Image;
                if (image != null)
                {
                    var wallpaperService = ServiceLocator.Current.GetInstance<IWallpaperService>();
                    var url = wallpaperService.GetUrl(image, new WallpaperSize(1920, 1080));
                    _backgroundImage.Source = new BitmapImage(new Uri(url));
                }
            }
        }
    }
}