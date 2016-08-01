using Newtonsoft.Json;

namespace BingoWallpaper.Models.Bing
{
    [JsonObject]
    public class Tooltips
    {
        [JsonProperty("loading")]
        public string Loading
        {
            get;
            set;
        }

        [JsonProperty("next")]
        public string Next
        {
            get;
            set;
        }

        [JsonProperty("previous")]
        public string Previous
        {
            get;
            set;
        }

        [JsonProperty("walle")]
        public string Walle
        {
            get;
            set;
        }

        [JsonProperty("walls")]
        public string Walls
        {
            get;
            set;
        }
    }
}