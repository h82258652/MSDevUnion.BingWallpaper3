using BingoWallpaper.Uwp.Controls;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BingoWallpaper.Uwp.Services
{
    public class AppToastService : IAppToastService
    {
        public async void ShowError(string message)
        {
            var toastPrompt = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Blue),
                Message = message,
                Icon = new SymbolIcon(Symbol.Accept)
            };
            // TODO change color and give a container.
            await toastPrompt.ShowAsync();
        }

        public async void ShowMessage(string message)
        {
            var toastPrompt = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Red),
                Message = message,
                Icon = new SymbolIcon(Symbol.Cancel)
            };
            // TODO same as show error
            await toastPrompt.ShowAsync();
        }
    }
}