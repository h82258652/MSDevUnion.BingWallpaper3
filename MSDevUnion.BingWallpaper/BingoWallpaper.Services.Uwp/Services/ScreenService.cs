using BingoWallpaper.Utils.WinAPI;

namespace BingoWallpaper.Services
{
    public class ScreenService : IScreenService
    {
        private const int SM_CXSCREEN = 0;

        private const int SM_CYSCREEN = 1;

        public uint ScreenHeightInRawPixels => (uint)User32.GetSystemMetrics(SM_CXSCREEN);

        public uint ScreenWidthInRawPixels => (uint)User32.GetSystemMetrics(SM_CYSCREEN);
    }
}