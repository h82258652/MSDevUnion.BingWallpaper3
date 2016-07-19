using BingoWallpaper.Uwp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public abstract class BindablePage : Page
    {
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            (DataContext as INavigable)?.Deactivate(e.Parameter);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            (DataContext as INavigable)?.Activate(e.Parameter);
        }
    }
}