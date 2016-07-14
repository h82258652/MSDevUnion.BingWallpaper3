using BingoWallpaper.Uwp.Extensions;
using Windows.UI.Xaml.Media.Animation;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public Storyboard RefreshBusyStoryboard => (Storyboard)Resources["RefreshBusyStoryboard"];

        public void BeginOrResumeRefreshBusyStoryboard()
        {
            RefreshBusyStoryboard.BeginOrResume();
        }
    }
}