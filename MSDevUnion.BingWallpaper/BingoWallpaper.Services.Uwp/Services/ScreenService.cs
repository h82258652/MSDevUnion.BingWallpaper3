using BingoWallpaper.Utils;

namespace BingoWallpaper.Services
{
    public class ScreenService : IScreenService
    {
        public uint ScreenHeightInRawPixels => DisplayInformationExtensions.ScreenHeightInRawPixels;

        public uint ScreenWidthInRawPixels => DisplayInformationExtensions.ScreenWidthInRawPixels;
    }
}