using BingoWallpaper.Models.LeanCloud;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface ILeanCloudWallpaperService : IWallpaperService
    {
        Task<Image> GetImageAsync(string objectId);
    }
}