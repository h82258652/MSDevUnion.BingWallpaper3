using BingoWallpaper.Uwp.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void FirstDoubleSizePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = (FirstDoubleSizePanel)sender;
            var width = e.NewSize.Width;
            panel.MaximumRowsOrColumns = Math.Max((int)(width / 320), 2);
        }
    }
}