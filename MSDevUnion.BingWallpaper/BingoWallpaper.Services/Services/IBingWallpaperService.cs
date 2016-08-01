using BingoWallpaper.Models.Bing;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IBingWallpaperService : IWallpaperService
    {
        Task<BingResult> GetAsync(int daysAgo, int count, string area);
    }
}