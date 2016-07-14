using BingoWallpaper.Models.LeanCloud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface ILeanCloudWallpaperService : IWallpaperService
    {
        Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area);

        Task<Image> GetImageAsync(string objectId);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds);

        Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area);
    }
}