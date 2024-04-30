using HttpRequestBuilder.Builder;
using HttpRequestBuilder.Model;

namespace HttpRequestBuilder.Request
{
    public sealed class RequestBuilder : IHostBuilder, IRouteBuilder, IOptionsBuilder, IHeaderOrBuilder
    {
        private readonly BuilderConfiguration _builderConfiguration;
        private RequestBuilder()
        {
            _builderConfiguration = new BuilderConfiguration();
        }

        /// <summary>
        /// Create new instance of RequestBuilder.
        /// </summary>
        /// <returns>RequestBuilder instance object.</returns>
        public static IHostBuilder Create() => new RequestBuilder();

        public IRouteBuilder WithUrl(string url)
        {
            throw new NotImplementedException();
        }

        public IOptionsBuilder WithBearerToken(string token)
        {
            throw new NotImplementedException();
        }

        public IHeaderOrBuilder WithHeader(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IHeaderOrBuilder WithHeader(Func<KeyValuePair<string, string>?> headerFunc)
        {
            throw new NotImplementedException();
        }

        public IHttpRequest Build()
        {
            throw new NotImplementedException();
        }
    }
}