using Newtonsoft.Json;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class LeanCloudPointer
    {
        [JsonProperty("className")]
        public string ClassName
        {
            get;
            set;
        }

        [JsonProperty("objectId")]
        public string ObjectId
        {
            get;
            set;
        }

        [JsonProperty("__type")]
        public string Type
        {
            get;
            set;
        }
    }
}