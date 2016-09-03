using BingoWallpaper.Models;

namespace BingoWallpaper.Services
{
    public interface ITileService
    {
        void UpdatePrimaryTile(IImage image, string text);
    }
}