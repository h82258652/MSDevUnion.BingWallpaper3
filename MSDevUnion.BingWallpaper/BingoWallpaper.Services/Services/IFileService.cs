using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IFileService
    {
        Task<string> PickerSaveFilePathAsync(string suggestedFileName);

        Task WriteFileAsync(string filePath, byte[] bytes);
    }
}