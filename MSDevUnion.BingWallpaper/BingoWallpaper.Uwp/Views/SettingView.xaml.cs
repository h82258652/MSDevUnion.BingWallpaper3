namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        protected override void PrepareLeaveStoryboard()
        {
            base.PrepareLeaveStoryboard();

            ContentBackgroundRectangleWidthLeaveAnimation.From = ContentBackgroundRectangle.ActualWidth;
            ContentBackgroundRectangleHeightLeaveAnimation.From = ContentBackgroundRectangle.ActualHeight;
            ContentBackgroundRectangleTranslateTransformLeaveAnimation.To = ActualHeight / 2 + 40;
        }
    }
}