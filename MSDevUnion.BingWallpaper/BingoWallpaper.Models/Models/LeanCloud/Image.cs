using Newtonsoft.Json;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class Image : AVObject, IImage
    {
        [JsonProperty("copyright")]
        public string Copyright
        {
            get;
            set;
        }

        /// <summary>
        /// 拥有 1920 x 1200 分辨率。
        /// </summary>
        [JsonProperty("wp")]
        public bool ExistWUXGA
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("urlbase")]
        public string UrlBase
        {
            get;
            set;
        }
    }
}