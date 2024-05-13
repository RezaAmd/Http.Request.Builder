using HttpRequestBuilder.Response;

namespace HttpRequestBuilder.Request
{
    public interface IHttpRequest
    {
        Task<IHttpResponse> SendAsync();
    }
}