using Newtonsoft.Json;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public abstract class LeanCloudResultBase
    {
        [JsonProperty("code")]
        public int ErrorCode
        {
            get;
            set;
        }

        [JsonProperty("error")]
        public string ErrorMessage
        {
            get;
            set;
        }
    }
}