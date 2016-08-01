using Newtonsoft.Json;
using System.Collections.Generic;

namespace BingoWallpaper.Models.Bing
{
    [JsonObject]
    public class Image : IImage
    {
        [JsonProperty("bot")]
        public int Bot
        {
            get;
            set;
        }

        [JsonProperty("copyright")]
        public string Copyright
        {
            get;
            set;
        }

        [JsonProperty("copyrightlink")]
        public string Copyrightlink
        {
            get;
            set;
        }

        [JsonProperty("drk")]
        public int Drk
        {
            get;
            set;
        }

        [JsonProperty("enddate")]
        public string EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 拥有 1920 x 1080 分辨率。
        /// </summary>
        [JsonProperty("wp")]
        public bool ExistWUXGA
        {
            get;
            set;
        }

        [JsonProperty("fullstartdate")]
        public string FullStartDate
        {
            get;
            set;
        }

        [JsonProperty("hs")]
        public IReadOnlyList<Hotspot> Hotspots
        {
            get;
            set;
        }

        [JsonProperty("hsh")]
        public string Hsh
        {
            get;
            set;
        }

        [JsonProperty("msg")]
        public IReadOnlyList<Message> Messages
        {
            get;
            set;
        }

        [JsonProperty("startdate")]
        public string StartDate
        {
            get;
            set;
        }

        [JsonProperty("top")]
        public int Top
        {
            get;
            set;
        }

        [JsonProperty("url")]
        public string Url
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