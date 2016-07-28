namespace BingoWallpaper.Services
{
    public interface IScreenService
    {
        uint ScreenHeightInRawPixels
        {
            get;
        }

        uint ScreenWidthInRawPixels
        {
            get;
        }
    }
}