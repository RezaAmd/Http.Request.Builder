using System.Net;

namespace HttpRequestBuilder.Response
{
    public readonly struct HttpResponse : IHttpResponse
    {
        public HttpStatusCode StatusCode { get; }
        public HttpContent Content { get; }
    }
}
