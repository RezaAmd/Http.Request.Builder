using Http.Request.Builder.Response;

namespace Http.Request.Builder.Request
{
    public interface IHttpRequest
    {
        Task<IHttpResponse> SendAsync();
    }
}