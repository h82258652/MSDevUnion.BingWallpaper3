using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Uwp.ViewModels;
using System.Collections;
using System.Diagnostics;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameter = (IDictionary)e.Parameter;
            Debug.Assert(parameter != null);
            ViewModel.Wallpaper = (Wallpaper)parameter["Wallpaper"];
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (ViewModel.IsBusy)
            {
                e.Cancel = true;
            }

            base.OnNavigatingFrom(e);
        }

        private async void DetailView_Loaded(object sender, RoutedEventArgs e)
        {
            await EllipseMask.LightOnAsync();
        }
    }
}