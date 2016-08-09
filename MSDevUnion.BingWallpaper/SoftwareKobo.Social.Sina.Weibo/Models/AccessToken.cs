using Newtonsoft.Json;

namespace SoftwareKobo.Social.Sina.Weibo.Models
{
    [JsonObject]
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Value
        {
            get;
            set;
        }
    }
}