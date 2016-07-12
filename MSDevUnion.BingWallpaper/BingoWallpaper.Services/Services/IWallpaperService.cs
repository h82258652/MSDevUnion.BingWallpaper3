using System.Collections.Generic;

namespace BingoWallpaper.Services
{
    public interface IWallpaperService
    {
        IReadOnlyList<string> GetSupportedAreas();
    }
}