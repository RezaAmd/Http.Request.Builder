using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Http.Request.Builder.Model
{
    /// <summary>
    /// Stores the data required to create the Http Client.
    /// </summary>
    internal class HttpRequestDetail
    {
        public HttpClient HttpClient { get; set; } = new HttpClient();
        public UriBuilder Uri { get; set; } = new UriBuilder();
        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public AuthenticationHeaderValue? Authentication { get; set; }
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public HttpContent? Content { get; set; }
        public int FailedAttemptsCount { get; set; } = 0;

        #region Ctor

        public HttpRequestDetail(string url)
        {
            Uri = new UriBuilder(url);
        }

        #endregion
    }
}