using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await EllipseMask.LightOnAsync();
        }
    }
}