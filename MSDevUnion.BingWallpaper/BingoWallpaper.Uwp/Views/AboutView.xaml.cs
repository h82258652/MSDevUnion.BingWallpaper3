using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var d = new VisualTransition().GeneratedDuration;
            new MessageDialog(d.ToString()).ShowAsync();

            CheckBox.IsChecked = true;
        }
    }
}