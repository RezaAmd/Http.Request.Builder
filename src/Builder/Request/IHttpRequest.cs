using HttpRequestBuilder.Builder;
using HttpRequestBuilder.Response;
using System.Net.Http.Headers;

namespace HttpRequestBuilder.Request
{
    public interface IHttpRequest
    {
        /// <summary>
        /// Provides readonly access to the URI built bby the <seealso cref="IClientBuilder"/> pipeline.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        /// Returns the authentication header configured in the <seealso cref="IClientBuilder"/> pipeline.
        /// </summary>
        AuthenticationHeaderValue AuthenticationHeader { get; }

        /// <summary>
        /// Returns the default request headers configured in the <seealso cref="IClientBuilder"/> pipeline.
        /// </summary>
        HttpRequestHeaders RequestHeaders { get; }

        Task<IHttpResponse> SendAsync();
    }
}
