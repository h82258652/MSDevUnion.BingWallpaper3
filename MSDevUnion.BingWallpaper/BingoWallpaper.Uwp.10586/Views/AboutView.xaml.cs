namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

        protected override void PrepareLeaveStoryboard()
        {
            base.PrepareLeaveStoryboard();

            LeaveAnimation.To = RootGrid.ActualWidth;
        }
    }
}