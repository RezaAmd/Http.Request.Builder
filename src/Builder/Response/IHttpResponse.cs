using System.Net;

namespace HttpRequestBuilder.Response
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }
        string Content { get; }
    }

    public interface IHttpResponse<out TSuccessValue>
        : IHttpResponse where TSuccessValue : class
    {
        TSuccessValue? Value { get; }
    }
}