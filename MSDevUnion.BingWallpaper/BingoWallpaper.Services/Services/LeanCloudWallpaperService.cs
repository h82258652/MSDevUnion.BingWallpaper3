using BingoWallpaper.Models.LeanCloud;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public class LeanCloudWallpaperService : ILeanCloudWallpaperService
    {
        public async Task<Image> GetImageAsync(string objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }
            if (objectId.Length <= 0)
            {
                throw new ArgumentException($"{nameof(objectId)} 不能为空字符串。", nameof(objectId));
            }

            string requestUrl = $"{Constants.LeanCloudUrlBase}/1.1/classes/Image/{objectId}";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                return JsonConvert.DeserializeObject<Image>(json);
            }
        }

        public IReadOnlyList<string> GetSupportedAreas()
        {
            return new[]
            {
                "de-DE",
                "en-AU",
                "en-CA",
                "en-GB",
                "en-IN",
                "en-US",
                "fr-CA",
                "fr-FR",
                "ja-JP",
                "pt-BR",
                "zh-CN",
            };
        }
    }
}