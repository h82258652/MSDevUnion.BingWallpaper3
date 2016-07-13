using Newtonsoft.Json;
using System;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class AVObject : LeanCloudResultBase
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt
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

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt
        {
            get;
            set;
        }
    }
}