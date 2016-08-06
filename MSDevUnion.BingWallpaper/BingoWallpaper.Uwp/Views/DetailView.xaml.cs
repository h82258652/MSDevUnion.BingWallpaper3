using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Uwp.ViewModels;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        public DetailViewModel ViewModel => (DetailViewModel)DataContext;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameter = (IDictionary)e.Parameter;
            Debug.Assert(parameter != null);
            ViewModel.Wallpaper = (Wallpaper)parameter["Wallpaper"];

            await EllipseMask.LightOnAsync();
        }

        private void DetailView_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            UpdateStoryboard();
        }

        private void DetailView_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void UpdateStoryboard()
        {
            LeaveStoryboardAnimation.To = Window.Current.Bounds.Height;
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateStoryboard();
        }
    }
}