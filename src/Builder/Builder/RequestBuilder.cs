using System.Net.Http.Headers;
using System.Text.Json;

namespace HttpRequestBuilder.Builder
{
    public sealed class RequestBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private readonly HttpRequestDetail _httpRequestDetail;
        private RequestBuilder(string url, HttpMethod? method = null)
        {
            _httpRequestDetail = new HttpRequestDetail(url);
            if(method is not null)
                _httpRequestDetail.Method = method;
        }

        /// <summary>
        /// Create new instance of RequestBuilder.
        /// </summary>
        /// <returns>RequestBuilder instance object.</returns>
        public static IRouteBuilder Create(string url, HttpMethod? method = null)
            => new RequestBuilder(url, method);

        #region URI & Authorization

        /// <summary>
        /// Authentication by bearer JWT token.
        /// </summary>
        /// <param name="token">JWT token</param>
        public IOptionsBuilder WithBearerToken(string token)
        {
            _httpRequestDetail.Authentication = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        #endregion

        #region Headers

        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            _httpRequestDetail.Headers.Add(key, value);
            return this;
        }
        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = (headerFunc?.Invoke()) ?? throw new HttpRequestBuilderException(nameof(WithHeader));
            _httpRequestDetail.Headers.Add(header.Key, header.Value);
            return this;
        }

        #endregion

        #region Body Content

        public IHeaderOrBuilder WithContentAsFormData(IList<KeyValuePair<string, string>> form)
        {
            var content = new MultipartFormDataContent();
            foreach (var _data in form)
            {
                content.Add(new StringContent(_data.Key), _data.Value);
            }
            return this;
        }

        public IHeaderOrBuilder WithContentAsFormUrlEncoded(IList<KeyValuePair<string, string>> form)
        {
            _httpRequestDetail.Content = new FormUrlEncodedContent(form);
            return this;
        }

        public IHeaderOrBuilder WithContentAsRaw(string raw, string mediaType = "application/json")
        {
            _httpRequestDetail.Content = new StringContent(raw, null, mediaType);
            return this;
        }

        public IHeaderOrBuilder WithDataFromBodyAsJson<TData>(TData data)
        {
            _httpRequestDetail.Content = new StringContent(JsonSerializer.Serialize(data), null, "application/json");
            return this;
        }

        #endregion

        public IHttpRequest Build()
            => new HttpRequest(_httpRequestDetail);
    }
}