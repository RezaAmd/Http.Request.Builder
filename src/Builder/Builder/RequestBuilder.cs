using System.Net.Http.Headers;

namespace HttpRequestBuilder.Builder
{
    public sealed class RequestBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private readonly HttpRequestDetail _httpRequest;
        private RequestBuilder(string url)
        {
            _httpRequest = new HttpRequestDetail(url);
        }

        /// <summary>
        /// Create new instance of RequestBuilder.
        /// </summary>
        /// <returns>RequestBuilder instance object.</returns>
        public static IRouteBuilder Create(string url)
            => new RequestBuilder(url);

        #region URI & Authorization

        /// <summary>
        /// Authentication by bearer JWT token.
        /// </summary>
        /// <param name="token">JWT token</param>
        public IOptionsBuilder WithBearerToken(string token)
        {
            _httpRequest.Authentication = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        #endregion

        #region Headers

        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            _httpRequest.Headers.Add(key, value);
            return this;
        }
        /// <summary>
        /// Add extra header for request.
        /// </summary>
        public IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc)
        {
            var header = (headerFunc?.Invoke()) ?? throw new HttpRequestBuilderException(nameof(WithHeader));
            _httpRequest.Headers.Add(header.Key, header.Value);
            return this;
        }

        #endregion

        public IHttpRequest Build()
        {
            throw new NotImplementedException();
        }

        #region Body Content
        public IHeaderOrBuilder WithContentAsFormData(KeyValuePair<string, string> data)
        {
            throw new NotImplementedException();
        }

        public IHeaderOrBuilder WithContentAsFormUrlEncoded(KeyValuePair<string, string> data)
        {
            throw new NotImplementedException();
        }

        public IHeaderOrBuilder WithContentAsRaw(string raw, string mediaType = "application/json")
        {
            throw new NotImplementedException();
        }

        public IHeaderOrBuilder WithDataFromBodyAsJson<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}