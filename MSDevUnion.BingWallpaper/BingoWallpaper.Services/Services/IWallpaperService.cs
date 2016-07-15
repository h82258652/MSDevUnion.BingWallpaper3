using BingoWallpaper.Models;
using System.Collections.Generic;

namespace BingoWallpaper.Services
{
    public interface IWallpaperService
    {
        IReadOnlyList<string> GetSupportedAreas();

        IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes();

        string GetUrl(IImage image, WallpaperSize size);
    }
}