using System;
using Windows.UI.Xaml.Media.Animation;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class StoryboardExtensions
    {
        public static void BeginOrResume(this Storyboard storyboard)
        {
            if (storyboard == null)
            {
                throw new ArgumentNullException(nameof(storyboard));
            }

            if (storyboard.GetCurrentState() == ClockState.Stopped)
            {
                storyboard.Begin();
            }
            else
            {
                storyboard.Resume();
            }
        }
    }
}