﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SoftwareKobo.Social.Sina.Weibo.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToUriQuery(this IDictionary<string, string> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return string.Join("&", from temp in dictionary
                                    select WebUtility.UrlEncode(temp.Key) + "=" + WebUtility.UrlEncode(temp.Value));
        }
    }
}