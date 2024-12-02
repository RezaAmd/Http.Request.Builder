using System.Net;

namespace Http.Request.Builder.Response
{
    public interface IBaseHttpResponse
    {
        HttpStatusCode StatusCode { get; }
    }
    public interface IHttpResponse : IBaseHttpResponse
    {
        string Content { get; }
        int FailedAttemptsCount { get; }
    }

    public interface IHttpResponse<out TSuccessContent>
        : IBaseHttpResponse where TSuccessContent : class
    {
        TSuccessContent? Content { get; }
    }
}