using System.Net;

namespace HttpRequestBuilder.Response
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }
        HttpContent Content { get; }
    }

    public interface IHttpResponse<out TSuccessValue> where TSuccessValue : class
    {
        TSuccessValue? Value { get; }
    }
}