using Windows.System.Profile;

namespace BingoWallpaper.Helpers
{
    public static class DeviceFamilyHelper
    {
        public static bool IsMobile => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
    }
}