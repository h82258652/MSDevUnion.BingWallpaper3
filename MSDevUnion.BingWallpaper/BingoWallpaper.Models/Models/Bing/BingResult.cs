using Newtonsoft.Json;
using System.Collections.Generic;

namespace BingoWallpaper.Models.Bing
{
    [JsonObject]
    public class BingResult
    {
        [JsonProperty("images")]
        public IReadOnlyList<Image> Images
        {
            get;
            set;
        }

        [JsonProperty("tooltips")]
        public Tooltips Tooltips
        {
            get;
            set;
        }
    }
}