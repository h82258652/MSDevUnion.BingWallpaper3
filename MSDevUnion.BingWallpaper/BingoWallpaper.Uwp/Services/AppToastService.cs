using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Views;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BingoWallpaper.Uwp.Services
{
    public class AppToastService : IAppToastService
    {
        private static Panel ToastPromptContainer
        {
            get
            {
                var rootView = (RootView)Window.Current.Content;
                return rootView.ToastPromptContainer;
            }
        }

        public async void ShowError(string message)
        {
            var toastPrompt = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Red),
                Message = message,
                Icon = new SymbolIcon(Symbol.Cancel)
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }

        public async void ShowMessage(string message)
        {
            var toastPrompt = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Blue),
                Message = message,
                Icon = new SymbolIcon(Symbol.Accept)
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }
    }
}