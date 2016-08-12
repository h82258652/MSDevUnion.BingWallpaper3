using Windows.ApplicationModel.Resources;

namespace BingoWallpaper.Uwp
{
    internal static class LocalizedStrings
    {
        private const string ConstantsReswName = "Constants";

        internal static string SaveSuccess => ResourceLoader.GetForCurrentView(ConstantsReswName).GetString("SaveSuccess");

        internal static string SetFailed => ResourceLoader.GetForCurrentView(ConstantsReswName).GetString("SetFailed");

        internal static string SetSuccess => ResourceLoader.GetForCurrentView(ConstantsReswName).GetString("SetSuccess");

        internal static string ShareSuccess => ResourceLoader.GetForCurrentView(ConstantsReswName).GetString("ShareSuccess");
    }
}