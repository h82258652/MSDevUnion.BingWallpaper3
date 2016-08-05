using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Views;
using FontAwesome.UWP;
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
            var toastPrompt = new ToastPrompt()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 234, 23, 32)),
                Message = message,
                Icon = new FontAwesome.UWP.FontAwesome()
                {
                    Icon = FontAwesomeIcon.Close
                }
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }

        public async void ShowInformation(string message)
        {
            var toastPrompt = new ToastPrompt()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 0, 156, 243)),
                Message = message,
                Icon = new FontAwesome.UWP.FontAwesome()
                {
                    Icon = FontAwesomeIcon.InfoCircle
                }
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }

        public async void ShowMessage(string message)
        {
            var toastPrompt = new ToastPrompt()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 19, 192, 77)),
                Message = message,
                Icon = new FontAwesome.UWP.FontAwesome()
                {
                    Icon = FontAwesomeIcon.Check
                }
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }

        public async void ShowWarning(string message)
        {
            ToastPrompt toastPrompt = new ToastPrompt()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 255, 193, 0)),
                Message = message,
                Icon = new FontAwesome.UWP.FontAwesome()
                {
                    Icon = FontAwesomeIcon.Warning
                }
            };
            ToastPromptContainer.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            ToastPromptContainer.Children.Remove(toastPrompt);
        }
    }
}