using SoftwareKobo.Social.Sina.Weibo.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace SoftwareKobo.Social.Sina.Weibo
{
    public class WeiboClient
    {
        public async void ShareImageAsync(byte[] image, string text)
        {
        }

        public async Task AuthorizeAsync(string appKey, string appSecret, string redirectUri)
        {
            if (appKey == null)
            {
                throw new ArgumentNullException(nameof(appKey));
            }
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            if (redirectUri == null)
            {
                throw new ArgumentNullException(nameof(redirectUri));
            }

            var query = new Dictionary<string, string>()
            {
                ["client_id"] = appKey,
                ["redirect_uri"] = redirectUri
            };
            var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
            if (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile")
            {
                query["display"] = "mobile";
            }
            if (CultureInfo.CurrentUICulture.Name.StartsWith("zh", StringComparison.OrdinalIgnoreCase) == false)
            {
                query["language"] = "en";
            }
            var requestUri = new Uri("https://api.weibo.com/oauth2/authorize?" + query.ToUriQuery(), UriKind.Absolute);
            var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requestUri,
                new Uri(redirectUri));
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    var responseUrl = result.ResponseData;
                    var responseUri = new Uri(responseUrl);
                    var decoder = new WwwFormUrlDecoder(responseUri.Query);
                    var authorizeCode = decoder.GetFirstValueByName("code");

                    var parameters = new Dictionary<string, string>()
                    {
                        ["client_id"] = appKey,
                        ["client_secret"] = appSecret,
                        ["grant_type"] = "authorization_code",
                        ["code"] = authorizeCode,
                        ["redirect_uri"] = redirectUri
                    };
                    var content = new FormUrlEncodedContent(parameters);
                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("https://api.weibo.com/oauth2/access_token", content);
                        var json = await response.Content.ReadAsStringAsync();
                        var accessToken = JsonConvert.
                    }
                    break;

                case WebAuthenticationStatus.UserCancel:
                    break;

                case WebAuthenticationStatus.ErrorHttp:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}