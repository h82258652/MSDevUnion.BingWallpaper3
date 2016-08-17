using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IBingoShareService
    {
        Task<bool> ShareToSinaWeiboAsync(byte[] image, string text);

        void ShareToSystem(byte[] image, string text);

        Task ShareToWechatAsync(byte[] image, string text);
    }
}