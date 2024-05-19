using System.Net;

namespace HttpRequestBuilder.Response
{
    public readonly struct HttpResponse : IHttpResponse
    {
        public HttpResponse(HttpStatusCode status, string content)
        {
            StatusCode = status;
            Content = content;
        }
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }
    }
}
