using BingoWallpaper.Extensions;
using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;

namespace BingoWallpaper.Services
{
    public class LeanCloudWallpaperServiceWithCache : ILeanCloudWallpaperService
    {
        public async Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            var viewMonth = new DateTime(year, month, 1);
            if (viewMonth < BingoWallpaper.Constants.MinimumViewMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(viewMonth));
            }
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }
            if (area.Length <= 0)
            {
                throw new ArgumentException($"{nameof(area)} 不能为空字符串。", nameof(area));
            }

            var where = new
            {
                market = area,
                date = new Dictionary<string, string>()
                {
                    {
                        "$regex",
                        @"\Q" + viewMonth.ToString("yyyyMM") + @"\E"
                    }
                }
            };

            string requestUrl = $"{BingoWallpaper.Constants.LeanCloudUrlBase}/1.1/classes/Archive?where={WebUtility.UrlEncode(JsonConvert.SerializeObject(where))}&order=-date";

            var cacheFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, BingoWallpaper.Constants.LeanCloudWallpaperDataCacheFolderName, HashHelper.GenerateMd5Hash(requestUrl)) + ".json";
            LeanCloudResultCollection<Archive> result = null;
            if (File.Exists(cacheFilePath))
            {
                var json = await FileExtensions.ReadAllTextAsync(cacheFilePath);
                try
                {
                    result = JsonConvert.DeserializeObject<LeanCloudResultCollection<Archive>>(json);
                    if (result.Count() >= DateTime.DaysInMonth(year, month))
                    {
                        // 已经缓存过当月所有信息，直接返回。
                        return result;
                    }
                }
                catch (Exception)
                {
                    // 缓存不可用，丢弃缓存。
                    File.Delete(cacheFilePath);
                }
            }

            try
            {
                using (var client = CreateHttpClient())
                {
                    var json = await client.GetStringAsync(requestUrl);
                    result = JsonConvert.DeserializeObject<LeanCloudResultCollection<Archive>>(json);

                    if (result.Any())
                    {
                        try
                        {
                            // 创建缓存文件夹（假设不存在）。
                            Directory.CreateDirectory(Path.GetDirectoryName(cacheFilePath));
                            await FileExtensions.WriteAllTextAsync(cacheFilePath, json);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (result == null || result.Any() == false)
                {
                    throw;
                }
            }

            return result;
        }

        public Task<Image> GetImageAsync(string objectId)
        {
            throw new NotImplementedException();
        }

        public async Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
        {
            if (objectIds == null)
            {
                throw new ArgumentNullException(nameof(objectIds));
            }

            var where = new
            {
                objectId = new Dictionary<string, IEnumerable<string>>()
                {
                    {
                        "$in",
                        objectIds
                    }
                }
            };

            string requestUrl = $"{BingoWallpaper.Constants.LeanCloudUrlBase}/1.1/classes/Image?where={WebUtility.UrlEncode(JsonConvert.SerializeObject(where))}&order=-updatedAt";

            var cacheFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, BingoWallpaper.Constants.LeanCloudWallpaperDataCacheFolderName, HashHelper.GenerateMd5Hash(requestUrl)) + ".json";
            if (File.Exists(cacheFilePath))
            {
                var json = await FileExtensions.ReadAllTextAsync(cacheFilePath);
                try
                {
                    return JsonConvert.DeserializeObject<LeanCloudResultCollection<Image>>(json);
                }
                catch (Exception)
                {
                    // 缓存不可用，丢弃缓存。
                    File.Delete(cacheFilePath);
                }
            }

            using (var client = CreateHttpClient())
            {
                var json = await client.GetStringAsync(requestUrl);
                var result = JsonConvert.DeserializeObject<LeanCloudResultCollection<Image>>(json);

                if (result.Any())
                {
                    try
                    {
                        // 创建缓存文件夹（假设不存在）。
                        Directory.CreateDirectory(Path.GetDirectoryName(cacheFilePath));
                        await FileExtensions.WriteAllTextAsync(cacheFilePath, json);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                return result;
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

        public IReadOnlyList<WallpaperSize> GetSupportedWallpaperSizes()
        {
            return new[]
            {
                new WallpaperSize(480,800),
                new WallpaperSize(768,1280),
                new WallpaperSize(800,480),
                new WallpaperSize(1080,1920),
                new WallpaperSize(1366,768),
                new WallpaperSize(1920,1080),
                new WallpaperSize(1920,1200),
            };
        }

        public string GetUrl(IImage image, WallpaperSize size)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            if (GetSupportedWallpaperSizes().Contains(size) == false)
            {
                throw new NotSupportedException();
            }

            return $"{BingoWallpaper.Constants.PenBeatUrlBase}{image.UrlBase}_{size.Width}x{size.Height}.jpg";
        }

        public async Task<IEnumerable<Wallpaper>> GetWallpapersAsync(int year, int month, string area)
        {
            var viewMonth = new DateTime(year, month, 1);
            if (viewMonth < BingoWallpaper.Constants.MinimumViewMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(viewMonth));
            }
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }
            if (area.Length <= 0)
            {
                throw new ArgumentException($"{nameof(area)} 不能为空字符串。", nameof(area));
            }

            var archives = (await GetArchivesAsync(year, month, area)).ToList();
            var imageIds = archives.Select(temp => temp.Image.ObjectId);
            var images = await GetImagesAsync(imageIds);
            return from archive in archives
                   let image = images.FirstOrDefault(temp => temp.ObjectId == archive.Image.ObjectId)
                   select new Wallpaper()
                   {
                       Archive = archive,
                       Image = image
                   };
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            headers.Add("X-AVOSCloud-Application-Id", BingoWallpaper.Constants.LeanCloudAppId);
            headers.Add("X-AVOSCloud-Application-Key", BingoWallpaper.Constants.LeanCloudAppKey);
            return client;
        }
    }
}