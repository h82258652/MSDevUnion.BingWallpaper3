using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IBingoShareService
    {
        Task ShareToSinaWeiboAsync(byte[] image, string text);

        Task ShareToWechatAsync(byte[] image, string text);
    }
}